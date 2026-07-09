using Opc.Ua;
using Robotics.ReferenceClient.Client;
using Robotics.ReferenceClient.Discovery;
using Robotics.ReferenceClient.Reporting;

namespace Robotics.ReferenceClient;

internal static class Program
{
    private const string DefaultEndpointUrl = "opc.tcp://localhost:4840/RoboticsReferenceServer";

    public static async Task<int> Main(string[] args)
    {
        string endpointUrl = args.FirstOrDefault(argument => argument.StartsWith("opc.tcp://", StringComparison.OrdinalIgnoreCase))
            ?? DefaultEndpointUrl;

        Console.WriteLine("OPC UA Robotics Reference Client");
        Console.WriteLine($"Endpoint: {endpointUrl}");

        try
        {
            var clientApplication = new OpcUaClientApplication();
            ApplicationConfiguration configuration = await clientApplication.CreateConfigurationAsync();

            using var session = await new OpcUaSessionFactory(configuration).CreateSessionAsync(endpointUrl);

            var discovery = new RoboticsDiscoveryService(session);
            DiscoveryReport report = discovery.Discover(endpointUrl);

            new ConsoleDiscoveryReportPrinter().Print(report);

            if (!report.Connected || report.RoboticsNamespaceIndex is null)
            {
                return 1;
            }

            return 0;
        }
        catch (ServiceResultException ex) when (ex.StatusCode == StatusCodes.BadTcpEndpointUrlInvalid ||
                                                ex.StatusCode == StatusCodes.BadConnectionClosed ||
                                                ex.StatusCode == StatusCodes.BadTimeout)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Connected: no");
            Console.WriteLine("The OPC UA reference server is not reachable. Start Robotics.ReferenceServer and retry.");
            Console.WriteLine($"UA status: {ex.StatusCode}");
            Console.ResetColor();
            return 1;
        }
        catch (Exception ex) when (ex is ServiceResultException or Opc.Ua.ServiceResultException or System.Net.Sockets.SocketException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Connected: no");
            Console.WriteLine("The OPC UA reference server is not reachable or rejected the session.");
            Console.WriteLine(ex.Message);
            Console.ResetColor();
            return 1;
        }
    }
}
