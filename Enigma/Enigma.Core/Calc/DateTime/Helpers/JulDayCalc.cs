// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Calc.DateTime.Helpers;


/// <inheritdoc/>
public sealed class JulDayCalc : IJulDayCalc
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