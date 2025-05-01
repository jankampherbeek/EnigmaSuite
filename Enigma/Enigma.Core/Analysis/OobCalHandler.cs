// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Enigma.Facades.Se;

namespace Enigma.Core.Analysis;

/// <summary>Handler for the calculation of an Out of Bounds calendar.</summary>
public interface IOobCalHandler
{
    /// <summary>Handle the calculation of an Out of Bounds calendar.</summary>
    /// <param name="request">Request.</param>
    /// <returns>All out of bound events.</returns>
    public List<OobCalEvent> CreateOobCalendar(OobCalRequest request);
}


/// <inheritdoc/>
public sealed class OobCalHandler(IOobCalendarCalc oobCalc, IJulDayFacade julDayFacade) : IOobCalHandler
{
    private const double HOURS_PER_DAY = 24.0;

    /// <inheritdoc/>
    public List<OobCalEvent> CreateOobCalendar(OobCalRequest request)
    {
        IEnumerable<OobSecJdEvent> jdEvents = oobCalc.CreateOobCalendar(request).OrderBy(p => p.SecJd);
        return ConvertToCalendarDates(jdEvents, request);
    }

    private List<OobCalEvent> ConvertToCalendarDates(IEnumerable<OobSecJdEvent> jdEvents, OobCalRequest request)
    {
        List<OobCalEvent> oobCalEvents = [];
        var radixJd = request.JdStart;
        var zoneCorr = request.TimeOffset / HOURS_PER_DAY;
        foreach (var jdEvent in jdEvents)
        {
            var jdSpanInSecDays = jdEvent.SecJd - radixJd;
            var jdSpanInYears = jdSpanInSecDays * EnigmaConstants.TROPICAL_YEAR_IN_DAYS + zoneCorr;
            var dTime = julDayFacade.DateTimeFromJd(radixJd + jdSpanInYears, request.Cal);
            oobCalEvents.Add(new OobCalEvent(jdEvent.Point, jdEvent.EventType, dTime.Year, dTime.Month, dTime.Day));
        }

        return oobCalEvents;
    }
    
}