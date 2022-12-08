// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Research;

/// <summary>Available methods to perform research.</summary>
public enum ResearchMethods
{
    CountCelPosInSigns,
    CountCelPosInHouses,
    CountAspects,
    CountUnaspected,
    CountOccupiedMidpoints,
    CountHarmonicConjunctions
}

public static class ResearchMethodsExtensions
{
    /// <summary>Retrieve text id for resourcebundle</summary>
    /// <param name="method">The method, is automatically filled.</param>
    /// <returns>Id for the resource bundle.</returns>
    public static string TextInRbId(this ResearchMethods method)
    {
        return method switch
        {
            ResearchMethods.CountCelPosInSigns => "ref.enum.researchmethods.countposinsigns",
            ResearchMethods.CountCelPosInHouses => "ref.enum.researchmethods.countposinhouses",
            ResearchMethods.CountAspects => "ref.enum.researchmethods.countaspects",
            ResearchMethods.CountUnaspected => "ref.enum.researchmethods.countunaspected",
            ResearchMethods.CountOccupiedMidpoints => "ref.enum.researchmethods.countoccupiedmidpoints",
            ResearchMethods.CountHarmonicConjunctions => "ref.enum.researchmethods.countharmonicconjunctions",
            _ => "",
        };
    }
}