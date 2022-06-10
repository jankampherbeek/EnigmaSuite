// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.CalcVars;
using Enigma.Domain.DateTime;
using Enigma.Domain.Locational;
using System.Collections.Generic;

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
}


public class ChartsEnumFacade : IChartsEnumFacade
{

    private ICalendarSpecifications _calendarSpecifications;
    private IChartCategorySpecifications _chartCategorySpecifications;
    private IDirections4GeoLatSpecifications _directions4GeoLatSpecifications;
    private IDirections4GeoLongSpecifications _directions4GeoLongSpecifications;
    private IRoddenRatingSpecifications _roddenRatingSpecifications;
    private ITimeZoneSpecifications _timeZoneSpecifications;
    private IYearCountSpecifications _yearCountSpecifications;

    public ChartsEnumFacade(ICalendarSpecifications calendarSpecifications,
                            IChartCategorySpecifications chartCategorySpecifications,
                            IDirections4GeoLatSpecifications directions4GeoLatSpecifications,
                            IDirections4GeoLongSpecifications directions4GeoLongSpecifications,
                            IRoddenRatingSpecifications roddenRatingSpecifications,
                            ITimeZoneSpecifications timeZoneSpecifications,
                            IYearCountSpecifications yearCountSpecifications)
    {
        _calendarSpecifications = calendarSpecifications;
        _chartCategorySpecifications = chartCategorySpecifications;
        _directions4GeoLatSpecifications = directions4GeoLatSpecifications;
        _directions4GeoLongSpecifications = directions4GeoLongSpecifications;
        _roddenRatingSpecifications = roddenRatingSpecifications;
        _timeZoneSpecifications = timeZoneSpecifications;
        _yearCountSpecifications = yearCountSpecifications;
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
}