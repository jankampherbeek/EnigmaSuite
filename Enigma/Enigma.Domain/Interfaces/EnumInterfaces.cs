// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;

namespace Enigma.Domain.Interfaces;

/// <summary>Specifications for a calendar.</summary>
public interface ICalendarSpecifications
{
    /// <param name="calendar">The calendar, from the enum Calendars.</param>
    /// <returns>A record CalendarDetails with the specifications.</returns>
    public CalendarDetails DetailsForCalendar(Calendars calendar);

    /// <returns>Calendardetails for all items in the enum Calendars.</returns>
    public List<CalendarDetails> AllCalendarDetails();

    /// <param name="calendarIndex">The index for the requested item from Calendars. 
    /// Throws an exception if no Calendar for the given index does exist.</param>
    /// <returns>Instance from enum Calendars that corresponds with the given index.</returns>
    public Calendars CalendarForIndex(int calendarIndex);
}

/// <summary>
/// Specifications for a Time Zone.
/// </summary>
public interface ITimeZoneSpecifications
{
    /// <param name="timeZone">The Time Zone, from the enum Timezones.</param>
    /// <returns>A record TimeZoneDetails with the specifications.</returns>
    public TimeZoneDetails DetailsForTimeZone(TimeZones timeZone);

    /// <returns>Details for all items in the enum TimeZones.</returns>
    public List<TimeZoneDetails> AllTimeZoneDetails();


    /// <param name="timeZoneIndex">The index for the requested item from TimeZones. 
    /// Throws an exception if no TimeZone for the given index does exist.</param>
    /// <returns>Instance from enum TimeZones that corresponds with the given index.</returns>
    public TimeZones TimeZoneForIndex(int timeZoneIndex);
}



public interface IYearCountSpecifications
{

    /// <param name="yearCount">The YearCount, from the enum YearCounts.</param>
    /// <returns>A record YearCountDetails with the specifications.</returns>
    public YearCountDetails DetailsForYearCount(YearCounts yearCount);

    ///<returns>Details for all items in enum YearCounts.</returns>
    public List<YearCountDetails> AllDetailsForYearCounts();

    /// <summary>Returns a value from the enum YearCounts that corresponds with an index.</summary>
    /// <param name="yearCountIndex">The index for the requested item from YearCounts. 
    /// Throws an exception if no YearCount for the given index does exist.</param>
    /// <returns>Instance from enum YearCounts that corresponds with the given index.</returns>
    public YearCounts YearCountForIndex(int yearCountIndex);
}

/// <summary>
/// Specifications for an aspect.
/// </summary>
public interface IAspectSpecifications
{
    /// <summary>
    /// Defines the specifications for an aspect.
    /// </summary>
    /// <param name="aspect">The aspect for which the details are defined.</param>
    /// <returns>A record AspectDetails with the required information.</returns>
    public AspectDetails DetailsForAspect(AspectTypes aspect);
}

/// <summary>Specifications for an orb method.</summary>
public interface IOrbMethodSpecifications
{
    /// <summary>Returns the specification for an orb method.</summary>
    /// <param name="orbMethod">The orb method, from the enum ErbMethods.</param>
    /// <returns>A record OrbMethodDetails with the specification of the zodiac type.</returns>
    public OrbMethodDetails DetailsForOrbMethod(OrbMethods orbMethod);
    public List<OrbMethodDetails> AllOrbMethodDetails();
    public OrbMethods OrbMethodForIndex(int index);

}

/// <summary>
/// Specifications for Arabic Points/Hellenistic Lots.
/// </summary>
public interface IArabicPointSpecifications
{
    /// <summary>Returns the specification for an Arabic point.</summary>
    /// <param name="arabicPoint">The Arabic point, from the enum ArabicPoints.</param>
    /// <returns>A record ArabicPointDetails with the specification of the Arabic point.</returns>
    public ArabicPointDetails DetailsForArabicPoint(ArabicPoints arabicPoint);
    public List<ArabicPointDetails> AllArabicPointDetails();
}

/// <summary>Specifications for an ayanamsha.</summary>
public interface IAyanamshaSpecifications
{
    /// <summary>Returns the specification for an ayanamsha.</summary>
    /// <param name="ayanamsha">The ayanamsha, from the enum Ayanamshas.</param>
    /// <returns>A record AyanamshaDetails with the specification of the ayanamsha.</returns>
    public AyanamshaDetails DetailsForAyanamsha(Ayanamshas ayanamsha);
    public List<AyanamshaDetails> AllAyanamshaDetails();

