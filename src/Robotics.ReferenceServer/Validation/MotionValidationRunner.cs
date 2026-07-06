using System.Text;
using Robotics.ReferenceServer.Simulation;
using Robotics.Shared;

namespace Robotics.ReferenceServer.Validation;

public sealed class MotionValidationRunner
{
    private static readonly TimeSpan SampleInterval = TimeSpan.FromMilliseconds(20);
    private static readonly TimeSpan MaximumSettlingScenarioDuration = TimeSpan.FromSeconds(30);
    private static readonly TimeSpan BoundedScenarioDuration = TimeSpan.FromSeconds(8);
    private const double JointLimitToleranceDegrees = 0.001;
    private const double SettlingPositionToleranceDegrees = 0.25;
    private const double SettlingVelocityToleranceDegreesPerSecond = 0.5;
    private const double VelocityToleranceRatio = 1.001;
    private const double AccelerationToleranceRatio = 1.001;
    private const double TeleportVelocityMultiplier = 1.5;

    private static readonly IReadOnlyDictionary<RobotAxisName, double> DemoMotionTargets = new Dictionary<RobotAxisName, double>
    {
        [RobotAxisName.S] = 45,
        [RobotAxisName.L] = -30,
        [RobotAxisName.U] = 60,
        [RobotAxisName.R] = 20,
        [RobotAxisName.B] = 15,
        [RobotAxisName.T] = 90
    };

    public string Run()
    {
        var simulation = new RobotSimulationService();
        RobotTelemetrySnapshot initialSnapshot = simulation.GetSnapshot();
        if (initialSnapshot.AxisStates.Count == 0)
        {
            return FormatUnavailableReport("The simulation did not expose any axis states.");
        }

        var axisReports = initialSnapshot.AxisStates
            .Select(axis => new AxisValidationReport(axis))
            .ToDictionary(axis => axis.AxisName);

        RobotJointTarget[] validationTarget = CreateSafeValidationTarget(initialSnapshot.AxisStates);
        RobotJointTarget[] homeTarget = CreateHomeTarget(initialSnapshot.AxisStates);
        RobotJointTarget[] demoTarget = CreateDemoMotionTarget(initialSnapshot.AxisStates);

        var scenarios = new List<ScenarioValidationReport>
        {
            RunSettlingScenario(simulation, axisReports, "Home to non-trivial joint target", validationTarget),
            RunSettlingScenario(simulation, axisReports, "Target back to home", homeTarget),
            RunBoundedScenario(simulation, axisReports, "Demo motion bounded run", demoTarget, BoundedScenarioDuration)
        };

        return FormatReport(axisReports.Values.OrderBy(axis => axis.AxisName), scenarios);
    }

    private static ScenarioValidationReport RunSettlingScenario(
        RobotSimulationService simulation,
        IReadOnlyDictionary<RobotAxisName, AxisValidationReport> axisReports,
        string scenarioName,
        IReadOnlyCollection<RobotJointTarget> targets)
    {
        simulation.SetJointTargets(targets);

        TimeSpan elapsed = TimeSpan.Zero;
        RobotTelemetrySnapshot previousSnapshot = simulation.GetSnapshot();
        RecordSnapshot(axisReports, previousSnapshot, null, SampleInterval);

        if (IsSettled(previousSnapshot))
        {
            return ScenarioValidationReport.CreateSettlingResult(
                scenarioName,
                passed: true,
                elapsed,
                MaximumSettlingScenarioDuration,
                previousSnapshot);
        }

        while (elapsed < MaximumSettlingScenarioDuration)
        {
            simulation.Update(SampleInterval);
            elapsed += SampleInterval;

            RobotTelemetrySnapshot snapshot = simulation.GetSnapshot();
            RecordSnapshot(axisReports, snapshot, previousSnapshot, SampleInterval);
            previousSnapshot = snapshot;

            if (IsSettled(snapshot))
            {
                return ScenarioValidationReport.CreateSettlingResult(
                    scenarioName,
                    passed: true,
                    elapsed,
                    MaximumSettlingScenarioDuration,
                    snapshot);
            }
        }

        return ScenarioValidationReport.CreateSettlingResult(
            scenarioName,
            passed: false,
            elapsed,
            MaximumSettlingScenarioDuration,
            previousSnapshot);
    }

