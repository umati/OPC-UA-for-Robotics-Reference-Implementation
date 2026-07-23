namespace Robotics.ClientGateway.Dtos;

public sealed record LiveConnectionMessageDto(
    string Type,
    string RobotId,
    string DisplayName,
    string EndpointUrl,
    bool Connected,
    string Selection,
    int PublishingIntervalMs,
    int SamplingIntervalMs,
    uint QueueSize,
    bool DiscardOldest);

public sealed record LiveSnapshotMessageDto(
    string Type,
    string RobotId,
    string DisplayName,
    string EndpointUrl,
    DateTime GeneratedAtUtc,
    IReadOnlyList<SnapshotSectionDto> Sections,
    MotionInventoryDto? MotionInventory = null);

public sealed record LiveDataChangeMessageDto(
    string Type,
    string RobotId,
    string DisplayName,
    string EndpointUrl,
    DateTime TimestampUtc,
    string Label,
    string BrowseName,
    string NodeId,
    string StatusCode,
    DateTime? SourceTimestamp,
    DateTime? ServerTimestamp,
    string ValueText,
    string Discovery,
    string? StableKey = null,
    string? MotionDeviceKey = null,
    string? AxisKey = null);

public sealed record LiveErrorMessageDto(
    string Type,
    string RobotId,
    string DisplayName,
    string Error,
    IReadOnlyList<string>? Details = null);

public sealed record LiveClosedMessageDto(
    string Type,
    string RobotId,
    string DisplayName,
    string Reason);

public sealed record LiveStreamOptions(
    SnapshotSelection Selection,
    int PublishingIntervalMs,
    int SamplingIntervalMs,
    uint QueueSize,
    bool DiscardOldest,
    bool SendInitialSnapshot);
