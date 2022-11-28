// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Enums;

/// <summary>Supported zodiacal points.</summary>
public enum ZodiacalPoints
{
    ZeroAries = 0, ZeroCancer = 1
}

/// <summary>Details for a Mundane Point.</summary>
public record ZodiacalPointDetails
{
    readonly public ZodiacalPoints ZodiacalPoint;
    readonly public string TextId;
    readonly public string TextIdAbbreviated;

    /// <param name="zodiacalPoint">The Zodiacal Point.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    /// <param name=textIdAbbreviated">Abbreviated version for textId.</param>
    public ZodiacalPointDetails(ZodiacalPoints zodiacalPoint, string textId, string textIdAbbreviated)
    {
        ZodiacalPoint = zodiacalPoint;
        TextId = textId;
        TextIdAbbreviated = textIdAbbreviated;
    }
}



/// <inheritdoc/>
public class ZodiacalPointSpecifications : IZodiacalPointSpecifications
{
    /// <inheritdoc/>
    public ZodiacalPointDetails DetailsForPoint(ZodiacalPoints point)
    {
        return point switch
        {
            ZodiacalPoints.ZeroAries => new ZodiacalPointDetails(point, "ref.enum.zodiacalpoints.id.zeroar", "ref.enum.zodiacalpoints.idabbr.zeroar"),
            ZodiacalPoints.ZeroCancer => new ZodiacalPointDetails(point, "ref.enum.zodiacalpoints.id.zerocn", "ref.enum.zodiacalpoints.idabbr.zerocn"),
            _ => throw new ArgumentException("Zodiacal Point unknown : " + point.ToString())
        };

    }

    /// <inheritdoc/>
    public ZodiacalPointDetails DetailsForPoint(int pointId)
    {
        ZodiacalPoints point = (ZodiacalPoints)pointId;
        return DetailsForPoint(point);
    }


}