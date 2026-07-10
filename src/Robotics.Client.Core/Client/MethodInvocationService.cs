using System.Globalization;
using Opc.Ua;
using Opc.Ua.Client;
using Robotics.Client.Core.Reporting;

namespace Robotics.Client.Core.Client;

public sealed class MethodInvocationService(Session session)
{
    public MethodInvocationResult Invoke(DiscoveryReport report, MethodInvocationRequest invocationRequest)
    {
        IReadOnlyList<MethodReport> matches = FindMethods(report, invocationRequest);
        if (matches.Count == 0)
        {
            return MethodInvocationResult.Failure(
                MethodInvocationOutcome.Missing,
                invocationRequest.QualifiedTarget,
                "Expected method or parent object is missing.");
        }

        if (matches.Count > 1)
        {
            IReadOnlyList<string> warnings = matches
                .Select(match => $"ObjectId={match.ParentNodeId ?? "missing"} | MethodId={match.NodeId ?? "missing"} | Evidence={match.Evidence}")
                .ToArray();

            return MethodInvocationResult.Failure(
                MethodInvocationOutcome.Ambiguous,
                invocationRequest.QualifiedTarget,
                "Ambiguous discovered method target.",
                warnings: warnings);
        }

        MethodReport method = matches[0];
        if (!method.Found || string.IsNullOrWhiteSpace(method.NodeId) || string.IsNullOrWhiteSpace(method.ParentNodeId))
        {
            return MethodInvocationResult.Failure(
                MethodInvocationOutcome.Missing,
                invocationRequest.QualifiedTarget,
                "Expected method or parent object is missing.",
                method.Evidence);
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
            return MethodInvocationResult.Failure(
                MethodInvocationOutcome.InvalidNodeId,
                invocationRequest.QualifiedTarget,
                $"Discovered ObjectId or MethodId is not a valid NodeId: {ex.Message}",
                method.Evidence);
        }

        if (!TryCreateInputArguments(method, invocationRequest, out VariantCollection inputArguments, out IReadOnlyList<MethodInvocationArgumentValue> inputArgumentValues, out MethodInvocationOutcome inputFailureOutcome, out string? error))
        {
            return MethodInvocationResult.Failure(
                inputFailureOutcome,
                invocationRequest.QualifiedTarget,
                error ?? "Input argument validation failed.",
                method.Evidence);
        }

        var callRequest = new CallMethodRequest
        {
            ObjectId = objectId,
            MethodId = methodId,
            InputArguments = inputArguments
        };

        session.Call(
            requestHeader: null,
            methodsToCall: new CallMethodRequestCollection { callRequest },
            results: out CallMethodResultCollection results,
            diagnosticInfos: out DiagnosticInfoCollection diagnosticInfos);

        if (results.Count == 0)
        {
            return MethodInvocationResult.Failure(
                MethodInvocationOutcome.UnexpectedResult,
                invocationRequest.QualifiedTarget,
                "BadUnexpectedError",
                method.Evidence);
        }

        CallMethodResult result = results[0];
        string callStatusCode = result.StatusCode.ToString();
        IReadOnlyList<MethodInvocationArgumentValue> outputArguments = CreateOutputArgumentValues(method.OutputArguments.Arguments, result.OutputArguments);

        return new MethodInvocationResult(
            MethodInvocationOutcome.Called,
            invocationRequest.QualifiedTarget,
            objectId.ToString(),
            methodId.ToString(),
            inputArgumentValues,
            callStatusCode,
            StatusCode.IsGood(result.StatusCode),
            outputArguments,
            result.InputArgumentResults.Select(statusCode => statusCode.ToString()).ToArray(),
            diagnosticInfos.Select(diagnosticInfo => diagnosticInfo.ToString()).ToArray(),
            Warnings: [],
            Error: StatusCode.IsGood(result.StatusCode) ? null : callStatusCode,
            method.Evidence);
    }

    public bool TryGetInputArgumentMetadata(
        DiscoveryReport report,
        MethodInvocationRequest invocationRequest,
        out IReadOnlyList<MethodArgumentReport> arguments,
        out string? status,
        out string? error)
    {
        arguments = [];
        status = null;
        error = null;

        IReadOnlyList<MethodReport> matches = FindMethods(report, invocationRequest);
        if (matches.Count == 0)
        {
            error = $"expected method or parent object is missing: {invocationRequest.QualifiedTarget}";
            return false;
        }

        if (matches.Count > 1)
        {
            error = $"ambiguous target {invocationRequest.QualifiedTarget}; {matches.Count} discovered methods match.";
            return false;
        }

        MethodReport method = matches[0];
        if (!method.Found || string.IsNullOrWhiteSpace(method.NodeId) || string.IsNullOrWhiteSpace(method.ParentNodeId))
        {
            error = $"expected method or parent object is missing: {invocationRequest.QualifiedTarget}. Evidence: {method.Evidence}";
            return false;
        }

        status = method.InputArguments.Status;
        arguments = method.InputArguments.Arguments;
        return true;
    }

