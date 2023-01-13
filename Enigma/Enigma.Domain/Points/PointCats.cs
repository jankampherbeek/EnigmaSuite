// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Points;


/// <summary>Categories of points.</summary>
/// <remarks>
/// Categorization of any point that can be shown in a chart. 
/// Classic: Sun, Moon and visible planets,.
/// Modern: Uranus, Neptune, Pluto.
/// Mathpoint: mathematical points like the lunar node. 
/// Minor: Plutoids (except Pluto), planetoids, centaurs. 
/// Hypothetical: hypothetical bodies and points.
/// Mundane: specific mundane points like Mc, Ascendant and vertex.
/// Cusp: housecusps.
/// Zodiac: specific zodiac points like Zero Aries.
/// Arabic: Arabic points.
/// </remarks>
public enum PointCats
{
    None = -1, Classic = 0, Modern = 1, MathPoint = 2, Minor = 3, Hypothetical = 4, Mundane = 5, Cusp = 6, Zodiac = 7, Arabic = 8
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
            PointCats.Classic => new PointCatDetails(cat, "ref.enum.pointcats.classic"),
            PointCats.Modern => new PointCatDetails(cat, "ref.enum.pointcats.modern"),
            PointCats.MathPoint => new PointCatDetails(cat, "ref.enum.pointcats.math"),
            PointCats.Minor => new PointCatDetails(cat, "ref.enum.pointcats.minor"),
            PointCats.Hypothetical => new PointCatDetails(cat, "ref.enum.pointcats.hypothetical"),
            PointCats.Mundane => new PointCatDetails(cat, "ref.enum.pointcats.mundane"),
            PointCats.Cusp => new PointCatDetails(cat, "ref.enum.pointcats.cusp"),
            PointCats.Zodiac => new PointCatDetails(cat, "ref.enum.pointcats.zodiac"),
            PointCats.Arabic => new PointCatDetails(cat, "ref.enum.pointcats.arabic"),
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

