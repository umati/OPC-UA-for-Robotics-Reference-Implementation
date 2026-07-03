using Robotics.Shared;

namespace Robotics.ReferenceServer.Simulation;

public sealed class SixAxisRobot
{
    public const string Manufacturer = "Reference Implementation";
    public const string Model = "SixAxisRobot";
    public const string SerialNumber = "SIM-6AXIS-0001";
    public const string ProductCode = "REF-SIX-AXIS-SIM";

    private const double AmbientTemperatureCelsius = 24.0;
    private const double PositionToleranceDegrees = 0.01;
    private const double VelocityToleranceDegreesPerSecond = 0.05;
    private const double MaxIntegrationStepSeconds = 0.02;

    private readonly AxisRuntimeState[] _axes;
    private CartesianPose _cartesianPose;

    public SixAxisRobot()
    {
        _axes =
        [
            new AxisRuntimeState(new RobotJointLimits { AxisName = RobotAxisName.S, MinPositionDegrees = -170, MaxPositionDegrees = 170, MaxVelocityDegreesPerSecond = 150, MaxAccelerationDegreesPerSecondSquared = 300 }),
            new AxisRuntimeState(new RobotJointLimits { AxisName = RobotAxisName.L, MinPositionDegrees = -100, MaxPositionDegrees = 135, MaxVelocityDegreesPerSecond = 140, MaxAccelerationDegreesPerSecondSquared = 280 }),
            new AxisRuntimeState(new RobotJointLimits { AxisName = RobotAxisName.U, MinPositionDegrees = -150, MaxPositionDegrees = 150, MaxVelocityDegreesPerSecond = 160, MaxAccelerationDegreesPerSecondSquared = 320 }),
            new AxisRuntimeState(new RobotJointLimits { AxisName = RobotAxisName.R, MinPositionDegrees = -190, MaxPositionDegrees = 190, MaxVelocityDegreesPerSecond = 220, MaxAccelerationDegreesPerSecondSquared = 440 }),
            new AxisRuntimeState(new RobotJointLimits { AxisName = RobotAxisName.B, MinPositionDegrees = -120, MaxPositionDegrees = 120, MaxVelocityDegreesPerSecond = 220, MaxAccelerationDegreesPerSecondSquared = 440 }),
            new AxisRuntimeState(new RobotJointLimits { AxisName = RobotAxisName.T, MinPositionDegrees = -360, MaxPositionDegrees = 360, MaxVelocityDegreesPerSecond = 360, MaxAccelerationDegreesPerSecondSquared = 720 })
        ];

        _cartesianPose = CalculateApproximatePose();
    }

    public RobotTelemetrySnapshot GetSnapshot()
    {
        RobotAxisState[] axisStates = _axes.Select(axis => axis.ToAxisState()).ToArray();

        return new RobotTelemetrySnapshot
        {
            Manufacturer = Manufacturer,
            Model = Model,
            SerialNumber = SerialNumber,
            ProductCode = ProductCode,
            IsMoving = axisStates.Any(axis => Math.Abs(axis.TargetPositionDegrees - axis.PositionDegrees) > PositionToleranceDegrees
                || Math.Abs(axis.VelocityDegreesPerSecond) > VelocityToleranceDegreesPerSecond),
            Speed = axisStates.Max(axis => Math.Abs(axis.VelocityDegreesPerSecond)),
            Acceleration = _axes.Max(axis => Math.Abs(axis.LastAccelerationDegreesPerSecondSquared)),
            CartesianPose = _cartesianPose,
            AxisStates = axisStates,
            TimestampUtc = DateTimeOffset.UtcNow
        };
    }

    public void SetJointTarget(RobotJointTarget target)
    {
        AxisRuntimeState axis = GetAxis(target.AxisName);
        axis.TargetPositionDegrees = Clamp(
            target.TargetPositionDegrees,
            axis.Limits.MinPositionDegrees,
            axis.Limits.MaxPositionDegrees);
    }

    public void SetJointTargets(IEnumerable<RobotJointTarget> targets)
    {
        foreach (RobotJointTarget target in targets)
        {
            SetJointTarget(target);
        }
    }

    public void StopMotion()
    {
        foreach (AxisRuntimeState axis in _axes)
        {
            axis.TargetPositionDegrees = axis.PositionDegrees;
        }
    }

    public void Update(TimeSpan elapsed)
    {
        if (elapsed <= TimeSpan.Zero)
        {
            return;
        }

        double remainingSeconds = elapsed.TotalSeconds;
        while (remainingSeconds > 0)
        {
            double stepSeconds = Math.Min(remainingSeconds, MaxIntegrationStepSeconds);
            foreach (AxisRuntimeState axis in _axes)
            {
                UpdateAxis(axis, stepSeconds);
            }

            remainingSeconds -= stepSeconds;
        }

        _cartesianPose = CalculateApproximatePose();
    }

    private AxisRuntimeState GetAxis(RobotAxisName axisName)
    {
        return _axes.First(axis => axis.Limits.AxisName == axisName);
    }