    public Ayanamshas AyanamshaForIndex(int index);
}

/// <summary>Specifications for a Chart category.</summary>
public interface IChartCategorySpecifications
{
    /// <param name="category">The category, from the enum ChartCategories.</param>
    /// <returns>A record ChartCategoryDetails with the specifications.</returns>
    public ChartCategoryDetails DetailsForCategory(ChartCategories category);

    ///<returns>Details for all items in enum ChartCategories.</returns>
    public List<ChartCategoryDetails> AllChartCatDetails();

    /// <summary>
    /// Returns a value from the enum ChartCategories that corresponds with an index.
    /// </summary>
    /// <param name="chartCategoryIndex">The index for the requested item from ChartCategories. 
    /// Throws an exception if no ChartCategories for the given index does exist.</param>
    /// <returns>Instance from enum ChartCategories that corresponds with the given index.</returns>
    public ChartCategories ChartCategoryForIndex(int chartCategoryIndex);

}

/// <summary>Specifications for a coordinate system.</summary>
public interface ICoordinateSystemSpecifications
{
    /// <summary>Returns the specifications for a Coordinate System.</summary>
    /// <param name="coordinateSystem">The coordinate system, from the enum CoordinateSystems.</param>
    /// <returns>A record CoordinateSystemDetails with the specifications of the coordinate system.</returns>
    public CoordinateSystemDetails DetailsForCoordinateSystem(CoordinateSystems coordinateSystem);
}


/// <summary>
/// Specifications for the different housesystems.
/// </summary>
public interface IHouseSystemSpecifications
{
    /// <summary>
    /// Returns the specification for a house system.
    /// </summary>
    /// <param name="houseSystem">The house system, from the enum HouseSystems.</param>
    /// <returns>A record HouseSystemDetails with the specification of the house system.</returns>
    public HouseSystemDetails DetailsForHouseSystem(HouseSystems houseSystem);

    public List<HouseSystemDetails> AllHouseSystemDetails();

    public HouseSystems HouseSystemForIndex(int index);
}


/// <summary>Specifications for a Mundane Point.</summary>
public interface IMundanePointSpecifications
{
    /// <summary>Returns the specifications for a given Mundane Point.</summary>
    /// <param name="point">The mundane point for which to find the details.</param>
    /// <returns>A record MundanePointDetails with the specifications.</returns>
    public MundanePointDetails DetailsForPoint(MundanePoints point);

    /// <summary>Returns the specifications of a Mundane Point for a given id.</summary>
    /// <param name="pointId">The id of the mundane point for which to find the details.</param>
    /// <returns>A record MundanePointDetails with the specifications.</returns>
    public MundanePointDetails DetailsForPoint(int pointId);
}


/// <summary>Specifications for an observer position.</summary>
public interface IObserverPositionSpecifications
{
    /// <summary>Returns the specification for an observer position, typically geocentric, topocentric or heliocentric.</summary>
    /// <param name="observerPosition">The observer positions, from the enum ObserverPositions.</param>
    /// <returns>A record ObserverPositionDetails with the specification of the observer position.</returns>
    public ObserverPositionDetails DetailsForObserverPosition(ObserverPositions observerPosition);

    public List<ObserverPositionDetails> AllObserverPositionDetails();

    public ObserverPositions ObserverPositionForIndex(int index);
}

/// <summary>Specification for a Projection Type.</summary>
public interface IProjectionTypeSpecifications
{
    /// <summary>Returns the details for a Projection Type.</summary>
    /// <param name="projectionType">Instance from the enum ProjectionTypes.</param>
    /// <returns>A record PRojectionTypeDetails with the specifications.</returns>
    public ProjectionTypeDetails DetailsForProjectionType(ProjectionTypes projectionType);

    /// <summary>Returns a value from the enum ProjectionTypes that corresponds with an index.</summary>
    /// <param name="projectionTypeIndex">The index for the requested item from ProjectionTypes. 
    /// Throws an exception if no ProjectionTypes for the given index does exist.</param>
    /// <returns>Instance from enum ProjectionTypes that corresponds with the given index.</returns>
    public ProjectionTypes ProjectionTypeForIndex(int projectionTypeIndex);
    public List<ProjectionTypeDetails> AllProjectionTypeDetails();
}

/// <summary>Specifications for a Rodden rating.</summary>
public interface IRoddenRatingSpecifications
{
    /// <param name="rating">The Rodden rating, from the enum RoddenRatings.</param>
    /// <returns>A record RoddenRatingDetails with the specifications.</returns>
    public RoddenRatingDetails DetailsForRating(RoddenRatings rating);

