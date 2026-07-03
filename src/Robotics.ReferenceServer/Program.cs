using Opc.Ua;
using Opc.Ua.Configuration;
using Robotics.ReferenceServer.Simulation;
using Robotics.Shared;

namespace Robotics.ReferenceServer;

internal static class Program
{
    private const string EndpointUrl = "opc.tcp://localhost:4840/RoboticsReferenceServer";

    public static async Task Main(string[] args)
    {
        if (args.Contains("--simulate-once", StringComparer.OrdinalIgnoreCase))
        {
            RunSimulationSmokeTest();
            return;
        }

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

    private static void RunSimulationSmokeTest()
    {
        var simulation = new RobotSimulationService();
        simulation.SetJointTargets(
        [
            new RobotJointTarget { AxisName = RobotAxisName.S, TargetPositionDegrees = 45 },
            new RobotJointTarget { AxisName = RobotAxisName.L, TargetPositionDegrees = -30 },
            new RobotJointTarget { AxisName = RobotAxisName.U, TargetPositionDegrees = 60 },
            new RobotJointTarget { AxisName = RobotAxisName.R, TargetPositionDegrees = 20 },
            new RobotJointTarget { AxisName = RobotAxisName.B, TargetPositionDegrees = 15 },
            new RobotJointTarget { AxisName = RobotAxisName.T, TargetPositionDegrees = 90 }
        ]);

        TimeSpan updateStep = TimeSpan.FromMilliseconds(20);
        TimeSpan printInterval = TimeSpan.FromMilliseconds(500);
        TimeSpan totalDuration = TimeSpan.FromSeconds(5);
        TimeSpan simulatedElapsed = TimeSpan.Zero;
        TimeSpan nextPrint = printInterval;

        Console.WriteLine("Running one-shot robot simulation smoke test.");

        while (simulatedElapsed < totalDuration)
        {
            simulation.Update(updateStep);
            simulatedElapsed += updateStep;

            if (simulatedElapsed >= nextPrint)
            {
                PrintTelemetry(simulatedElapsed, simulation.GetSnapshot());
                nextPrint += printInterval;
            }
        }

        Console.WriteLine("Final snapshot:");
        PrintTelemetry(simulatedElapsed, simulation.GetSnapshot());
    }

    private static void PrintTelemetry(TimeSpan simulatedElapsed, RobotTelemetrySnapshot snapshot)
    {
        Console.WriteLine(
            $"t={simulatedElapsed:hh\\:mm\\:ss\\.fff} timestamp={snapshot.TimestampUtc:O} IsMoving={snapshot.IsMoving} Speed={snapshot.Speed:F2}deg/s Acceleration={snapshot.Acceleration:F2}deg/s^2");

        foreach (RobotAxisState axis in snapshot.AxisStates.OrderBy(axis => axis.AxisName))
        {
            Console.WriteLine(
                $"  {axis.AxisName}: position={axis.PositionDegrees:F2}deg velocity={axis.VelocityDegreesPerSecond:F2}deg/s target={axis.TargetPositionDegrees:F2}deg");
        }
    }
}
