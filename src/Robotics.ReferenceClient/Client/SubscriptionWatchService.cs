using Opc.Ua;
using Opc.Ua.Client;
using Robotics.Client.Core.Client;
using Robotics.Client.Core.Discovery;
using Robotics.Client.Core.Reporting;

namespace Robotics.ReferenceClient.Client;

internal sealed class SubscriptionWatchService(Session session)
{
    private static readonly string[] StateSections = ["SystemOperation", "TaskControlOperation"];
    private static readonly string[] EquipmentSections = ["MotionDevice", "Axes", "PowerTrains"];

    private readonly object _consoleLock = new();
    private int _notificationCount;
    private bool _verboseWatch;

    public async Task<int> WatchAsync(DiscoveryReport report, WatchCommand command)
    {
        _verboseWatch = command.VerboseWatch;

        IReadOnlyList<SnapshotNode> candidates = SelectNodes(new SnapshotDiscoveryService(session).Discover(report), command.Selection);
        if (candidates.Count == 0)
        {
            Console.WriteLine("Mode: watch");
            Console.WriteLine($"Selection: {command.SelectionLabel}");
            if (command.SelectionWasDefaulted)
            {
                Console.WriteLine("Selection default: states (implementation default)");
            }

            Console.WriteLine("No candidate Variable nodes were discovered for the requested watch selection.");
            return 1;
        }

        Console.WriteLine("Mode: watch");
        Console.WriteLine($"Selection: {command.SelectionLabel}");
        if (command.SelectionWasDefaulted)
        {
            Console.WriteLine("Selection default: states (implementation default)");
        }

        Console.WriteLine($"Duration: {command.DurationSeconds} s");
        Console.WriteLine($"Publishing interval: {command.PublishingIntervalMilliseconds} ms");
        Console.WriteLine($"Sampling interval: {command.SamplingIntervalMilliseconds} ms");
        Console.WriteLine($"Queue size: {command.QueueSize}");
        Console.WriteLine($"Discard oldest: {command.DiscardOldest.ToString().ToLowerInvariant()}");
        Console.WriteLine($"Monitored items: {candidates.Count}");

        PrintInitialSnapshot(candidates);

        using var subscription = new Subscription
        {
            PublishingInterval = command.PublishingIntervalMilliseconds,
            PublishingEnabled = true,
            TimestampsToReturn = TimestampsToReturn.Both,
            DisplayName = "Robotics.ReferenceClient watch"
        };

        List<MonitoredItem> requestedItems = candidates.Select(node => CreateMonitoredItem(node, command)).ToList();
        Verbose("creating subscription");
        Verbose($"adding monitored items: {requestedItems.Count}");
        subscription.AddItems(requestedItems);

        try
        {
            session.AddSubscription(subscription);
            Verbose("subscription added to session");
            await subscription.CreateAsync(CancellationToken.None);
            Verbose("subscription created on server");

            if (subscription.ChangesPending)
            {
                Verbose("applying pending subscription changes");
                await subscription.ApplyChangesAsync(CancellationToken.None);
            }

            if (!subscription.CurrentPublishingEnabled)
            {
                Verbose("enabling publishing mode on server");
                await subscription.SetPublishingModeAsync(true, CancellationToken.None);
            }
        }
        catch (Exception ex) when (ex is ServiceResultException or InvalidOperationException or ArgumentException)
        {
            Console.WriteLine($"Subscription creation failed: {ex.Message}");
            if (ex is ServiceResultException serviceResultException)
            {
                Console.WriteLine($"UA status: {serviceResultException.StatusCode}");
            }

            return 1;
        }

        IReadOnlyList<MonitoredItem> createdItems = requestedItems.Where(item => item.Created).ToArray();
        Console.WriteLine($"Subscription id: {subscription.Id}");
        Console.WriteLine($"Revised publishing interval: {subscription.CurrentPublishingInterval} ms");
        Console.WriteLine($"Publishing enabled: {subscription.CurrentPublishingEnabled.ToString().ToLowerInvariant()}");
        Console.WriteLine($"Monitored items created: {createdItems.Count}/{requestedItems.Count}");
        foreach (MonitoredItem failedItem in requestedItems.Where(item => !item.Created))
        {
            var node = (SnapshotNode)failedItem.Handle;
            string status = failedItem.Status?.Error?.ToString() ?? "not created";
            Console.WriteLine($"Monitored item failed: {node.Label} | NodeId={node.NodeId} | Status={status}");
        }

        PrintVerboseMonitoredItemStatus(requestedItems);

        if (createdItems.Count == 0)
        {
            Console.WriteLine("All monitored items failed to create.");
            await DeleteSubscriptionAsync(subscription);
            return 1;
        }

        Console.WriteLine($"Monitored items active: {createdItems.Count}");
        Console.WriteLine();
        Console.WriteLine("Data changes:");

        await Task.Delay(TimeSpan.FromSeconds(command.DurationSeconds));

        await DeleteSubscriptionAsync(subscription);

        Console.WriteLine();
        Console.WriteLine("Watch summary:");
        Console.WriteLine($"- Notifications received: {_notificationCount}");
        Console.WriteLine($"- Monitored items: {createdItems.Count}");
        Console.WriteLine("- Result: completed");
        return 0;
    }