    ///<returns>Details for all items in enum RoddenRatings.</returns>
    public List<RoddenRatingDetails> AllDetailsForRating();

    /// <summary>
    /// Returns a value from the enum RoddenRatings that corresponds with an index.
    /// </summary>
    /// <param name="roddenRatingIndex">The index for the requested item from RoddenRatings. 
    /// Throws an exception if no RoddenRatings for the given index does exist.</param>
    /// <returns>Instance from enum RoddenRatings that corresponds with the given index.</returns>
    public RoddenRatings RoddenRatingForIndex(int roddenRatingIndex);
}

/// <summary>Specifications for a celestial point.</summary>
public interface ICelPointSpecifications
{
    /// <summary>Returns the specifications for a celestial point.</summary>
    /// <param name="point">The celestial point for which to find the details.</param>
    /// <returns>A record CelPointDetails with the specifications.</returns>
    public CelPointDetails DetailsForPoint(CelPoints point);
}

/// <summary>Specifications for a celestial point Category.</summary>
public interface ICelPointCatSpecifications
{
    /// <summary>Returns the details for a celestial point category.</summary>
    /// <param name="category">The category, from the enum CelPointCats.</param>
    /// <returns>A record CelPointCatDetails with the specifications.</returns>
    public CelPointCatDetails DetailsForCategory(CelPointCats category);
}

/// <summary>Specifications for a Zodiacal Point.</summary>
public interface IZodiacalPointSpecifications
{
    /// <summary>Returns the specifications for a given Zodiacal Point.</summary>
    /// <param name="point">The zodiacal point for which to find the details.</param>
    /// <returns>A record ZodiacalPointDetails with the specifications.</returns>
    public ZodiacalPointDetails DetailsForPoint(ZodiacalPoints point);

    /// <summary>Returns the specifications of a Zodiacal Point for a given id.</summary>
    /// <param name="pointId">The id of the zodiacal point for which to find the details.</param>
    /// <returns>A record ZodiacalPointDetails with the specifications.</returns>
    public ZodiacalPointDetails DetailsForPoint(int pointId);
}

/// <summary>Specifications for a zodiac type.</summary>
public interface IZodiacTypeSpecifications
{
    /// <summary>Returns the specification for a zodiac type.</summary>
    /// <param name="zodiacType">The zodiac type, from the enum ZodiacTypes.</param>
    /// <returns>A record ZodiacTypeDetails with the specification of the zodiac type.</returns>
    public ZodiacTypeDetails DetailsForZodiacType(ZodiacTypes zodiacType);
    public List<ZodiacTypeDetails> AllZodiacTypeDetails();
    public ZodiacTypes ZodiacTypeForIndex(int index);

}

/// <summary>Specifications for the Direction of geographic latitude.</summary>
public interface IDirections4GeoLatSpecifications
{
    /// <param name="direction">The direction, from the enum Directions4GeoLat.</param>
    /// <returns>A record with the specifications.</returns>
    public Directions4GeoLatDetails DetailsForDirection(Directions4GeoLat direction);

    ///<returns>Details for all items in enum Directions4GeoLat.</returns>
    public List<Directions4GeoLatDetails> AllDirectionDetails();

    /// <summary>
    /// Returns a value from the enum Directions4GeoLat that corresponds with an index.
    /// </summary>
    /// <param name="directionIndex">The index for the requested item from Directions4GeoLat. 
    /// Throws an exception if no direction for the given index does exist.</param>
    /// <returns>Instance from enum Directions4GeoLat that corresponds with the given index.</returns>
    public Directions4GeoLat DirectionForIndex(int directionIndex);
}

/// <summary>Specifications for the Direction of geographic longitude.</summary>
public interface IDirections4GeoLongSpecifications
{
    /// <param name="direction">The direction, from the enum Directions4GeoLong.</param>
    /// <returns>A record with the specifications.</returns>
    public Directions4GeoLongDetails DetailsForDirection(Directions4GeoLong direction);

    ///<returns>Details for all items in enum Directions4GeoLong.</returns>
    public List<Directions4GeoLongDetails> AllDirectionDetails();

    /// <summary>
    /// Returns a value from the enum Directions4GeoLong that corresponds with an index.
    /// </summary>
    /// <param name="directionIndex">The index for the requested item from Directions4GeoLong. 
    /// Throws an exception if no direction for the given index does exist.</param>
    /// <returns>Instance from enum Directions4GeoLong that corresponds with the given index.</returns>
    public Directions4GeoLong DirectionForIndex(int directionIndex);
}

