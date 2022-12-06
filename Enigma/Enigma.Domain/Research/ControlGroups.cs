// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Research;


/// <summary>Types of controlgroups.</summary>
public enum ControlGroupTypes
{
    StandardShift = 1,
    GroupMemberShift = 2
}


public static class ControlGroupTypesExtensions
{
    /// <summary>Retrieve details for ControlGroupTypes.</summary>
    /// <param name="cgType">The control group type, is automatically filled.</param>
    /// <returns>Details for the control group type.</returns>
    public static ControlGroupTypeDetails GetDetails(this ControlGroupTypes cgType)
    {
        return cgType switch
        {
            ControlGroupTypes.StandardShift => new ControlGroupTypeDetails(cgType, "ref.enum.controlgrouptypes.standardshift"),
            ControlGroupTypes.GroupMemberShift => new ControlGroupTypeDetails(cgType, "ref.enum.controlgrouptypes.groupmembershift"),
            _ => throw new ArgumentException("Controlgroup type unknown : " + cgType.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum ControlGroupTypes.</summary>
    /// <param name="cal">The control group type, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<ControlGroupTypeDetails> AllDetails(this ControlGroupTypes cgType)
    {
        var allDetails = new List<ControlGroupTypeDetails>();
        foreach (ControlGroupTypes currentCgType in Enum.GetValues(typeof(ControlGroupTypes)))
        {
            allDetails.Add(currentCgType.GetDetails());
        }
        return allDetails;
    }

    /// <summary>Find control group type for an index.</summary>
    /// <param name="cgType">Any control group type, automatically filled.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The control group type for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ControlGroupTypes ControlGroupTypeForIndex(this ControlGroupTypes cgType, int index)
    {
        foreach (ControlGroupTypes currentCgType in Enum.GetValues(typeof(ControlGroupTypes)))
        {
            if ((int)currentCgType == index) return currentCgType;
        }
        throw new ArgumentException("Could not find control group type for index : " + index);
    }

}


/// <summary>Details for ControlGroupTypes </summary>
/// <param name="controlGroupType">Instance from enum ControlGroupTypes.</param>
/// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
public record ControlGroupTypeDetails(ControlGroupTypes ControlGroupType, string TextId);




