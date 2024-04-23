// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.References;

/// <summary>Available methods to perform research.</summary>
public enum ResearchMethods
{
    CountPosInSigns = 0,
    CountPosInHouses = 1,
    CountAspects = 2,
    CountUnaspected = 3,
    CountOccupiedMidpoints = 4,
    CountHarmonicConjunctions = 5,
    CountDeclinationParallels = 6,
    CountDeclinationMidpoints = 7,
    CountOob = 8
}


/// <summary>Specifications for a research method.</summary>
/// <param name="ResearchMethod"/>
/// <param name="MinNumberOfPoints">Minimal number of points to be used for this method.</param>
/// <param name="Text">Descriptive text.</param>
public record ResearchMethodDetails(ResearchMethods ResearchMethod, int MinNumberOfPoints, string Text);


/// <summary>Extension methods for ResearchMethods.</summary>
public static class ResearchMethodsExtensions
{
 
    /// <summary>Retrieve details for research method.</summary>
    /// <param name="method">The method.</param>
    /// <returns>Details for the method.</returns>
    /// <exception cref="ArgumentException">Is thrown if ResearchMethod is unknown or None.</exception>
    public static ResearchMethodDetails GetDetails(this ResearchMethods method)
    {
        return method switch
        {
            ResearchMethods.CountPosInSigns => new ResearchMethodDetails(method, 1, "Count positions in signs"),
            ResearchMethods.CountPosInHouses => new ResearchMethodDetails(method, 1, "Count positions in houses"),
            ResearchMethods.CountAspects => new ResearchMethodDetails(method, 2, "Count aspects"),
            ResearchMethods.CountUnaspected => new ResearchMethodDetails(method, 1,"Count unaspected celestial points"),
            ResearchMethods.CountOccupiedMidpoints => new ResearchMethodDetails(method, 3,"Count occupied midpoints"),
            ResearchMethods.CountHarmonicConjunctions => new ResearchMethodDetails(method, 1,"Count harmonic conjunctions"),
            ResearchMethods.CountDeclinationParallels => new ResearchMethodDetails(method, 2, "Count parallels"),
            ResearchMethods.CountDeclinationMidpoints => new ResearchMethodDetails(method, 3, "Count midpoints in declination"),
            ResearchMethods.CountOob => new ResearchMethodDetails(method, 1, "Count OOB (Out of bound) positions"),
            _ => throw new ArgumentException("ResearchMethod unknown : " + method)
        };
    }

    /// <summary>Retrieve details for items in the enum ResearchMethods.</summary>
    /// <returns>All details.</returns>
    public static List<ResearchMethodDetails> AllDetails()
    {
        return (from ResearchMethods methodCurrent in Enum.GetValues(typeof(ResearchMethods)) 
            select methodCurrent.GetDetails()).ToList();
    }

    /// <summary>Find ResearchMethod for given index.</summary>
    /// <param name="index">The index to search for.</param>
    /// <returns>The ResearchMethod.</returns>
    /// <exception cref="ArgumentException">Is thrown if ResearchMethod for given index could not be found.</exception>
    public static ResearchMethods ResearchMethodForIndex(int index)
    {
        foreach (ResearchMethods methodCurrent in Enum.GetValues(typeof(ResearchMethods)))
        {
            if (index >= 0 && (int)methodCurrent == index) return methodCurrent;
        }
        throw new ArgumentException("Could not find valid ResearchMethod for index : " + index);
    }

}





