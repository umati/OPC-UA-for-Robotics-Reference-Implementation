using Opc.Ua.Client;
using Robotics.Client.Core.Client;
using Robotics.Client.Core.Reporting;
using Robotics.ReferenceClient.Reporting;

namespace Robotics.ReferenceClient.Client;

internal sealed class MethodCallService(Session session)
{
    private readonly MethodInvocationService _methodInvocationService = new(session);

    public int Invoke(DiscoveryReport report, MethodCallCommand command, SnapshotReadService? snapshotReadService = null)
    {
        Console.WriteLine($"Mode: call");
        Console.WriteLine($"Target: {command.QualifiedTarget}");

        PrintSnapshotIfEnabled(report, command, "before", command.SnapshotOptions.Before, snapshotReadService);
        MethodCallResult result = InvokeStep(report, command);
        PrintSnapshotIfEnabled(report, command, "after", command.SnapshotOptions.After, snapshotReadService);
        return result.Succeeded ? 0 : 1;
    }

    public MethodCallResult InvokeStep(DiscoveryReport report, MethodCallCommand command)
    {
        MethodInvocationResult result = _methodInvocationService.Invoke(report, ToInvocationRequest(command));

        if (result.ObjectId is null || result.MethodId is null)
        {
            Console.WriteLine($"Error: {result.Error}");
            if (result.Evidence is not null)
            {
                Console.WriteLine($"Evidence: {result.Evidence}");
            }

            foreach (string warning in result.Warnings)
            {
                Console.WriteLine($"- {warning}");
            }

            return MethodCallResult.Failed(command.QualifiedTarget, result.Error ?? "Method invocation failed.");
        }

        Console.WriteLine($"ObjectId: {result.ObjectId}");
        Console.WriteLine($"MethodId: {result.MethodId}");
        Console.WriteLine("InputArguments:");
        PrintArguments(result.InputArguments);
        Console.WriteLine($"Call StatusCode: {result.CallStatusCode}");

        if (result.InputArgumentResults.Count > 0)
        {
            Console.WriteLine("InputArgumentResults:");
            for (int index = 0; index < result.InputArgumentResults.Count; index++)
            {
                Console.WriteLine($"- {index}: {result.InputArgumentResults[index]}");
            }
        }

        if (result.DiagnosticInfos.Count > 0)
        {
            Console.WriteLine("DiagnosticInfos:");
            for (int index = 0; index < result.DiagnosticInfos.Count; index++)
            {
                Console.WriteLine($"- {index}: {result.DiagnosticInfos[index]}");
            }
        }

        Console.WriteLine("OutputArguments:");
        PrintArguments(result.OutputArguments);

        if (!result.Success)
        {
            Console.WriteLine("Result: failed");
            return MethodCallResult.Failed(command.QualifiedTarget, result.Error ?? result.CallStatusCode ?? "Method invocation failed.");
        }

        return MethodCallResult.Success(command.QualifiedTarget, result.CallStatusCode ?? "Good");
    }

    public bool TryGetInputArgumentMetadata(
        DiscoveryReport report,
        MethodCallCommand command,
        out IReadOnlyList<MethodArgumentReport> arguments,
        out string? status,
        out string? error)
    {
        arguments = [];
        status = null;
        error = null;

        return _methodInvocationService.TryGetInputArgumentMetadata(
            report,
            ToInvocationRequest(command),
            out arguments,
            out status,
            out error);
    }

    private static void PrintSnapshotIfEnabled(
        DiscoveryReport report,
        MethodCallCommand command,
        string timing,
        bool enabled,
        SnapshotReadService? snapshotReadService)
    {
        if (!enabled || snapshotReadService is null)
        {
            return;
        }

        // Official specification truth: this is a read-only OPC UA attribute read; method-call success remains
        // defined only by the OPC UA Call StatusCode printed by InvokeStep.
        // Local NodeSet/generated-code truth: snapshot discovery starts from the C1/C2 discovered nodes and
        // generated Robotics BrowseName constants.
        // Implementation decision: snapshot read failures are printed per value and do not override call status.
        SnapshotReport snapshot = snapshotReadService.Read(report);
        new ConsoleSnapshotPrinter().Print(timing, command.QualifiedTarget, snapshot);
    }

    private static MethodInvocationRequest ToInvocationRequest(MethodCallCommand command)
    {
        MethodInvocationTarget target = command.Target == MethodCallTarget.TaskControlOperation
            ? MethodInvocationTarget.TaskControlOperation
            : MethodInvocationTarget.SystemOperation;
        return MethodInvocationRequest.Positional(target, command.MethodName, command.InputValues);
    }

    private static void PrintArguments(IReadOnlyList<MethodInvocationArgumentValue> arguments)
    {
        if (arguments.Count == 0)
        {
            Console.WriteLine("- none");
            return;
        }

        foreach (MethodInvocationArgumentValue argument in arguments)
        {
            Console.WriteLine($"- {argument.Name} = {argument.ValueText} | DataType={argument.DataType}");
        }
    }
}
