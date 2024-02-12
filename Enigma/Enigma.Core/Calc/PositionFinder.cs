// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.Calc;

/// <summary>Search for positions for  aspecific ChartPoint.</summary>
public interface IPositionFinder
{
    /// <summary>Search for a position for the Sun.</summary>
    /// <param name="posToFind">The position to find in longitude or in ra.</param>
    /// <param name="startJd">The Julian Day to start the search.</param>
    /// <param name="startInterval">The initial interval in days.</param>
    /// <param name="maxMargin">The max allowed margin.</param>
    /// <param name="coordSys">Coordinate system, should be ecliptical or equatorial.</param>
    /// <param name="observerPos">Observer position, should be geocentric or topocentric.</param>
    /// <param name="location">Location, only used for topocentric positions.</param>
    /// <exception cref="ArgumentException">Thrown when a wrong coordinate system is used.</exception>
    /// <returns>The first JD when the position will occur.</returns>
    public double FindJdForPositionSun(double posToFind, double startJd, double startInterval, double maxMargin, CoordinateSystems coordSys, ObserverPositions observerPos, Location location);
}

/// <inheritdoc/>
public sealed class PositionFinder: IPositionFinder
{
    private readonly ICalcUtFacade _calcUtFacade;
    private readonly ISeFlags _seFlags;
    private readonly IChartPointsMapping _mapping;

    public PositionFinder(ICalcUtFacade calcUtFacade, ISeFlags seFlags, IChartPointsMapping mapping)
    {
        _calcUtFacade = calcUtFacade;
        _seFlags = seFlags;
        _mapping = mapping;
    }

    /// <inheritdoc/>
    public double FindJdForPositionSun(double posToFind, double startJd, double startInterval, double maxMargin, CoordinateSystems coordSys, ObserverPositions observerPos, Location location)
    {
        if (coordSys != CoordinateSystems.Ecliptical && coordSys != CoordinateSystems.Equatorial) 
        {
            Log.Error("PositionFinder.FindJdForPositionSun: Wrong coordinatesystem: {Csys}", coordSys);
            throw new ArgumentException("Wrong coordinate system");
        }
        if (observerPos == ObserverPositions.TopoCentric)
        {
            SeInitializer.SetTopocentric(location.GeoLong, location.GeoLat, 0.0);    
        }
        int flags = _seFlags.DefineFlags(coordSys, observerPos, ZodiacTypes.Tropical);
        int seId = _mapping.SeIdForCelestialPoint(ChartPoints.Sun);
        double newJd = startJd;
        double currentInterval = startInterval;
        double oldPos = CalcPos(seId, newJd, flags);
        bool checkForZeroAries = (oldPos - posToFind) > 180.0;
        while (true) {
            newJd += currentInterval;
            double newPos = CalcPos(seId, newJd, flags);

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
