using Robotics.Client.Core.Reporting;

namespace Robotics.Client.Core.Client;

public sealed record DataChangeNotification(
    SnapshotNode Node,
    string StatusCode,
    DateTime TimestampUtc,
    DateTime? SourceTimestamp,
    DateTime? ServerTimestamp,
    string ValueText,
    string? StableKey = null,
    string? MotionDeviceKey = null,
    string? AxisKey = null);

public sealed record SubscriptionDataChangeItemStatus(
    SnapshotNode Node,
    bool Created,
    string Status,
    uint? ServerId,
    double? RevisedSamplingInterval);
