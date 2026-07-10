namespace Robotics.Client.Core.Client;

public sealed record SubscriptionDataChangeOptions(
    int PublishingIntervalMilliseconds,
    int SamplingIntervalMilliseconds,
    uint QueueSize,
    bool DiscardOldest);
