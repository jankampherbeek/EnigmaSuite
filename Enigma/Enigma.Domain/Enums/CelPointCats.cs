// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Enums;


/// <summary>Types of celestial points.</summary>
/// <remarks>
/// CLASSIC: Sun, Moon and visible planets, 
/// MODERN: Uranus, Neptune, Pluto, 
/// MATHPOINT: mathematical points like the lunare node, 
/// MINOR: Plutoids (except Pluto), planetoids, centaurs, 
/// HYPOTHETICAL: hypothetical bodies and points.
/// </remarks>
public enum CelPointCats
{
    Classic, Modern, MathPoint, Minor, Hypothetical
}

/// <summary>Details for the Category of a celestial point.</summary>
public record CelPointCatDetails
{
    readonly public CelPointCats Category;
    readonly public string TextId;

    /// <param name="category">The category of the celestial point.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public CelPointCatDetails(CelPointCats category, string textId)
    {
        Category = category;
        TextId = textId;
    }
}


/// <inheritdoc/>
public class CelPointCatSpecifications : ICelPointCatSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the category was not recognized.</exception>
    public CelPointCatDetails DetailsForCategory(CelPointCats category)
    {
        return category switch
        {
            CelPointCats.Classic => new CelPointCatDetails(category, "enumCelPointCatClassic"),
            CelPointCats.Modern => new CelPointCatDetails(category, "enumCelPointCatModern"),
            CelPointCats.MathPoint => new CelPointCatDetails(category, "enumCelPointCatMathPoint"),
            CelPointCats.Minor => new CelPointCatDetails(category, "enumCelPointCatMinor"),
            CelPointCats.Hypothetical => new CelPointCatDetails(category, "enumCelPointCatHypothetical"),
            _ => throw new ArgumentException("CelPointCats unknown : " + category.ToString())
        };
    }
}


