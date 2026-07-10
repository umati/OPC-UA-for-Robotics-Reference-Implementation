namespace Robotics.ClientGateway.Dtos;

public sealed record LiveConnectionMessageDto(
    string Type,
    string EndpointUrl,
    bool Connected,
    string Selection,
    int PublishingIntervalMs,
    int SamplingIntervalMs,
    uint QueueSize,
    bool DiscardOldest);

public sealed record LiveSnapshotMessageDto(
    string Type,
    DateTime GeneratedAtUtc,
    IReadOnlyList<SnapshotSectionDto> Sections);

public sealed record LiveDataChangeMessageDto(
    string Type,
    DateTime TimestampUtc,
    string Label,
    string BrowseName,
    string NodeId,
    string StatusCode,
    DateTime? SourceTimestamp,
    DateTime? ServerTimestamp,
    string ValueText,
    string Discovery);

public sealed record LiveErrorMessageDto(
    string Type,
    string Error,
    IReadOnlyList<string>? Details = null);

public sealed record LiveClosedMessageDto(
    string Type,
    string Reason);

public sealed record LiveStreamOptions(
    SnapshotSelection Selection,
    int PublishingIntervalMs,
    int SamplingIntervalMs,
    uint QueueSize,
    bool DiscardOldest,
    bool SendInitialSnapshot);