    private static IReadOnlyList<MethodReport> FindMethods(DiscoveryReport report, MethodInvocationRequest invocationRequest)
    {
        // Official specification truth: OPC UA Method calls require the Method NodeId and the Object NodeId
        // that owns the executable method in the server address space.
        // Local NodeSet/generated-code truth: discovery selected Robotics TaskControlOperation and
        // SystemOperation methods by generated Robotics BrowseName constants and runtime browse results.
        // Implementation decision: choose only from the discovered report and fail on ambiguity.
        return invocationRequest.Target switch
        {
            MethodInvocationTarget.TaskControlOperation => report.MotionDeviceSystems
                .SelectMany(system => system.Controllers)
                .SelectMany(controller => controller.TaskControls)
                .SelectMany(taskControl => taskControl.Methods)
                .Where(method => string.Equals(method.Name, invocationRequest.MethodName, StringComparison.Ordinal))
                .ToArray(),

            MethodInvocationTarget.SystemOperation => report.MotionDeviceSystems
                .SelectMany(system => system.Controllers)
                .Select(controller => controller.SystemOperation)
                .Where(operation => operation is not null)
                .SelectMany(operation => operation!.Methods)
                .Where(method => string.Equals(method.Name, invocationRequest.MethodName, StringComparison.Ordinal))
                .ToArray(),

            _ => []
        };
    }

    private static bool TryCreateInputArguments(
        MethodReport method,
        MethodInvocationRequest invocationRequest,
        out VariantCollection inputArguments,
        out IReadOnlyList<MethodInvocationArgumentValue> inputArgumentValues,
        out MethodInvocationOutcome failureOutcome,
        out string? error)
    {
        inputArguments = new VariantCollection();
        inputArgumentValues = [];
        failureOutcome = MethodInvocationOutcome.InvalidInput;
        error = null;

        int suppliedCount = invocationRequest.PositionalInputValues.Count + invocationRequest.NamedInputValues.Count;

        // Official specification truth: InputArguments, when present, describes method inputs as OPC UA Arguments.
        // Local NodeSet/generated-code truth: the report contains the server runtime InputArguments property.
        // Implementation decision: missing/none is accepted only with no supplied values.
        if (method.InputArguments.Status == "missing" || method.InputArguments.Status == "none")
        {
            if (suppliedCount == 0)
            {
                return true;
            }

            error = $"method runtime metadata reports zero inputs, but {suppliedCount} input value(s) were supplied.";
            return false;
        }

        if (method.InputArguments.Status != "listed")
        {
            failureOutcome = MethodInvocationOutcome.UnsupportedMetadata;
            error = $"method InputArguments are {method.InputArguments.Status}: {method.InputArguments.Diagnostic ?? "no diagnostic"}";
            return false;
        }

        IReadOnlyList<MethodArgumentReport> metadata = method.InputArguments.Arguments;
        if (!TryResolveSuppliedValues(metadata, invocationRequest, out IReadOnlyList<string> suppliedValues, out error))
        {
            return false;
        }

        if (suppliedValues.Count != metadata.Count)
        {
            error = $"input count mismatch: metadata requires {metadata.Count}, supplied {suppliedValues.Count}.";
            return false;
        }

        var values = new List<MethodInvocationArgumentValue>();
        for (int index = 0; index < metadata.Count; index++)
        {
            MethodArgumentReport argument = metadata[index];
            if (!TryConvertScalar(argument, suppliedValues[index], out Variant variant, out failureOutcome, out error))
            {
                return false;
            }

            inputArguments.Add(variant);
            values.Add(new MethodInvocationArgumentValue(
                GetArgumentName(argument, index),
                argument.DataType,
                argument.ValueRank,
                FormatValue(variant.Value)));
        }

        inputArgumentValues = values;
        return true;
    }

