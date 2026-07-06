using System.Text.Json;

namespace Robotics.ReferenceServer.ControlBridge;

internal sealed class RobotControlBridgeHttpHandler
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private static readonly HashSet<string> AllowedDevOrigins = new(StringComparer.OrdinalIgnoreCase)
    {
        "http://localhost:5173",
        "http://127.0.0.1:5173"
    };

    private readonly RobotControlBridgeServiceRegistry _serviceRegistry;

    // Local demo/operator bridge for the browser visualization.
    // It is intentionally not a replacement for a standards-pure OPC UA client;
    // a real OPC UA client remains a later interoperability milestone.
    public RobotControlBridgeHttpHandler(RobotControlBridgeServiceRegistry serviceRegistry)
    {
        _serviceRegistry = serviceRegistry;
    }

    public static bool IsAllowedDevOrigin(string? origin)
    {
        return origin is not null && AllowedDevOrigins.Contains(origin);
    }

    public RobotControlHttpResponse Handle(RobotControlHttpRequest request)
    {
        if (!request.Path.StartsWith("/control", StringComparison.OrdinalIgnoreCase))
        {
            return CreateJsonResponse(404, RobotControlCommandResult.Reject(
                "Endpoint not found.",
                RobotControlCommandFailureKind.NotFound));
        }

        if (string.Equals(request.Method, "OPTIONS", StringComparison.OrdinalIgnoreCase))
        {
            return new RobotControlHttpResponse(204, string.Empty);
        }

        if (!string.Equals(request.Method, "POST", StringComparison.OrdinalIgnoreCase))
        {
            return CreateJsonResponse(405, RobotControlCommandResult.Reject(
                "Control bridge endpoints require POST.",
                RobotControlCommandFailureKind.InvalidArgument));
        }

        RobotControlCommandService? service = _serviceRegistry.CommandService;
        if (service is null)
        {
            return CreateJsonResponse(503, RobotControlCommandResult.Reject(
                "Control bridge is not connected to the robot command service yet.",
                RobotControlCommandFailureKind.InvalidState));
        }

        RobotControlCommandResult result = request.Path.ToLowerInvariant() switch
        {
            "/control/demo/start" => service.StartDemoMotion(),
            "/control/motion/stop" => service.StopMotion(),
            "/control/programs/load-sample" => service.LoadSampleProgram(ReadStringProperty(request.Body, "sampleName")),
            "/control/programs/load-json" => service.LoadProgramFromJson(ReadStringProperty(request.Body, "programJson")),
            "/control/programs/start" => service.StartProgram(),
            "/control/programs/pause" => service.PauseProgram(),
            "/control/programs/resume" => service.ResumeProgram(),
            "/control/programs/stop" => service.StopProgram(),
            _ => RobotControlCommandResult.Reject("Control endpoint not found.", RobotControlCommandFailureKind.NotFound)
        };

        return CreateJsonResponse(GetStatusCode(result), result);
    }

    private static string? ReadStringProperty(string requestBody, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(requestBody))
        {
            return null;
        }

        try
        {
            using JsonDocument document = JsonDocument.Parse(requestBody);
            return document.RootElement.ValueKind == JsonValueKind.Object
                && document.RootElement.TryGetProperty(propertyName, out JsonElement value)
                && value.ValueKind == JsonValueKind.String
                ? value.GetString()
                : null;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static RobotControlHttpResponse CreateJsonResponse(int statusCode, RobotControlCommandResult result)
    {
        string body = JsonSerializer.Serialize(
            new
            {
                accepted = result.Accepted,
                message = result.Message
            },
            JsonOptions);

        return new RobotControlHttpResponse(statusCode, body);
    }

    private static int GetStatusCode(RobotControlCommandResult result)
    {
        if (result.Accepted)
        {
            return 200;
        }

        return result.FailureKind switch
        {
            RobotControlCommandFailureKind.InvalidArgument => 400,
            RobotControlCommandFailureKind.InvalidState => 409,
            RobotControlCommandFailureKind.NotFound => 404,
            RobotControlCommandFailureKind.Unexpected => 500,
            _ => 400
        };
    }
}

internal sealed record RobotControlHttpRequest(
    string Method,
    string Path,
    IReadOnlyDictionary<string, string> Headers,
    string Body);

internal sealed record RobotControlHttpResponse(int StatusCode, string Body);
