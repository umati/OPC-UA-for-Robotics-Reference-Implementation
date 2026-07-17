using System.Text.Json;
using System.Text.Json.Serialization;

namespace Robotics.ClientGateway.Dtos;

public sealed class MethodCallRequestDto
{
    public string? ProgramName { get; init; }

    public int? StopMode { get; init; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalProperties { get; init; }
}

public sealed record MethodCallResponseDto(
    string EndpointUrl,
    string Target,
    string? ObjectId,
    string? MethodId,
    IReadOnlyList<MethodCallArgumentDto> InputArguments,
    string? CallStatusCode,
    bool Success,
    IReadOnlyList<MethodCallArgumentDto> OutputArguments,
    IReadOnlyList<string> InputArgumentResults,
    IReadOnlyList<string> DiagnosticInfos,
    IReadOnlyList<string> Warnings);

public sealed record MethodCallArgumentDto(
    string Name,
    string DataType,
    int ValueRank,
    string ValueText);

public sealed record MethodCallResultDto(
    MethodCallResponseDto? Response,
    ErrorDto? Error,
    int StatusCode);

public static class MethodCallRequestDtoExtensions
{
    public static IReadOnlyDictionary<string, string> ToNamedInputs(this MethodCallRequestDto? request)
    {
        if (request is null)
        {
            return new Dictionary<string, string>();
        }

        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (request.ProgramName is not null)
        {
            values["programName"] = request.ProgramName;
        }

        if (request.StopMode is not null)
        {
            values["stopMode"] = request.StopMode.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        if (request.AdditionalProperties is not null)
        {
            foreach ((string key, JsonElement value) in request.AdditionalProperties)
            {
                values[key] = ToInvariantText(value);
            }
        }

        return values;
    }

    private static string ToInvariantText(JsonElement value)
    {
        return value.ValueKind switch
        {
            JsonValueKind.String => value.GetString() ?? string.Empty,
            JsonValueKind.Number => value.GetRawText(),
            JsonValueKind.True => "true",
            JsonValueKind.False => "false",
            JsonValueKind.Null => "null",
            _ => value.GetRawText()
        };
    }
}
