// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Points;


/// <summary>Categories of points.</summary>
/// <remarks>
/// Categorization of any point that can be shown in a chart. 
/// Common: planets and comprable points (lights, nodes, plutoids, asteroids, hypothetical planets etc.
/// Angle: specific mundane points like Mc, Ascendant and Vertex.
/// Cusp: housecusps.
/// Zodiac: specific zodiac points like Zero Aries.
/// Arabic: Arabic points.
/// FixStar: stars and comparable objects, like nebulae.
/// </remarks>
public enum PointCats
{
    None = -1, Common = 0, Angle = 1, Cusp = 2, Zodiac = 3, Arabic = 4, FixStar = 5
}


/// <summary>Details for the category of a point.</summary>
/// <param name="Category">The category of the point.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record PointCatDetails(PointCats Category, string TextId);



/// <summary>Extension class for the enum PointCats.</summary>
public static class PointCatsExtensions
{
    /// <summary>Retrieve details for point category.</summary>
    /// <param name="cat">The point category.</param>
    /// <returns>Details for the point category.</returns>
    public static PointCatDetails GetDetails(this PointCats cat)
    {
        return cat switch
        {
            PointCats.Common => new PointCatDetails(cat, "ref.enum.pointcats.common"),
            PointCats.Angle => new PointCatDetails(cat, "ref.enum.pointcats.angle"),
            PointCats.Cusp => new PointCatDetails(cat, "ref.enum.pointcats.cusp"),
            PointCats.Zodiac => new PointCatDetails(cat, "ref.enum.pointcats.zodiac"),
            PointCats.Arabic => new PointCatDetails(cat, "ref.enum.pointcats.arabic"),
            PointCats.FixStar => new PointCatDetails(cat, "ref.enum.pointcats.fixstar"),
            _ => throw new ArgumentException("PointCat unknown : " + cat.ToString())
        };
    }


    /// <summary>Retrieve details for items in the enum PointCats.</summary>
    /// <returns>All details.</returns>
    public static List<PointCatDetails> AllDetails(this PointCats _)
    {
        var allDetails = new List<PointCatDetails>();
        foreach (PointCats currentCat in Enum.GetValues(typeof(PointCats)))
        {
            if (currentCat != PointCats.None)
            {
                allDetails.Add(currentCat.GetDetails());
            }
        }
        return allDetails;
    }


    /// <summary>Find point category for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The point category for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static PointCats PointCatForIndex(this PointCats _, int index)
    {
        foreach (PointCats currentCat in Enum.GetValues(typeof(PointCats)))
        {
            if ((int)currentCat == index) return currentCat;
        }
        string errorText = "PointCats.PointCatForInedex(): Could not find point category for index : " + index;
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }

}