    private static ScenarioValidationReport RunBoundedScenario(
        RobotSimulationService simulation,
        IReadOnlyDictionary<RobotAxisName, AxisValidationReport> axisReports,
        string scenarioName,
        IReadOnlyCollection<RobotJointTarget> targets,
        TimeSpan duration)
    {
        int startingViolationCount = GetTotalViolationCount(axisReports.Values);
        simulation.SetJointTargets(targets);

        TimeSpan elapsed = TimeSpan.Zero;
        RobotTelemetrySnapshot previousSnapshot = simulation.GetSnapshot();
        RecordSnapshot(axisReports, previousSnapshot, null, SampleInterval);

        while (elapsed < duration)
        {
            simulation.Update(SampleInterval);
            elapsed += SampleInterval;

            RobotTelemetrySnapshot snapshot = simulation.GetSnapshot();
            RecordSnapshot(axisReports, snapshot, previousSnapshot, SampleInterval);
            previousSnapshot = snapshot;
        }

        int scenarioViolationCount = GetTotalViolationCount(axisReports.Values) - startingViolationCount;
        return ScenarioValidationReport.CreateBoundedResult(
            scenarioName,
            scenarioViolationCount == 0,
            elapsed,
            duration,
            previousSnapshot,
            scenarioViolationCount);
    }

    private static bool IsSettled(RobotTelemetrySnapshot snapshot)
    {
        return snapshot.AxisStates.All(axis =>
            Math.Abs(axis.TargetPositionDegrees - axis.PositionDegrees) <= SettlingPositionToleranceDegrees
            && Math.Abs(axis.VelocityDegreesPerSecond) <= SettlingVelocityToleranceDegreesPerSecond);
    }

    private static RobotJointTarget[] CreateSafeValidationTarget(IReadOnlyList<RobotAxisState> axisStates)
    {
        return axisStates
            .Select(axis => new RobotJointTarget
            {
                AxisName = axis.AxisName,
                TargetPositionDegrees = CreateSafeTargetFromLimits(axis)
            })
            .ToArray();
    }

    private static RobotJointTarget[] CreateHomeTarget(IReadOnlyList<RobotAxisState> axisStates)
    {
        return axisStates
            .Select(axis => new RobotJointTarget
            {
                AxisName = axis.AxisName,
                TargetPositionDegrees = Clamp(0, axis.MinPositionDegrees, axis.MaxPositionDegrees)
            })
            .ToArray();
    }

    private static RobotJointTarget[] CreateDemoMotionTarget(IReadOnlyList<RobotAxisState> axisStates)
    {
        return axisStates
            .Select(axis =>
            {
                double requestedTarget = DemoMotionTargets.TryGetValue(axis.AxisName, out double target)
                    ? target
                    : CreateSafeTargetFromLimits(axis);

                return new RobotJointTarget
                {
                    AxisName = axis.AxisName,
                    TargetPositionDegrees = Clamp(requestedTarget, axis.MinPositionDegrees, axis.MaxPositionDegrees)
                };
            })
            .ToArray();
    }

    private static double CreateSafeTargetFromLimits(RobotAxisState axis)
    {
        return axis.AxisName switch
        {
            RobotAxisName.S => PositiveLimitFraction(axis, 0.70),
            RobotAxisName.L => NegativeLimitFraction(axis, 0.70),
            RobotAxisName.U => PositiveLimitFraction(axis, 0.65),
            RobotAxisName.R => NegativeLimitFraction(axis, 0.70),
            RobotAxisName.B => PositiveLimitFraction(axis, 0.60),
            RobotAxisName.T => PositiveLimitFraction(axis, 0.60),
            _ => Clamp(0, axis.MinPositionDegrees, axis.MaxPositionDegrees)
        };
    }

