// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Facades;
using E4C.Core.Shared.Domain;
using E4C.Shared.References;

namespace E4C.Core.CalendarAndClock.DateTime;

/// <summary>Calculations for Julian Day.</summary>
public interface IDateTimeCalc
{
    public SimpleDateTime CalcDateTime(double julDay, Calendars calendar);
}

/// <inheritdoc/>
public class DateTimeCalc : IDateTimeCalc
{
    private readonly IRevJulFacade _revJulFacade;

    public DateTimeCalc(IRevJulFacade revJulFacade) => _revJulFacade = revJulFacade;

    /// <summary>Calculate Date and time.</summary>
    /// <param name="julDay"/>
    /// <param name="calendar""/>
    /// <returns>Calculated JD for UT.</returns>
    public SimpleDateTime CalcDateTime(double julDay, Calendars calendar)
    {
        return _revJulFacade.DateTimeFromJd(julDay, calendar);
    }

}