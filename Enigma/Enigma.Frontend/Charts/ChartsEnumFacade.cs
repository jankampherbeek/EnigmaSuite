// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Charts;

// TODO: move ChartsEnumFacade to Enigma.Domain and rename to EnumFacade.


/// <summary>
/// Simple facade to manage the number of imports that is required to use enums.
/// </summary>
public interface IChartsEnumFacade
{


    public List<CalendarDetails> AllCalendarDetails();
    public List<ChartCategoryDetails> AllChartCategoryDetails();
    public List<Directions4GeoLatDetails> AllDirections4GeoLatDetails();
    public List<Directions4GeoLongDetails> AllDirections4GeoLongDetails();
    public List<RoddenRatingDetails> AllRoddenRatingDetails();
    public List<TimeZoneDetails> AllTimeZoneDetails();
    public List<YearCountDetails> AllYearCountDetails();
    public IHouseSystemSpecifications Houses();
    public IAyanamshaSpecifications Ayanamshas();
    public IZodiacTypeSpecifications ZodiacTypes();
    public IObserverPositionSpecifications ObserverPositions();
    public IProjectionTypeSpecifications ProjectionTypes();

    public IOrbMethodSpecifications OrbMethods();
}


public class ChartsEnumFacade : IChartsEnumFacade
{


    private readonly ICalendarSpecifications _calendarSpecifications;
    private readonly IChartCategorySpecifications _chartCategorySpecifications;
    private readonly IDirections4GeoLatSpecifications _directions4GeoLatSpecifications;
    private readonly IDirections4GeoLongSpecifications _directions4GeoLongSpecifications;
    private readonly IRoddenRatingSpecifications _roddenRatingSpecifications;
    private readonly ITimeZoneSpecifications _timeZoneSpecifications;
    private readonly IYearCountSpecifications _yearCountSpecifications;
    private readonly IHouseSystemSpecifications _houseSystemSpecifications;
    private readonly IAyanamshaSpecifications _ayanamshaSpecifications;
    private readonly IZodiacTypeSpecifications _zodiacTypeSpecifications;
    private readonly IObserverPositionSpecifications _observerPositionSpecifications;
    private readonly IProjectionTypeSpecifications _projectionTypeSpecifications;
    private readonly IOrbMethodSpecifications _orbMethodSpecifications;

    public ChartsEnumFacade(ICalendarSpecifications calendarSpecifications,
                            IChartCategorySpecifications chartCategorySpecifications,
                            IDirections4GeoLatSpecifications directions4GeoLatSpecifications,
                            IDirections4GeoLongSpecifications directions4GeoLongSpecifications,
                            IRoddenRatingSpecifications roddenRatingSpecifications,
                            ITimeZoneSpecifications timeZoneSpecifications,
                            IYearCountSpecifications yearCountSpecifications,
                            IHouseSystemSpecifications houseSystemSpecifications,
                            IAyanamshaSpecifications ayanamshaSpecifications,
                            IZodiacTypeSpecifications zodiacTypeSpecifications,
                            IObserverPositionSpecifications observerPositionSpecifications,
                            IProjectionTypeSpecifications projectionTypeSpecifications,
                            IOrbMethodSpecifications orbMethodSpecifications)
    {
        _calendarSpecifications = calendarSpecifications;
        _chartCategorySpecifications = chartCategorySpecifications;
        _directions4GeoLatSpecifications = directions4GeoLatSpecifications;
        _directions4GeoLongSpecifications = directions4GeoLongSpecifications;
        _roddenRatingSpecifications = roddenRatingSpecifications;
        _timeZoneSpecifications = timeZoneSpecifications;
        _yearCountSpecifications = yearCountSpecifications;
        _houseSystemSpecifications = houseSystemSpecifications;
        _ayanamshaSpecifications = ayanamshaSpecifications;
        _zodiacTypeSpecifications = zodiacTypeSpecifications;
        _observerPositionSpecifications = observerPositionSpecifications;
        _projectionTypeSpecifications = projectionTypeSpecifications;
        _orbMethodSpecifications = orbMethodSpecifications;
    }


    public List<CalendarDetails> AllCalendarDetails()
    {
        return _calendarSpecifications.AllCalendarDetails();
    }

    public List<ChartCategoryDetails> AllChartCategoryDetails()
    {
        return _chartCategorySpecifications.AllChartCatDetails();
    }

    public List<Directions4GeoLatDetails> AllDirections4GeoLatDetails()
    {
        return _directions4GeoLatSpecifications.AllDirectionDetails();
    }

    public List<Directions4GeoLongDetails> AllDirections4GeoLongDetails()
    {
        return _directions4GeoLongSpecifications.AllDirectionDetails();
    }

    public List<RoddenRatingDetails> AllRoddenRatingDetails()
    {
        return _roddenRatingSpecifications.AllDetailsForRating();
    }

    public List<TimeZoneDetails> AllTimeZoneDetails()
    {
        return _timeZoneSpecifications.AllTimeZoneDetails();
    }

    public List<YearCountDetails> AllYearCountDetails()
    {
        return _yearCountSpecifications.AllDetailsForYearCounts();
    }

    public IHouseSystemSpecifications Houses()
    {
        return _houseSystemSpecifications;
    }

    public IAyanamshaSpecifications Ayanamshas()
    {
        return _ayanamshaSpecifications;
    }

    public IZodiacTypeSpecifications ZodiacTypes()
    {
        return _zodiacTypeSpecifications;
    }

    public IObserverPositionSpecifications ObserverPositions()
    {
        return _observerPositionSpecifications;
    }

    public IProjectionTypeSpecifications ProjectionTypes()
    {
        return _projectionTypeSpecifications;
    }

    public IOrbMethodSpecifications OrbMethods()
    {
        return _orbMethodSpecifications;
    }
}