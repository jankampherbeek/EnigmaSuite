// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using domain.shared;
using System;

namespace E4C.Shared.References;


/// <summary>Zodiac types, e.g. sidereal or tropical.</summary>
public enum ZodiacTypes
{
    Sidereal, Tropical
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
}

/// <inheritdoc/>
public class ZodiacTypeSpecifications : IZodiacTypeSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the zodiac type was not recognized.</exception>
    ZodiacTypeDetails IZodiacTypeSpecifications.DetailsForZodiacType(ZodiacTypes zodiacType)
    {
        return zodiacType switch
        {
            // No specific flag for tropical.
            ZodiacTypes.Tropical => new ZodiacTypeDetails(zodiacType, 0, "zodiacTypeTropical"),
            ZodiacTypes.Sidereal => new ZodiacTypeDetails(zodiacType, Constants.SEFLG_SIDEREAL, "zodiacTypeSidereal"),
            _ => throw new ArgumentException("Zodiac type unknown : " + zodiacType.ToString())
        };
    }
}
