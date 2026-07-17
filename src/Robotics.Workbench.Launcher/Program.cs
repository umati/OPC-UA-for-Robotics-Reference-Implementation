using System.Diagnostics;

string packageRoot = AppContext.BaseDirectory;
string gatewayPath = Path.Combine(packageRoot, "app", "Robotics.ClientGateway.exe");

if (!File.Exists(gatewayPath))
{
    Console.Error.WriteLine("OPC UA Robotics Workbench could not start.");
    Console.Error.WriteLine($"The gateway executable is missing: {gatewayPath}");
    Console.Error.WriteLine("Keep the package contents together and restore the app folder.");
    return 1;
}

var startInfo = new ProcessStartInfo
{
    FileName = gatewayPath,
    WorkingDirectory = packageRoot,
    UseShellExecute = false
};
foreach (string argument in args)
    startInfo.ArgumentList.Add(argument);
startInfo.Environment["OPCUA_ROBOTICS_WORKBENCH_ROOT"] = packageRoot;

using var gateway = Process.Start(startInfo);
if (gateway is null)
{
    Console.Error.WriteLine("OPC UA Robotics Workbench could not start the gateway process.");
    return 1;
}

gateway.WaitForExit();
return gateway.ExitCode;
