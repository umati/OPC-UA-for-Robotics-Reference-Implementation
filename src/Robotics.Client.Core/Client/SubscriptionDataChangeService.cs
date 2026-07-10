using Opc.Ua;
using Opc.Ua.Client;
using Robotics.Client.Core.Reporting;

namespace Robotics.Client.Core.Client;

public sealed class SubscriptionDataChangeService(Session session)
{
    public async Task<SubscriptionDataChangeResult> CreateAsync(
        IReadOnlyList<SnapshotNode> nodes,
        SubscriptionDataChangeOptions options,
        Action<DataChangeNotification> onNotification,
        Action<string> onCallbackError,
        CancellationToken cancellationToken)
    {
        var subscription = new Subscription
        {
            PublishingInterval = options.PublishingIntervalMilliseconds,
            PublishingEnabled = true,
            TimestampsToReturn = TimestampsToReturn.Both,
            DisplayName = "Robotics live data stream"
        };

        List<MonitoredItem> requestedItems = nodes
            .Select(node => CreateMonitoredItem(node, options, onNotification, onCallbackError))
            .ToList();

        try
        {
            subscription.AddItems(requestedItems);
            session.AddSubscription(subscription);

            await subscription.CreateAsync(cancellationToken);
            if (subscription.ChangesPending)
            {
                await subscription.ApplyChangesAsync(cancellationToken);
            }

            if (!subscription.CurrentPublishingEnabled)
            {
                await subscription.SetPublishingModeAsync(true, cancellationToken);
            }
        }
        catch
        {
            await DeleteSubscriptionBestEffortAsync(subscription);
            throw;
        }

        IReadOnlyList<SubscriptionDataChangeItemStatus> itemStatuses = requestedItems
            .Select(ToItemStatus)
            .ToArray();

        return new SubscriptionDataChangeResult(
            new SubscriptionDataChangeRegistration(subscription),
            itemStatuses);
    }

    private static MonitoredItem CreateMonitoredItem(
        SnapshotNode node,
        SubscriptionDataChangeOptions options,
        Action<DataChangeNotification> onNotification,
        Action<string> onCallbackError)
    {
        var item = new MonitoredItem
        {
            DisplayName = node.Label,
            StartNodeId = node.NodeId,
            AttributeId = Attributes.Value,
            MonitoringMode = MonitoringMode.Reporting,
            SamplingInterval = options.SamplingIntervalMilliseconds,
            QueueSize = options.QueueSize,
            DiscardOldest = options.DiscardOldest,
            Handle = node
        };

        item.Notification += (_, _) => OnNotification(item, onNotification, onCallbackError);
        return item;
    }

    private static void OnNotification(
        MonitoredItem item,
        Action<DataChangeNotification> onNotification,
        Action<string> onCallbackError)
    {
        try
        {
            if (item.Handle is not SnapshotNode node)
            {
                return;
            }

            foreach (DataValue dataValue in item.DequeueValues())
            {
                string valueText = StatusCode.IsNotGood(dataValue.StatusCode)
                    ? "<not available>"
                    : SafeValueRenderer.Format(dataValue.Value);

                onNotification(new DataChangeNotification(
                    node,
                    dataValue.StatusCode.ToString(),
                    DateTime.UtcNow,
                    ToNullableTimestamp(dataValue.SourceTimestamp),
                    ToNullableTimestamp(dataValue.ServerTimestamp),
                    valueText));
            }
        }
        catch (Exception ex)
        {
            onCallbackError($"Notification handling failed for {item.DisplayName}: {ex.Message}");
        }
    }

    private static SubscriptionDataChangeItemStatus ToItemStatus(MonitoredItem item)
    {
        var node = (SnapshotNode)item.Handle;
        return new SubscriptionDataChangeItemStatus(
            node,
            item.Created,
            item.Status?.Error?.ToString() ?? "Good",
            item.Status?.Id,
            item.Status?.SamplingInterval);
    }

    private static DateTime? ToNullableTimestamp(DateTime timestamp)
    {
        return timestamp == DateTime.MinValue || timestamp == default
            ? null
            : timestamp;
    }

    private static async Task DeleteSubscriptionBestEffortAsync(Subscription subscription)
    {
        try
        {
            await subscription.DeleteAsync(true, CancellationToken.None);
        }
        catch (Exception ex) when (ex is ServiceResultException or InvalidOperationException)
        {
        }
        finally
        {
            subscription.Dispose();
        }
    }
}

public sealed record SubscriptionDataChangeResult(
    SubscriptionDataChangeRegistration Registration,
    IReadOnlyList<SubscriptionDataChangeItemStatus> Items);
