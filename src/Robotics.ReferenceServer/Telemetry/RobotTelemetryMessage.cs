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
            IsMoving = robotSnapshot.IsMoving
        };
    }
}
