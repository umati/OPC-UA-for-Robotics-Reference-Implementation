using Microsoft.Extensions.Options;
using Opc.Ua;
using Robotics.Client.Core.Client;
using Robotics.Client.Core.Discovery;
using Robotics.Client.Core.Reporting;
using Robotics.ClientGateway.Dtos;
using Robotics.ClientGateway.Options;

namespace Robotics.ClientGateway.Services;

public sealed class GatewayOpcUaClient(
    IOptions<OpcUaOptions> opcUaOptions,
    ILogger<GatewayOpcUaClient> logger)
{
    public async Task<OpcUaStatusDto> GetStatusAsync(CancellationToken cancellationToken)
    {
        string endpointUrl = opcUaOptions.Value.EndpointUrl;

        try
        {
            ApplicationConfiguration configuration = await CreateConfigurationAsync();
            using var session = await new OpcUaSessionFactory(configuration).CreateSessionAsync(endpointUrl, cancellationToken);

            int roboticsNamespaceIndex = session.NamespaceUris.GetIndex(Opc.Ua.Robotics.Namespaces.Robotics);
            return new OpcUaStatusDto(
                endpointUrl,
                Connected: true,
                RoboticsNamespaceFound: roboticsNamespaceIndex >= 0,
                RoboticsNamespaceIndex: roboticsNamespaceIndex >= 0 ? roboticsNamespaceIndex : null,
                Error: null);
        }
        catch (Exception ex) when (IsExpectedOpcUaFailure(ex))
        {
            return new OpcUaStatusDto(
                endpointUrl,
                Connected: false,
                RoboticsNamespaceFound: false,
                RoboticsNamespaceIndex: null,
                Error: ToSafeError(ex));
        }
    }

    public async Task<DiscoveryDto> DiscoverAsync(CancellationToken cancellationToken)
    {
        string endpointUrl = opcUaOptions.Value.EndpointUrl;

        try
        {
            ApplicationConfiguration configuration = await CreateConfigurationAsync();

            // Implementation decision for C7: use a short-lived OPC UA session per request.
            // A reconnect/session manager belongs in a later streaming/API phase.
            using var session = await new OpcUaSessionFactory(configuration).CreateSessionAsync(endpointUrl, cancellationToken);
            DiscoveryReport report = new RoboticsDiscoveryService(session).Discover(endpointUrl);
            return ToDto(report);
        }
        catch (Exception ex) when (IsExpectedOpcUaFailure(ex))
        {
            return new DiscoveryDto(
                endpointUrl,
                Connected: false,
                RoboticsNamespaceIndex: null,
                Warnings: [ToSafeError(ex)],
                MotionDeviceSystems: []);
        }
    }

    private async Task<ApplicationConfiguration> CreateConfigurationAsync()
    {
        // Implementation decision: reuse the core UA client configuration discipline with a distinct
        // gateway application identity and repo-local development PKI store.
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

    private static bool IsExpectedOpcUaFailure(Exception ex)
    {
        return ex is ServiceResultException or System.Net.Sockets.SocketException or InvalidOperationException;
    }

    private static string ToSafeError(Exception ex)
    {
        if (ex is ServiceResultException serviceResultException)
        {
            return serviceResultException.StatusCode.ToString();
        }

        return ex.Message;
    }

    private static DiscoveryDto ToDto(DiscoveryReport report)
    {
        return new DiscoveryDto(
            report.EndpointUrl,
            report.Connected,
            report.RoboticsNamespaceIndex,
            report.Warnings,
            report.MotionDeviceSystems.Select(ToDto).ToArray());
    }

    private static MotionDeviceSystemDto ToDto(MotionDeviceSystemReport report)
    {
        return new MotionDeviceSystemDto(
            ToDto(report.Node),
            ToDtoOrNull(report.ControllersFolder),
            ToDtoOrNull(report.MotionDevicesFolder),
            report.Controllers.Select(ToDto).ToArray(),
            report.MotionDevices.Select(ToDto).ToArray());
    }

    private static ControllerDto ToDto(ControllerReport report)
    {
        return new ControllerDto(
            ToDto(report.Node),
            ToDtoOrNull(report.TaskControlsFolder),
            report.TaskControls.Select(ToDto).ToArray(),
            report.SystemOperation is null ? null : ToDto(report.SystemOperation));
    }

    private static MotionDeviceDto ToDto(MotionDeviceReport report)
    {
        return new MotionDeviceDto(
            ToDto(report.Node),
            ToDtoOrNull(report.AxesFolder),
            ToDtoOrNull(report.PowerTrainsFolder),
            report.Axes.Select(ToDto).ToArray(),
            report.PowerTrains.Select(ToDto).ToArray());
    }

    private static TaskControlDto ToDto(TaskControlReport report)
    {
        return new TaskControlDto(
            ToDto(report.Node),
            ToDtoOrNull(report.TaskControlOperation),
            report.Methods.Select(ToDto).ToArray());
    }

    private static OperationDto ToDto(OperationReport report)
    {
        return new OperationDto(
            ToDto(report.Node),
            ToDtoOrNull(report.StateMachine),
            report.IsExpectedType,
            report.Methods.Select(ToDto).ToArray());
    }

    private static MethodDto ToDto(MethodReport report)
    {
        return new MethodDto(
            report.Name,
            report.Found,
            report.BrowseName,
            report.DisplayName,
            report.NodeId,
            report.ParentNodeId,
            ToDto(report.InputArguments),
            ToDto(report.OutputArguments),
            report.Evidence);
    }

    private static MethodArgumentsDto ToDto(MethodArgumentsReport report)
    {
        return new MethodArgumentsDto(
            report.Status,
            report.PropertyNodeId,
            report.Arguments.Select(ToDto).ToArray(),
            report.Diagnostic);
    }

    private static MethodArgumentDto ToDto(MethodArgumentReport report)
    {
        return new MethodArgumentDto(
            report.Name,
            report.DataType,
            report.ValueRank,
            report.ArrayDimensions,
            report.Description);
    }

    private static NodeDiscoveryDto? ToDtoOrNull(NodeDiscoveryInfo? report)
    {
        return report is null ? null : ToDto(report);
    }

    private static NodeDiscoveryDto ToDto(NodeDiscoveryInfo report)
    {
        return new NodeDiscoveryDto(
            report.BrowseName,
            report.DisplayName,
            report.NodeId,
            report.TypeDefinition,
            report.Evidence);
    }
}
