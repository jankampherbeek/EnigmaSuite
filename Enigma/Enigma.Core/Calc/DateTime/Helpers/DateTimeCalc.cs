// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Calc.DateTime.Helpers;


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