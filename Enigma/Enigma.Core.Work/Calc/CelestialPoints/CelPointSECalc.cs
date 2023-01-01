// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Points;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Work.Calc.CelestialPoints;


/// <inheritdoc/>
public class CelPointSECalc : ICelPointSECalc
{
    private readonly ICalcUtFacade _calcUtFacade;

    public CelPointSECalc(ICalcUtFacade calcUtFacade)
    {
        _calcUtFacade = calcUtFacade;
    }


    public PosSpeed[] CalculateCelPoint(CelPoints celPoint, double jdnr, Location location, int flags)
    {
        int pointId = celPoint.GetDetails().SeId;
        double[] positions = _calcUtFacade.PosCelPointFromSe(jdnr, pointId, flags);
        var mainPos = new PosSpeed(positions[0], positions[3]);
        var deviation = new PosSpeed(positions[1], positions[4]);
        var distance = new PosSpeed(positions[2], positions[5]);
        return new PosSpeed[] { mainPos, deviation, distance };
    }

}