    private static IReadOnlyList<SnapshotNode> SelectNodes(IReadOnlyList<SnapshotNode> nodes, WatchSelection selection)
    {
        return selection switch
        {
            WatchSelection.States => nodes.Where(node => StateSections.Contains(node.SectionName, StringComparer.Ordinal)).ToArray(),
            WatchSelection.Equipment => nodes.Where(node => EquipmentSections.Contains(node.SectionName, StringComparer.Ordinal)).ToArray(),
            WatchSelection.All => nodes.Where(node =>
                    StateSections.Contains(node.SectionName, StringComparer.Ordinal) ||
                    EquipmentSections.Contains(node.SectionName, StringComparer.Ordinal))
                .ToArray(),
            _ => []
        };
    }

    private void PrintInitialSnapshot(IReadOnlyList<SnapshotNode> candidates)
    {
        SnapshotReport snapshot = new SnapshotReadService(session).ReadNodes(candidates);

        Console.WriteLine();
        Console.WriteLine("Initial snapshot:");
        foreach (SnapshotValueReport value in snapshot.Sections.SelectMany(section => section.Values))
        {
            string evidence = value.Heuristic ? " | Discovery=heuristic" : string.Empty;
            Console.WriteLine($"- {value.Label} | NodeId={value.NodeId} | Status={value.StatusCode} | Value={value.Value}{evidence}");
        }

        Console.WriteLine();
    }

    private MonitoredItem CreateMonitoredItem(SnapshotNode node, WatchCommand command)
    {
        var item = new MonitoredItem
        {
            DisplayName = node.Label,
            StartNodeId = node.NodeId,
            AttributeId = Attributes.Value,
            MonitoringMode = MonitoringMode.Reporting,
            SamplingInterval = command.SamplingIntervalMilliseconds,
            QueueSize = command.QueueSize,
            DiscardOldest = command.DiscardOldest,
            Handle = node
        };

        item.Notification += OnNotification;
        return item;
    }

    private void OnNotification(MonitoredItem item, MonitoredItemNotificationEventArgs e)
    {
        try
        {
            Verbose($"notification callback invoked: {item.DisplayName}");
            if (item.Handle is not SnapshotNode node)
            {
                return;
            }

            foreach (DataValue dataValue in item.DequeueValues())
            {
                Interlocked.Increment(ref _notificationCount);
                string value = StatusCode.IsNotGood(dataValue.StatusCode)
                    ? "<not available>"
                    : SafeValueRenderer.Format(dataValue.Value);
                string sourceTimestamp = IsDefaultTimestamp(dataValue.SourceTimestamp) ? string.Empty : $" | SourceTimestamp={dataValue.SourceTimestamp:O}";
                string serverTimestamp = IsDefaultTimestamp(dataValue.ServerTimestamp) ? string.Empty : $" | ServerTimestamp={dataValue.ServerTimestamp:O}";
                string evidence = node.Heuristic ? " | Discovery=heuristic" : string.Empty;

                lock (_consoleLock)
                {
                    Console.WriteLine(
                        $"[{DateTime.Now:HH:mm:ss.fff}] {node.Label} | NodeId={node.NodeId} | Status={dataValue.StatusCode} | Value={value}{sourceTimestamp}{serverTimestamp}{evidence}");
                }
            }
        }
        catch (Exception ex)
        {
            lock (_consoleLock)
            {
                Console.WriteLine($"Notification callback failed for {item.DisplayName}: {ex.Message}");
            }
        }
    }

    private async Task DeleteSubscriptionAsync(Subscription subscription)
    {
        try
        {
            await subscription.DeleteAsync(true, CancellationToken.None);
        }
        catch (Exception ex) when (ex is ServiceResultException or InvalidOperationException)
        {
            Console.WriteLine($"Subscription cleanup reported: {ex.Message}");
        }
        finally
        {
            subscription.Dispose();
        }
    }

    private static bool IsDefaultTimestamp(DateTime timestamp)
    {
        return timestamp == DateTime.MinValue || timestamp == default;
    }

    private void PrintVerboseMonitoredItemStatus(IReadOnlyList<MonitoredItem> items)
    {
        if (!_verboseWatch)
        {
            return;
        }

        Console.WriteLine("Monitored item status:");
        foreach (MonitoredItem item in items)
        {
            var node = (SnapshotNode)item.Handle;
            string serverId = item.Status?.Id.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? "none";
            string status = item.Status?.Error?.ToString() ?? "Good";
            string revisedSamplingInterval = item.Status?.SamplingInterval.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? "unknown";
            Console.WriteLine(
                $"- {node.Label} | NodeId={node.NodeId} | Created={item.Created.ToString().ToLowerInvariant()} | ServerId={serverId} | Status={status} | RevisedSamplingInterval={revisedSamplingInterval} ms");
        }
    }

    private void Verbose(string message)
    {
        if (!_verboseWatch)
        {
            return;
        }

        lock (_consoleLock)
        {
            Console.WriteLine($"[watch verbose] {message}");
        }
    }
}
