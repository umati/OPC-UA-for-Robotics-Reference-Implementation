using Opc.Ua;
using Opc.Ua.Configuration;

namespace Robotics.ReferenceServer;

internal static class Program
{
    private const string EndpointUrl = "opc.tcp://localhost:4840/RoboticsReferenceServer";

    public static async Task Main()
    {
        try
        {
            using var completed = new ManualResetEventSlim();

            Console.CancelKeyPress += (_, eventArgs) =>
            {
                eventArgs.Cancel = true;
                completed.Set();
            };

            var application = new ApplicationInstance
            {
                ApplicationName = RoboticsServerConfiguration.ApplicationName,
                ApplicationType = ApplicationType.Server,
                ApplicationConfiguration = RoboticsServerConfiguration.Create(EndpointUrl)
            };

            await application.CheckApplicationInstanceCertificates(
                silent: false,
                lifeTimeInMonths: 120);

            await application.StartAsync(new RoboticsServer());

            Console.WriteLine("Robotics reference server started.");
            Console.WriteLine($"Endpoint: {EndpointUrl}");
            Console.WriteLine("Press Ctrl+C to stop.");

            completed.Wait();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Server startup failed.");
            Console.WriteLine(ex);
            Console.ResetColor();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
}