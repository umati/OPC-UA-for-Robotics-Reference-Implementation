using Microsoft.Extensions.Options;
using Opc.Ua;
using HttpStatusCodes = Microsoft.AspNetCore.Http.StatusCodes;
using Robotics.Client.Core.Client;
using Robotics.Client.Core.Discovery;
using Robotics.Client.Core.Reporting;
using Robotics.ClientGateway.Dtos;
using Robotics.ClientGateway.Options;

namespace Robotics.ClientGateway.Services;

public sealed class GatewayOpcUaClient(
    IOptions<OpcUaOptions> opcUaOptions,
    ILogger<GatewayOpcUaClient> logger,
    IWebHostEnvironment environment,
    IConfiguration configuration)
{
    private static readonly string[] StateSnapshotSections = ["SystemOperation", "TaskControlOperation"];
    private static readonly string[] EquipmentSnapshotSections = ["MotionDevice", "Axes", "PowerTrains"];

    public async Task<OpcUaStatusDto> GetStatusAsync(CancellationToken cancellationToken)
    {
        return await GetStatusAsync(opcUaOptions.Value.EndpointUrl, cancellationToken);
    }

    public async Task<OpcUaStatusDto> GetStatusAsync(string endpointUrl, CancellationToken cancellationToken, string robotId = "unknown", string displayName = "unknown")
    {

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
                Error: ToSafeError(ex, endpointUrl, robotId, displayName));
        }
    }

    public async Task<DiscoveryDto> DiscoverAsync(CancellationToken cancellationToken)
    {
        return await DiscoverAsync(opcUaOptions.Value.EndpointUrl, cancellationToken);
    }

    public async Task<DiscoveryDto> DiscoverAsync(string endpointUrl, CancellationToken cancellationToken, string robotId = "unknown", string displayName = "unknown")
    {

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
                Warnings: [ToSafeError(ex, endpointUrl, robotId, displayName)],
                MotionDeviceSystems: []);
        }
    }

    public async Task<SnapshotResult> GetSnapshotAsync(SnapshotSelection selection, CancellationToken cancellationToken)
    {
        return await GetSnapshotAsync(opcUaOptions.Value.EndpointUrl, selection, cancellationToken);
    }

    public async Task<SnapshotResult> GetSnapshotAsync(string endpointUrl, SnapshotSelection selection, CancellationToken cancellationToken, string robotId = "unknown", string displayName = "unknown")
    {

        try
        {
            ApplicationConfiguration configuration = await CreateConfigurationAsync();

            // Implementation decision for C8: keep C7's short-lived session per HTTP request.
            using var session = await new OpcUaSessionFactory(configuration).CreateSessionAsync(endpointUrl, cancellationToken);

            DiscoveryReport discoveryReport = new RoboticsDiscoveryService(session).Discover(endpointUrl);
            if (!discoveryReport.Connected)
            {
                return Failure(
                    "OPC UA connection failed",
                    "Discovery did not report an active OPC UA connection.",
                    endpointUrl,
                    HttpStatusCodes.Status502BadGateway);
            }

            if (discoveryReport.RoboticsNamespaceIndex is null)
            {
                return Failure(
                    "Robotics discovery failed",
                    string.Join("; ", discoveryReport.Warnings.DefaultIfEmpty("Robotics namespace is missing.")),
                    endpointUrl,
                    HttpStatusCodes.Status424FailedDependency);
            }

            // Official specification truth: these are read-only OPC UA Value/DataType/ValueRank attribute reads.
            // Local NodeSet/generated-code truth: snapshot discovery starts from the standards-aware discovery report
            // and generated Robotics BrowseName constants already used by the console client.
            // Implementation decision: the gateway filters core-discovered snapshot sections by category and does not
            // invent additional browse rules, NodeIds, state semantics, or value interpretations.
            IReadOnlyList<SnapshotNode> snapshotNodes = new SnapshotDiscoveryService(session).Discover(discoveryReport);
            IReadOnlyList<SnapshotNode> selectedNodes = FilterSnapshotNodes(snapshotNodes, selection);
            SnapshotReport snapshotReport = new SnapshotReadService(session).ReadNodes(selectedNodes);
            IReadOnlyList<string> selectedSections = GetSelectedSections(selection);

            var snapshot = new SnapshotDto(
                endpointUrl,
                Connected: true,
                RoboticsNamespaceFound: true,
                RoboticsNamespaceIndex: discoveryReport.RoboticsNamespaceIndex,
                GeneratedAtUtc: DateTime.UtcNow,
                Sections: snapshotReport.Sections
                    .Where(section => selectedSections.Contains(section.Name, StringComparer.Ordinal))
                    .Select(ToDto)
                    .ToArray(),
                Warnings: discoveryReport.Warnings);

            return new SnapshotResult(snapshot, Error: null, HttpStatusCodes.Status200OK);
        }
        catch (Exception ex) when (IsExpectedOpcUaFailure(ex))
        {
            return Failure(
                "OPC UA connection failed",
                ToSafeError(ex, endpointUrl, robotId, displayName),
                endpointUrl,
                HttpStatusCodes.Status502BadGateway);
        }
    }

    public async Task<(RobotDiagnosticsDto? Diagnostics, ErrorDto? Error, int StatusCode)> GetDiagnosticsAsync(string robotId,string displayName,string endpointUrl,CancellationToken cancellationToken)
    {
        DiscoveryDto discovery=await DiscoverAsync(endpointUrl,cancellationToken,robotId,displayName);
        SnapshotResult snapshotResult=await GetSnapshotAsync(endpointUrl,SnapshotSelection.All,cancellationToken,robotId,displayName);
        if(snapshotResult.Snapshot is null)return (null,snapshotResult.Error,snapshotResult.StatusCode);
        SnapshotDto snapshot=snapshotResult.Snapshot;
        var sections=snapshot.Sections.Select(section=>new RobotDiagnosticsSectionDto(section.Name,section.Values.Count,section.Values.Count(v=>v.StatusCode.StartsWith("Good",StringComparison.OrdinalIgnoreCase)),section.Values.Count(v=>!v.StatusCode.StartsWith("Good",StringComparison.OrdinalIgnoreCase)),section.Values.Select(v=>new RobotDiagnosticsValueDto(v.Label,v.BrowseName,v.NodeId,v.StatusCode,v.SourceTimestamp,v.ServerTimestamp)).ToArray())).ToArray();
        return (new RobotDiagnosticsDto(robotId,displayName,endpointUrl,DateTime.UtcNow,snapshot.Connected&&discovery.Connected,snapshot.RoboticsNamespaceFound&&discovery.RoboticsNamespaceIndex is not null,snapshot.RoboticsNamespaceIndex,discovery.Warnings.Concat(snapshot.Warnings).Distinct(StringComparer.Ordinal).ToArray(),sections),null,HttpStatusCodes.Status200OK);
    }

    public async Task<MethodCallResultDto> CallTaskAsync(
        string methodName,
        MethodCallRequestDto? request,
        CancellationToken cancellationToken)
    {
        return await CallOperationMethodAsync(
            MethodInvocationTarget.TaskControlOperation,
            opcUaOptions.Value.EndpointUrl,
            methodName,
            request,
            GetInputAliases(methodName),
            cancellationToken);
    }

    public Task<MethodCallResultDto> CallTaskAsync(
        string endpointUrl,
        string methodName,
        MethodCallRequestDto? request,
        CancellationToken cancellationToken) =>
        CallOperationMethodAsync(MethodInvocationTarget.TaskControlOperation, endpointUrl, methodName, request,
            GetInputAliases(methodName), cancellationToken);

    public async Task<MethodCallResultDto> CallSystemAsync(
        string methodName,
        MethodCallRequestDto? request,
        CancellationToken cancellationToken)
    {
        return await CallOperationMethodAsync(
            MethodInvocationTarget.SystemOperation,
            opcUaOptions.Value.EndpointUrl,
            methodName,
            request,
            GetInputAliases(methodName),
            cancellationToken);
    }

    public Task<MethodCallResultDto> CallSystemAsync(
        string endpointUrl,
        string methodName,
        MethodCallRequestDto? request,
        CancellationToken cancellationToken) =>
        CallOperationMethodAsync(MethodInvocationTarget.SystemOperation, endpointUrl, methodName, request,
            GetInputAliases(methodName), cancellationToken);

    private async Task<MethodCallResultDto> CallOperationMethodAsync(
        MethodInvocationTarget target,
        string endpointUrl,
        string methodName,
        MethodCallRequestDto? request,
        IReadOnlyDictionary<string, string> inputAliases,
        CancellationToken cancellationToken)
    {
        try
        {
            ApplicationConfiguration configuration = await CreateConfigurationAsync();

            // Implementation decision for C9: keep C7/C8's short-lived session per HTTP request and reuse
            // core discovery plus the shared method invocation service.
            using var session = await new OpcUaSessionFactory(configuration).CreateSessionAsync(endpointUrl, cancellationToken);

            DiscoveryReport discoveryReport = new RoboticsDiscoveryService(session).Discover(endpointUrl);
            if (!discoveryReport.Connected)
            {
                return MethodCallFailure(
                    "OPC UA connection failed",
                    "Discovery did not report an active OPC UA connection.",
                    endpointUrl,
                    HttpStatusCodes.Status502BadGateway);
            }

            if (discoveryReport.RoboticsNamespaceIndex is null)
            {
                return MethodCallFailure(
                    "Robotics discovery failed",
                    string.Join("; ", discoveryReport.Warnings.DefaultIfEmpty("Robotics namespace is missing.")),
                    endpointUrl,
                    HttpStatusCodes.Status424FailedDependency);
            }

            var invocationRequest = new MethodInvocationRequest(
                target,
                methodName,
                PositionalInputValues: [],
                request.ToNamedInputs(),
                inputAliases);

            MethodInvocationResult invocationResult = new MethodInvocationService(session).Invoke(discoveryReport, invocationRequest);
            if (invocationResult.Outcome != MethodInvocationOutcome.Called)
            {
                return MethodCallFailure(
                    GetMethodCallError(invocationResult.Outcome),
                    GetMethodCallErrorDetails(invocationResult),
                    endpointUrl,
                    GetMethodCallHttpStatusCode(invocationResult.Outcome));
            }

            return new MethodCallResultDto(
                new MethodCallResponseDto(
                    endpointUrl,
                    invocationResult.Target,
                    invocationResult.ObjectId,
                    invocationResult.MethodId,
                    invocationResult.InputArguments.Select(ToDto).ToArray(),
                    invocationResult.CallStatusCode,
                    invocationResult.Success,
                    invocationResult.OutputArguments.Select(ToDto).ToArray(),
                    invocationResult.InputArgumentResults,
                    invocationResult.DiagnosticInfos,
                    invocationResult.Warnings),
                Error: null,
                HttpStatusCodes.Status200OK);
        }
        catch (Exception ex) when (IsExpectedOpcUaFailure(ex))
        {
            return MethodCallFailure(
                "OPC UA connection failed",
                ToSafeError(ex),
                endpointUrl,
                HttpStatusCodes.Status502BadGateway);
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
            pkiRootPath: Path.Combine(ResolvePackageRoot(), environment.IsProduction() ? "certificates" : "pki/client-gateway"),
            traceOutputPath: Path.Combine(ResolvePackageRoot(), environment.IsProduction() ? "logs" : "Logs", "RoboticsClientGateway.opcua.log"),
            developmentCertificateAccepted: subject =>
            {
                logger.LogWarning(
                    "Development certificate policy: auto-accepting untrusted server certificate. Certificate subject: {Subject}",
                    subject);
            },
            autoAcceptUntrustedCertificates: opcUaOptions.Value.AutoAcceptUntrustedCertificates,
            applicationCertificateDirectory: environment.IsProduction() ? "application" : "own");

        return await application.CreateConfigurationAsync();
    }

    private string ResolvePackageRoot() =>
        Path.GetFullPath(configuration["PackageRoot"] ?? Environment.GetEnvironmentVariable("OPCUA_ROBOTICS_WORKBENCH_ROOT") ?? AppContext.BaseDirectory);

    private static bool IsExpectedOpcUaFailure(Exception ex)
    {
        return ex is ServiceResultException or System.Net.Sockets.SocketException or InvalidOperationException;
    }

    private string ToSafeError(Exception ex, string endpointUrl = "unknown", string robotId = "unknown", string displayName = "unknown")
    {
        if (HasBadCertificateUntrusted(ex))
        {
            string certificateRoot = Path.GetFullPath(Path.Combine(ResolvePackageRoot(),
                environment.IsProduction() ? "certificates" : "pki/client-gateway"));
            string rejected = Path.Combine(certificateRoot, "rejected");
            string trusted = Path.Combine(certificateRoot, "trusted");
            return $"BadCertificateUntrusted for robotId '{robotId}' ({displayName}) at endpointUrl '{endpointUrl}'. " +
                   $"The public server certificate was written to the rejected certificate folder: '{rejected}'. " +
                   $"Review that certificate, then place only the public certificate file in the trusted certificate folder: '{trusted}'. " +
                   "Restart the Workbench or retry the connection afterward.";
        }

        return ex.Message;
    }

    private static bool HasBadCertificateUntrusted(Exception exception)
    {
        for (Exception? current = exception; current is not null; current = current.InnerException)
        {
            if (current is ServiceResultException serviceResultException &&
                serviceResultException.StatusCode == Opc.Ua.StatusCodes.BadCertificateUntrusted)
                return true;
            if (current.Message.Contains("BadCertificateUntrusted", StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    private static SnapshotResult Failure(string error, string details, string endpointUrl, int statusCode)
    {
        return new SnapshotResult(
            Snapshot: null,
            new ErrorDto(error, details, endpointUrl),
            statusCode);
    }

    private static MethodCallResultDto MethodCallFailure(string error, string details, string endpointUrl, int statusCode)
    {
        return new MethodCallResultDto(
            Response: null,
            new ErrorDto(error, details, endpointUrl),
            statusCode);
    }

    private static IReadOnlyDictionary<string, string> GetInputAliases(string methodName)
    {
        if (string.Equals(methodName, Opc.Ua.Robotics.BrowseNames.LoadByName, StringComparison.Ordinal))
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["programName"] = "string"
            };
        }

        if (string.Equals(methodName, Opc.Ua.Robotics.BrowseNames.Stop, StringComparison.Ordinal))
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["stopMode"] = "integer"
            };
        }

        return new Dictionary<string, string>();
    }

    private static string GetMethodCallError(MethodInvocationOutcome outcome)
    {
        return outcome switch
        {
            MethodInvocationOutcome.Missing => "Method target not found",
            MethodInvocationOutcome.Ambiguous => "Method target is ambiguous",
            MethodInvocationOutcome.UnsupportedMetadata => "Unsupported method metadata",
            MethodInvocationOutcome.InvalidInput => "Invalid method input",
            MethodInvocationOutcome.InvalidNodeId => "Invalid discovered method NodeId",
            _ => "Method invocation failed"
        };
    }

    private static string GetMethodCallErrorDetails(MethodInvocationResult result)
    {
        var details = new List<string>();
        if (!string.IsNullOrWhiteSpace(result.Error))
        {
            details.Add(result.Error);
        }
        else
        {
            details.Add("Method invocation was rejected before OPC UA Call.");
        }

        if (!string.IsNullOrWhiteSpace(result.Evidence))
        {
            details.Add($"Evidence: {result.Evidence}");
        }

        details.AddRange(result.Warnings);
        return string.Join(" ", details);
    }

    private static int GetMethodCallHttpStatusCode(MethodInvocationOutcome outcome)
    {
        return outcome switch
        {
            MethodInvocationOutcome.Missing => HttpStatusCodes.Status404NotFound,
            MethodInvocationOutcome.Ambiguous => HttpStatusCodes.Status409Conflict,
            MethodInvocationOutcome.UnsupportedMetadata => HttpStatusCodes.Status422UnprocessableEntity,
            MethodInvocationOutcome.InvalidInput => HttpStatusCodes.Status400BadRequest,
            MethodInvocationOutcome.InvalidNodeId => HttpStatusCodes.Status424FailedDependency,
            _ => HttpStatusCodes.Status502BadGateway
        };
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
            _ => StateSnapshotSections.Concat(EquipmentSnapshotSections).ToArray()
        };
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

    private static MethodCallArgumentDto ToDto(MethodInvocationArgumentValue argument)
    {
        return new MethodCallArgumentDto(
            argument.Name,
            argument.DataType,
            argument.ValueRank,
            argument.ValueText);
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

    private static PowerTrainDto ToDto(PowerTrainReport report) =>
        new(ToDto(report.Node), report.Motors.Select(motor => new MotorDto(ToDto(motor.Node))).ToArray());

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
