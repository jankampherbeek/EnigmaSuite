// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Interfaces;
using System.Drawing;

namespace Enigma.Domain.Enums;

/// <summary>Supported zodiacal points.</summary>
public enum ZodiacalPoints
{
    ZeroAries = 0, ZeroCancer = 1
}


public static class ZodiacalPointsExtensions
{
    /// <summary>Retrieve details for zodiacal point.</summary>
    /// <param name="point">The zodiacal point, is automatically filled.</param>
    /// <returns>Details for the zodiacal point.</returns>
    public static ZodiacalPointDetails GetDetails(this ZodiacalPoints point)
    {
        return point switch
        {
            ZodiacalPoints.ZeroAries => new ZodiacalPointDetails(point, "ref.enum.zodiacalpoints.id.zeroar", "ref.enum.zodiacalpoints.idabbr.zeroar"),
            ZodiacalPoints.ZeroCancer => new ZodiacalPointDetails(point, "ref.enum.zodiacalpoints.id.zerocn", "ref.enum.zodiacalpoints.idabbr.zerocn"),
            _ => throw new ArgumentException("Zodiacal Point unknown : " + point.ToString())
        };
    }


    /// <summary>Find zodiacal point for an index.</summary>
    /// <param name="point">Any zodiacal point, filled automatically.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The zodiacal point for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ZodiacalPoints ZodiacalPointForIndex(this ZodiacalPoints point, int index)
    {
        foreach (ZodiacalPoints currentPoint in Enum.GetValues(typeof(ZodiacalPoints)))
            {
            if ((int)currentPoint == index) return currentPoint;
        }
        throw new ArgumentException("Could not find zodiacal point for index : " + index);
    }
	
}




/// <summary>Details for a Mundane Point.</summary>
/// <param name="ZodiacalPoint">The Zodiacal Point.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
/// <param name=TextIdAbbreviated">Abbreviated version for TextId.</param>
public record ZodiacalPointDetails(ZodiacalPoints ZodiacalPoint, string TextId, string TextIdAbbreviated);
