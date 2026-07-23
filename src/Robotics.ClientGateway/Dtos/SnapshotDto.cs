namespace Robotics.ClientGateway.Dtos;

public sealed record SnapshotDto(
    string EndpointUrl,
    bool Connected,
    bool RoboticsNamespaceFound,
    int? RoboticsNamespaceIndex,
    DateTime GeneratedAtUtc,
    IReadOnlyList<SnapshotSectionDto> Sections,
    IReadOnlyList<string> Warnings,
    MotionInventoryDto? MotionInventory = null);

public sealed record SnapshotSectionDto(
    string Name,
    IReadOnlyList<SnapshotValueDto> Values);

public sealed record SnapshotValueDto(
    string Label,
    string BrowseName,
    string NodeId,
    string? DataType,
    int? ValueRank,
    string StatusCode,
    DateTime? SourceTimestamp,
    DateTime? ServerTimestamp,
    string ValueText,
    string Discovery,
    string? EngineeringUnits = null,
    string? EURange = null,
    EngineeringUnitMetadataDto? EngineeringUnit = null,
    EuRangeMetadataDto? EURangeMetadata = null,
    string? EngineeringUnitsRaw = null,
    string? EURangeRaw = null,
    string? StableKey = null,
    string? MotionDeviceKey = null,
    string? AxisKey = null);

public sealed record MotionInventoryDto(string RobotIdentity, IReadOnlyList<MotionDeviceSystemInventoryDto> MotionDeviceSystems, IReadOnlyList<string> Diagnostics);
public sealed record MotionDeviceSystemInventoryDto(NodeDiscoveryDto System, IReadOnlyList<MotionDeviceInventoryDto> MotionDevices);
public sealed record MotionDeviceInventoryDto(NodeDiscoveryDto MotionDevice, IReadOnlyList<AxisInventoryDto> Axes, IReadOnlyList<string> Diagnostics);
public sealed record AxisInventoryDto(NodeDiscoveryDto Axis, string MotionDeviceKey, string StableKey, SnapshotValueDto? ActualPosition, IReadOnlyList<string> Diagnostics);

public sealed record EngineeringUnitMetadataDto(string? NamespaceUri, int? UnitId, string? DisplayName, string? Description, string? RawDiagnostic = null);
public sealed record EuRangeMetadataDto(double Low, double High, string? RawDiagnostic = null);

public enum SnapshotSelection
{
    States,
    Equipment,
    All
}

public sealed record SnapshotResult(
    SnapshotDto? Snapshot,
    ErrorDto? Error,
    int StatusCode);

public sealed record RobotDiagnosticsDto(string RobotId,string DisplayName,string EndpointUrl,DateTime GeneratedAtUtc,bool Connected,bool RoboticsNamespaceFound,int? RoboticsNamespaceIndex,IReadOnlyList<string> Warnings,IReadOnlyList<RobotDiagnosticsSectionDto> Sections);
public sealed record RobotDiagnosticsSectionDto(string Name,int ValueCount,int GoodCount,int NonGoodCount,IReadOnlyList<RobotDiagnosticsValueDto> Values);
public sealed record RobotDiagnosticsValueDto(string Label,string BrowseName,string NodeId,string StatusCode,DateTime? SourceTimestamp,DateTime? ServerTimestamp);
