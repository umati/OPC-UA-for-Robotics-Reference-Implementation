using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using HttpStatusCodes = Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.Extensions.Options;
using Opc.Ua;
using Robotics.Client.Core.Client;
using CoreDataChangeNotification = Robotics.Client.Core.Client.DataChangeNotification;
using Robotics.Client.Core.Discovery;
using Robotics.Client.Core.Reporting;
using Robotics.ClientGateway.Dtos;
using Robotics.ClientGateway.Options;


namespace Robotics.ClientGateway.Services;

public sealed class LiveStreamService(
    IOptions<OpcUaOptions> opcUaOptions,
    ILogger<LiveStreamService> logger)
{
    private static readonly string[] StateSnapshotSections = ["SystemOperation", "TaskControlOperation"];
    private static readonly string[] EquipmentSnapshotSections = ["MotionDevice", "Axes", "PowerTrains"];
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public Task HandleAsync(HttpContext context, CancellationToken cancellationToken) => HandleAsync(context, new RobotConnectionOptions
    {
        Id = "default-robot", DisplayName = "Default Robot Server", EndpointUrl = opcUaOptions.Value.EndpointUrl, Enabled = true
    }, cancellationToken);

    public async Task HandleAsync(HttpContext context, RobotConnectionOptions robot, CancellationToken cancellationToken)
    {
        if (!context.WebSockets.IsWebSocketRequest)
        {
            context.Response.StatusCode = HttpStatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(
                new ErrorDto("WebSocket required", "Use a WebSocket client for /ws/robotics/live.", opcUaOptions.Value.EndpointUrl),
                cancellationToken);
            return;
        }

        if (!TryParseOptions(context.Request.Query, out LiveStreamOptions options, out string? validationError))
        {
            context.Response.StatusCode = HttpStatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(
                new ErrorDto("Invalid live stream query", validationError ?? "Invalid live stream query.", opcUaOptions.Value.EndpointUrl),
                cancellationToken);
            return;
        }

        using WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await StreamAsync(webSocket, robot, options, cancellationToken);
    }

    private async Task StreamAsync(WebSocket webSocket, RobotConnectionOptions robot, LiveStreamOptions options, CancellationToken requestAborted)
    {
        string endpointUrl = robot.EndpointUrl;
        using var streamCancellation = CancellationTokenSource.CreateLinkedTokenSource(requestAborted);
        Channel<object> outgoing = Channel.CreateUnbounded<object>(new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        });

        Task receiveTask = ReceiveUntilClosedAsync(webSocket, streamCancellation);
        Task writerTask = WriteMessagesAsync(webSocket, outgoing.Reader, streamCancellation.Token);

        try
        {
            ApplicationConfiguration configuration = await CreateConfigurationAsync();
            using var session = await new OpcUaSessionFactory(configuration).CreateSessionAsync(endpointUrl, streamCancellation.Token);
            logger.LogInformation("OPC UA session opened for robot {RobotId} at {EndpointUrl}", robot.Id, endpointUrl);

            await outgoing.Writer.WriteAsync(new LiveConnectionMessageDto(
                "connection",
                robot.Id, robot.DisplayName,
                endpointUrl,
                Connected: true,
                ToSelectionText(options.Selection),
                options.PublishingIntervalMs,
                options.SamplingIntervalMs,
                options.QueueSize,
                options.DiscardOldest), streamCancellation.Token);

            DiscoveryReport discoveryReport = new RoboticsDiscoveryService(session).Discover(endpointUrl);
            if (!discoveryReport.Connected || discoveryReport.RoboticsNamespaceIndex is null)
            {
                await SendErrorAndCloseAsync(
                    outgoing,
                    streamCancellation,
                    robot,
                    "Robotics discovery failed.",
                    discoveryReport.Warnings,
                    "discovery failed");
                return;
            }

            // Official OPC UA truth: this stream uses a Subscription with MonitoredItems on the Value attribute
            // and forwards DataChange StatusCodes without interpreting Robotics state transitions.
            // Local NodeSet/generated-code truth: candidate Variables come from RoboticsDiscoveryService plus
            // SnapshotDiscoveryService, which uses generated Robotics BrowseName constants and current address-space discovery.
            // Implementation decision: the gateway filters those discovered snapshot/watch nodes by selection and serializes
            // notifications as WebSocket JSON; it does not hardcode instance NodeIds or poll values as notifications.
            IReadOnlyList<SnapshotNode> snapshotNodes = new SnapshotDiscoveryService(session).Discover(discoveryReport);
            IReadOnlyList<SnapshotNode> selectedNodes = FilterSnapshotNodes(snapshotNodes, options.Selection);
            if (selectedNodes.Count == 0)
            {
                await SendErrorAndCloseAsync(
                    outgoing,
                    streamCancellation,
                    robot,
                    "No live-data nodes were discovered for the requested selection.",
                    null,
                    "no nodes");
                return;
            }

            if (options.SendInitialSnapshot)
            {
                SnapshotReport snapshotReport = new SnapshotReadService(session).ReadNodes(selectedNodes);
                await outgoing.Writer.WriteAsync(new LiveSnapshotMessageDto(
                    "snapshot",
                    robot.Id, robot.DisplayName, endpointUrl,
                    DateTime.UtcNow,
                    snapshotReport.Sections.Select(ToDto).ToArray()), streamCancellation.Token);
            }

            await using SubscriptionDataChangeRegistration registration = await CreateSubscriptionAsync(
                session,
                selectedNodes,
                options,
                robot,
                outgoing,
                streamCancellation.Token);

            await receiveTask;
            await outgoing.Writer.WriteAsync(new LiveClosedMessageDto("closed", robot.Id, robot.DisplayName, "client closed"), CancellationToken.None);
        }
        catch (OperationCanceledException)
        {
            await TryWriteAsync(outgoing.Writer, new LiveClosedMessageDto("closed", robot.Id, robot.DisplayName, "request cancelled"));
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Live robotics stream failed for endpoint {EndpointUrl}", endpointUrl);
            await TryWriteAsync(outgoing.Writer, new LiveErrorMessageDto("error", robot.Id, robot.DisplayName, ToSafeError(ex)));
        }
        finally
        {
            outgoing.Writer.TryComplete();
            await WaitForWriterAsync(writerTask);
            if (webSocket.State == WebSocketState.Open)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "stream ended", CancellationToken.None);
            }

            streamCancellation.Cancel();
        }
    }

    private async Task<SubscriptionDataChangeRegistration> CreateSubscriptionAsync(
        Opc.Ua.Client.Session session,
        IReadOnlyList<SnapshotNode> selectedNodes,
        LiveStreamOptions options,
        RobotConnectionOptions robot,
        Channel<object> outgoing,
        CancellationToken cancellationToken)
    {
        var subscriptionOptions = new SubscriptionDataChangeOptions(
            options.PublishingIntervalMs,
            options.SamplingIntervalMs,
            options.QueueSize,
            options.DiscardOldest);

        SubscriptionDataChangeResult result = await new SubscriptionDataChangeService(session).CreateAsync(
            selectedNodes,
            subscriptionOptions,
            notification =>
            {
                if (!outgoing.Writer.TryWrite(ToDto(notification, robot)))
                {
                    logger.LogWarning("Live robotics stream output channel rejected DataChange notification for {Label}", notification.Node.Label);
                }
            },
            error =>
            {
                logger.LogWarning("Live robotics stream notification callback failed: {Error}", error);
                outgoing.Writer.TryWrite(new LiveErrorMessageDto("error", robot.Id, robot.DisplayName, error));
            },
            cancellationToken);

        logger.LogInformation("OPC UA subscription created for robot {RobotId} at {EndpointUrl}; requested monitored item count {Count}", robot.Id, robot.EndpointUrl, selectedNodes.Count);

        IReadOnlyList<SubscriptionDataChangeItemStatus> failedItems = result.Items
            .Where(item => !item.Created)
            .ToArray();
        IReadOnlyList<SubscriptionDataChangeItemStatus> activeItems = result.Items
            .Where(item => item.Created)
            .ToArray();

        if (failedItems.Count > 0)
        {
            await outgoing.Writer.WriteAsync(new LiveErrorMessageDto(
                "error",
                robot.Id,
                robot.DisplayName,
                activeItems.Count > 0
                    ? "Some monitored items failed to create."
                    : "All monitored items failed to create.",
                failedItems.Select(item => $"{item.Node.Label}: {item.Status}").ToArray()), cancellationToken);
        }

        if (activeItems.Count == 0)
        {
            await result.Registration.DisposeAsync();
            throw new InvalidOperationException("All monitored items failed to create.");
        }

        logger.LogInformation("OPC UA subscription active for robot {RobotId} at {EndpointUrl}; monitored item count {Count}", robot.Id, robot.EndpointUrl, activeItems.Count);

        return result.Registration;
    }

    private async Task SendErrorAndCloseAsync(
        Channel<object> outgoing,
        CancellationTokenSource streamCancellation,
        RobotConnectionOptions robot,
        string error,
        IReadOnlyList<string>? details,
        string closeReason)
    {
        await outgoing.Writer.WriteAsync(new LiveErrorMessageDto("error", robot.Id, robot.DisplayName, error, details), streamCancellation.Token);
        await outgoing.Writer.WriteAsync(new LiveClosedMessageDto("closed", robot.Id, robot.DisplayName, closeReason), streamCancellation.Token);
    }

    private static async Task ReceiveUntilClosedAsync(WebSocket webSocket, CancellationTokenSource streamCancellation)
    {
        byte[] buffer = new byte[1024];
        var receiveBuffer = new ArraySegment<byte>(buffer);
        try
        {
            while (webSocket.State == WebSocketState.Open && !streamCancellation.IsCancellationRequested)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(receiveBuffer, streamCancellation.Token);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            streamCancellation.Cancel();
        }
    }

    private static async Task WriteMessagesAsync(WebSocket webSocket, ChannelReader<object> reader, CancellationToken cancellationToken)
    {
        await foreach (object message in reader.ReadAllAsync(cancellationToken))
        {
            if (webSocket.State != WebSocketState.Open)
            {
                break;
            }

            string json = JsonSerializer.Serialize(message, message.GetType(), JsonOptions);
            byte[] payload = Encoding.UTF8.GetBytes(json);
            await webSocket.SendAsync(new ArraySegment<byte>(payload), WebSocketMessageType.Text, endOfMessage: true, cancellationToken);
        }
    }

    private static async Task TryWriteAsync(ChannelWriter<object> writer, object message)
    {
        try
        {
            await writer.WriteAsync(message, CancellationToken.None);
        }
        catch (ChannelClosedException)
        {
        }
    }

    private static async Task WaitForWriterAsync(Task writerTask)
    {
        try
        {
            await writerTask;
        }
        catch (OperationCanceledException)
        {
        }
        catch (WebSocketException)
        {
        }
    }

    private async Task<ApplicationConfiguration> CreateConfigurationAsync()
    {
        var application = new OpcUaClientApplication(
            applicationName: "Robotics.ClientGateway",
            applicationUriSuffix: "RoboticsClientGateway",
            productUri: "urn:RoboticsReferenceImplementation:ClientGateway",
            pkiRootPath: "pki/client-gateway",
            traceOutputPath: "Logs/RoboticsClientGateway.log",
            developmentCertificateAccepted: subject =>
            {
                logger.LogWarning(
                    "Development certificate policy: auto-accepting untrusted server certificate. Certificate subject: {Subject}",
                    subject);
            });

        return await application.CreateConfigurationAsync();
    }

    private static bool TryParseOptions(IQueryCollection query, out LiveStreamOptions options, out string? error)
    {
        options = new LiveStreamOptions(SnapshotSelection.States, 500, 250, 10, true, true);
        error = null;

        if (!TryParseSelection(query["selection"].FirstOrDefault(), out SnapshotSelection selection))
        {
            error = "selection must be one of: states, equipment, all.";
            return false;
        }

        if (!TryParsePositiveInt(query["publishingIntervalMs"].FirstOrDefault(), 500, out int publishingIntervalMs))
        {
            error = "publishingIntervalMs must be a positive integer.";
            return false;
        }

        if (!TryParsePositiveInt(query["samplingIntervalMs"].FirstOrDefault(), 250, out int samplingIntervalMs))
        {
            error = "samplingIntervalMs must be a positive integer.";
            return false;
        }

        if (!TryParsePositiveUInt(query["queueSize"].FirstOrDefault(), 10, out uint queueSize))
        {
            error = "queueSize must be a positive integer.";
            return false;
        }

        if (!TryParseBool(query["discardOldest"].FirstOrDefault(), true, out bool discardOldest))
        {
            error = "discardOldest must be true or false.";
            return false;
        }

        if (!TryParseBool(query["sendInitialSnapshot"].FirstOrDefault(), true, out bool sendInitialSnapshot))
        {
            error = "sendInitialSnapshot must be true or false.";
            return false;
        }

        options = new LiveStreamOptions(
            selection,
            publishingIntervalMs,
            samplingIntervalMs,
            queueSize,
            discardOldest,
            sendInitialSnapshot);
        return true;
    }

    private static bool TryParseSelection(string? selection, out SnapshotSelection parsed)
    {
        if (string.IsNullOrWhiteSpace(selection) ||
            string.Equals(selection, "states", StringComparison.OrdinalIgnoreCase))
        {
            parsed = SnapshotSelection.States;
            return true;
        }

        if (string.Equals(selection, "equipment", StringComparison.OrdinalIgnoreCase))
        {
            parsed = SnapshotSelection.Equipment;
            return true;
        }

        if (string.Equals(selection, "all", StringComparison.OrdinalIgnoreCase))
        {
            parsed = SnapshotSelection.All;
            return true;
        }

        parsed = SnapshotSelection.States;
        return false;
    }

    private static bool TryParsePositiveInt(string? value, int defaultValue, out int parsed)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            parsed = defaultValue;
            return true;
        }

        return int.TryParse(value, out parsed) && parsed > 0;
    }

    private static bool TryParsePositiveUInt(string? value, uint defaultValue, out uint parsed)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            parsed = defaultValue;
            return true;
        }

        return uint.TryParse(value, out parsed) && parsed > 0;
    }

    private static bool TryParseBool(string? value, bool defaultValue, out bool parsed)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            parsed = defaultValue;
            return true;
        }

        return bool.TryParse(value, out parsed);
    }

    private static IReadOnlyList<SnapshotNode> FilterSnapshotNodes(IReadOnlyList<SnapshotNode> nodes, SnapshotSelection selection)
    {
        IReadOnlyList<string> selectedSections = GetSelectedSections(selection);
        return nodes
            .Where(node => selectedSections.Contains(node.SectionName, StringComparer.Ordinal))
            .ToArray();
    }

    private static IReadOnlyList<string> GetSelectedSections(SnapshotSelection selection)
    {
        return selection switch
        {
            SnapshotSelection.States => StateSnapshotSections,
            SnapshotSelection.Equipment => EquipmentSnapshotSections,
            SnapshotSelection.All => StateSnapshotSections.Concat(EquipmentSnapshotSections).ToArray(),
            _ => StateSnapshotSections
        };
    }

    private static string ToSelectionText(SnapshotSelection selection)
    {
        return selection switch
        {
            SnapshotSelection.States => "states",
            SnapshotSelection.Equipment => "equipment",
            SnapshotSelection.All => "all",
            _ => "states"
        };
    }

    private static LiveDataChangeMessageDto ToDto(CoreDataChangeNotification notification, RobotConnectionOptions robot)
    {
        return new LiveDataChangeMessageDto(
            "dataChange",
            robot.Id, robot.DisplayName, robot.EndpointUrl,
            notification.TimestampUtc,
            notification.Node.Label,
            notification.Node.BrowseName,
            notification.Node.NodeId.ToString(),
            notification.StatusCode,
            notification.SourceTimestamp,
            notification.ServerTimestamp,
            notification.ValueText,
            notification.Node.Heuristic ? "heuristic" : "standard");
    }

    private static SnapshotSectionDto ToDto(SnapshotSectionReport report)
    {
        return new SnapshotSectionDto(
            report.Name,
            report.Values.Select(ToDto).ToArray());
    }

    private static SnapshotValueDto ToDto(SnapshotValueReport report)
    {
        return new SnapshotValueDto(
            report.Label,
            report.BrowseName,
            report.NodeId,
            report.DataType,
            report.ValueRank,
            report.StatusCode,
            report.SourceTimestamp,
            report.ServerTimestamp,
            report.Value,
            report.Heuristic ? "heuristic" : "standard",
            report.EngineeringUnits,
            report.EURange);
    }

    private static bool IsExpectedOpcUaFailure(Exception ex)
    {
        return ex is ServiceResultException or System.Net.Sockets.SocketException or InvalidOperationException or WebSocketException;
    }

    private static string ToSafeError(Exception ex)
    {
        if (ex is ServiceResultException serviceResultException)
        {
            return serviceResultException.StatusCode.ToString();
        }

        return ex.Message;
    }
}
