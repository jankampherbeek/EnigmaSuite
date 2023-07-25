// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Serilog;

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>Zodiac types, e.g. sidereal or tropical.</summary>
public enum ZodiacTypes
{
    Sidereal = 0, Tropical = 1
}

/// <summary>Details for a zodiac Type</summary>
/// <param name="Type">The zodiac Type</param>
/// <param name="ValueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris</param>
/// <param name="Text">Descriptive text</param>
public record ZodiacTypeDetails(ZodiacTypes Type, int ValueForFlag, string Text);

/// <summary>Extension class for enum ZodiacTypes.</summary>
public static class ZodiacTypeExtensions
{
    /// <summary>Retrieve dtails for zodiac Type.</summary>
    /// <param name="zType">The zodiac Type, is automatically filled.</param>
    /// <returns>Details for the zodiac Type.</returns>
    public static ZodiacTypeDetails GetDetails(this ZodiacTypes zType)
    {
        return zType switch
        {
            // No specific flag for tropical.
            ZodiacTypes.Tropical => new ZodiacTypeDetails(zType, 0, "Tropical"),
            ZodiacTypes.Sidereal => new ZodiacTypeDetails(zType, EnigmaConstants.SEFLG_SIDEREAL, "Sidereal"),
            _ => throw new ArgumentException("Zodiactype unknown" + ": " + zType)
        };
    }

    /// <summary>Retrieve details for items in the enum ZodiacTypes.</summary>
    /// <returns>All details.</returns>
    public static List<ZodiacTypeDetails> AllDetails(this ZodiacTypes _)
    {
        return (from ZodiacTypes currentZodT in Enum.GetValues(typeof(ZodiacTypes)) select currentZodT.GetDetails()).ToList();
    }


    /// <summary>Find zodiac type for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The zodiac type for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ZodiacTypes ZodiacTypeForIndex(this ZodiacTypes _, int index)
    {
        foreach (ZodiacTypes currentZodT in Enum.GetValues(typeof(ZodiacTypes)))
        {
            if ((int)currentZodT == index) return currentZodT;
        }
        string errorText = "ZodiacTypes.ZodiacTypeForIndex():Could not find Zodiac Type for index : " + index;
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }
}


