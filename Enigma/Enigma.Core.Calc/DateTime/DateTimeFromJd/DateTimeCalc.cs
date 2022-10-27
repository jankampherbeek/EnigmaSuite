// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc.SeFacades;
using Enigma.Domain.DateTime;

namespace Enigma.Core.Calc.DateTime.DateTimeFromJd;

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
    /// <param name="calendar"/>
    /// <returns>Calculated JD for UT.</returns>
    public SimpleDateTime CalcDateTime(double julDay, Calendars calendar)
    {
        return _revJulFacade.DateTimeFromJd(julDay, calendar);
    }

}