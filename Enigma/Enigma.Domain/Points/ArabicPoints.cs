// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.Points;

/// <summary>Supported Arabic Points (Hellenistic Lots).</summary>
public enum ArabicPoints
{
    None = -1,
    FortunaSect = 0,
    FortunaNoSect = 1
}


/// <summary>Details for an Arabic Point.</summary>
/// <remarks>Actually: Hellenistic Lots, but the name 'Arabic Points' is more common.</remarks>
/// <param name="ArabicPoint">The Arabic Point.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
/// <param name="DefaultGlyph">Character to show default glyph. Empty if no glyph is available.</param> 
public record ArabicPointDetails(ArabicPoints ArabicPoint, string TextId, string DefaultGlyph);


/// <summary>Extension class for enum ArabicPoints.</summary>
public static class ArabicPointsExtensions
{
    /// <summary>Retrieve details for an Arabic Point.</summary>
    /// <param name="point">The Arabic Point.</param>
    /// <returns>Details for the Arabic Point.</returns>
    public static ArabicPointDetails GetDetails(this ArabicPoints point)
    {
        return point switch
        {
            ArabicPoints.FortunaSect => new ArabicPointDetails(point, "ref.enum.arabicpoint.fortunasect", "e"),
            ArabicPoints.FortunaNoSect => new ArabicPointDetails(point, "ref.enum.arabicpoint.fortunanosect", "e"),
            _ => throw new ArgumentException("Arabic Point unknown : " + point.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum ArabicPoints.</summary>
    /// <param name="_">Any Arabic Point.</param>
    /// <returns>All details.</returns>
    public static List<ArabicPointDetails> AllDetails(this ArabicPoints _)
    {
        var allDetails = new List<ArabicPointDetails>();
        foreach (ArabicPoints currentPoint in Enum.GetValues(typeof(ArabicPoints)))
        {
            allDetails.Add(currentPoint.GetDetails());
        }
        return allDetails;
    }

    /// <summary>Find Arabic Point for an index.</summary>
    /// <param name="_">Any Arabic Point.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The Arabic Point for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ArabicPoints ArabicPointForIndex(this ArabicPoints _, int index)
    {
        foreach (ArabicPoints currentPoint in Enum.GetValues(typeof(ArabicPoints)))
        {
            if ((int)currentPoint == index) return currentPoint;
        }
        throw new ArgumentException("Could not find Arabic Point for index : " + index);
    }

}