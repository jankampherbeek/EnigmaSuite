// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Constants;

namespace Enigma.Domain.CalcVars;

/// <summary>Zodiac types, e.g. sidereal or tropical.</summary>
public enum ZodiacTypes
{
    Sidereal = 0, Tropical = 1
}

/// <summary>Details for a zodiac type.</summary>
public record ZodiacTypeDetails
{
    readonly public ZodiacTypes ZodiacType;
    readonly public int ValueForFlag;
    readonly public string TextId;

    /// <param name="type">The zodiac type.</param>
    /// <param name="valueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public ZodiacTypeDetails(ZodiacTypes type, int valueForFlag, string textId)
    {
        ZodiacType = type;
        ValueForFlag = valueForFlag;
        TextId = textId;
    }
}

/// <summary>Specifications for a zodiac type.</summary>
public interface IZodiacTypeSpecifications
{
    /// <summary>Returns the specification for a zodiac type.</summary>
    /// <param name="zodiacType">The zodiac type, from the enum ZodiacTypes.</param>
    /// <returns>A record ZodiacTypeDetails with the specification of the zodiac type.</returns>
    public ZodiacTypeDetails DetailsForZodiacType(ZodiacTypes zodiacType);
    public List<ZodiacTypeDetails> AllZodiacTypeDetails();
    public ZodiacTypes ZodiacTypeForIndex(int index);

}

/// <inheritdoc/>
public class ZodiacTypeSpecifications : IZodiacTypeSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the zodiac type was not recognized.</exception>
    public ZodiacTypeDetails DetailsForZodiacType(ZodiacTypes zodiacType) => zodiacType switch
    {
        // No specific flag for tropical.
        ZodiacTypes.Tropical => new ZodiacTypeDetails(zodiacType, 0, "ref.enum.zodiactype.tropical"),
        ZodiacTypes.Sidereal => new ZodiacTypeDetails(zodiacType, EnigmaConstants.SEFLG_SIDEREAL, "ref.enum.zodiactype.sidereal"),
        _ => throw new ArgumentException("Zodiac type unknown : " + zodiacType.ToString())
    };

    public List<ZodiacTypeDetails> AllZodiacTypeDetails()
    {
        var allDetails = new List<ZodiacTypeDetails>();
        foreach (ZodiacTypes zodiacType in Enum.GetValues(typeof(ZodiacTypes)))
        {
            allDetails.Add(DetailsForZodiacType(zodiacType));
        }
        return allDetails;
    }

    public ZodiacTypes ZodiacTypeForIndex(int index)
    {
        foreach (ZodiacTypes zodiacType in Enum.GetValues(typeof(ZodiacTypes)))
        {
            if ((int)zodiacType == index) return zodiacType;
        }
        throw new ArgumentException("Could not find Zodiac Type for index : " + index);
    }
}
