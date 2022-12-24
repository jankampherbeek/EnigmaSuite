// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Analysis;

public enum OrbMethods
{
    FixMajorMinor = 0,
    Weighted = 1
  //  Dynamic = 2
}

public static class OrbMethodsExtensions
{
    /// <summary>Retrieve details for orb orbMethod.</summary>
    /// <param name="orbMethod">The orb orbMethod, is automatically filled.</param>
    /// <returns>Details for the orb orbMethod.</returns>
    public static OrbMethodDetails GetDetails(this OrbMethods orbMethod)
    {
        return orbMethod switch
        {
            OrbMethods.FixMajorMinor => new OrbMethodDetails(orbMethod, "ref.enum.orbmethod.fixmajorminor"),
            OrbMethods.Weighted => new OrbMethodDetails(orbMethod, "ref.enum.orbmethod.weighted"),
  //          OrbMethods.Dynamic => new OrbMethodDetails(orbMethod, "ref.enum.orbmethod.dynamic"),
            _ => throw new ArgumentException("OrbMethod unknown : " + orbMethod.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum OrbMethods.</summary>
    /// <param name="orbMethod">The orb method, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<OrbMethodDetails> AllDetails(this OrbMethods orbMethod)
    {
        var allDetails = new List<OrbMethodDetails>();
        foreach (OrbMethods currentMethod in Enum.GetValues(typeof(OrbMethods)))
        {
            allDetails.Add(currentMethod.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find orb method for an index.</summary>
    /// <param name="orbMethod">Any orb method, automatically filled.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The orb method for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static OrbMethods OrbMethodForIndex(this OrbMethods orbMethod, int index)
    {
        foreach (OrbMethods currentMethod in Enum.GetValues(typeof(OrbMethods)))
        {
            if ((int)currentMethod == index) return currentMethod;
        }
        throw new ArgumentException("Could not find Orb Method for index : " + index);
    }

}


/// <summary>Details for an orb orbMethod.</summary>
/// <param name="OrbMethod">The orb orbMethod.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record OrbMethodDetails(OrbMethods OrbMethod, string TextId);