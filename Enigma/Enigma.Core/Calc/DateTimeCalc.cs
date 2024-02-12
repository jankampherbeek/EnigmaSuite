// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

/// <summary>Calculations for Julian Day.</summary>
public interface IDateTimeCalc
{
    /// <summary>Calculate Date and time.</summary>
    /// <param name="julDay"/>
    /// <param name="calendar"/>
    /// <returns>Calculated JD for UT.</returns>
    public SimpleDateTime CalcDateTime(double julDay, Calendars calendar);
}

/// <inheritdoc/>
public sealed class DateTimeCalc : IDateTimeCalc
{
    private readonly IRevJulFacade _revJulFacade;

    public DateTimeCalc(IRevJulFacade revJulFacade) => _revJulFacade = revJulFacade;


    /// <inheritdoc/>
    public SimpleDateTime CalcDateTime(double julDay, Calendars calendar)
    {
        return _revJulFacade.DateTimeFromJd(julDay, calendar);
    }

}