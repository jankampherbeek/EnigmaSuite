// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using System;

namespace E4C.domain.shared.references;

/// <summary>
/// Different methods to calculate a solar system point.
/// </summary>
/// <remarks>
/// SE: Calculation via the Swiss Ephemeris.
/// Elements: Calculation based on ecliptical elements.
/// Numeric: Calculation based on simple numerics, mostly addition of a constant value. 
/// </remarks>
public enum CalculationTypes
{
    SE, Elements, Numeric
}

/// <summary>
/// Types of points in the solar system.
/// </summary>
/// <remarks>
/// CLASSIC: Sun, Moon and visible planets, 
/// MODERN: Uranus, Neptune, Pluto, 
/// MATHPOINT: mathematical points like the lunare node, 
/// MINOR: Plutoids (except Pluto), planetoids, centaurs, 
/// HYPOTHETICAL: hypothetical bodies and points.
/// </remarks>
public enum SolSysPointCats
{
    Classic, Modern, MathPoint, Minor, Hypothetical
}

/// <summary>
/// Details for the Category of a Solar System Point.
/// </summary>
public record SolSysPointCatDetails
{
    readonly public SolSysPointCats Category;
    readonly public string TextId;

    /// <summary>
    /// Construct details for a Solar System Point.
    /// </summary>
    /// <param name="category">The category of the Solar System Point.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public SolSysPointCatDetails(SolSysPointCats category, string textId)
    {
        Category = category;
        TextId = textId;
    }
}

/// <summary>
/// Specifications for a Solar Systempoint Category.
/// </summary>
public interface ISolSysPointCatSpecifications
{
    /// <summary>
    /// Returns the details for a Solar System Point Category.
    /// </summary>
    /// <param name="category">The category, from the enum SolSysPointCats.</param>
    /// <returns>A record SolSysPointCatDetails with the specifications.</returns>
    public SolSysPointCatDetails DetailsForCategory(SolSysPointCats category);
}


public class SolSysPointCatSpecifications : ISolSysPointCatSpecifications
{
    /// <exception cref="ArgumentException">Is thrown if the category was not recognized.</exception>
    public SolSysPointCatDetails DetailsForCategory(SolSysPointCats category)
    {
        return category switch
        {
            SolSysPointCats.Classic => new SolSysPointCatDetails(category, "enumSolSysPointCatClassic"),
            SolSysPointCats.Modern => new SolSysPointCatDetails(category, "enumSolSysPointCatModern"),
            SolSysPointCats.MathPoint => new SolSysPointCatDetails(category, "enumSolSysPointCatMathPoint"),
            SolSysPointCats.Minor => new SolSysPointCatDetails(category, "enumSolSysPointCatMinor"),
            SolSysPointCats.Hypothetical => new SolSysPointCatDetails(category, "enumSolSysPointCatHypothetical"),
            _ => throw new ArgumentException("SolSysPointCats unknown : " + category.ToString())
        };
    }
}

/// <summary>
/// Supported points in the Solar System (Planets, lights, Plutoids etc.).
/// </summary>
public enum SolarSystemPoints
{
    Sun, Moon, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto, MeanNode, TrueNode, Chiron, PersephoneRam, HermesRam, DemeterRam,
    CupidoUra, HadesUra, ZeusUra, KronosUra, ApollonUra, AdmetosUra, VulcanusUra, PoseidonUra, Eris
}

/// <summary>
/// Details for a Solar System Point.
/// </summary>
public record SolarSystemPointDetails
{
    readonly public SolarSystemPoints SolarSystemPoint;
    readonly public SolSysPointCats SolSysPointCat;
    readonly public CalculationTypes CalculationType;
    readonly public int SeId;
    readonly public bool UseForHeliocentric;
    readonly public bool UseForGeocentric;
    readonly public string TextId;

    /// <summary>
    /// Constructor for the details of a Solar System Point.
    /// </summary>
    /// <param name="solarSystemPoint">The Solar System Point.</param>
    /// <param name="solSysPointCat">The category for the Solar System Point.</param>
    /// <param name="calculationType">The type of calculation to be performed.</param>
    /// <param name="seId">The id as used by the Swiss Ephemeris.</param>
    /// <param name="useForHeliocentric">True if a heliocentric position can be calculated.</param>
    /// <param name="useForGeocentric">True if a geocentric position can be calculated.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public SolarSystemPointDetails(SolarSystemPoints solarSystemPoint, SolSysPointCats solSysPointCat, CalculationTypes calculationType, int seId, bool useForHeliocentric, bool useForGeocentric, string textId)
    {
        SolarSystemPoint = solarSystemPoint;
        SolSysPointCat = solSysPointCat;
        CalculationType = calculationType;
        SeId = seId;
        UseForHeliocentric = useForHeliocentric;
        UseForGeocentric = useForGeocentric;
        TextId = textId;
    }
}