    private static double PositiveLimitFraction(RobotAxisState axis, double fraction)
    {
        if (axis.MaxPositionDegrees > 0)
        {
            return Clamp(axis.MaxPositionDegrees * fraction, axis.MinPositionDegrees, axis.MaxPositionDegrees);
        }

        return Clamp(0, axis.MinPositionDegrees, axis.MaxPositionDegrees);
    }

    private static double NegativeLimitFraction(RobotAxisState axis, double fraction)
    {
        if (axis.MinPositionDegrees < 0)
        {
            return Clamp(axis.MinPositionDegrees * fraction, axis.MinPositionDegrees, axis.MaxPositionDegrees);
        }

        return Clamp(0, axis.MinPositionDegrees, axis.MaxPositionDegrees);
    }

    private static void RecordSnapshot(
        IReadOnlyDictionary<RobotAxisName, AxisValidationReport> axisReports,
        RobotTelemetrySnapshot snapshot,
        RobotTelemetrySnapshot? previousSnapshot,
        TimeSpan sampleInterval)
    {
        foreach (RobotAxisState axisState in snapshot.AxisStates)
        {
            RobotAxisState? previousAxisState = previousSnapshot?.AxisStates
                .FirstOrDefault(axis => axis.AxisName == axisState.AxisName);

            axisReports[axisState.AxisName].Record(axisState, previousAxisState, sampleInterval);
        }
    }

    private static int GetTotalViolationCount(IEnumerable<AxisValidationReport> axisReports)
    {
        return axisReports.Sum(report => report.TotalViolationCount);
    }

    private static double GetMaximumPositionError(RobotTelemetrySnapshot snapshot)
    {
        return snapshot.AxisStates.Max(axis => Math.Abs(axis.TargetPositionDegrees - axis.PositionDegrees));
    }

    private static double GetMaximumAbsoluteVelocity(RobotTelemetrySnapshot snapshot)
    {
        return snapshot.AxisStates.Max(axis => Math.Abs(axis.VelocityDegreesPerSecond));
    }

    private static IReadOnlyList<AxisSettlingSnapshot> GetAxisSettlingSnapshots(RobotTelemetrySnapshot snapshot)
    {
        return snapshot.AxisStates
            .OrderBy(axis => axis.AxisName)
            .Select(axis => new AxisSettlingSnapshot(
                axis.AxisName,
                Math.Abs(axis.TargetPositionDegrees - axis.PositionDegrees),
                Math.Abs(axis.VelocityDegreesPerSecond)))
            .ToArray();
    }

