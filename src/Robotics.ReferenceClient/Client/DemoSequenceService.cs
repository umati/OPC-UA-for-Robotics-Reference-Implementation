using Robotics.Client.Core.Client;
using Robotics.Client.Core.Reporting;
using Robotics.ReferenceClient.Reporting;

namespace Robotics.ReferenceClient.Client;

internal sealed class DemoSequenceService(MethodCallService methodCallService, SnapshotReadService? snapshotReadService = null)
{
    public async Task<int> ExecuteAsync(DiscoveryReport report, DemoCommand command)
    {
        IReadOnlyList<DemoStep> steps = CreateSteps(command);
        var results = new List<DemoStepResult>();
        DemoStepResult? firstFailedStep = null;

        Console.WriteLine("Mode: demo");
        Console.WriteLine($"Demo: {command.DemoName}");
        if (UsesProgram(command.DemoName))
        {
            Console.WriteLine($"Program: {command.ProgramName}");
            if (command.ProgramWasDefaulted)
            {
                Console.WriteLine($"Program default: {DemoCommand.DefaultProgramName} (implementation default)");
            }
        }

        Console.WriteLine($"Delay: {command.DelayMilliseconds} ms");
        Console.WriteLine($"Continue on failure: {command.ContinueOnFailure.ToString().ToLowerInvariant()}");
        Console.WriteLine();
        Console.WriteLine("Sequencing note:");
        Console.WriteLine("- Official specification truth: runtime Method metadata and OPC UA Call StatusCode are the method-call facts used here.");
        Console.WriteLine("- Local NodeSet/generated-code truth: the current server exposes the discovered TaskControlOperation and SystemOperation methods.");
        Console.WriteLine("- Implementation decision: this client composes those discovered methods into a local demo workflow for manual demonstration.");

        for (int index = 0; index < steps.Count; index++)
        {
            DemoStep step = steps[index];
            Console.WriteLine();
            Console.WriteLine($"Step {index + 1}/{steps.Count}: {step.Name}");
            var stepCommand = new MethodCallCommand(step.Target, step.MethodName, [], command.SnapshotOptions);
            PrintSnapshotIfEnabled(report, stepCommand, "before", command.SnapshotOptions.Before);

            IReadOnlyList<string>? inputs = ResolveInputs(report, step, command, out string? inputDiagnostic, out bool refusedBeforeCall);
            if (!string.IsNullOrWhiteSpace(inputDiagnostic))
            {
                Console.WriteLine(inputDiagnostic);
            }

            DemoStepResult stepResult;
            if (refusedBeforeCall || inputs is null)
            {
                string statusCode = "NotCalled";
                Console.WriteLine($"Call StatusCode: {statusCode}");
                Console.WriteLine("Result: failed");
                stepResult = new DemoStepResult(step.Name, false, statusCode);
            }
            else
            {
                var callCommand = new MethodCallCommand(step.Target, step.MethodName, inputs, command.SnapshotOptions);
                MethodCallResult callResult = methodCallService.InvokeStep(report, callCommand);
                string statusCode = callResult.StatusCode ?? callResult.Error ?? "failed before call";
                stepResult = new DemoStepResult(step.Name, callResult.Succeeded, statusCode);
            }

            PrintSnapshotIfEnabled(report, stepCommand, "after", command.SnapshotOptions.After);

            results.Add(stepResult);
            if (!stepResult.Succeeded)
            {
                firstFailedStep ??= stepResult;
                if (!command.ContinueOnFailure)
                {
                    break;
                }
            }

            if (index + 1 < steps.Count && command.DelayMilliseconds > 0)
            {
                await Task.Delay(command.DelayMilliseconds);
            }
        }

        PrintSummary(results, firstFailedStep);
        return firstFailedStep is null ? 0 : 1;
    }

    private void PrintSnapshotIfEnabled(
        DiscoveryReport report,
        MethodCallCommand command,
        string timing,
        bool enabled)
    {
        if (!enabled || snapshotReadService is null)
        {
            return;
        }

        SnapshotReport snapshot = snapshotReadService.Read(report);
        new ConsoleSnapshotPrinter().Print(timing, command.QualifiedTarget, snapshot);
    }

