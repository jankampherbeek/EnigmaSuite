// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Enigma.Facades.Interfaces;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.Handlers.Calc.Specials.Helpers;

/// <inheritdoc/>
public sealed class PositionFinder: IPositionFinder
{
    private readonly ICalcUtFacade _calcUtFacade;
    private readonly ISeFlags _seFlags;
    private readonly IChartPointsMapping _mapping;

    public PositionFinder(ICalcUtFacade calcUtFacade, ISeFlags seFlags)
    {
        _calcUtFacade = calcUtFacade;
        _seFlags = seFlags;
    }

    /// <inheritdoc/>
    public double FindJdForPositionSun(double posToFind, double startJd, double startInterval, double maxMargin, CoordinateSystems coordSys, ObserverPositions observerPos, Location location)
    {
        if (coordSys != CoordinateSystems.Ecliptical && coordSys != CoordinateSystems.Equatorial) 
        {
            string errorTxt = "PositionFinder.FindJdForPositionSun: Wrong coordinatesystem: " + coordSys;
            Log.Error(errorTxt);
            throw new ArgumentException(errorTxt);
        }
        if (observerPos == ObserverPositions.TopoCentric)
        {
            SeInitializer.SetTopocentric(location.GeoLong, location.GeoLat, 0.0);      // TODO backlog optionally replace 0.0 with value for altitude above sealevel in meters. 
        }
        int flags = _seFlags.DefineFlags(coordSys, observerPos, ZodiacTypes.Tropical);
        int seId = _mapping.SeIdForCelestialPoint(ChartPoints.Sun);
        double newJd = startJd;
        double currentInterval = startInterval;
        double newPos;
        double oldPos = CalcPos(seId, newJd, flags);
        bool checkForZeroAries = (oldPos - posToFind) > 180.0;
        while (true) {
            newJd += currentInterval;
            newPos = CalcPos(seId, newJd, flags);

            if (!checkForZeroAries && (newPos - posToFind) < maxMargin)
            {
                break;
            }
            if (!checkForZeroAries && (newPos > posToFind) )
            {
                newJd -= currentInterval;
                currentInterval /= 10.0;
                newJd += currentInterval;
            }
            oldPos = newPos;
            checkForZeroAries = (oldPos - posToFind) > 180.0;
        }
        return newJd;
    }

    private double CalcPos(int seId, double jd, int flags)
    {
        double[] positions = _calcUtFacade.PositionFromSe(jd, seId, flags);
        return positions[0];
    }

}
