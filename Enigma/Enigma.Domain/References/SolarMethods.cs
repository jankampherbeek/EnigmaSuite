// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;

/// <summary>Methods for a solar.</summary>
public enum SolarMethods
{
    TropicalParallax = 1, TropicalNoParallax = 2, SiderealParallax = 3, SiderealNoParallax = 4
}

/// <summary>Details for a solars.</summary>
/// <param name="Method">Method to use.</param>
/// <param name="MethodName">Name for method.</param>
public record SolarMethodDetails(SolarMethods Method, string MethodName);


/// <summary>Extension class for SolarMethods.</summary>
public static class SolarMethodsExtensions
{
    /// <summary>Retrieve details for a solar method.</summary>
    /// <param name="method">The method.</param>
    /// <returns>Details for the solar method.</returns>
    public static SolarMethodDetails GetDetails(this SolarMethods method)
    {
        return method switch
        {
            SolarMethods.SiderealParallax => new SolarMethodDetails(method, "Sidereal return with parallax"),
            SolarMethods.TropicalParallax => new SolarMethodDetails(method, "Tropical return with parallax"),
            SolarMethods.SiderealNoParallax => new SolarMethodDetails(method, "Sidereal return, no parallax"),
            SolarMethods.TropicalNoParallax => new SolarMethodDetails(method, "Tropical return, no parallax"),
            _ => throw new ArgumentException("Solar method unknown : " + method)
        };
    }

    /// <summary>Retrieve details for items in the enum SolarMethods.</summary>
    /// <returns>All details.</returns>
    public static List<SolarMethodDetails> AllDetails()
    {
        return (from SolarMethods currentMethod in Enum.GetValues(typeof(SolarMethods))
            select currentMethod.GetDetails()).ToList();
    }

    /// <summary>Find solar method for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The method for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static SolarMethods MethodForIndex(int index)
    {
        foreach (SolarMethods currentMethod in Enum.GetValues(typeof(SolarMethods)))
        {
            if ((int)currentMethod == index) return currentMethod;
        }

        Log.Error("SolarMethods.MethodForIndex(): Could not find method for index : {Index}", index);
        throw new ArgumentException("Wrong index for SolarMethods");
    }
}
   