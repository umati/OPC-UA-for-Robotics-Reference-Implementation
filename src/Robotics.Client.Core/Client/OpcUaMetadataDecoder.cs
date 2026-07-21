using System.Globalization;
using Opc.Ua;
using Robotics.Client.Core.Reporting;

namespace Robotics.Client.Core.Client;

/// <summary>Decodes standard OPC UA structured metadata without exposing SDK objects in the snapshot contract.</summary>
public static class OpcUaMetadataDecoder
{
    public static EngineeringUnitDecodeResult DecodeEngineeringUnits(object? value)
    {
        string? rawDiagnostic = value is ExtensionObject extensionObject
            ? SafeValueRenderer.Format(extensionObject)
            : null;

        EUInformation? information = Unwrap<EUInformation>(value);
        if (information is null)
        {
            return new EngineeringUnitDecodeResult(null, null, rawDiagnostic);
        }

        string? displayName = information.DisplayName?.Text;
        string? description = information.Description?.Text;
        string? humanReadable = FirstNonEmpty(displayName, description);
        return new EngineeringUnitDecodeResult(
            new EngineeringUnitMetadataReport(
                information.NamespaceUri,
                information.UnitId,
                displayName,
                description,
                null),
            humanReadable,
            null);
    }

    public static EuRangeDecodeResult DecodeRange(object? value)
    {
        string? rawDiagnostic = value is ExtensionObject extensionObject
            ? SafeValueRenderer.Format(extensionObject)
            : null;

        Opc.Ua.Range? range = Unwrap<Opc.Ua.Range>(value);
        if (range is null)
        {
            return new EuRangeDecodeResult(null, rawDiagnostic);
        }

        string text = $"{range.Low.ToString(CultureInfo.InvariantCulture)} … {range.High.ToString(CultureInfo.InvariantCulture)}";
        return new EuRangeDecodeResult(new EuRangeMetadataReport(range.Low, range.High, null), text);
    }

    private static T? Unwrap<T>(object? value) where T : class
    {
        object? candidate = value is Variant variant ? variant.Value : value;
        if (candidate is T typed)
        {
            return typed;
        }

        if (candidate is ExtensionObject extensionObject)
        {
            if (extensionObject.Body is T body)
            {
                return body;
            }

            try
            {
                return ExtensionObject.ToEncodeable(extensionObject) as T;
            }
            catch (Exception)
            {
                return null;
            }
        }

        return null;
    }

    private static string? FirstNonEmpty(params string?[] values) =>
        values.FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
}
