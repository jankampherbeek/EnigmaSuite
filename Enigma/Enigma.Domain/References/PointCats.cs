// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;


/// <summary>Categories of points.</summary>
/// <remarks>
/// Categorization of any point that can be shown in a chart. 
/// Common: planets and comprable points (lights, nodes, plutoids, asteroids, hypothetical planets etc.
/// Angle: specific mundane points like Mc, Ascendant and Vertex.
/// Cusp: housecusps.
/// Zodiac: specific zodiac points like Zero Aries.
/// Lots: Lots points.
/// FixStar: stars and comparable objects, like nebulae.
/// </remarks>
public enum PointCats
{
    Common = 0, Angle = 1, Cusp = 2, Zodiac = 3, Lots = 4, FixStar = 5
}


/// <summary>Details for the category of a point.</summary>
/// <param name="Category">The category of the point.</param>
/// <param name="RbKey">Key to descriptive text in resource bundle.</param>
public record PointCatDetails(PointCats Category, string RbKey);



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
            PointCats.Common => new PointCatDetails(cat, "ref.pointcat.common"),
            PointCats.Angle => new PointCatDetails(cat, "ref.pointcat.angle"),
            PointCats.Cusp => new PointCatDetails(cat, "ref.pointcat.cusp"),
            PointCats.Zodiac => new PointCatDetails(cat, "ref.pointcat.zodiac"),
            PointCats.Lots => new PointCatDetails(cat, "ref.pointcat.lots"),
            PointCats.FixStar => new PointCatDetails(cat, "ref.pointcat.fixstar"),
            _ => throw new ArgumentException("PointCat unknown : " + cat)
        };
    }

    
    /// <summary>Retrieve details for items in the enum PointCats.</summary>
    /// <returns>All details.</returns>
    public static List<PointCatDetails> AllDetails()
    {
        return (from PointCats currentCat in Enum.GetValues(typeof(PointCats)) 
            select currentCat.GetDetails()).ToList();
    }


    /// <summary>Find point category for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The point category for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static PointCats PointCatForIndex(int index)
    {
        foreach (PointCats currentCat in Enum.GetValues(typeof(PointCats)))
        {
            if ((int)currentCat == index) return currentCat;
        }
        Log.Error("PointCats.PointCatForIndex(): Could not find point category for index : {Index}", index);
        throw new ArgumentException("Wrong index for PointCats");
    }

}

