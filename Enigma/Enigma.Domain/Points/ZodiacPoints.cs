// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.Points;

/// <summary>Supported Zodiac Points.</summary>
public enum ZodiacPoints
{
    None = -1,
    ZeroAries = 0,
    ZeroCancer = 1
}


/// <summary>Details for a zodiacal point.</summary>
/// <param name="ZodiacPoint">The Arabic Point.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
/// <param name="TextAbbrId">Id to find an abbreviated descriptive text in a resource bundle.</param>
/// <param name="DefaultGlyph">Character to show default glyph. Empty if no glyph is available.</param> 
public record ZodiacPointDetails(ZodiacPoints ZodiacPoint, string TextId, string TextabbrId, string DefaultGlyph);


/// <summary>Extension class for enum ZodiacPoints.</summary>
public static class ZodiacPointsExtensions
{
    /// <summary>Retrieve details for a Zodiac Point.</summary>
    /// <param name="point">The Zodiac Point.</param>
    /// <returns>Details for the Zodiac Point.</returns>
    public static ZodiacPointDetails GetDetails(this ZodiacPoints point)
    {
        return point switch
        {
            ZodiacPoints.ZeroAries => new ZodiacPointDetails(point, "ref.enum.zodiacpoints.id.zeroar", "ref.enum.zodiacpoints.idabbr.zeroar", "1"),
            ZodiacPoints.ZeroCancer => new ZodiacPointDetails(point, "ref.enum.zodiacpoints.id.zerocn", "ref.enum.zodiacpoints.idabbr.zerocn", "4"),
            _ => throw new ArgumentException("Zodiac Point unknown : " + point.ToString())
        }; 
    }

    /// <summary>Retrieve details for items in the enum ZodiacPoints.</summary>
    /// <param name="_">Any Zodiac Point.</param>
    /// <returns>All details.</returns>
    public static List<ZodiacPointDetails> AllDetails(this ZodiacPoints _)
    {
        var allDetails = new List<ZodiacPointDetails>();
        foreach (ZodiacPoints currentPoint in Enum.GetValues(typeof(ZodiacPoints)))
        {
            allDetails.Add(currentPoint.GetDetails());
        }
        return allDetails;
    }

    /// <summary>Find Zodiac Point for an index.</summary>
    /// <param name="_">Any Zodiac Point.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The Zodiac Point for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ZodiacPoints ZodiacPointForIndex(this ZodiacPoints _, int index)
    {
        foreach (ZodiacPoints currentPoint in Enum.GetValues(typeof(ZodiacPoints)))
        {
            if ((int)currentPoint == index) return currentPoint;
        }
        throw new ArgumentException("Could not find Zodiac Point for index : " + index);
    }

}