    private static string FormatUnavailableReport(string reason)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Motion Validation Report");
        builder.AppendLine("========================");
        builder.AppendLine("Overall result: FAIL");
        builder.AppendLine();
        builder.AppendLine("Validator could not execute required scenarios.");
        AppendInvariant(builder, $"- {reason}");
        return builder.ToString();
    }

    private static string FormatReport(
        IEnumerable<AxisValidationReport> axisReports,
        IReadOnlyCollection<ScenarioValidationReport> scenarios)
    {
        AxisValidationReport[] reports = [.. axisReports];
        bool passed = scenarios.All(scenario => scenario.Passed)
            && reports.All(report => report.TotalViolationCount == 0);

        var builder = new StringBuilder();
        builder.AppendLine("Motion Validation Report");
        builder.AppendLine("========================");
        AppendInvariant(builder, $"Overall result: {(passed ? "PASS" : "FAIL")}");
        AppendInvariant(builder, $"Sample interval: {SampleInterval.TotalMilliseconds:F0} ms");
        AppendInvariant(builder, $"Settling timeout: {MaximumSettlingScenarioDuration.TotalSeconds:F0} s");
        AppendInvariant(builder, $"Settling tolerance: position <= {SettlingPositionToleranceDegrees:F2} deg, velocity <= {SettlingVelocityToleranceDegreesPerSecond:F2} deg/s");
        builder.AppendLine();

        builder.AppendLine("Scenarios");
        foreach (ScenarioValidationReport scenario in scenarios)
        {
            string status = scenario.Passed ? "PASS" : "FAIL";
            AppendInvariant(builder, $"- [{status}] {GetScenarioKindLabel(scenario.Kind)} - {scenario.Name}");
            AppendInvariant(builder, $"  elapsed simulated time: {scenario.Elapsed.TotalSeconds:F2} s");
            AppendInvariant(builder, $"  final maximum position error: {scenario.FinalMaximumPositionErrorDegrees:F3} deg");
            AppendInvariant(builder, $"  final maximum absolute velocity: {scenario.FinalMaximumAbsoluteVelocityDegreesPerSecond:F3} deg/s");

            if (scenario.Kind == ScenarioKind.FiniteSettling)
            {
                AppendInvariant(builder, $"  timeout: {scenario.Timeout.TotalSeconds:F0} s");
            }
            else
            {
                AppendInvariant(builder, $"  bounded run duration: {scenario.Timeout.TotalSeconds:F0} s");
                AppendInvariant(builder, $"  safety violations during scenario: {scenario.ScenarioViolationCount}");
            }
        }

        builder.AppendLine();
        builder.AppendLine("Axis Results");

        foreach (AxisValidationReport report in reports)
        {
            builder.AppendLine();
            AppendInvariant(builder, $"{report.AxisName}-axis");
            AppendInvariant(builder, $"  Position range: {report.MinimumObservedPositionDegrees:F3} to {report.MaximumObservedPositionDegrees:F3} deg");
            AppendInvariant(builder, $"  Max abs velocity: {report.MaximumObservedAbsoluteVelocityDegreesPerSecond:F3} deg/s");
            AppendInvariant(builder, $"  Max abs acceleration: {report.MaximumObservedAbsoluteAccelerationDegreesPerSecondSquared:F3} deg/s^2");
            AppendInvariant(builder, $"  Joint limit violations: {report.JointLimitViolations}");
            AppendInvariant(builder, $"  Velocity limit violations: {report.VelocityLimitViolations}");
            AppendInvariant(builder, $"  Acceleration limit violations: {report.AccelerationLimitViolations}");
            AppendInvariant(builder, $"  Teleport-like jumps: {report.TeleportLikeJumps}");

            foreach (string failureReason in report.FailureReasons)
            {
                AppendInvariant(builder, $"  Failure: {failureReason}");
            }
        }

        if (scenarios.Any(scenario => !scenario.Passed))
        {
            builder.AppendLine();
            builder.AppendLine("Scenario Failures");
            foreach (ScenarioValidationReport scenario in scenarios.Where(scenario => !scenario.Passed))
            {
                if (scenario.Kind == ScenarioKind.FiniteSettling)
                {
                    AppendInvariant(
                        builder,
                        $"- {scenario.Name} did not settle within {scenario.Timeout.TotalSeconds:F0} s; final max error {scenario.FinalMaximumPositionErrorDegrees:F3} deg, final max velocity {scenario.FinalMaximumAbsoluteVelocityDegreesPerSecond:F3} deg/s.");
                    foreach (AxisSettlingSnapshot axis in scenario.FinalAxisStates)
                    {
                        AppendInvariant(
                            builder,
                            $"  {axis.AxisName}: error {axis.PositionErrorDegrees:F3} deg, velocity {axis.AbsoluteVelocityDegreesPerSecond:F3} deg/s");
                    }
                }
                else
                {
                    AppendInvariant(
                        builder,
                        $"- {scenario.Name} recorded {scenario.ScenarioViolationCount} safety violation(s) during the bounded run.");
                }
            }
        }

        return builder.ToString();
    }

    private static void AppendInvariant(StringBuilder builder, FormattableString value)
    {
        builder.AppendLine(FormattableString.Invariant(value));
    }

    private static string GetScenarioKindLabel(ScenarioKind scenarioKind)
    {
        return scenarioKind switch
        {
            ScenarioKind.FiniteSettling => "finite settling",
            ScenarioKind.BoundedMotion => "bounded motion",
            _ => scenarioKind.ToString()
        };
    }

    private static double Clamp(double value, double min, double max)
    {
        return Math.Min(Math.Max(value, min), max);
    }

    private sealed class AxisValidationReport
    {
        private readonly double _minimumAllowedPositionDegrees;
        private readonly double _maximumAllowedPositionDegrees;
        private readonly double _maximumAllowedVelocityDegreesPerSecond;
        private readonly double _maximumAllowedAccelerationDegreesPerSecondSquared;
        private readonly List<string> _failureReasons = [];

        public AxisValidationReport(RobotAxisState initialAxisState)
        {
            AxisName = initialAxisState.AxisName;
            _minimumAllowedPositionDegrees = initialAxisState.MinPositionDegrees;
            _maximumAllowedPositionDegrees = initialAxisState.MaxPositionDegrees;
            _maximumAllowedVelocityDegreesPerSecond = initialAxisState.MaxVelocityDegreesPerSecond;
            _maximumAllowedAccelerationDegreesPerSecondSquared = initialAxisState.MaxAccelerationDegreesPerSecondSquared;
            MinimumObservedPositionDegrees = initialAxisState.PositionDegrees;
            MaximumObservedPositionDegrees = initialAxisState.PositionDegrees;
        }

        public RobotAxisName AxisName { get; }

        public double MinimumObservedPositionDegrees { get; private set; }

        public double MaximumObservedPositionDegrees { get; private set; }

        public double MaximumObservedAbsoluteVelocityDegreesPerSecond { get; private set; }

        public double MaximumObservedAbsoluteAccelerationDegreesPerSecondSquared { get; private set; }

        public int JointLimitViolations { get; private set; }

        public int VelocityLimitViolations { get; private set; }

        public int AccelerationLimitViolations { get; private set; }

        public int TeleportLikeJumps { get; private set; }

        public int TotalViolationCount =>
            JointLimitViolations
            + VelocityLimitViolations
            + AccelerationLimitViolations
            + TeleportLikeJumps;

        public IReadOnlyList<string> FailureReasons => _failureReasons;

        public void Record(
            RobotAxisState axisState,
            RobotAxisState? previousAxisState,
            TimeSpan sampleInterval)
        {
            MinimumObservedPositionDegrees = Math.Min(MinimumObservedPositionDegrees, axisState.PositionDegrees);
            MaximumObservedPositionDegrees = Math.Max(MaximumObservedPositionDegrees, axisState.PositionDegrees);

            double absoluteVelocity = Math.Abs(axisState.VelocityDegreesPerSecond);
            MaximumObservedAbsoluteVelocityDegreesPerSecond = Math.Max(MaximumObservedAbsoluteVelocityDegreesPerSecond, absoluteVelocity);

            if (axisState.PositionDegrees < _minimumAllowedPositionDegrees - JointLimitToleranceDegrees
                || axisState.PositionDegrees > _maximumAllowedPositionDegrees + JointLimitToleranceDegrees)
            {
                JointLimitViolations++;
                AddFailureReasonInvariant(
                    $"position {axisState.PositionDegrees:F3} deg outside allowed range {_minimumAllowedPositionDegrees:F3} to {_maximumAllowedPositionDegrees:F3} deg");
            }

            if (absoluteVelocity > _maximumAllowedVelocityDegreesPerSecond * VelocityToleranceRatio)
            {
                VelocityLimitViolations++;
                AddFailureReasonInvariant(
                    $"velocity {absoluteVelocity:F3} deg/s exceeds limit {_maximumAllowedVelocityDegreesPerSecond:F3} deg/s");
            }

            if (previousAxisState is null)
            {
                return;
            }

            double elapsedSeconds = sampleInterval.TotalSeconds;
            double absoluteAcceleration = Math.Abs(
                (axisState.VelocityDegreesPerSecond - previousAxisState.VelocityDegreesPerSecond) / elapsedSeconds);

            MaximumObservedAbsoluteAccelerationDegreesPerSecondSquared =
                Math.Max(MaximumObservedAbsoluteAccelerationDegreesPerSecondSquared, absoluteAcceleration);

            if (absoluteAcceleration > _maximumAllowedAccelerationDegreesPerSecondSquared * AccelerationToleranceRatio)
            {
                AccelerationLimitViolations++;
                AddFailureReasonInvariant(
                    $"acceleration {absoluteAcceleration:F3} deg/s^2 exceeds limit {_maximumAllowedAccelerationDegreesPerSecondSquared:F3} deg/s^2");
            }

            double absolutePositionStep = Math.Abs(axisState.PositionDegrees - previousAxisState.PositionDegrees);
            double teleportThreshold = _maximumAllowedVelocityDegreesPerSecond * elapsedSeconds * TeleportVelocityMultiplier;
            if (absolutePositionStep > teleportThreshold)
            {
                TeleportLikeJumps++;
                AddFailureReasonInvariant(
                    $"position step {absolutePositionStep:F3} deg exceeds teleport threshold {teleportThreshold:F3} deg");
            }
        }

        private void AddFailureReasonInvariant(FormattableString failureReason)
        {
            string invariantFailureReason = FormattableString.Invariant(failureReason);
            if (_failureReasons.Count < 5 && !_failureReasons.Contains(invariantFailureReason, StringComparer.Ordinal))
            {
                _failureReasons.Add(invariantFailureReason);
            }
        }
    }

    private sealed record ScenarioValidationReport(
        string Name,
        ScenarioKind Kind,
        bool Passed,
        TimeSpan Elapsed,
        TimeSpan Timeout,
        double FinalMaximumPositionErrorDegrees,
        double FinalMaximumAbsoluteVelocityDegreesPerSecond,
        int ScenarioViolationCount,
        IReadOnlyList<AxisSettlingSnapshot> FinalAxisStates)
    {
        public static ScenarioValidationReport CreateSettlingResult(
            string name,
            bool passed,
            TimeSpan elapsed,
            TimeSpan timeout,
            RobotTelemetrySnapshot finalSnapshot)
        {
            return new ScenarioValidationReport(
                name,
                ScenarioKind.FiniteSettling,
                passed,
                elapsed,
                timeout,
                MotionValidationRunner.GetMaximumPositionError(finalSnapshot),
                MotionValidationRunner.GetMaximumAbsoluteVelocity(finalSnapshot),
                0,
                MotionValidationRunner.GetAxisSettlingSnapshots(finalSnapshot));
        }

        public static ScenarioValidationReport CreateBoundedResult(
            string name,
            bool passed,
            TimeSpan elapsed,
            TimeSpan duration,
            RobotTelemetrySnapshot finalSnapshot,
            int scenarioViolationCount)
        {
            return new ScenarioValidationReport(
                name,
                ScenarioKind.BoundedMotion,
                passed,
                elapsed,
                duration,
                MotionValidationRunner.GetMaximumPositionError(finalSnapshot),
                MotionValidationRunner.GetMaximumAbsoluteVelocity(finalSnapshot),
                scenarioViolationCount,
                MotionValidationRunner.GetAxisSettlingSnapshots(finalSnapshot));
        }
    }

    private sealed record AxisSettlingSnapshot(
        RobotAxisName AxisName,
        double PositionErrorDegrees,
        double AbsoluteVelocityDegreesPerSecond);

    private enum ScenarioKind
    {
        FiniteSettling,
        BoundedMotion
    }
}
