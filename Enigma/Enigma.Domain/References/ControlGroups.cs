// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.References;


/// <summary>Types of controlgroups.</summary>
public enum ControlGroupTypes
{
    StandardShift = 0,  
    GroupMemberShift = 1     
}


/// <summary>Details for ControlGroupTypes </summary>
/// <param name="ControlGroupType">Instance from enum ControlGroupTypes.</param>
/// <param name="Text">Descriptive text.</param>
public record ControlGroupTypeDetails(ControlGroupTypes ControlGroupType, string Text);

/// <summary>Extension class for enum ControlGroupTypes.</summary>
public static class ControlGroupTypesExtensions
{
    /// <summary>Retrieve details for ControlGroupTypes.</summary>
    /// <param name="cgType">The control group type, is automatically filled.</param>
    /// <returns>Details for the control group type.</returns>
    public static ControlGroupTypeDetails GetDetails(this ControlGroupTypes cgType)
    {
        return cgType switch
        {
            ControlGroupTypes.StandardShift => new ControlGroupTypeDetails(cgType, "Standard shifting of location, date, and time"),
            ControlGroupTypes.GroupMemberShift => new ControlGroupTypeDetails(cgType, "Shift members of groups"),   
            _ => throw new ArgumentException("Controlgroup type unknown : " + cgType)
        };
    }

    /// <summary>Retrieve details for items in the enum ControlGroupTypes.</summary>
    /// <returns>All details.</returns>
    public static List<ControlGroupTypeDetails> AllDetails()
    {
        return (from ControlGroupTypes currentCgType in Enum.GetValues(typeof(ControlGroupTypes)) select currentCgType.GetDetails()).ToList();
    }

    /// <summary>Find control group type for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The control group type for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ControlGroupTypes ControlGroupTypeForIndex(int index)
    {
        foreach (ControlGroupTypes currentCgType in Enum.GetValues(typeof(ControlGroupTypes)))
        {
            if ((int)currentCgType == index) return currentCgType;
        }
        throw new ArgumentException("Could not find control group type for index : " + index);
    }

}







