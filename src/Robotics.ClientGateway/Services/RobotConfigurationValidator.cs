using System.Text.RegularExpressions;
using Robotics.ClientGateway.Options;

namespace Robotics.ClientGateway.Services;

public static class RobotConfigurationValidator
{
    private static readonly Regex IdPattern = new("^[A-Za-z0-9][A-Za-z0-9._~-]*$", RegexOptions.Compiled);

    public static IReadOnlyList<string> Validate(IConfiguration configuration, string filePath, bool requireExternalFile)
    {
        if (requireExternalFile && !File.Exists(filePath))
            return [$"The required configuration file was not found: {filePath}"];

        var robots = configuration.GetSection("Robots").Get<RobotConnectionOptions[]>() ?? [];
        if (robots.Length == 0) return ["No robots are configured. Add at least one enabled robot to robots.json."];

        var errors = new List<string>();
        var ids = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var enabledEndpoints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < robots.Length; i++)
        {
            var robot = robots[i];
            var name = string.IsNullOrWhiteSpace(robot.Id) ? $"entry {i + 1}" : $"Robot \"{robot.Id}\"";
            if (string.IsNullOrWhiteSpace(robot.Id)) errors.Add($"{name}: id is required.");
            else if (!IdPattern.IsMatch(robot.Id)) errors.Add($"{name}: id must start with a letter or digit and contain only letters, digits, '.', '_', '~', or '-'.");
            else if (!ids.Add(robot.Id)) errors.Add($"{name}: id is duplicated.");
            if (string.IsNullOrWhiteSpace(robot.DisplayName)) errors.Add($"{name}: displayName is required.");

            if (!Uri.TryCreate(robot.EndpointUrl, UriKind.Absolute, out var endpoint) ||
                !string.Equals(endpoint.Scheme, "opc.tcp", StringComparison.OrdinalIgnoreCase) || endpoint.Port is < 1 or > 65535 || string.IsNullOrWhiteSpace(endpoint.Host))
                errors.Add($"{name}: endpointUrl is invalid: {robot.EndpointUrl}. Expected opc.tcp://host:port/path.");
            else if (robot.Enabled && !enabledEndpoints.Add(endpoint.AbsoluteUri))
                errors.Add($"{name}: endpointUrl duplicates another enabled robot: {robot.EndpointUrl}.");
        }

        if (!robots.Any(robot => robot.Enabled)) errors.Add("At least one robot must have enabled set to true.");
        return errors;
    }
}
