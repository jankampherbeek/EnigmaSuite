// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Analysis;

public enum OrbMethods
{
    FixMajorMinor = 0,
    Weighted = 1,
    Dynamic = 2
}



/// <summary>Details for an orb method.</summary>
public record OrbMethodDetails
{
    readonly public OrbMethods OrbMethod;
    readonly public string TextId;

    /// <param name="orbMethod">The orb method.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public OrbMethodDetails(OrbMethods orbMethod, string textId)
    {
        OrbMethod = orbMethod;
        TextId = textId;
    }
}


/// <inheritdoc/>
public class OrbMethodSpecifications : IOrbMethodSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the orb method was not recognized.</exception>
    public OrbMethodDetails DetailsForOrbMethod(OrbMethods orbMethod) => orbMethod switch
    {
        OrbMethods.FixMajorMinor => new OrbMethodDetails(orbMethod, "ref.enum.orbmethod.fixmajorminor"),
        OrbMethods.Weighted => new OrbMethodDetails(orbMethod, "ref.enum.orbmethod.weighted"),
        OrbMethods.Dynamic => new OrbMethodDetails(orbMethod, "ref.enum.orbmethod.dynamic"),
        _ => throw new ArgumentException("Orb method unknown : " + orbMethod.ToString())
    };

    public List<OrbMethodDetails> AllOrbMethodDetails()
    {
        var allDetails = new List<OrbMethodDetails>();
        foreach (OrbMethods orbMethod in Enum.GetValues(typeof(OrbMethods)))
        {
            allDetails.Add(DetailsForOrbMethod(orbMethod));
        }
        return allDetails;
    }

    public OrbMethods OrbMethodForIndex(int index)
    {
        foreach (OrbMethods orbMethod in Enum.GetValues(typeof(OrbMethods)))
        {
            if ((int)orbMethod == index) return orbMethod;
        }
        throw new ArgumentException("Could not find Orb Method for index : " + index);
    }
}