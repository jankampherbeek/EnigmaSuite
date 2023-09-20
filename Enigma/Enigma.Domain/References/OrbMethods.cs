// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.References;

public enum OrbMethods
{
   // FixMajorMinor = 0,
    Weighted = 1
}


/// <summary>Details for an orb orbMethod.</summary>
/// <param name="OrbMethod">The orb orbMethod.</param>
/// <param name="Text">Descriptive text.</param>
public record OrbMethodDetails(OrbMethods OrbMethod, string Text);


public static class OrbMethodsExtensions
{
    /// <summary>Retrieve details for orb orbMethod.</summary>
    /// <param name="orbMethod">The orb orbMethod.</param>
    /// <returns>Details for the orb orbMethod.</returns>
    public static OrbMethodDetails GetDetails(this OrbMethods orbMethod)
    {
        return orbMethod switch
        {
       //     OrbMethods.FixMajorMinor => new OrbMethodDetails(orbMethod, "ref.enum.orbmethod.fixmajorminor"),
            OrbMethods.Weighted => new OrbMethodDetails(orbMethod, "Weighted"),
            _ => throw new ArgumentException("OrbMethod unknown : " + orbMethod)
        };
    }

    /// <summary>Retrieve details for items in the enum OrbMethods.</summary>
    /// <returns>All details.</returns>
    public static List<OrbMethodDetails> AllDetails()
    {
        return (from OrbMethods currentMethod in Enum.GetValues(typeof(OrbMethods)) 
            select currentMethod.GetDetails()).ToList();
    }


    /// <summary>Find orb method for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The orb method for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static OrbMethods OrbMethodForIndex(int index)
    {
        foreach (OrbMethods currentMethod in Enum.GetValues(typeof(OrbMethods)))
        {
            if ((int)currentMethod == index) return currentMethod;
        }
        throw new ArgumentException("Could not find Orb Method.");
    }

}

