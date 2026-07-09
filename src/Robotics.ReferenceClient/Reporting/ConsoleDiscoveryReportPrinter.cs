namespace Robotics.ReferenceClient.Reporting;

internal sealed class ConsoleDiscoveryReportPrinter
{
    public void Print(DiscoveryReport report)
    {
        Console.WriteLine($"Connected: {(report.Connected ? "yes" : "no")}");
        Console.WriteLine($"Robotics namespace index: {report.RoboticsNamespaceIndex?.ToString() ?? "missing"}");
        Console.WriteLine();

        foreach (string warning in report.Warnings)
        {
            Console.WriteLine($"Warning: {warning}");
        }

        Console.WriteLine("MotionDeviceSystems:");
        if (report.MotionDeviceSystems.Count == 0)
        {
            Console.WriteLine("- missing");
        }

        foreach (MotionDeviceSystemReport system in report.MotionDeviceSystems)
        {
            PrintNode("- ", system.Node);
            Console.WriteLine();

            Console.WriteLine("  Controllers:");
            if (system.ControllersFolder is null)
            {
                Console.WriteLine("  - missing");
            }
            else if (system.Controllers.Count == 0)
            {
                Console.WriteLine("  - unresolved: Controllers folder found, but no ControllerType children proved");
            }

            foreach (ControllerReport controller in system.Controllers)
            {
                PrintNode("  - ", controller.Node);
                PrintTaskControls(controller);
                PrintSystemOperation(controller.SystemOperation);
            }

            Console.WriteLine();
            Console.WriteLine("  MotionDevices:");
            if (system.MotionDevicesFolder is null)
            {
                Console.WriteLine("  - missing");
            }
            else if (system.MotionDevices.Count == 0)
            {
                Console.WriteLine("  - unresolved: MotionDevices folder found, but no MotionDeviceType children proved");
            }

            foreach (MotionDeviceReport motionDevice in system.MotionDevices)
            {
                PrintNode("  - ", motionDevice.Node);

                Console.WriteLine("    Axes:");
                if (motionDevice.AxesFolder is null)
                {
                    Console.WriteLine("    - missing");
                }
                else if (motionDevice.Axes.Count == 0)
                {
                    Console.WriteLine("    - unresolved: Axes folder found, but no AxisType children proved");
                }

                foreach (NodeDiscoveryInfo axis in motionDevice.Axes)
                {
                    PrintNode("    - ", axis);
                }

                Console.WriteLine("    PowerTrains:");
                if (motionDevice.PowerTrainsFolder is null)
                {
                    Console.WriteLine("    - missing");
                }
                else if (motionDevice.PowerTrains.Count == 0)
                {
                    Console.WriteLine("    - unresolved: PowerTrains folder found, but no PowerTrainType children proved");
                }

                foreach (NodeDiscoveryInfo powerTrain in motionDevice.PowerTrains)
                {
                    PrintNode("    - ", powerTrain);
                }
            }
        }
    }

    private static void PrintTaskControls(ControllerReport controller)
    {
        Console.WriteLine("    TaskControls:");
        if (controller.TaskControlsFolder is null)
        {
            Console.WriteLine("    - missing");
            return;
        }

        if (controller.TaskControls.Count == 0)
        {
            Console.WriteLine("    - unresolved: TaskControls folder found, but no TaskControlType children proved");
            return;
        }

        foreach (TaskControlReport taskControl in controller.TaskControls)
        {
            PrintNode("    - ", taskControl.Node, includeTypeDefinition: false);
            string operationStatus = taskControl.TaskControlOperation is null
                ? "not found"
                : $"found | NodeId={taskControl.TaskControlOperation.NodeId}";
            Console.WriteLine($"      TaskControlOperation: {operationStatus}");
            Console.WriteLine("      Methods:");
            PrintMethods("      ", taskControl.Methods);
        }
    }

    private static void PrintSystemOperation(OperationReport? operation)
    {
        Console.WriteLine("    SystemOperation:");
        if (operation is null)
        {
            Console.WriteLine("    - not found");
            return;
        }

        Console.WriteLine($"    - found | NodeId={operation.Node.NodeId} | TypeDefinition={operation.Node.TypeDefinition}");
        if (!operation.IsExpectedType)
        {
            Console.WriteLine("      unresolved: SystemOperation node was found but its SystemOperationType compatibility was not proved");
        }

        Console.WriteLine("      Methods:");
        PrintMethods("      ", operation.Methods);
    }

    private static void PrintMethods(string indent, IReadOnlyList<MethodReport> methods)
    {
        if (methods.Count == 0)
        {
            Console.WriteLine($"{indent}- none proved");
            return;
        }

        foreach (MethodReport method in methods)
        {
            string status = method.Found ? "found" : "not found";
            string nodeId = method.NodeId is null ? string.Empty : $" | NodeId={method.NodeId}";
            Console.WriteLine($"{indent}- {method.Name}: {status}{nodeId}");
            if (!method.Found)
            {
                Console.WriteLine($"{indent}  Evidence: {method.Evidence}");
                continue;
            }

            if (!string.IsNullOrWhiteSpace(method.BrowseName))
            {
                Console.WriteLine($"{indent}  BrowseName={method.BrowseName} | DisplayName={method.DisplayName ?? string.Empty}");
            }

            if (!string.IsNullOrWhiteSpace(method.ParentNodeId))
            {
                Console.WriteLine($"{indent}  ParentNodeId={method.ParentNodeId}");
            }

            PrintArguments($"{indent}  ", "InputArguments", method.InputArguments);
            PrintArguments($"{indent}  ", "OutputArguments", method.OutputArguments);
        }
    }

    private static void PrintArguments(string indent, string label, MethodArgumentsReport report)
    {
        string propertyNodeId = report.PropertyNodeId is null ? string.Empty : $" | NodeId={report.PropertyNodeId}";
        string diagnostic = report.Diagnostic is null ? string.Empty : $" | {report.Diagnostic}";
        Console.WriteLine($"{indent}{label}: {report.Status}{propertyNodeId}{diagnostic}");

        foreach (MethodArgumentReport argument in report.Arguments)
        {
            Console.WriteLine(
                $"{indent}- Name={argument.Name} | DataType={argument.DataType} | ValueRank={argument.ValueRank} | ArrayDimensions={argument.ArrayDimensions} | Description={argument.Description}");
        }
    }

    private static void PrintNode(string prefix, NodeDiscoveryInfo node, bool includeTypeDefinition = true)
    {
        if (includeTypeDefinition)
        {
            Console.WriteLine($"{prefix}{node.BrowseName} | NodeId={node.NodeId} | TypeDefinition={node.TypeDefinition}");
            return;
        }

        Console.WriteLine($"{prefix}{node.BrowseName} | NodeId={node.NodeId}");
    }
}
