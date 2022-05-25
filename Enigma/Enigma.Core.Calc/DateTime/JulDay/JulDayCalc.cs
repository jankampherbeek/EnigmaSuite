// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc.SeFacades;
using Enigma.Domain.DateTime;

namespace Enigma.Core.Calc.DateTime.JulDay;

/// <summary>Calculations for Julian Day.</summary>
public interface IJulDayCalc
{
    /// <summary>Calculate Julian Day for Universal Time.</summary>
    /// <param name="dateTime"/>
    /// <returns>Calculated JD for UT.</returns>
    public double CalcJulDayUt(SimpleDateTime dateTime);

    /// <summary>Calculate Delta T</summary>
    /// <param name="juldayUt">Julian Day for UT.</param>
    /// <returns>The value for delta t in seconds and fractions of seconds.</returns>
    public double CalcDeltaT(double juldayUt);
}

/// <inheritdoc/>
public class JulDayCalc : IJulDayCalc
{
    private readonly IJulDayFacade _julDayFacade;

    public JulDayCalc(IJulDayFacade julDayFacade) => _julDayFacade = julDayFacade;

    /// <inheritdoc/>
    public double CalcDeltaT(double juldayUt)
    {
        return _julDayFacade.DeltaTFromSe(juldayUt);
    }

    /// <inheritdoc/>
    public double CalcJulDayUt(SimpleDateTime dateTime)
    {
        return _julDayFacade.JdFromSe(dateTime);
    }

}