    private IReadOnlyList<string>? ResolveInputs(
        DiscoveryReport report,
        DemoStep step,
        DemoCommand command,
        out string? diagnostic,
        out bool refusedBeforeCall)
    {
        diagnostic = null;
        refusedBeforeCall = false;

        if (step.IsLoadByName)
        {
            return [command.ProgramName];
        }

        if (!step.IsStop)
        {
            return [];
        }

        var stopCommand = new MethodCallCommand(step.Target, step.MethodName, [], SnapshotOptions.None);
        if (!methodCallService.TryGetInputArgumentMetadata(report, stopCommand, out IReadOnlyList<MethodArgumentReport> arguments, out string? status, out string? error))
        {
            diagnostic = $"Error: {error}";
            refusedBeforeCall = true;
            return null;
        }

        int inputCount = status is "missing" or "none" ? 0 : arguments.Count;
        if (inputCount == 0)
        {
            if (command.StopModeWasSupplied)
            {
                diagnostic = $"Stop mode ignored because runtime metadata requires zero inputs.";
            }

            return [];
        }

        if (status != "listed")
        {
            diagnostic = $"Error: Stop InputArguments are {status}; refusing before call.";
            refusedBeforeCall = true;
            return null;
        }

        if (inputCount > 1)
        {
            diagnostic = $"Error: Stop runtime metadata requires {inputCount} inputs; demo Stop supports only zero or one input and refuses before call.";
            refusedBeforeCall = true;
            return null;
        }

        return [command.StopMode.ToString(System.Globalization.CultureInfo.InvariantCulture)];
    }

    private static IReadOnlyList<DemoStep> CreateSteps(DemoCommand command)
    {
        return command.DemoName.ToLowerInvariant() switch
        {
            "system-ready-cycle" =>
            [
                new("SystemOperation.GetReady", MethodCallTarget.SystemOperation, "GetReady"),
                new("SystemOperation.Start", MethodCallTarget.SystemOperation, "Start"),
                new("SystemOperation.Stop", MethodCallTarget.SystemOperation, "Stop"),
                new("SystemOperation.StandDown", MethodCallTarget.SystemOperation, "StandDown")
            ],
            "task-program-cycle" =>
            [
                new("TaskControlOperation.LoadByName", MethodCallTarget.TaskControlOperation, "LoadByName", IsLoadByName: true),
                new("TaskControlOperation.Start", MethodCallTarget.TaskControlOperation, "Start"),
                new("TaskControlOperation.Stop", MethodCallTarget.TaskControlOperation, "Stop"),
                new("TaskControlOperation.ResetToProgramStart", MethodCallTarget.TaskControlOperation, "ResetToProgramStart"),
                new("TaskControlOperation.UnloadProgram", MethodCallTarget.TaskControlOperation, "UnloadProgram")
            ],
            "full-reference-cycle" =>
            [
                new("SystemOperation.GetReady", MethodCallTarget.SystemOperation, "GetReady"),
                new("SystemOperation.Start", MethodCallTarget.SystemOperation, "Start"),
                new("TaskControlOperation.LoadByName", MethodCallTarget.TaskControlOperation, "LoadByName", IsLoadByName: true),
                new("TaskControlOperation.Start", MethodCallTarget.TaskControlOperation, "Start"),
                new("TaskControlOperation.Stop", MethodCallTarget.TaskControlOperation, "Stop"),
                new("TaskControlOperation.ResetToProgramStart", MethodCallTarget.TaskControlOperation, "ResetToProgramStart"),
                new("TaskControlOperation.UnloadProgram", MethodCallTarget.TaskControlOperation, "UnloadProgram"),
                new("SystemOperation.Stop", MethodCallTarget.SystemOperation, "Stop"),
                new("SystemOperation.StandDown", MethodCallTarget.SystemOperation, "StandDown")
            ],
            _ => []
        };
    }

    private static bool UsesProgram(string demoName)
    {
        return string.Equals(demoName, "task-program-cycle", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(demoName, "full-reference-cycle", StringComparison.OrdinalIgnoreCase);
    }

    private static void PrintSummary(IReadOnlyList<DemoStepResult> results, DemoStepResult? firstFailedStep)
    {
        Console.WriteLine();
        Console.WriteLine("Sequence summary:");
        foreach (DemoStepResult result in results)
        {
            Console.WriteLine($"- {result.StepName}: {result.StatusCode}");
        }

        if (firstFailedStep is null)
        {
            Console.WriteLine("Overall result: success");
            return;
        }

        Console.WriteLine("Overall result: failed");
        Console.WriteLine($"First failed step: {firstFailedStep.StepName}");
        Console.WriteLine($"StatusCode: {firstFailedStep.StatusCode}");
    }

    private sealed record DemoStep(
        string Name,
        MethodCallTarget Target,
        string MethodName,
        bool IsLoadByName = false)
    {
        public bool IsStop => string.Equals(MethodName, "Stop", StringComparison.Ordinal);
    }
}