    private static void UpdateAxis(AxisRuntimeState axis, double elapsedSeconds)
    {
        double distanceToTarget = axis.TargetPositionDegrees - axis.PositionDegrees;
        if (Math.Abs(distanceToTarget) <= PositionToleranceDegrees
            && Math.Abs(axis.VelocityDegreesPerSecond) <= VelocityToleranceDegreesPerSecond)
        {
            axis.PositionDegrees = axis.TargetPositionDegrees;
            axis.VelocityDegreesPerSecond = 0;
            axis.LastAccelerationDegreesPerSecondSquared = 0;
            UpdateThermalState(axis, elapsedSeconds, 0);
            return;
        }

        double direction = Math.Sign(distanceToTarget);
        double maxAcceleration = axis.Limits.MaxAccelerationDegreesPerSecondSquared;
        double maxVelocity = axis.Limits.MaxVelocityDegreesPerSecond;
        double desiredSpeed = Math.Min(maxVelocity, Math.Sqrt(2 * maxAcceleration * Math.Abs(distanceToTarget)));
        double desiredVelocity = direction * desiredSpeed;
        double previousVelocity = axis.VelocityDegreesPerSecond;
        double velocityDelta = Clamp(
            desiredVelocity - previousVelocity,
            -maxAcceleration * elapsedSeconds,
            maxAcceleration * elapsedSeconds);

        double nextVelocity = Clamp(previousVelocity + velocityDelta, -maxVelocity, maxVelocity);
        double nextPosition = axis.PositionDegrees + ((previousVelocity + nextVelocity) / 2.0 * elapsedSeconds);

        if (WouldCrossTarget(axis.PositionDegrees, nextPosition, axis.TargetPositionDegrees)
            && Math.Abs(nextVelocity) <= VelocityToleranceDegreesPerSecond)
        {
            nextPosition = axis.TargetPositionDegrees;
            nextVelocity = 0;
        }

        nextPosition = Clamp(nextPosition, axis.Limits.MinPositionDegrees, axis.Limits.MaxPositionDegrees);

        axis.PositionDegrees = nextPosition;
        axis.VelocityDegreesPerSecond = nextVelocity;
        axis.LastAccelerationDegreesPerSecondSquared = velocityDelta / elapsedSeconds;

        double accelerationRatio = Math.Abs(axis.LastAccelerationDegreesPerSecondSquared) / maxAcceleration;
        double velocityRatio = Math.Abs(axis.VelocityDegreesPerSecond) / maxVelocity;
        double motorLoadPercent = Clamp((velocityRatio * 55) + (accelerationRatio * 35), 0, 100);
        axis.MotorLoadPercent = Smooth(axis.MotorLoadPercent, motorLoadPercent, 0.25);
        UpdateThermalState(axis, elapsedSeconds, axis.MotorLoadPercent);
    }

    private CartesianPose CalculateApproximatePose()
    {
        double s = DegreesToRadians(GetAxis(RobotAxisName.S).PositionDegrees);
        double l = DegreesToRadians(GetAxis(RobotAxisName.L).PositionDegrees);
        double u = DegreesToRadians(GetAxis(RobotAxisName.U).PositionDegrees);

        double lowerArmMillimeters = 450;
        double upperArmMillimeters = 360;
        double reach = (Math.Cos(l) * lowerArmMillimeters) + (Math.Cos(l + u) * upperArmMillimeters);

        // Placeholder until proper forward kinematics is implemented from the robotics information model.
        return new CartesianPose
        {
            X = reach * Math.Cos(s),
            Y = reach * Math.Sin(s),
            Z = 520 + (Math.Sin(l) * lowerArmMillimeters) + (Math.Sin(l + u) * upperArmMillimeters),
            Rx = GetAxis(RobotAxisName.R).PositionDegrees,
            Ry = GetAxis(RobotAxisName.B).PositionDegrees,
            Rz = GetAxis(RobotAxisName.S).PositionDegrees + GetAxis(RobotAxisName.T).PositionDegrees
        };
    }

    private static bool WouldCrossTarget(double currentPosition, double nextPosition, double targetPosition)
    {
        return Math.Sign(targetPosition - currentPosition) != Math.Sign(targetPosition - nextPosition);
    }

    private static double Clamp(double value, double min, double max)
    {
        return Math.Min(Math.Max(value, min), max);
    }

    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    private static double Smooth(double currentValue, double targetValue, double factor)
    {
        return currentValue + ((targetValue - currentValue) * factor);
    }

    private static void UpdateThermalState(AxisRuntimeState axis, double elapsedSeconds, double motorLoadPercent)
    {
        if (motorLoadPercent > 0)
        {
            axis.TemperatureCelsius += (motorLoadPercent / 100.0) * 0.08 * elapsedSeconds;
            return;
        }

        axis.TemperatureCelsius = Math.Max(
            AmbientTemperatureCelsius,
            axis.TemperatureCelsius - (0.03 * elapsedSeconds));
    }

    private sealed class AxisRuntimeState
    {
        public AxisRuntimeState(RobotJointLimits limits)
        {
            Limits = limits;
            PositionDegrees = Clamp(0, limits.MinPositionDegrees, limits.MaxPositionDegrees);
            TargetPositionDegrees = PositionDegrees;
            TemperatureCelsius = AmbientTemperatureCelsius;
        }

        public RobotJointLimits Limits { get; }

        public double PositionDegrees { get; set; }

        public double VelocityDegreesPerSecond { get; set; }

        public double TargetPositionDegrees { get; set; }

        public double MotorLoadPercent { get; set; }

        public double TemperatureCelsius { get; set; }

        public double LastAccelerationDegreesPerSecondSquared { get; set; }

        public RobotAxisState ToAxisState()
        {
            return new RobotAxisState
            {
                AxisName = Limits.AxisName,
                PositionDegrees = PositionDegrees,
                VelocityDegreesPerSecond = VelocityDegreesPerSecond,
                TargetPositionDegrees = TargetPositionDegrees,
                MotorLoadPercent = MotorLoadPercent,
                TemperatureCelsius = TemperatureCelsius,
                MinPositionDegrees = Limits.MinPositionDegrees,
                MaxPositionDegrees = Limits.MaxPositionDegrees,
                MaxVelocityDegreesPerSecond = Limits.MaxVelocityDegreesPerSecond,
                MaxAccelerationDegreesPerSecondSquared = Limits.MaxAccelerationDegreesPerSecondSquared
            };
        }
    }
}