    private static bool TryResolveSuppliedValues(
        IReadOnlyList<MethodArgumentReport> metadata,
        MethodInvocationRequest invocationRequest,
        out IReadOnlyList<string> suppliedValues,
        out string? error)
    {
        suppliedValues = [];
        error = null;

        if (invocationRequest.PositionalInputValues.Count > 0 && invocationRequest.NamedInputValues.Count > 0)
        {
            error = "input values must be supplied either positionally or by name, not both.";
            return false;
        }

        if (invocationRequest.PositionalInputValues.Count > 0)
        {
            suppliedValues = invocationRequest.PositionalInputValues;
            return true;
        }

        if (invocationRequest.NamedInputValues.Count == 0)
        {
            suppliedValues = [];
            return true;
        }

        if (metadata.Count != invocationRequest.NamedInputValues.Count)
        {
            error = $"input count mismatch: metadata requires {metadata.Count}, supplied {invocationRequest.NamedInputValues.Count}.";
            return false;
        }

        var values = new List<string>();
        var usedKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (int index = 0; index < metadata.Count; index++)
        {
            MethodArgumentReport argument = metadata[index];
            if (TryGetNamedInputValue(invocationRequest.NamedInputValues, argument.Name, out string? value, out string? matchedKey))
            {
                values.Add(value);
                usedKeys.Add(matchedKey!);
                continue;
            }

            if (TryGetAliasValue(invocationRequest, argument, out value, out matchedKey))
            {
                values.Add(value);
                usedKeys.Add(matchedKey!);
                continue;
            }

            error = $"input '{GetArgumentName(argument, index)}' was not supplied.";
            return false;
        }

        string? unusedKey = invocationRequest.NamedInputValues.Keys.FirstOrDefault(key => !usedKeys.Contains(key));
        if (unusedKey is not null)
        {
            error = $"input '{unusedKey}' does not match runtime InputArguments metadata.";
            return false;
        }

        suppliedValues = values;
        return true;
    }

    private static bool TryGetAliasValue(
        MethodInvocationRequest invocationRequest,
        MethodArgumentReport argument,
        out string? value,
        out string? matchedKey)
    {
        value = null;
        matchedKey = null;

        if (invocationRequest.InputAliases.Count == 0 ||
            !TryParseNodeId(argument.DataType, out NodeId? dataType))
        {
            return false;
        }

        foreach ((string aliasName, string aliasKind) in invocationRequest.InputAliases)
        {
            if (!invocationRequest.NamedInputValues.TryGetValue(aliasName, out string? candidate))
            {
                continue;
            }

            if (string.Equals(aliasKind, "string", StringComparison.Ordinal) && dataType == DataTypeIds.String)
            {
                value = candidate;
                matchedKey = aliasName;
                return true;
            }

            if (string.Equals(aliasKind, "integer", StringComparison.Ordinal) && IsSupportedIntegerDataType(dataType))
            {
                value = candidate;
                matchedKey = aliasName;
                return true;
            }
        }

        return false;
    }

    private static bool TryGetNamedInputValue(
        IReadOnlyDictionary<string, string> suppliedValues,
        string argumentName,
        out string? value,
        out string? matchedKey)
    {
        value = null;
        matchedKey = null;

        if (string.IsNullOrWhiteSpace(argumentName))
        {
            return false;
        }

        foreach ((string key, string candidate) in suppliedValues)
        {
            if (string.Equals(key, argumentName, StringComparison.OrdinalIgnoreCase))
            {
                value = candidate;
                matchedKey = key;
                return true;
            }
        }

        return false;
    }

    private static bool TryConvertScalar(
        MethodArgumentReport argument,
        string suppliedValue,
        out Variant variant,
        out MethodInvocationOutcome failureOutcome,
        out string? error)
    {
        variant = Variant.Null;
        failureOutcome = MethodInvocationOutcome.InvalidInput;
        error = null;

        if (argument.ValueRank >= 0)
        {
            failureOutcome = MethodInvocationOutcome.UnsupportedMetadata;
            error = $"unsupported argument datatype {argument.DataType}: array arguments are not supported.";
            return false;
        }

        if (!TryParseNodeId(argument.DataType, out NodeId? dataType))
        {
            failureOutcome = MethodInvocationOutcome.UnsupportedMetadata;
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
                failureOutcome = MethodInvocationOutcome.UnsupportedMetadata;
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

    private static IReadOnlyList<MethodInvocationArgumentValue> CreateOutputArgumentValues(
        IReadOnlyList<MethodArgumentReport> metadata,
        VariantCollection outputArguments)
    {
        var values = new List<MethodInvocationArgumentValue>();
        for (int index = 0; index < outputArguments.Count; index++)
        {
            MethodArgumentReport? argument = index < metadata.Count ? metadata[index] : null;
            values.Add(new MethodInvocationArgumentValue(
                argument is null ? $"Argument{index}" : GetArgumentName(argument, index),
                argument?.DataType ?? "unknown",
                argument?.ValueRank ?? -1,
                FormatValue(outputArguments[index].Value)));
        }

        return values;
    }

    private static bool IsSupportedIntegerDataType(NodeId dataType)
    {
        return dataType == DataTypeIds.Int32 ||
            dataType == DataTypeIds.UInt32 ||
            dataType == DataTypeIds.Int64 ||
            dataType == DataTypeIds.UInt64;
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

    private static string GetArgumentName(MethodArgumentReport argument, int index)
    {
        return string.IsNullOrWhiteSpace(argument.Name) ? $"Argument{index}" : argument.Name;
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
