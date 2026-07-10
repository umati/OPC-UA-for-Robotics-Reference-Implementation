using System.Globalization;
using Opc.Ua;
using Opc.Ua.Client;
using Robotics.Client.Core.Client;
using Robotics.Client.Core.Reporting;
using Robotics.ReferenceClient.Reporting;

namespace Robotics.ReferenceClient.Client;

internal sealed class MethodCallService(Session session)
{
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
        IReadOnlyList<MethodReport> matches = FindMethods(report, command);
        if (matches.Count == 0)
        {
            Console.WriteLine($"Error: expected method or parent object is missing: {command.QualifiedTarget}");
            return MethodCallResult.Failed(command.QualifiedTarget, "Expected method or parent object is missing.");
        }

        if (matches.Count > 1)
        {
            Console.WriteLine($"Error: ambiguous target {command.QualifiedTarget}; {matches.Count} discovered methods match.");
            foreach (MethodReport match in matches)
            {
                Console.WriteLine($"- ObjectId={match.ParentNodeId ?? "missing"} | MethodId={match.NodeId ?? "missing"} | Evidence={match.Evidence}");
            }

            return MethodCallResult.Failed(command.QualifiedTarget, "Ambiguous discovered method target.");
        }

        MethodReport method = matches[0];
        if (!method.Found || string.IsNullOrWhiteSpace(method.NodeId) || string.IsNullOrWhiteSpace(method.ParentNodeId))
        {
            Console.WriteLine($"Error: expected method or parent object is missing: {command.QualifiedTarget}");
            Console.WriteLine($"Evidence: {method.Evidence}");
            return MethodCallResult.Failed(command.QualifiedTarget, "Expected method or parent object is missing.");
        }

        NodeId objectId;
        NodeId methodId;
        try
        {
            objectId = NodeId.Parse(method.ParentNodeId);
            methodId = NodeId.Parse(method.NodeId);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Error: discovered ObjectId or MethodId is not a valid NodeId: {ex.Message}");
            return MethodCallResult.Failed(command.QualifiedTarget, "Discovered ObjectId or MethodId is not a valid NodeId.");
        }

        Console.WriteLine($"ObjectId: {objectId}");
        Console.WriteLine($"MethodId: {methodId}");

        if (!TryCreateInputArguments(method, command.InputValues, out VariantCollection inputArguments, out string? error))
        {
            Console.WriteLine($"Error: {error}");
            return MethodCallResult.Failed(command.QualifiedTarget, error ?? "Input argument validation failed.");
        }

        Console.WriteLine("InputArguments:");
        PrintInputArguments(method.InputArguments.Arguments, inputArguments);

        var request = new CallMethodRequest
        {
            ObjectId = objectId,
            MethodId = methodId,
            InputArguments = inputArguments
        };

        session.Call(
            requestHeader: null,
            methodsToCall: new CallMethodRequestCollection { request },
            results: out CallMethodResultCollection results,
            diagnosticInfos: out DiagnosticInfoCollection diagnosticInfos);

        if (results.Count == 0)
        {
            Console.WriteLine("Call StatusCode: BadUnexpectedError");
            Console.WriteLine("Result: failed");
            return MethodCallResult.Failed(command.QualifiedTarget, "BadUnexpectedError");
        }

        CallMethodResult result = results[0];
        Console.WriteLine($"Call StatusCode: {result.StatusCode}");

        if (result.InputArgumentResults.Count > 0)
        {
            Console.WriteLine("InputArgumentResults:");
            for (int index = 0; index < result.InputArgumentResults.Count; index++)
            {
                Console.WriteLine($"- {index}: {result.InputArgumentResults[index]}");
            }
        }

        if (diagnosticInfos.Count > 0)
        {
            Console.WriteLine("DiagnosticInfos:");
            for (int index = 0; index < diagnosticInfos.Count; index++)
            {
                Console.WriteLine($"- {index}: {diagnosticInfos[index]}");
            }
        }

