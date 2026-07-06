using Robotics.ReferenceServer.Simulation;
using Robotics.Shared;
using System.Globalization;

namespace Robotics.ReferenceServer.Telemetry;

internal sealed record AxisTelemetryMessage
{
    public required double PositionDeg { get; init; }

    public required double VelocityDegPerSec { get; init; }
}

internal sealed record RobotTelemetryMessage
{
    public required string TimestampUtc { get; init; }

    public required IReadOnlyDictionary<string, AxisTelemetryMessage> Axes { get; init; }

    public string? CurrentProgramName { get; init; }

    public required string ProgramExecutionState { get; init; }

    public int? CurrentStepIndex { get; init; }

    public required int TotalStepCount { get; init; }

    public string? CurrentStepType { get; init; }

    public string? CurrentStepName { get; init; }

    public IReadOnlyDictionary<string, double>? ActiveTargetJointAngles { get; init; }

    public IReadOnlyDictionary<string, double>? NextTargetJointAngles { get; init; }

    public IReadOnlyList<IReadOnlyDictionary<string, double>>? QueuedTargetJointAngles { get; init; }

    public required bool IsMoving { get; init; }

    public static RobotTelemetryMessage FromSnapshots(
        RobotTelemetrySnapshot robotSnapshot,
        RobotProgramExecutionSnapshot programSnapshot)
    {
        var axes = new Dictionary<string, AxisTelemetryMessage>(StringComparer.Ordinal);

        foreach (RobotAxisState axisState in robotSnapshot.AxisStates.OrderBy(axis => axis.AxisName))
        {
            axes[axisState.AxisName.ToString()] = new AxisTelemetryMessage
            {
                PositionDeg = axisState.PositionDegrees,
                VelocityDegPerSec = axisState.VelocityDegreesPerSecond
            };
        }

        return new RobotTelemetryMessage
        {
            TimestampUtc = robotSnapshot.TimestampUtc.UtcDateTime.ToString("O", CultureInfo.InvariantCulture),
            Axes = axes,
            CurrentProgramName = string.IsNullOrWhiteSpace(programSnapshot.CurrentProgramName)
                ? null
                : programSnapshot.CurrentProgramName,
            ProgramExecutionState = programSnapshot.State.ToString(),
            CurrentStepIndex = programSnapshot.CurrentStepIndex >= 0 ? programSnapshot.CurrentStepIndex : null,
            TotalStepCount = programSnapshot.TotalStepCount,
            CurrentStepType = programSnapshot.CurrentStepType,
            CurrentStepName = programSnapshot.CurrentStepName,
            // Program/path preview data is metadata from the server-side program executor.
            // The browser may draw an approximate visual preview, but the simulation remains the source of truth.
            ActiveTargetJointAngles = ToJointAngleDictionary(programSnapshot.ActiveTargetJointAngles),
            NextTargetJointAngles = ToJointAngleDictionary(programSnapshot.NextTargetJointAngles),
            QueuedTargetJointAngles = programSnapshot.QueuedTargetJointAngles.Count == 0
                ? null
                : programSnapshot.QueuedTargetJointAngles
                    .Select(ToJointAngleDictionary)
                    .OfType<IReadOnlyDictionary<string, double>>()
                    .ToArray(),
            IsMoving = robotSnapshot.IsMoving
        };
    }

    private static IReadOnlyDictionary<string, double>? ToJointAngleDictionary(JointMoveTarget? target)
    {
        if (target is null)
        {
            return null;
        }

        return new Dictionary<string, double>(StringComparer.Ordinal)
        {
            [RobotAxisName.S.ToString()] = target.S,
            [RobotAxisName.L.ToString()] = target.L,
            [RobotAxisName.U.ToString()] = target.U,
            [RobotAxisName.R.ToString()] = target.R,
            [RobotAxisName.B.ToString()] = target.B,
            [RobotAxisName.T.ToString()] = target.T
        };
    }
}
