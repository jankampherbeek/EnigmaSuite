// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Enums;

/// <summary>Supported mundane.</summary>
public enum MundanePoints
{
    Ascendant = 0, Mc = 1, EastPoint = 2, Vertex = 3
}

public static class MundanePointsExtensions
{
    /// <summary>Retrieve details for MundanePoints.</summary>
    /// <param name="point">The mundane point, is automatically filled.</param>
    /// <returns>Details for the mundane point.</returns>
    public static MundanePointDetails GetDetails(this MundanePoints point)
    {
        return point switch
        {
            MundanePoints.Ascendant => new MundanePointDetails(point, "ref.enum.mundanepoint.id.asc", "ref.enum.mundanepoint.idabbr.asc"),
            MundanePoints.Mc => new MundanePointDetails(point, "ref.enum.mundanepoint.id.mc", "ref.enum.mundanepoint.idabbr.mc"),
            MundanePoints.EastPoint => new MundanePointDetails(point, "ref.enum.mundanepoint.id.eastpoint", "ref.enum.mundanepoint.idabbr.eastpoint"),
            MundanePoints.Vertex => new MundanePointDetails(point, "ref.enum.mundanepoint.id.vertex", "ref.enum.mundanepoint.idabbr.vertex"),
            _ => throw new ArgumentException("MundanePoint unknown : " + point.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum MundanePoints.</summary>
    /// <param name="point">Any mundane point, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<MundanePointDetails> AllDetails(this MundanePoints point)
    {
        var allDetails = new List<MundanePointDetails>();
        foreach (MundanePoints currentPoint in Enum.GetValues(typeof(MundanePoints)))
        {
            allDetails.Add(currentPoint.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find mundane point for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <param name="point">Any mundane point, is automatically filled.</param>
    /// <returns>The mundane point.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static MundanePoints MundanePointForIndex(this MundanePoints point, int index)
    {
        foreach (MundanePoints mPoint in Enum.GetValues(typeof(MundanePoints)))
        {
            if ((int)mPoint == index) return mPoint;
        }
        throw new ArgumentException("Could not find mundane point for index : " + index);
    }

}




/// <summary>Details for a Mundane Point.</summary>
/// <param name="MundanePoint">The Mundane Point.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
/// <param name="TextIdAbbreviated">Abbreviated version for TextId.</param>
public record MundanePointDetails(MundanePoints MundanePoint, string TextId, string TextIdAbbreviated);