/// <summary>
/// Specifications for a Solar System Point.
/// </summary>
public interface ISolarSystemPointSpecifications
{
    /// <summary>
    /// Returns the specifications for a Solar System Point.
    /// </summary>
    /// <param name="point">The solar system point for which to find the details.</param>
    /// <returns>A record SolSysPointCatDetails with the specifications.</returns>
    public SolarSystemPointDetails DetailsForPoint(SolarSystemPoints point);
}

public class SolarSystemPointSpecifications : ISolarSystemPointSpecifications
{
    /// <exception cref="ArgumentException">Is thrown if the Solar System Point was not recognized.</exception>
    SolarSystemPointDetails ISolarSystemPointSpecifications.DetailsForPoint(SolarSystemPoints point)
    {
        return point switch
        {
            SolarSystemPoints.Sun => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, Constants.SE_SUN, false, true, "sun"),
            SolarSystemPoints.Moon => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, Constants.SE_MOON, false, true, "moon"),
            SolarSystemPoints.Mercury => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, Constants.SE_MERCURY, true, true, "mercury"),
            SolarSystemPoints.Venus => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, Constants.SE_VENUS, true, true, "venus"),
            SolarSystemPoints.Earth => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, Constants.SE_EARTH, true, false, "earth"),
            SolarSystemPoints.Mars => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, Constants.SE_MARS, true, true, "mars"),
            SolarSystemPoints.Jupiter => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, Constants.SE_JUPITER, true, true, "jupiter"),
            SolarSystemPoints.Saturn => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, Constants.SE_SATURN, true, true, "saturn"),
            SolarSystemPoints.Uranus => new SolarSystemPointDetails(point, SolSysPointCats.Modern, CalculationTypes.SE, Constants.SE_URANUS, true, true, "uranus"),
            SolarSystemPoints.Neptune => new SolarSystemPointDetails(point, SolSysPointCats.Modern, CalculationTypes.SE, Constants.SE_NEPTUNE, true, true, "neptune"),
            SolarSystemPoints.Pluto => new SolarSystemPointDetails(point, SolSysPointCats.Modern, CalculationTypes.SE, Constants.SE_PLUTO, true, true, "pluto"),
            SolarSystemPoints.MeanNode => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, CalculationTypes.SE, Constants.SE_MEAN_NODE, false, true, "meanNode"),
            SolarSystemPoints.TrueNode => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, CalculationTypes.SE, Constants.SE_TRUE_NODE, false, true, "trueNode"),
            SolarSystemPoints.Chiron => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, Constants.SE_CHIRON, true, true, "chiron"),
            SolarSystemPoints.PersephoneRam => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.Elements, Constants.SE_PERSEPHONE_RAM, true, true, "persephone_ram"),
            SolarSystemPoints.HermesRam => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.Elements, Constants.SE_HERMES_RAM, true, true, "hermes_ram"),
            SolarSystemPoints.DemeterRam => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.Elements, Constants.SE_DEMETER_RAM, true, true, "demeter_ram"),
            SolarSystemPoints.CupidoUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, Constants.SE_CUPIDO_URA, true, true, "cupido_ura"),
            SolarSystemPoints.HadesUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, Constants.SE_HADES_URA, true, true, "hades_ura"),
            SolarSystemPoints.ZeusUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, Constants.SE_ZEUS_URA, true, true, "zeus_ura"),
            SolarSystemPoints.KronosUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, Constants.SE_KRONOS_URA, true, true, "kronos_ura"),
            SolarSystemPoints.ApollonUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, Constants.SE_APOLLON_URA, true, true, "apollon_ura"),
            SolarSystemPoints.AdmetosUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, Constants.SE_ADMETOS_URA, true, true, "admetos_ura"),
            SolarSystemPoints.VulcanusUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, Constants.SE_VULCANUS_URA, true, true, "vulcanus_ura"),
            SolarSystemPoints.PoseidonUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, Constants.SE_POSEIDON_URA, true, true, "poseidon_ura"),
            SolarSystemPoints.Eris => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, Constants.SE_ERIS, true, true, "eris"),
            _ => throw new ArgumentException("SolarSystemPoint unknown : " + point.ToString())
        };
    }
}