using Opc.Ua;

namespace Robotics.ReferenceServer.InformationModel;

/// <summary>
/// Engineering-unit metadata used by the reference simulator's rotational axes.
/// </summary>
public static class RobotEngineeringUnitMetadata
{
    public const string UneceNamespaceUri = "http://www.opcfoundation.org/UA/units/un/cefact";
    public const int DegreeUnitId = 17476;

    public static EUInformation Degrees => new()
    {
        NamespaceUri = UneceNamespaceUri,
        UnitId = DegreeUnitId,
        DisplayName = new LocalizedText("°"),
        Description = new LocalizedText("degree [unit of angle]")
    };

    public static BaseVariableState? FindEngineeringUnitsProperty(
        BaseVariableState actualPosition,
        ISystemContext context)
    {
        ArgumentNullException.ThrowIfNull(actualPosition);
        ArgumentNullException.ThrowIfNull(context);

        var children = new List<BaseInstanceState>();
        actualPosition.GetChildren(context, children);

        return children.FirstOrDefault(child =>
            child is BaseVariableState variable
            && variable.BrowseName.Name == BrowseNames.EngineeringUnits) as BaseVariableState;
    }

    public static bool InitializeActualPositionEngineeringUnits(
        BaseVariableState actualPosition,
        ISystemContext context)
    {
        BaseVariableState? engineeringUnits = FindEngineeringUnitsProperty(actualPosition, context);
        if (engineeringUnits is null)
        {
            return false;
        }

        engineeringUnits.Value = Degrees;
        engineeringUnits.StatusCode = StatusCodes.Good;
        engineeringUnits.Timestamp = DateTime.UtcNow;
        engineeringUnits.ClearChangeMasks(context, false);
        return true;
    }
}
