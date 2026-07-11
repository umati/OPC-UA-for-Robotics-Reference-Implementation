using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Robotics.ClientGateway.Dtos;
using Robotics.ClientGateway.Options;

namespace Robotics.ClientGateway.Services;

public sealed class RobotConnectionRegistry(
    IOptions<OpcUaOptions> opcUaOptions,
    IConfiguration configuration)
{
    private readonly IReadOnlyList<RobotConnectionOptions> robots = BuildRobots(opcUaOptions.Value, configuration);

    public IReadOnlyList<RobotConnectionDto> GetEnabledRobots() => robots
        .Where(robot => robot.Enabled)
        .Select(ToDto)
        .ToArray();

    public RobotConnectionOptions? FindEnabled(string id) => robots.FirstOrDefault(robot =>
        robot.Enabled && string.Equals(robot.Id, id, StringComparison.OrdinalIgnoreCase));

    private static IReadOnlyList<RobotConnectionOptions> BuildRobots(OpcUaOptions opcUa, IConfiguration configuration)
    {
        var configured = configuration.GetSection("Robots").Get<RobotConnectionOptions[]>() ?? [];
        if (configured.Length > 0)
        {
            return configured.Where(IsValid).ToArray();
        }

        return [new RobotConnectionOptions
        {
            Id = "default-robot",
            DisplayName = "Default Robot Server",
            EndpointUrl = opcUa.EndpointUrl,
            Enabled = true
        }];
    }

    private static bool IsValid(RobotConnectionOptions robot) =>
        Regex.IsMatch(robot.Id ?? string.Empty, "^[A-Za-z0-9][A-Za-z0-9._~-]*$") &&
        !string.IsNullOrWhiteSpace(robot.DisplayName) &&
        Uri.TryCreate(robot.EndpointUrl, UriKind.Absolute, out _);

    private static RobotConnectionDto ToDto(RobotConnectionOptions robot) =>
        new(robot.Id, robot.DisplayName, robot.EndpointUrl, robot.Enabled);
}
