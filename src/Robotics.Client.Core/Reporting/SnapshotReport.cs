using Opc.Ua;

namespace Robotics.Client.Core.Reporting;

public sealed record SnapshotReport(IReadOnlyList<SnapshotSectionReport> Sections);

public sealed record SnapshotSectionReport(
    string Name,
    IReadOnlyList<SnapshotValueReport> Values);

public sealed record SnapshotValueReport(
    string Label,
    string BrowseName,
    string NodeId,
    string? DataType,
    int? ValueRank,
    string StatusCode,
    DateTime? SourceTimestamp,
    DateTime? ServerTimestamp,
    string Value,
    bool Heuristic,
    string? EngineeringUnits = null,
    string? EURange = null,
    EngineeringUnitMetadataReport? EngineeringUnit = null,
    EuRangeMetadataReport? EURangeMetadata = null,
    string? EngineeringUnitsRaw = null,
    string? EURangeRaw = null,
    string? StableKey = null,
    string? MotionDeviceKey = null,
    string? AxisKey = null);

public sealed record EngineeringUnitMetadataReport(
    string? NamespaceUri,
    int? UnitId,
    string? DisplayName,
    string? Description,
    string? RawDiagnostic = null);

public sealed record EuRangeMetadataReport(
    double Low,
    double High,
    string? RawDiagnostic = null);

public sealed record EngineeringUnitDecodeResult(
    EngineeringUnitMetadataReport? Metadata,
    string? HumanReadable,
    string? RawDiagnostic);

public sealed record EuRangeDecodeResult(
    EuRangeMetadataReport? Metadata,
    string? HumanReadable,
    string? RawDiagnostic = null);

public sealed record SnapshotNode(
    string SectionName,
    string Label,
    string BrowseName,
    NodeId NodeId,
    bool Heuristic,
    string? StableKey = null,
    string? MotionDeviceKey = null,
    string? AxisKey = null);
