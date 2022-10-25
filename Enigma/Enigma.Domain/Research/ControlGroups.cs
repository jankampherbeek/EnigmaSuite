// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Engima.Domain.Research;


/// <summary>Types of controlgroups.</summary>
public enum ControlGroupTypes
{
    StandardShift = 1,
    GroupMemberShift = 2
}

/// <summary>Details for ControlGroupTypes </summary>
public record ControlGroupTypeDetails
{
    readonly public ControlGroupTypes ControlGroupType;
    readonly public string TextId;

    /// <param name="controlGroupType">Instance from enum ControlGroupTypes.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public ControlGroupTypeDetails(ControlGroupTypes controlGroupType, string textId)
    {
        ControlGroupType = controlGroupType;
        TextId = textId;
    }
}

/// <summary>Specifications for ControlGroupTypes.</summary>
public interface IControlGroupTypeSpecifications
{
    /// <param name="controlGroupType">The controlgroup type, from the enum ControlGroupTypes.</param>
    /// <returns>A record CongrolGroupTypeDetails with the specification of the controlgroup type.</returns>
    public ControlGroupTypeDetails DetailsForControlGroupType(ControlGroupTypes controlGroupType);
    /// <returns>List with details for all controlgroup types.</returns>
    public List<ControlGroupTypeDetails> AllControlGroupTypeDetails();
    /// <summary>Find controlgroup type from enum.</summary>
    /// <param name="controlGroupTypeIndex">Index in enum.</param>
    /// <returns>If found: the control group type Otherwise an ArgumentException is thrown.</returns>
    public ControlGroupTypes ControlGroupTypeForIndex(int controlGroupTypeIndex);
}


/// <inheritdoc/>
public class ControlGroupTypeSpecifications : IControlGroupTypeSpecifications
{
    /// <inheritdoc/>
    public List<ControlGroupTypeDetails> AllControlGroupTypeDetails()
    {
        var allDetails = new List<ControlGroupTypeDetails>();
        foreach (ControlGroupTypes controlGroupType in Enum.GetValues(typeof(ControlGroupTypes)))
        {
            allDetails.Add(DetailsForControlGroupType(controlGroupType));
        }
        return allDetails;
    }

    /// <inheritdoc/>
    public ControlGroupTypeDetails DetailsForControlGroupType(ControlGroupTypes controlGroupType) => controlGroupType switch
    {
        ControlGroupTypes.StandardShift => new ControlGroupTypeDetails(controlGroupType, "ref.enum.controlgrouptypes.standardshift"),
        ControlGroupTypes.GroupMemberShift => new ControlGroupTypeDetails(controlGroupType, "ref.enum.controlgrouptypes.groupmembershift"),
        _ => throw new ArgumentException("Controlgroup type unknown : " + controlGroupType.ToString())
    };

    /// <inheritdoc/>
    public ControlGroupTypes ControlGroupTypeForIndex(int controlGroupTypeIndex)
    {
        foreach (ControlGroupTypes cgType in Enum.GetValues(typeof(ControlGroupTypes)))
        {
            if ((int)cgType == controlGroupTypeIndex) return cgType;
        }
        throw new ArgumentException("Could not find ControlGroupTypes for index : " + controlGroupTypeIndex);
    }

}




