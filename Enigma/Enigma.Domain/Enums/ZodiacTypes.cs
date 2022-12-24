// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;

namespace Enigma.Domain.Enums;

/// <summary>Zodiac types, e.g. sidereal or tropical.</summary>
public enum ZodiacTypes
{
    Sidereal = 0, Tropical = 1
}


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
            ZodiacTypes.Tropical => new ZodiacTypeDetails(zType, 0, "ref.enum.zodiactype.tropical"),
            ZodiacTypes.Sidereal => new ZodiacTypeDetails(zType, EnigmaConstants.SEFLG_SIDEREAL, "ref.enum.zodiactype.sidereal"),
            _ => throw new ArgumentException("Zodiactype unknown" + ": " + zType.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum ZodiacTypes.</summary>
    /// <param name="zodiacType">The zodiac type, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<ZodiacTypeDetails> AllDetails(this ZodiacTypes zodiacType)
    {
        var allDetails = new List<ZodiacTypeDetails>();
        foreach (ZodiacTypes currentZodT in Enum.GetValues(typeof(ZodiacTypes)))
        {
            allDetails.Add(currentZodT.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find zodiac type for an index.</summary>
    /// <param name="zodiacType">Any zodiac type, is automatically filled.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The zodiac type for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ZodiacTypes ZodiacTypeForIndex(this ZodiacTypes zodiacType, int index)
    {
        foreach (ZodiacTypes currentZodT in Enum.GetValues(typeof(ZodiacTypes)))
        {
            if ((int)currentZodT == index) return currentZodT;
        }
        throw new ArgumentException("Could not find Zodiac Type for index : " + index);
    }
}


/// <summary>Details for a zodiac Type.</summary>
/// <param name="Type">The zodiac Type.</param>
/// <param name="ValueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record ZodiacTypeDetails(ZodiacTypes Type, int ValueForFlag, string TextId);