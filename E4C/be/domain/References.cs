// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;

namespace E4C.be.domain
{
    #region SolSysPointCategories
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
        readonly public SolSysPointCats category;
        readonly public string textId;

        /// <summary>
        /// Construct details for a Solar System Point.
        /// </summary>
        /// <param name="category">The category of the Solar System Point.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public SolSysPointCatDetails(SolSysPointCats category, string textId)
        {
            this.category = category;
            this.textId = textId;
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

    #endregion

    /// <summary>
    /// Enum for Gregorian and Julian Calendar.
    /// </summary>
    public enum Calendars
    {
        Gregorian, Julian
    }

    #region SolarSystemPoints
    /// <summary>
    /// Supported points in the Solar System (Planets, lights, Plutoids etc.).
    /// </summary>
    public enum SolarSystemPoints
    {
        Sun, Moon, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto, MeanNode, TrueNode, Chiron
    }

    /// <summary>
    /// Details for a Solar System Point.
    /// </summary>
    public record SolarSystemPointDetails
    {
        readonly public SolarSystemPoints solarSystemPoint;
        readonly public SolSysPointCats solSysPointCat;
        readonly public int seId;
        readonly public bool useForHeliocentric;
        readonly public bool useForGeocentric;
        readonly public string textId;

        /// <summary>
        /// Constructor for the details of a Solar System Point.
        /// </summary>
        /// <param name="solarSystemPoint">The Solar System Point.</param>
        /// <param name="solSysPointCat">The category for the Solar System Point.</param>
        /// <param name="seId">The id as used by the Swiss Ephemeris.</param>
        /// <param name="useForHeliocentric">True if a heliocentric position can be calculated.</param>
        /// <param name="useForGeocentric">True if a geocentric position can be calculated.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public SolarSystemPointDetails(SolarSystemPoints solarSystemPoint, SolSysPointCats solSysPointCat, int seId, bool useForHeliocentric, bool useForGeocentric, string textId)
        {
            this.solarSystemPoint = solarSystemPoint;
            this.solSysPointCat = solSysPointCat;
            this.seId = seId;
            this.useForHeliocentric = useForHeliocentric;
            this.useForGeocentric = useForGeocentric;
            this.textId = textId;
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
        /// <param name="category">The category, from the enum SolSysPointCats.</param>
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
                SolarSystemPoints.Sun => new SolarSystemPointDetails(point, SolSysPointCats.Classic, Constants.SE_SUN, false, true, "solSysPointSun"),
                SolarSystemPoints.Moon => new SolarSystemPointDetails(point, SolSysPointCats.Classic, Constants.SE_SUN, false, true, "solSysPointMoon"),
                SolarSystemPoints.Mercury => new SolarSystemPointDetails(point, SolSysPointCats.Classic, Constants.SE_MERCURY, true, true, "solSysPointMercury"),
                SolarSystemPoints.Venus => new SolarSystemPointDetails(point, SolSysPointCats.Classic, Constants.SE_VENUS, true, true, "solSysPointVenus"),
                SolarSystemPoints.Earth => new SolarSystemPointDetails(point, SolSysPointCats.Classic, Constants.SE_EARTH, true, false, "solSysPointEarth"),
                SolarSystemPoints.Mars => new SolarSystemPointDetails(point, SolSysPointCats.Classic, Constants.SE_MARS, true, true, "solSysPointMars"),
                SolarSystemPoints.Jupiter => new SolarSystemPointDetails(point, SolSysPointCats.Classic, Constants.SE_JUPITER, true, true, "solSysPointJupiter"),
                SolarSystemPoints.Saturn => new SolarSystemPointDetails(point, SolSysPointCats.Classic, Constants.SE_SATURN, true, true, "solSysPointSaturn"),
                SolarSystemPoints.Uranus => new SolarSystemPointDetails(point, SolSysPointCats.Modern, Constants.SE_URANUS, true, true, "solSysPointUranus"),
                SolarSystemPoints.Neptune => new SolarSystemPointDetails(point, SolSysPointCats.Modern, Constants.SE_NEPTUNE, true, true, "solSysPointNeptune"),
                SolarSystemPoints.Pluto => new SolarSystemPointDetails(point, SolSysPointCats.Modern, Constants.SE_PLUTO, true, true, "solSysPointPluto"),
                SolarSystemPoints.MeanNode => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, Constants.SE_MEAN_NODE, false, true, "solSysPointMeanNode"),
                SolarSystemPoints.TrueNode => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, Constants.SE_TRUE_NODE, false, true, "solSysPointTrueNode"),
                SolarSystemPoints.Chiron => new SolarSystemPointDetails(point, SolSysPointCats.Minor, Constants.SE_CHIRON, true, true, "solSysPointChiron"),
                _ => throw new ArgumentException("SolarSystemPoint unknown : " + point.ToString())
            };
        }
    }
    #endregion

    #region CoordinateSystems
    /// <summary>
    /// Coordinate systems, used to define a position.
    /// </summary>
    public enum CoordinateSystems
    {
        Ecliptical, Equatorial, Horizontal
    }

    /// <summary>
    /// Details for a coordinate system.
    /// </summary>
    public record CoordinateSystemDetails
    {
        readonly public CoordinateSystems coordinateSystem;
        readonly public int valueForFlag;
        readonly public string textId;

        /// <summary>
        /// Constructor for the details of a coordinate system.
        /// </summary>
        /// <param name="system">The coordinate system.</param>
        /// <param name="valueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public CoordinateSystemDetails(CoordinateSystems system, int valueForFlag, string textId)
        {
            coordinateSystem = system;
            this.valueForFlag = valueForFlag;
            this.textId = textId;
        }
    }

    /// <summary>
    /// Specifications for a coordinate system.
    /// </summary>
    public interface ICoordinateSystemSpecifications
    {
        /// <summary>
        /// Returns the specifications for a Coordinate System.
        /// </summary>
        /// <param name="coordinateSystem">The coordinate system, from the enum CoordinateSystems.</param>
        /// <returns>A record CoordinateSystemDetails with the specifications of the coordinate system.</returns>
        public CoordinateSystemDetails DetailsForCoordinateSystem(CoordinateSystems coordinateSystem);
    }

    public class CoordinateSystemSpecifications : ICoordinateSystemSpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the Coordinate System was not recognized.</exception>
        CoordinateSystemDetails ICoordinateSystemSpecifications.DetailsForCoordinateSystem(CoordinateSystems coordinateSystem)
        {
            return coordinateSystem switch
            {
                // No specific flags for ecliptical and horizontal.
                CoordinateSystems.Ecliptical => new CoordinateSystemDetails(coordinateSystem, 0, "coordinateSysEcliptic"),
                CoordinateSystems.Equatorial => new CoordinateSystemDetails(coordinateSystem, Constants.SEFLG_EQUATORIAL, "coordinateSysEquatorial"),
                CoordinateSystems.Horizontal => new CoordinateSystemDetails(coordinateSystem, 0, "coordinateSysHorizontal"),
                _ => throw new ArgumentException("Coordinate system unknown : " + coordinateSystem.ToString())
            };
        }
    }

    #endregion

    #region ObserverPositions
    /// <summary>
    /// Observer positions, the center points for the calculation of positions of celestial bodies.
    /// </summary>
    public enum ObserverPositions
    {
        HelioCentric, GeoCentric, TopoCentric
    }

    /// <summary>
    /// Details for an observer position.
    /// </summary>
    public record ObserverPositionDetails
    {
        readonly public ObserverPositions observerPosition;
        readonly public int valueForFlag;
        readonly public string textId;

        /// <summary>
        /// Constructor for the details of an observer position.
        /// </summary>
        /// <param name="position">The observer position.</param>
        /// <param name="valueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public ObserverPositionDetails(ObserverPositions position, int valueForFlag, string textId)
        {
            observerPosition = position;
            this.valueForFlag = valueForFlag;
            this.textId = textId;
        }
    }

    /// <summary>
    /// Specifications for an observer position.
    /// </summary>
    public interface IObserverPositionSpecifications
    {
        /// <summary>
        /// Returns the specification for an observer position.
        /// </summary>
        /// <param name="observerPosition">The observer positions, from the enum ObserverPositions.</param>
        /// <returns>A record ObserverPositionDetails with the specification of the coordinate system.</returns>
        public ObserverPositionDetails DetailsForObserverPosition(ObserverPositions observerPosition);
    }

    public class ObserverPositionSpecifications: IObserverPositionSpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the Observer Position was not recognized.</exception>
        ObserverPositionDetails IObserverPositionSpecifications.DetailsForObserverPosition(ObserverPositions observerPosition)
        {
            return observerPosition switch
            {
                // No specific flags for geocentric.
                ObserverPositions.HelioCentric => new ObserverPositionDetails(observerPosition, Constants.SEFLG_HELCTR, "observerPosHelioCentric"),
                ObserverPositions.GeoCentric => new ObserverPositionDetails(observerPosition, 0, "observerPosGeoCentric"),
                ObserverPositions.TopoCentric => new ObserverPositionDetails(observerPosition, Constants.SEFLG_TOPOCTR, "observerPosTopoCentric"),
                _ => throw new ArgumentException("Observer position unknown : " + observerPosition.ToString())
            };
        }

    }
    #endregion

    #region ZodiacTypes
    /// <summary>
    /// Zodiac types, e.g. sidereal or tropical.
    /// </summary>
    public enum ZodiacTypes
    {
        Sidereal, Tropical
    }

    /// <summary>
    /// Details for a zodiac type.
    /// </summary>
    public record ZodiacTypeDetails
    {
        readonly public ZodiacTypes zodiacType;
        readonly public int valueForFlag;
        readonly public string textId;

        /// <summary>
        /// Constructor for the details of a zodiac type.
        /// </summary>
        /// <param name="type">The zodiac type.</param>
        /// <param name="valueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public ZodiacTypeDetails(ZodiacTypes type, int valueForFlag, string textId)
        {
            zodiacType = type;
            this.valueForFlag = valueForFlag;
            this.textId = textId;
        }
    }

    /// <summary>
    /// Specifications for a zodiac type.
    /// </summary>
    public interface IZodiacTypeSpecifications
    {
        /// <summary>
        /// Returns the specification for a zodiac type.
        /// </summary>
        /// <param name="zodiacType">The zodiac type, from the enum ZodiacTypes.</param>
        /// <returns>A record ZodiacTypeDetails with the specification of the zodiac type.</returns>
        public ZodiacTypeDetails DetailsForZodiacType(ZodiacTypes zodiacType);
    }

    public class ZodiacTypeSpecifications: IZodiacTypeSpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the zodiac type was not recognized.</exception>
        ZodiacTypeDetails IZodiacTypeSpecifications.DetailsForZodiacType(ZodiacTypes zodiacType)
        {
            return zodiacType switch
            {
                // No specific flag for tropical.
                ZodiacTypes.Tropical => new ZodiacTypeDetails(zodiacType, 0, "zodiacTypeTropical"),
                ZodiacTypes.Sidereal => new ZodiacTypeDetails(zodiacType, Constants.SEFLG_SIDEREAL, "zodiacTypeSidereal"),
                _ => throw new ArgumentException("Zodiac type unknown : " + zodiacType.ToString())
            };
        }
    }
    #endregion

    #region HouseSystems

    /// <summary>
    /// Supported house systems.
    /// </summary>
    public enum HouseSystems
    {
        NoHouses, Placidus, Koch, Porphyri, Regiomontanus, Campanus, Alcabitius, TopoCentric, Krusinski, Apc, Morin, 
        WholeSign, EqualAsc, EqualMc, EqualAries, Vehlow,  Axial, Horizon, Carter,  Gauquelin, SunShine, SunShineTreindl
    }

    /// <summary>
    /// Specifications for a house system.
    /// </summary>
    public record HouseSystemDetails
    {
        readonly public HouseSystems HouseSystem;
        readonly public bool SeSupported;
        readonly public char SeId;
        readonly public int NrOfCusps;
        readonly public bool CounterClockWise;
        readonly public bool QuadrantSystem;
        readonly public string TextId;

        /// <summary>
        /// Constructor for the details of a house system.
        /// </summary>
        /// <param name="HouseSystem">The house system.</param>
        /// <param name="SeSupported">True if the house system is supported by the Swiss Ephyemeris.</param>
        /// <param name="SeId">A character that defines the house system for the Swiss Ephemeris. If SeSuported = false, SeId will have the value 0 and is ignored.</param>
        /// <param name="NrOfCusps">Number of cusps for this house system.</param>
        /// <param name="CounterClockWise">True if the cusps are counterclockwise, otherwise false.</param>
        /// <param name="QuadrantSystem">True if the system is a quadrant system (Asc. = cusp 1, MC = cusp 10).</param>
        /// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
        public HouseSystemDetails(HouseSystems HouseSystem, bool SeSupported, char SeId, int NrOfCusps, bool CounterClockWise, bool QuadrantSystem, string TextId)
        {
            this.HouseSystem = HouseSystem; 
            this.SeSupported = SeSupported;
            this.SeId = SeId;
            this.NrOfCusps = NrOfCusps;
            this.CounterClockWise = CounterClockWise;
            this.QuadrantSystem = QuadrantSystem;
            this.TextId = TextId;
        }
    }

    /// <summary>
    /// Specifications for a house system.
    /// </summary>
    public interface IHouseSystemSpecifications
    {
        /// <summary>
        /// Returns the specification for a house system.
        /// </summary>
        /// <param name="HouseSystem">The house system, from the enum HouseSystems.</param>
        /// <returns>A record HouseSystemDetails with the specification of the house system.</returns>
        public HouseSystemDetails DetailsForHouseSystem(HouseSystems HouseSystem);
    }

    public class HouseSystemSpecifications: IHouseSystemSpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the house system was not recognized.</exception>
        HouseSystemDetails IHouseSystemSpecifications.DetailsForHouseSystem(HouseSystems HouseSystem)
        {
            return HouseSystem switch
            {
                HouseSystems.NoHouses => new HouseSystemDetails(HouseSystem, true, 'W', 0, false, false, "houseSystemNoHouses"),
                HouseSystems.Placidus => new HouseSystemDetails(HouseSystem, true, 'P', 12, true, true, "houseSystemPlacidus"),
                HouseSystems.Koch => new HouseSystemDetails(HouseSystem, true, 'K', 12, true, true, "houseSystemKoch"),
                HouseSystems.Porphyri => new HouseSystemDetails(HouseSystem, true, 'O', 12, true, true, "houseSystemPorphyri"),
                HouseSystems.Regiomontanus => new HouseSystemDetails(HouseSystem, true, 'R', 12, true, true, "houseSystemRegiomontanus"),
                HouseSystems.Campanus => new HouseSystemDetails(HouseSystem, true, 'C', 12, true, true, "houseSystemCampanus"),
                HouseSystems.Alcabitius => new HouseSystemDetails(HouseSystem, true, 'B', 12, true, true, "houseSystemAlcabitius"),
                HouseSystems.TopoCentric => new HouseSystemDetails(HouseSystem, true, 'T', 12, true, true, "houseSystemTopoCentric"),
                HouseSystems.Krusinski => new HouseSystemDetails(HouseSystem, true, 'U', 12, true, true, "houseSystemKrusinski"),
                HouseSystems.Apc => new HouseSystemDetails(HouseSystem, true, 'Y', 12, true, true, "houseSystemApc"),
                HouseSystems.Morin => new HouseSystemDetails(HouseSystem, true, 'M', 12, true, false, "houseSystemMorin"),
                HouseSystems.WholeSign => new HouseSystemDetails(HouseSystem, true, 'W', 12, true, false, "houseSystemWholeSign"),
                HouseSystems.EqualAsc => new HouseSystemDetails(HouseSystem, true, 'A', 12, true, false, "houseSystemEqualAsc"),
                HouseSystems.EqualMc => new HouseSystemDetails(HouseSystem, true, 'D', 12, true, false, "houseSystemEqualMc"),
                HouseSystems.EqualAries => new HouseSystemDetails(HouseSystem, true, 'N', 12, true, false, "houseSystemEqualAries"),
                HouseSystems.Vehlow => new HouseSystemDetails(HouseSystem, true, 'V', 12, true, false, "houseSystemVehlow"),
                HouseSystems.Axial => new HouseSystemDetails(HouseSystem, true, 'X', 12, true, false, "houseSystemAxial"),
                HouseSystems.Horizon => new HouseSystemDetails(HouseSystem, true, 'H', 12, true, false, "houseSystemHorizon"),
                HouseSystems.Carter => new HouseSystemDetails(HouseSystem, true, 'F', 12, true, false, "houseSystemCarter"),
                HouseSystems.Gauquelin => new HouseSystemDetails(HouseSystem, true, 'G', 36, true, false, "houseSystemGauquelin"),
                HouseSystems.SunShine => new HouseSystemDetails(HouseSystem, true, 'i', 12, true, false, "houseSystemSunShine"),
                HouseSystems.SunShineTreindl => new HouseSystemDetails(HouseSystem, true, 'I', 12, true, false, "houseSystemSunShineTreindl"),
                _ => throw new ArgumentException("House system type unknown : " + HouseSystem.ToString())
            };
        }
    }

    #endregion
}