        Console.WriteLine("OutputArguments:");
        PrintOutputArguments(method.OutputArguments.Arguments, result.OutputArguments);

        if (StatusCode.IsNotGood(result.StatusCode))
        {
            Console.WriteLine("Result: failed");
            return MethodCallResult.Failed(command.QualifiedTarget, result.StatusCode.ToString());
        }

        return MethodCallResult.Success(command.QualifiedTarget, result.StatusCode.ToString());
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

        IReadOnlyList<MethodReport> matches = FindMethods(report, command);
        if (matches.Count == 0)
        {
            error = $"expected method or parent object is missing: {command.QualifiedTarget}";
            return false;
        }

        if (matches.Count > 1)
        {
            error = $"ambiguous target {command.QualifiedTarget}; {matches.Count} discovered methods match.";
            return false;
        }

        MethodReport method = matches[0];
        if (!method.Found || string.IsNullOrWhiteSpace(method.NodeId) || string.IsNullOrWhiteSpace(method.ParentNodeId))
        {
            error = $"expected method or parent object is missing: {command.QualifiedTarget}. Evidence: {method.Evidence}";
            return false;
        }

        status = method.InputArguments.Status;
        arguments = method.InputArguments.Arguments;
        return true;
    }

    private static IReadOnlyList<MethodReport> FindMethods(DiscoveryReport report, MethodCallCommand command)
    {
        // Official specification truth: OPC UA Method calls require the Method NodeId and the Object NodeId
        // that owns the executable method in the server address space.
        // Local NodeSet/generated-code truth: C1/C2 discovery already selected Robotics TaskControlOperation
        // and SystemOperation methods by generated Robotics BrowseName constants and runtime browse results.
        // Implementation decision: choose only from the discovered report, never construct MethodIds, and fail
        // instead of guessing when more than one discovered target matches the requested logical label.
        return command.Target switch
        {
            MethodCallTarget.TaskControlOperation => report.MotionDeviceSystems
                .SelectMany(system => system.Controllers)
                .SelectMany(controller => controller.TaskControls)
                .SelectMany(taskControl => taskControl.Methods)
                .Where(method => string.Equals(method.Name, command.MethodName, StringComparison.Ordinal))
                .ToArray(),

            MethodCallTarget.SystemOperation => report.MotionDeviceSystems
                .SelectMany(system => system.Controllers)
                .Select(controller => controller.SystemOperation)
                .Where(operation => operation is not null)
                .SelectMany(operation => operation!.Methods)
                .Where(method => string.Equals(method.Name, command.MethodName, StringComparison.Ordinal))
                .ToArray(),

            _ => []
        };
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

    private static bool TryCreateInputArguments(
        MethodReport method,
        IReadOnlyList<string> suppliedValues,
        out VariantCollection inputArguments,
        out string? error)
    {
        inputArguments = new VariantCollection();
        error = null;

        // Official specification truth: InputArguments, when present, describes method inputs as OPC UA Arguments.
        // Local NodeSet/generated-code truth: the report contains the server runtime InputArguments property read by C2.
        // Implementation decision: only missing/none with zero supplied values is accepted as zero-input.
        if (method.InputArguments.Status == "missing" || method.InputArguments.Status == "none")
        {
            if (suppliedValues.Count == 0)
            {
                return true;
            }

            error = $"method runtime metadata reports zero inputs, but {suppliedValues.Count} input value(s) were supplied.";
            return false;
        }

        if (method.InputArguments.Status != "listed")
        {
            error = $"method InputArguments are {method.InputArguments.Status}: {method.InputArguments.Diagnostic ?? "no diagnostic"}";
            return false;
        }

        if (suppliedValues.Count != method.InputArguments.Arguments.Count)
        {
            error = $"input count mismatch: metadata requires {method.InputArguments.Arguments.Count}, supplied {suppliedValues.Count}.";
            return false;
        }

        for (int index = 0; index < method.InputArguments.Arguments.Count; index++)
        {
            MethodArgumentReport argument = method.InputArguments.Arguments[index];
            if (!TryConvertScalar(argument, suppliedValues[index], out Variant variant, out error))
            {
                return false;
            }

            inputArguments.Add(variant);
        }

        return true;
    }

    private static bool TryConvertScalar(MethodArgumentReport argument, string suppliedValue, out Variant variant, out string? error)
    {
        variant = Variant.Null;
        error = null;

        if (argument.ValueRank >= 0)
        {
            error = $"unsupported argument datatype {argument.DataType}: array arguments are not supported.";
            return false;
        }

        if (!TryParseNodeId(argument.DataType, out NodeId? dataType))
        {
            error = $"unsupported argument datatype {argument.DataType}: DataType NodeId could not be parsed.";
            return false;
        }

        try
        {
            object converted;
            if (dataType == DataTypeIds.String)
            {
                converted = suppliedValue;
            }
            else if (dataType == DataTypeIds.Boolean)
            {
                converted = bool.Parse(suppliedValue);
            }
            else if (dataType == DataTypeIds.Int32)
            {
                converted = int.Parse(suppliedValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
            }
            else if (dataType == DataTypeIds.UInt32)
            {
                converted = uint.Parse(suppliedValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
            }
            else if (dataType == DataTypeIds.Int64)
            {
                converted = long.Parse(suppliedValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
            }
            else if (dataType == DataTypeIds.UInt64)
            {
                converted = ulong.Parse(suppliedValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
            }
            else if (dataType == DataTypeIds.Double)
            {
                converted = double.Parse(suppliedValue, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
            }
            else if (dataType == DataTypeIds.NodeId)
            {
                converted = NodeId.Parse(suppliedValue);
            }
            else
            {
                error = $"unsupported argument datatype {argument.DataType}";
                return false;
            }

            variant = new Variant(converted);
            return true;
        }
        catch (Exception ex) when (ex is FormatException or OverflowException or ArgumentException)
        {
            error = $"input '{argument.Name}' could not be converted to {argument.DataType}: {ex.Message}";
            return false;
        }
    }

    private static bool TryParseNodeId(string text, out NodeId? nodeId)
    {
        try
        {
            nodeId = NodeId.Parse(text);
            return true;
        }
        catch (FormatException)
        {
            nodeId = null;
            return false;
        }
    }

    private static void PrintInputArguments(IReadOnlyList<MethodArgumentReport> metadata, VariantCollection inputArguments)
    {
        if (metadata.Count == 0)
        {
            Console.WriteLine("- none");
            return;
        }

        for (int index = 0; index < metadata.Count; index++)
        {
            string name = string.IsNullOrWhiteSpace(metadata[index].Name) ? $"Argument{index}" : metadata[index].Name;
            object? value = inputArguments[index].Value;
            Console.WriteLine($"- {name} = {FormatValue(value)} | DataType={metadata[index].DataType}");
        }
    }

    private static void PrintOutputArguments(IReadOnlyList<MethodArgumentReport> metadata, VariantCollection outputArguments)
    {
        if (outputArguments.Count == 0)
        {
            Console.WriteLine("- none");
            return;
        }

        for (int index = 0; index < outputArguments.Count; index++)
        {
            string name = index < metadata.Count && !string.IsNullOrWhiteSpace(metadata[index].Name)
                ? metadata[index].Name
                : $"Argument{index}";
            string dataType = index < metadata.Count ? metadata[index].DataType : "unknown";
            Console.WriteLine($"- {name} = {FormatValue(outputArguments[index].Value)} | DataType={dataType}");
        }
    }

    private static string FormatValue(object? value)
    {
        return value switch
        {
            null => "null",
            Array array => $"[{string.Join(", ", array.Cast<object?>().Select(FormatValue))}]",
            _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty
        };
    }
}
