// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Core.Calc.SolSysPoints;


/// <inheritdoc/>
public class SolSysPointSECalc : ISolSysPointSECalc
{
    private readonly ICalcUtFacade _calcUtFacade;
    private readonly ISolarSystemPointSpecifications _solarSystemPointSpecifications;

    public SolSysPointSECalc(ICalcUtFacade calcUtFacade, ISolarSystemPointSpecifications solarSystemPointSpecifications)
    {
        _calcUtFacade = calcUtFacade;
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
    }


    public PosSpeed[] CalculateSolSysPoint(SolarSystemPoints solarSystemPoint, double jdnr, Location location, int flags)
    {
        int pointId = _solarSystemPointSpecifications.DetailsForPoint(solarSystemPoint).SeId;
        double[] positions = _calcUtFacade.PosCelPointFromSe(jdnr, pointId, flags);
        var mainPos = new PosSpeed(positions[0], positions[3]);
        var deviation = new PosSpeed(positions[1], positions[4]);
        var distance = new PosSpeed(positions[2], positions[5]);
        return new PosSpeed[] { mainPos, deviation, distance };
    }

}