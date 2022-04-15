// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using domain.shared;
using E4C.core.facades;
using E4C.core.shared.domain;

namespace E4C.core.calendarandclock.julday;

/// <summary>Calculations for Julian Day.</summary>
public interface IJulDayCalc
{

    public double CalcJulDay(SimpleDateTime dateTime);
}

/// <inheritdoc/>
public class JulDayCalc : IJulDayCalc
{
    private readonly IJulDayFacade _julDayFacade;

    public JulDayCalc(IJulDayFacade julDayFacade) => _julDayFacade = julDayFacade;

    /// <summary>Calculate Julian Day for Universal Time.</summary>
    /// <param name="dateTime"/>
    /// <returns>Calculated JD for UT.</returns>
    public double CalcJulDay(SimpleDateTime dateTime)
    {
        return _julDayFacade.JdFromSe(dateTime);
    }





}