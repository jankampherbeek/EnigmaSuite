// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Enigma.Facades.Se;

namespace Enigma.Core.Handlers;

/// <summary>Handler for the calculation of an Out of Bounds calendar.</summary>
public interface IOobCalHandler
{
    /// <summary>Handle the calculation of an Out of Bounds calendar.</summary>
    /// <param name="request">Request.</param>
    /// <returns>All out of bound events.</returns>
    public List<OobCalEvent> CreateOobCalendar(OobCalRequest request);
}


// =================== Implementation ================================================================

/// <inheritdoc/>
public sealed class OobCalHandler: IOobCalHandler
{
    private const double HOURS_PER_DAY = 24.0;
    private readonly IOobCalendarCalc _oobCalc;
    private readonly IJulDayFacade _julDayFacade;
    
    public OobCalHandler(IOobCalendarCalc oobCalc, IJulDayFacade julDayFacade)
    {
        _oobCalc = oobCalc;
        _julDayFacade = julDayFacade;
    }
    
    /// <inheritdoc/>
    public List<OobCalEvent> CreateOobCalendar(OobCalRequest request)
    {
        IEnumerable<OobSecJdEvent> jdEvents = _oobCalc.CreateOobCalendar(request).OrderBy(p => p.SecJd);
        return ConvertToCalendarDates(jdEvents, request);
    }

    private List<OobCalEvent> ConvertToCalendarDates(IEnumerable<OobSecJdEvent> jdEvents, OobCalRequest request)
    {
        List<OobCalEvent> oobCalEvents = new();
        double radixJd = request.JdStart;
        double zoneCorr = request.TimeOffset / HOURS_PER_DAY;
        foreach (var jdEvent in jdEvents)
        {
            double jdSpanInSecDays = jdEvent.SecJd - radixJd;
            double jdSpanInYears = jdSpanInSecDays * EnigmaConstants.TROPICAL_YEAR_IN_DAYS + zoneCorr;
            SimpleDateTime dTime = _julDayFacade.DateTimeFromJd(radixJd + jdSpanInYears, request.Cal);
            oobCalEvents.Add(new OobCalEvent(jdEvent.Point, jdEvent.EventType, dTime.Year, dTime.Month, dTime.Day));
        }

        return oobCalEvents;
    }
    
}