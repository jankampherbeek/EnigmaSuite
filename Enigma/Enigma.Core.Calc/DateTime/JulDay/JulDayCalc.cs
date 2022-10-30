// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.SeFacades;
using Enigma.Domain.AstronCalculations;

namespace Enigma.Core.Calc.DateTime.JulDay;



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