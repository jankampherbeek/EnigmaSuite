// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Work.Calc.DateTime;



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