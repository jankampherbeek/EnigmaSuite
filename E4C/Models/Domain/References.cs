// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;

namespace E4C.Models.Domain
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

    #endregion

    #region Calendar
    /// <summary>
    /// Enum for Gregorian and Julian Calendar.
    /// </summary>
    public enum Calendars
    {
        Gregorian = 0, Julian = 1
    }

    /// <summary>
    /// Details for a calendar.
    /// </summary>
    public record CalendarDetails
    {
        readonly public Calendars Calendar;
        readonly public string TextId;

        /// <summary>
        /// Construct details for a calendar.
        /// </summary>
        /// <param name="calendar">The calendar.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public CalendarDetails(Calendars calendar, string textId)
        {
            Calendar = calendar;
            TextId = textId;
        }
    }

    /// <summary>
    /// Specifications for a calendar.
    /// </summary>
    public interface ICalendarSpecifications
    {
        /// <summary>
        /// Returns the details for a Calendar.
        /// </summary>
        /// <param name="calendar">The calendar, from the enum Calendars.</param>
        /// <returns>A record CalendarDetails with the specifications.</returns>
        public CalendarDetails DetailsForCalendar(Calendars calendar);

        /// <summary>
        /// Returns a value from the enum Calendars that corresponds with an index.
        /// </summary>
        /// <param name="calendarIndex">The index for the requested item from Calendars. 
        /// Throws an exception if no Calendar for the given index does exist.</param>
        /// <returns>Instance from enum Calendars that corresponds with the given index.</returns>
        public Calendars CalendarForIndex(int calendarIndex);
    }

    public class CalendarSpecifications : ICalendarSpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the calendar was not recognized.</exception>
        CalendarDetails ICalendarSpecifications.DetailsForCalendar(Calendars calendar)
        {
            return calendar switch
            {
                Calendars.Gregorian => new CalendarDetails(calendar, "ref.enum.calendar.gregorian"),
                Calendars.Julian => new CalendarDetails(calendar, "ref.enum.calendar.julian"),
                _ => throw new ArgumentException("Calendar unknown : " + calendar.ToString())
            };
        }

        public Calendars CalendarForIndex(int calendarIndex)
        {
            foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
            {
                if ((int)calendar == calendarIndex) return calendar;
            }
            throw new ArgumentException("Could not find Calendars for index : " + calendarIndex);
        }
    }
    #endregion

    #region YearCount
    /// <summary>
    /// Enum for Yearcounts.
    /// </summary>
    public enum YearCounts
    {
        CE = 0, BCE = 1, Astronomical = 2
    }

    /// <summary>
    /// Details for a yearcount.
    /// </summary>
    public record YearCountDetails
    {
        readonly public YearCounts YearCount;
        readonly public string TextId;

        /// <summary>
        /// Construct details for a YearCount.
        /// </summary>
        /// <param name="yearCount">The YearCount.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public YearCountDetails(YearCounts yearCount, string textId)
        {
            YearCount = yearCount;
            TextId = textId;
        }
    }

    /// <summary>
    /// Specifications for a YearCount.
    /// </summary>
    public interface IYearCountSpecifications
    {
        /// <summary>
        /// Returns the details for a YearCount.
        /// </summary>
        /// <param name="yearCount">The YearCount, from the enum YearCounts.</param>
        /// <returns>A record YearCountDetails with the specifications.</returns>
        public YearCountDetails DetailsForYearCount(YearCounts yearCount);

        /// <summary>
        /// Returns a value from the enum YearCounts that corresponds with an index.
        /// </summary>
        /// <param name="yearCountIndex">The index for the requested item from YearCounts. 
        /// Throws an exception if no YearCount for the given index does exist.</param>
        /// <returns>Instance from enum YearCounts that corresponds with the given index.</returns>
        public YearCounts YearCountForIndex(int yearCountIndex);
    }

    public class YearCountSpecifications : IYearCountSpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the calendar was not recognized.</exception>
        YearCountDetails IYearCountSpecifications.DetailsForYearCount(YearCounts yearCount)
        {
            return yearCount switch
            {
                YearCounts.CE => new YearCountDetails(yearCount, "ref.enum.yearcount.ce"),
                YearCounts.BCE => new YearCountDetails(yearCount, "ref.enum.yearcount.bce"),
                YearCounts.Astronomical => new YearCountDetails(yearCount, "ref.enum.yearcount.astronomical"),
                _ => throw new ArgumentException("YearCount unknown : " + yearCount.ToString())
            };
        }

        public YearCounts YearCountForIndex(int yearCountIndex)
        {
            foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
            {
                if ((int)yearCount == yearCountIndex) return yearCount;
            }
            throw new ArgumentException("Could not find YearCount for index : " + yearCountIndex);
        }
    }

    #endregion

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
        readonly public SolarSystemPoints SolarSystemPoint;
        readonly public SolSysPointCats SolSysPointCat;
        readonly public int SeId;
        readonly public bool UseForHeliocentric;
        readonly public bool UseForGeocentric;
        readonly public string TextId;

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
            SolarSystemPoint = solarSystemPoint;
            SolSysPointCat = solSysPointCat;
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
        readonly public CoordinateSystems CoordinateSystem;
        readonly public int ValueForFlag;
        readonly public string TextId;

        /// <summary>
        /// Constructor for the details of a coordinate system.
        /// </summary>
        /// <param name="system">The coordinate system.</param>
        /// <param name="valueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public CoordinateSystemDetails(CoordinateSystems system, int valueForFlag, string textId)
        {
            CoordinateSystem = system;
            ValueForFlag = valueForFlag;
            TextId = textId;
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
        readonly public ObserverPositions ObserverPosition;
        readonly public int ValueForFlag;
        readonly public string TextId;

        /// <summary>
        /// Constructor for the details of an observer position.
        /// </summary>
        /// <param name="position">The observer position.</param>
        /// <param name="valueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public ObserverPositionDetails(ObserverPositions position, int valueForFlag, string textId)
        {
            ObserverPosition = position;
            ValueForFlag = valueForFlag;
            TextId = textId;
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

    public class ObserverPositionSpecifications : IObserverPositionSpecifications
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
        readonly public ZodiacTypes ZodiacType;
        readonly public int ValueForFlag;
        readonly public string TextId;

        /// <summary>
        /// Constructor for the details of a zodiac type.
        /// </summary>
        /// <param name="type">The zodiac type.</param>
        /// <param name="valueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public ZodiacTypeDetails(ZodiacTypes type, int valueForFlag, string textId)
        {
            ZodiacType = type;
            ValueForFlag = valueForFlag;
            TextId = textId;
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

    public class ZodiacTypeSpecifications : IZodiacTypeSpecifications
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
        WholeSign, EqualAsc, EqualMc, EqualAries, Vehlow, Axial, Horizon, Carter, Gauquelin, SunShine, SunShineTreindl
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
        /// <param name="houseSystem">The house system.</param>
        /// <param name="seSupported">True if the house system is supported by the Swiss Ephyemeris.</param>
        /// <param name="seId">A character that identifies the house system for the Swiss Ephemeris. If SeSuported = false, SeId will have the value 0 and is ignored.</param>
        /// <param name="nrOfCusps">Number of cusps for this house system.</param>
        /// <param name="counterClockWise">True if the cusps are counterclockwise, otherwise false.</param>
        /// <param name="quadrantSystem">True if the system is a quadrant system (Asc. = cusp 1, MC = cusp 10).</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public HouseSystemDetails(HouseSystems houseSystem, bool seSupported, char seId, int nrOfCusps, bool counterClockWise, bool quadrantSystem, string textId)
        {
            HouseSystem = houseSystem;
            SeSupported = seSupported;
            SeId = seId;
            NrOfCusps = nrOfCusps;
            CounterClockWise = counterClockWise;
            QuadrantSystem = quadrantSystem;
            TextId = textId;
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
        /// <param name="houseSystem">The house system, from the enum HouseSystems.</param>
        /// <returns>A record HouseSystemDetails with the specification of the house system.</returns>
        public HouseSystemDetails DetailsForHouseSystem(HouseSystems houseSystem);
    }

    public class HouseSystemSpecifications : IHouseSystemSpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the house system was not recognized.</exception>
        HouseSystemDetails IHouseSystemSpecifications.DetailsForHouseSystem(HouseSystems houseSystem)
        {
            return houseSystem switch
            {
                HouseSystems.NoHouses => new HouseSystemDetails(houseSystem, true, 'W', 0, false, false, "houseSystemNoHouses"),
                HouseSystems.Placidus => new HouseSystemDetails(houseSystem, true, 'P', 12, true, true, "houseSystemPlacidus"),
                HouseSystems.Koch => new HouseSystemDetails(houseSystem, true, 'K', 12, true, true, "houseSystemKoch"),
                HouseSystems.Porphyri => new HouseSystemDetails(houseSystem, true, 'O', 12, true, true, "houseSystemPorphyri"),
                HouseSystems.Regiomontanus => new HouseSystemDetails(houseSystem, true, 'R', 12, true, true, "houseSystemRegiomontanus"),
                HouseSystems.Campanus => new HouseSystemDetails(houseSystem, true, 'C', 12, true, true, "houseSystemCampanus"),
                HouseSystems.Alcabitius => new HouseSystemDetails(houseSystem, true, 'B', 12, true, true, "houseSystemAlcabitius"),
                HouseSystems.TopoCentric => new HouseSystemDetails(houseSystem, true, 'T', 12, true, true, "houseSystemTopoCentric"),
                HouseSystems.Krusinski => new HouseSystemDetails(houseSystem, true, 'U', 12, true, true, "houseSystemKrusinski"),
                HouseSystems.Apc => new HouseSystemDetails(houseSystem, true, 'Y', 12, true, true, "houseSystemApc"),
                HouseSystems.Morin => new HouseSystemDetails(houseSystem, true, 'M', 12, true, false, "houseSystemMorin"),
                HouseSystems.WholeSign => new HouseSystemDetails(houseSystem, true, 'W', 12, true, false, "houseSystemWholeSign"),
                HouseSystems.EqualAsc => new HouseSystemDetails(houseSystem, true, 'A', 12, true, false, "houseSystemEqualAsc"),
                HouseSystems.EqualMc => new HouseSystemDetails(houseSystem, true, 'D', 12, true, false, "houseSystemEqualMc"),
                HouseSystems.EqualAries => new HouseSystemDetails(houseSystem, true, 'N', 12, true, false, "houseSystemEqualAries"),
                HouseSystems.Vehlow => new HouseSystemDetails(houseSystem, true, 'V', 12, true, false, "houseSystemVehlow"),
                HouseSystems.Axial => new HouseSystemDetails(houseSystem, true, 'X', 12, true, false, "houseSystemAxial"),
                HouseSystems.Horizon => new HouseSystemDetails(houseSystem, true, 'H', 12, true, false, "houseSystemHorizon"),
                HouseSystems.Carter => new HouseSystemDetails(houseSystem, true, 'F', 12, true, false, "houseSystemCarter"),
                HouseSystems.Gauquelin => new HouseSystemDetails(houseSystem, true, 'G', 36, true, false, "houseSystemGauquelin"),
                HouseSystems.SunShine => new HouseSystemDetails(houseSystem, true, 'i', 12, true, false, "houseSystemSunShine"),
                HouseSystems.SunShineTreindl => new HouseSystemDetails(houseSystem, true, 'I', 12, true, false, "houseSystemSunShineTreindl"),
                _ => throw new ArgumentException("House system type unknown : " + houseSystem.ToString())
            };
        }
    }

    #endregion

    #region Ayanamshas

    /// <summary>
    /// Supported ayanamshas.
    /// </summary>
    public enum Ayanamshas
    {
        Fagan, Lahiri, DeLuce, Raman, UshaShashi, Krishnamurti, DjwhalKhul, Yukteshwar, Bhasin, Kugler1, Kugler2, Kugler3, Huber, EtaPiscium, Aldebaran15Tau, Hipparchus, Sassanian,
        GalactCtr0Sag, J2000, J1900, B1950, SuryaSiddhanta, SuryaSiddhantaMeanSun, Aryabhata, AryabhataMeanSun, SsRevati, SsCitra, TrueCitra, TrueRevati, TruePushya, GalacticCtrBrand,
        GalacticEqIau1958, GalacticEq, GalacticEqMidMula, Skydram, TrueMula, Dhruva, Aryabhata522, Britton, GalacticCtrOCap
    }

    /// <summary>
    /// Specifications for an ayanamsha.
    /// </summary>
    public record AyanamshaDetails
    {
        readonly public Ayanamshas Ayanamsha;
        readonly public int SeId;
        readonly public string TextId;

        /// <summary>
        /// Constructor for the details of an ayanamsha.
        /// </summary>
        /// <param name="ayanamsha">The ayanamsha.</param>
        /// <param name="seId">Id that identifies the ayanamsha for the Swiss Ephemeris.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public AyanamshaDetails(Ayanamshas ayanamsha, int seId, string textId)
        {
            Ayanamsha = ayanamsha;
            SeId = seId;
            TextId = textId;
        }
    }

    /// <summary>
    /// Specifications for an ayanamsha.
    /// </summary>
    public interface IAyanamshaSpecifications
    {
        /// <summary>
        /// Returns the specification for an ayanamsha.
        /// </summary>
        /// <param name="ayanamsha">The ayanamsha, from the enum Ayanamshas.</param>
        /// <returns>A record AyanamshaDetails with the specification of the ayanamsha.</returns>
        public AyanamshaDetails DetailsForAyanamsha(Ayanamshas ayanamsha);
    }

    public class AyanamshaSpecifications : IAyanamshaSpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the ayanamsha was not recognized.</exception>
        AyanamshaDetails IAyanamshaSpecifications.DetailsForAyanamsha(Ayanamshas ayanamsha)
        {
            return ayanamsha switch
            {
                Ayanamshas.Fagan => new AyanamshaDetails(ayanamsha, 0, "ayanamshaFagan"),
                Ayanamshas.Lahiri => new AyanamshaDetails(ayanamsha, 1, "ayanamshaLahiri"),
                Ayanamshas.DeLuce => new AyanamshaDetails(ayanamsha, 2, "ayanamshaDeLuce"),
                Ayanamshas.Raman => new AyanamshaDetails(ayanamsha, 3, "ayanamshaRaman"),
                Ayanamshas.UshaShashi => new AyanamshaDetails(ayanamsha, 4, "ayanamshaUshaSashi"),
                Ayanamshas.Krishnamurti => new AyanamshaDetails(ayanamsha, 5, "ayanamshaKrishnamurti"),
                Ayanamshas.DjwhalKhul => new AyanamshaDetails(ayanamsha, 6, "ayanamshaDjwhalKhul"),
                Ayanamshas.Yukteshwar => new AyanamshaDetails(ayanamsha, 7, "ayanamshaYukteshwar"),
                Ayanamshas.Bhasin => new AyanamshaDetails(ayanamsha, 8, "ayanamshaBhasin"),
                Ayanamshas.Kugler1 => new AyanamshaDetails(ayanamsha, 9, "ayanamshaKugler1"),
                Ayanamshas.Kugler2 => new AyanamshaDetails(ayanamsha, 10, "ayanamshaKugler2"),
                Ayanamshas.Kugler3 => new AyanamshaDetails(ayanamsha, 11, "ayanamshaKugler3"),
                Ayanamshas.Huber => new AyanamshaDetails(ayanamsha, 12, "ayanamshaHuber"),
                Ayanamshas.EtaPiscium => new AyanamshaDetails(ayanamsha, 13, "ayanamshaEtaPiscium"),
                Ayanamshas.Aldebaran15Tau => new AyanamshaDetails(ayanamsha, 14, "ayanamshaAldebaran15Tau"),
                Ayanamshas.Hipparchus => new AyanamshaDetails(ayanamsha, 15, "ayanamshaHipparchus"),
                Ayanamshas.Sassanian => new AyanamshaDetails(ayanamsha, 16, "ayanamshaSassanian"),
                Ayanamshas.GalactCtr0Sag => new AyanamshaDetails(ayanamsha, 17, "ayanamshaGalactCtr0Sag"),
                Ayanamshas.J2000 => new AyanamshaDetails(ayanamsha, 18, "ayanamshaJ2000"),
                Ayanamshas.J1900 => new AyanamshaDetails(ayanamsha, 19, "ayanamshaJ1900"),
                Ayanamshas.B1950 => new AyanamshaDetails(ayanamsha, 20, "ayanamshaB1950"),
                Ayanamshas.SuryaSiddhanta => new AyanamshaDetails(ayanamsha, 21, "ayanamshaSuryaSiddhanta"),
                Ayanamshas.SuryaSiddhantaMeanSun => new AyanamshaDetails(ayanamsha, 22, "ayanamshaSuryaSiddhantaMeanSun"),
                Ayanamshas.Aryabhata => new AyanamshaDetails(ayanamsha, 23, "ayanamshaAryabhata"),
                Ayanamshas.AryabhataMeanSun => new AyanamshaDetails(ayanamsha, 24, "ayanamshaAryabhataMeanSun"),
                Ayanamshas.SsRevati => new AyanamshaDetails(ayanamsha, 25, "ayanamshaSsRevati"),
                Ayanamshas.SsCitra => new AyanamshaDetails(ayanamsha, 26, "ayanamshaSsCitra"),
                Ayanamshas.TrueCitra => new AyanamshaDetails(ayanamsha, 27, "ayanamshaTrueCitra"),
                Ayanamshas.TrueRevati => new AyanamshaDetails(ayanamsha, 28, "ayanamshaTrueRevati"),
                Ayanamshas.TruePushya => new AyanamshaDetails(ayanamsha, 29, "ayanamshaTruePushya"),
                Ayanamshas.GalacticCtrBrand => new AyanamshaDetails(ayanamsha, 30, "ayanamshaGalacticCtrBrand"),
                Ayanamshas.GalacticEqIau1958 => new AyanamshaDetails(ayanamsha, 31, "ayanamshaGalacticCtrEqIau1958"),
                Ayanamshas.GalacticEq => new AyanamshaDetails(ayanamsha, 32, "ayanamshaGalacticEq"),
                Ayanamshas.GalacticEqMidMula => new AyanamshaDetails(ayanamsha, 33, "ayanamshaGalacticEqMidMula"),
                Ayanamshas.Skydram => new AyanamshaDetails(ayanamsha, 34, "ayanamshaSkydram"),
                Ayanamshas.TrueMula => new AyanamshaDetails(ayanamsha, 35, "ayanamshaTrueMula"),
                Ayanamshas.Dhruva => new AyanamshaDetails(ayanamsha, 36, "ayanamshaDhruva"),
                Ayanamshas.Aryabhata522 => new AyanamshaDetails(ayanamsha, 37, "ayanamshaAryabhata522"),
                Ayanamshas.Britton => new AyanamshaDetails(ayanamsha, 38, "ayanamshaBritton"),
                Ayanamshas.GalacticCtrOCap => new AyanamshaDetails(ayanamsha, 39, "ayanamshaGalacticCtr0Cap"),
                _ => throw new ArgumentException("Ayanamsha unknown : " + ayanamsha.ToString())
            };
        }
    }

    #endregion

    #region ChartCategories
    /// <summary>
    /// Types of charts. Describes the application of a chart.
    /// </summary>
    public enum ChartCategories
    {
        Unknown = 0, Female = 1, Male = 2, Event = 3, Horary = 4, Election = 5 
    }

    /// <summary>
    /// Details for the Category of a chart.
    /// </summary>
    public record ChartCategoryDetails
    {
        readonly public ChartCategories Category;
        readonly public string TextId;

        /// <summary>
        /// Construct details for a Chart category.
        /// </summary>
        /// <param name="category">The category of the chart.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public ChartCategoryDetails(ChartCategories category, string textId)
        {
            Category = category;
            TextId = textId;
        }
    }

    /// <summary>
    /// Specifications for a Chart category.
    /// </summary>
    public interface IChartCategorySpecifications
    {
        /// <summary>
        /// Returns the details for a Chart category.
        /// </summary>
        /// <param name="category">The category, from the enum ChartCategories.</param>
        /// <returns>A record ChartCategoryDetails with the specifications.</returns>
        public ChartCategoryDetails DetailsForCategory(ChartCategories category);

        /// <summary>
        /// Returns a value from the enum ChartCategories that corresponds with an index.
        /// </summary>
        /// <param name="chartCategoryIndex">The index for the requested item from ChartCategories. 
        /// Throws an exception if no ChartCategories for the given index does exist.</param>
        /// <returns>Instance from enum ChartCategories that corresponds with the given index.</returns>
        public ChartCategories ChartCategoryForIndex(int chartCategoryIndex);
    
    }


    public class ChartCategorySpecifications : IChartCategorySpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the category was not recognized.</exception>
        public ChartCategoryDetails DetailsForCategory(ChartCategories category)
        {
            return category switch
            {
                ChartCategories.Female => new ChartCategoryDetails(category, "ref.enum.chartcategories.female"),
                ChartCategories.Male => new ChartCategoryDetails(category, "ref.enum.chartcategories.male"),
                ChartCategories.Event => new ChartCategoryDetails(category, "ref.enum.chartcategories.event"),
                ChartCategories.Horary => new ChartCategoryDetails(category, "ref.enum.chartcategories.horary"),
                ChartCategories.Election => new ChartCategoryDetails(category, "ref.enum.chartcategories.election"),
                ChartCategories.Unknown => new ChartCategoryDetails(category, "ref.enum.chartcategories.unknown"),
                _ => throw new ArgumentException("SolSysPointCats unknown : " + category.ToString())
            };
        }

        public ChartCategories ChartCategoryForIndex(int chartCategoryIndex)
        {
            foreach (ChartCategories category in Enum.GetValues(typeof(ChartCategories)))
            {
                if ((int)category == chartCategoryIndex) return category;
            }
            throw new ArgumentException("Could not find ChartCategories for index : " + chartCategoryIndex);
        }

    }

    #endregion

    #region TimeZones
    /// <summary>
    /// Timezones.
    /// </summary>
    public enum TimeZones
    {
        UT = 0, CET = 1, EET = 2, EAT = 3, IRST = 4, AMT = 5, AFT = 6, PKT = 7, IST = 8, IOT = 9, MMT = 10, ICT = 11, WST = 12, JST = 13, ACST = 14, AEST = 15, LHST = 16,
        NCT = 17, NZST = 18, SST = 19, HAST = 20, MART = 21, AKST = 22, PST = 23, MST = 24, CST = 25, EST = 26, AST = 27, NST = 28, BRT = 29, GST = 30, AZOT = 31, LMT = 32
    }

    /// <summary>
    /// Details for a Timezone.
    /// </summary>
    public record TimeZoneDetails
    {
        readonly public TimeZones TimeZone;
        readonly public double OffsetFromUt;
        readonly public string TextId;

        /// <summary>
        /// Construct details for a Time Zone.
        /// </summary>
        /// <param name="timeZone">The TimeZone.</param>
        /// <param name="offsetFromUt">The difference with Universal Time.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public TimeZoneDetails(TimeZones timeZone, double offsetFromUt, string textId)
        {
            TimeZone = timeZone;
            OffsetFromUt = offsetFromUt;
            TextId = textId;
        }
    }

    /// <summary>
    /// Specifications for a Time Zone.
    /// </summary>
    public interface ITimeZoneSpecifications
    {
        /// <summary>
        /// Returns the details for a Time Zone.
        /// </summary>
        /// <param name="timeZone">The Time Zone, from the enum Timezones.</param>
        /// <returns>A record TimeZoneDetails with the specifications.</returns>
        public TimeZoneDetails DetailsForTimeZone(TimeZones timeZone);

        /// <summary>
        /// Returns a value from the enum TimeZones that corresponds with an index.
        /// </summary>
        /// <param name="timeZoneIndex">The index for the requested item from TimeZones. 
        /// Throws an exception if no TimeZone for the given index does exist.</param>
        /// <returns>Instance from enum TimeZones that corresponds with the given index.</returns>
        public TimeZones TimeZoneForIndex(int yearCountIndex);
    }

    public class TimeZoneSpecifications : ITimeZoneSpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the Time Zone was not recognized.</exception>
        public TimeZoneDetails DetailsForTimeZone(TimeZones timeZone)
        {
            return timeZone switch
            {
                TimeZones.UT => new TimeZoneDetails(timeZone, 0.0, "ref.enum.timezone.ut"),
                TimeZones.CET => new TimeZoneDetails(timeZone, 1.0, "ref.enum.timezone.cet"),
                TimeZones.EET => new TimeZoneDetails(timeZone, 2.0, "ref.enum.timezone.eet"),
                TimeZones.EAT => new TimeZoneDetails(timeZone, 3.0, "ref.enum.timezone.eat"),
                TimeZones.IRST => new TimeZoneDetails(timeZone, 3.5, "ref.enum.timezone.irst"),
                TimeZones.AMT => new TimeZoneDetails(timeZone, 4.0, "ref.enum.timezone.amt"),
                TimeZones.AFT => new TimeZoneDetails(timeZone, 4.5, "ref.enum.timezone.aft"),
                TimeZones.PKT => new TimeZoneDetails(timeZone, 5.0, "ref.enum.timezone.pkt"),
                TimeZones.IST => new TimeZoneDetails(timeZone, 5.5, "ref.enum.timezone.ist"),
                TimeZones.IOT => new TimeZoneDetails(timeZone, 6.0, "ref.enum.timezone.iot"),
                TimeZones.MMT => new TimeZoneDetails(timeZone, 6.5, "ref.enum.timezone.mmt"),
                TimeZones.ICT => new TimeZoneDetails(timeZone, 7.0, "ref.enum.timezone.ict"),
                TimeZones.WST => new TimeZoneDetails(timeZone, 8.0, "ref.enum.timezone.wst"),
                TimeZones.JST => new TimeZoneDetails(timeZone, 9.0, "ref.enum.timezone.jst"),
                TimeZones.ACST => new TimeZoneDetails(timeZone, 9.5, "ref.enum.timezone.acst"),
                TimeZones.AEST => new TimeZoneDetails(timeZone, 10.0, "ref.enum.timezone.aest"),
                TimeZones.LHST => new TimeZoneDetails(timeZone, 10.5, "ref.enum.timezone.lhst"),
                TimeZones.NCT => new TimeZoneDetails(timeZone, 11.0, "ref.enum.timezone.nct"),
                TimeZones.NZST => new TimeZoneDetails(timeZone, 12.0, "ref.enum.timezone.nzst"),
                TimeZones.SST => new TimeZoneDetails(timeZone, -11.0, "ref.enum.timezone.sst"),
                TimeZones.HAST => new TimeZoneDetails(timeZone, -10.0, "ref.enum.timezone.hast"),
                TimeZones.MART => new TimeZoneDetails(timeZone, -9.5, "ref.enum.timezone.mart"),
                TimeZones.AKST => new TimeZoneDetails(timeZone, -9.0, "ref.enum.timezone.akst"),
                TimeZones.PST => new TimeZoneDetails(timeZone, -8.0, "ref.enum.timezone.pst"),
                TimeZones.MST => new TimeZoneDetails(timeZone, -7.0, "ref.enum.timezone.mst"),
                TimeZones.CST => new TimeZoneDetails(timeZone, -6.0, "ref.enum.timezone.cst"),
                TimeZones.EST => new TimeZoneDetails(timeZone, -5.0, "ref.enum.timezone.est"),
                TimeZones.AST => new TimeZoneDetails(timeZone, -4.0, "ref.enum.timezone.ast"),
                TimeZones.NST => new TimeZoneDetails(timeZone, -3.5, "ref.enum.timezone.nst"),
                TimeZones.BRT => new TimeZoneDetails(timeZone, -3.0, "ref.enum.timezone.brt"),
                TimeZones.GST => new TimeZoneDetails(timeZone, -2.0, "ref.enum.timezone.gst"),
                TimeZones.AZOT => new TimeZoneDetails(timeZone, -1.0, "ref.enum.timezone.azot"),
                TimeZones.LMT => new TimeZoneDetails(timeZone, 0.0, "ref.enum.timezone.lmt"),
                _ => throw new ArgumentException("TimeZones : " + timeZone.ToString())
            };
        }

        public TimeZones TimeZoneForIndex(int timeZoneIndex)
        {
            foreach (TimeZones timeZone in Enum.GetValues(typeof(TimeZones)))
            {
                if ((int)timeZone == timeZoneIndex) return timeZone;
            }
            throw new ArgumentException("Could not find TimeZone for index : " + timeZoneIndex);
        }
    }
    #endregion

    #region RoddenRating
    /// <summary>
    /// Ratings according to Rodden and an added 'Undefined' rating.
    /// </summary>
    /// <remarks>
    /// Suports:
    /// AA - Accurate.As recorded by family or state
    /// A - Quoted by the person, kin, friend, or associate
    /// B - Biography or autobiography
    /// C - Caution, no source
    /// DD - Dirty data.Conflicting quotes that are unqualified
    /// X - No time of birth
    /// XX - No date of birth
    /// Also added 'Unknown' which is not an original Rodden Rating.
    /// </remarks>
    public enum RoddenRatings
    {
        Unknown = 0, AA = 1, A = 2, B = 3, C = 4, DD = 5, X = 6, XX = 7
    }

    /// <summary>
    /// Details for the Category of a chart.
    /// </summary>
    public record RoddenRatingDetails
    {
        readonly public RoddenRatings Rating;
        readonly public string TextId;

        /// <summary>
        /// Construct details for a Rodden rating.
        /// </summary>
        /// <param name="rating">The standard acronym for the Rodden rating.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public RoddenRatingDetails(RoddenRatings rating, string textId)
        {
            Rating = rating;
            TextId = textId;
        }
    }

    /// <summary>
    /// Specifications for a Rodden rating.
    /// </summary>
    public interface IRoddenRatingSpecifications
    {
        /// <summary>
        /// Returns the details for a Rodden rating.
        /// </summary>
        /// <param name="rating">The Rodden rating, from the enum RoddenRatings.</param>
        /// <returns>A record RoddenRatingDetails with the specifications.</returns>
        public RoddenRatingDetails DetailsForRating(RoddenRatings rating);

        /// <summary>
        /// Returns a value from the enum RoddenRatings that corresponds with an index.
        /// </summary>
        /// <param name="roddenRatingIndex">The index for the requested item from RoddenRatings. 
        /// Throws an exception if no RoddenRatings for the given index does exist.</param>
        /// <returns>Instance from enum RoddenRatings that corresponds with the given index.</returns>
        public RoddenRatings RoddenRatingForIndex(int roddenRatingIndex);
    }


    public class RoddenRatingSpecifications : IRoddenRatingSpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the rating was not recognized.</exception>
        public RoddenRatingDetails DetailsForRating(RoddenRatings rating)
        {
            return rating switch
            {
                RoddenRatings.Unknown => new RoddenRatingDetails(rating, "ref.enum.roddenrating.unknown"),
                RoddenRatings.AA => new RoddenRatingDetails(rating, "ref.enum.roddenrating.aa"),
                RoddenRatings.A => new RoddenRatingDetails(rating, "ref.enum.roddenrating.a"),
                RoddenRatings.B => new RoddenRatingDetails(rating, "ref.enum.roddenrating.b"),
                RoddenRatings.C => new RoddenRatingDetails(rating, "ref.enum.roddenrating.c"),
                RoddenRatings.DD => new RoddenRatingDetails(rating, "ref.enum.roddenrating.dd"),
                RoddenRatings.X => new RoddenRatingDetails(rating, "ref.enum.roddenrating.x"),
                RoddenRatings.XX => new RoddenRatingDetails(rating, "ref.enum.roddenrating.xx"),
                _ => throw new ArgumentException("RoddenRatings unknown : " + rating.ToString())
            };
        }

        public RoddenRatings RoddenRatingForIndex(int roddenRatingIndex)
        {
            foreach (RoddenRatings rating in Enum.GetValues(typeof(RoddenRatings)))
            {
                if ((int)rating == roddenRatingIndex) return rating;
            }
            throw new ArgumentException("Could not find RoddenRatings for index : " + roddenRatingIndex);
        }
    }

    #endregion

    #region Location related
    public enum Directions4GeoLong
    {
        East = 1, West = -1
    }

    public enum Directions4GeoLat
    {
        North = 1, South = -1
    }

    #endregion

    #region ProjectionTypes

    /// <summary>
    /// Projection of the positions of celestial bodies.
    /// </summary>
    /// <remarks>
    /// twodimension: the default type of projection to a 2-dimensional chart.
    /// obliqueLongitude: a correction for the mundane position, also called 'true location', as used by the School of Ram.
    /// </remarks>
    public enum ProjectionTypes
    {
        twoDimensional = 0, obliqueLongitude = 1
    }

    /// <summary>
    /// Details for a Projection Type.
    /// </summary>
    public record ProjectionTypeDetails
    {
        readonly public ProjectionTypes ProjectionType; 
        readonly public string TextId;

        /// <summary>
        /// Construct details for a Projection Type
        /// </summary>
        /// <param name="projectionType">Instance of the enum ProjectionTypes.</param>
        /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
        public ProjectionTypeDetails(ProjectionTypes projectionType, string textId)
        {
            ProjectionType = projectionType;
            TextId = textId;
        }
    }

    /// <summary>
    /// Specification for a Projection Type
    /// </summary>
    public interface IProjectionTypeDetailSpecifications
    {
        /// <summary>
        /// Returns the details for a Projection Type.
        /// </summary>
        /// <param name="projectionType">Instance from the enum ProjectionTypes.</param>
        /// <returns>A record PRojectionTypeDetails with the specifications.</returns>
        public ProjectionTypeDetails DetailsForProjectionType(ProjectionTypes projectionType);

        /// <summary>
        /// Returns a value from the enum ProjectionTypes that corresponds with an index.
        /// </summary>
        /// <param name="projectionTypeIndex">The index for the requested item from ProjectionTypes. 
        /// Throws an exception if no ProjectionTypes for the given index does exist.</param>
        /// <returns>Instance from enum ProjectionTypes that corresponds with the given index.</returns>
        public ProjectionTypes ProjectionTypeForIndex(int projectionTypeIndex);
    }

    public class ProjectionTypeDetailSpecifications : IProjectionTypeDetailSpecifications
    {
        /// <exception cref="ArgumentException">Is thrown if the projection type was not recognized.</exception>
        public ProjectionTypeDetails DetailsForProjectionType(ProjectionTypes projectionType)
        {
            return projectionType switch
            {
                ProjectionTypes.twoDimensional => new ProjectionTypeDetails(projectionType, ""),
                ProjectionTypes.obliqueLongitude => new ProjectionTypeDetails(projectionType, ""),
                _ => throw new ArgumentException("ProjectionType unknown : " + projectionType.ToString())
            };
        }

        public ProjectionTypes ProjectionTypeForIndex(int projectionTypeIndex)
        {
            foreach (ProjectionTypes projectionType in Enum.GetValues(typeof(ProjectionTypes)))
            {
                if ((int)projectionType == projectionTypeIndex) return projectionType;
            }
            throw new ArgumentException("Could not find ProjectionTypes for index : " + projectionTypeIndex);
        }
    }


    #endregion
}