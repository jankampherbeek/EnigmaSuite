// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Enums;


/// <summary>Types of celestial points.</summary>
/// <remarks>
/// Classic: Sun, Moon and visible planets, 
/// Modern: Uranus, Neptune, Pluto, 
/// Mathpoint: mathematical points like the lunare node, 
/// Minor: Plutoids (except Pluto), planetoids, centaurs, 
/// Hypothetical: hypothetical bodies and points.
/// </remarks>
public enum CelPointCats
{
    Classic = 0, Modern = 1, MathPoint = 2, Minor = 3, Hypothetical = 4
}

public static class CelPointCatsExtensions
{
    /// <summary>Retrieve details for celestial point category.</summary>
    /// <param name="cat">The celestial point category, is automatically filled.</param>
    /// <returns>Details for the celestial point category.</returns>
    public static CelPointCatDetails GetDetails(this CelPointCats cat)
    {
        return cat switch
        {
            CelPointCats.Classic => new CelPointCatDetails(cat, "enumCelPointCatClassic"),
            CelPointCats.Modern => new CelPointCatDetails(cat, "enumCelPointCatModern"),
            CelPointCats.MathPoint => new CelPointCatDetails(cat, "enumCelPointCatMathPoint"),
            CelPointCats.Minor => new CelPointCatDetails(cat, "enumCelPointCatMinor"),
            CelPointCats.Hypothetical => new CelPointCatDetails(cat, "enumCelPointCatHypothetical"),
            _ => throw new ArgumentException("CelPointCats unknown : " + cat.ToString())
        };
    }


    /// <summary>Retrieve details for items in the enum CelPointCats.</summary>
    /// <param name="cat">The celestial point category, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<CelPointCatDetails> AllDetails(this CelPointCats cat)
    {
        var allDetails = new List<CelPointCatDetails>();
        foreach (CelPointCats currentCat in Enum.GetValues(typeof(CelPointCats)))
        {
            allDetails.Add(currentCat.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find celestial point category for an index.</summary>
    /// <param name="cat">Any celestial point category, automatically filled.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The celestial point category for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static CelPointCats CelPointCatForIndex(this CelPointCats cat, int index)
    {
        foreach (CelPointCats currentCat in Enum.GetValues(typeof(CelPointCats)))
        {
            if ((int)currentCat == index) return currentCat;
        }
        throw new ArgumentException("Could not find celestial point category for index : " + index);
    }
}

    /// <summary>Details for the Category of a celestial point.</summary>
    /// <param name="category">The category of the celestial point.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public record CelPointCatDetails(CelPointCats Category, string TextId); 