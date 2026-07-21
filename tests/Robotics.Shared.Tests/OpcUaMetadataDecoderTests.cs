using Opc.Ua;
using Robotics.Client.Core.Client;

namespace Robotics.Shared.Tests;

public class OpcUaMetadataDecoderTests
{
    private const string UneceNamespace = "http://www.opcfoundation.org/UA/units/un/cefact";

    [Fact]
    public void DecodesDirectEuInformationAndPreservesAllFields()
    {
        EUInformation value = new()
        {
            NamespaceUri = UneceNamespace,
            UnitId = 17476,
            DisplayName = new LocalizedText("°"),
            Description = new LocalizedText("degree [unit of angle]")
        };

        var decoded = OpcUaMetadataDecoder.DecodeEngineeringUnits(value);

        Assert.Equal("°", decoded.HumanReadable);
        Assert.Equal(UneceNamespace, decoded.Metadata?.NamespaceUri);
        Assert.Equal(17476, decoded.Metadata?.UnitId);
        Assert.Equal("°", decoded.Metadata?.DisplayName);
        Assert.Equal("degree [unit of angle]", decoded.Metadata?.Description);
        Assert.Null(decoded.RawDiagnostic);
    }

    [Fact]
    public void DecodesExtensionObjectBodyAndRadianMetadata()
    {
        EUInformation value = new()
        {
            NamespaceUri = UneceNamespace,
            UnitId = 4405297,
            DisplayName = new LocalizedText("rad"),
            Description = new LocalizedText("radian")
        };

        var decoded = OpcUaMetadataDecoder.DecodeEngineeringUnits(new ExtensionObject(value));

        Assert.Equal("rad", decoded.HumanReadable);
        Assert.Equal(4405297, decoded.Metadata?.UnitId);
        Assert.Equal("radian", decoded.Metadata?.Description);
        Assert.Null(decoded.RawDiagnostic);
        Assert.DoesNotContain("ExtensionObject(TypeId", decoded.HumanReadable);
    }

    [Fact]
    public void UnknownValidUnitRemainsStructuredAndIsNotReclassified()
    {
        EUInformation value = new()
        {
            NamespaceUri = "urn:vendor:units",
            UnitId = 77,
            DisplayName = new LocalizedText("widget"),
            Description = new LocalizedText("vendor widget unit")
        };

        var decoded = OpcUaMetadataDecoder.DecodeEngineeringUnits(value);

        Assert.Equal("widget", decoded.HumanReadable);
        Assert.Equal("urn:vendor:units", decoded.Metadata?.NamespaceUri);
        Assert.Equal(77, decoded.Metadata?.UnitId);
        Assert.Equal("vendor widget unit", decoded.Metadata?.Description);
    }

    [Fact]
    public void NullRemainsNull()
    {
        var decoded = OpcUaMetadataDecoder.DecodeEngineeringUnits(null);

        Assert.Null(decoded.Metadata);
        Assert.Null(decoded.HumanReadable);
        Assert.Null(decoded.RawDiagnostic);
    }

    [Fact]
    public void UndecodableExtensionObjectDoesNotThrowAndKeepsDiagnosticEvidence()
    {
        var decoded = OpcUaMetadataDecoder.DecodeEngineeringUnits(
            new ExtensionObject(new ExpandedNodeId((uint)887), new byte[] { 0x01, 0x02 }));

        Assert.Null(decoded.Metadata);
        Assert.Null(decoded.HumanReadable);
        Assert.Contains("ExtensionObject", decoded.RawDiagnostic);
    }

    [Fact]
    public void DecodesRangeAndPreservesItsStructuredRepresentation()
    {
        var decoded = OpcUaMetadataDecoder.DecodeRange(new ExtensionObject(new Opc.Ua.Range(-90, 90)));

        Assert.Equal(-90, decoded.Metadata?.Low);
        Assert.Equal(90, decoded.Metadata?.High);
        Assert.Equal("-90 … 90", decoded.HumanReadable);
    }
}
