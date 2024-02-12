// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

/// <summary>Calculations for celestial points.</summary>
public interface ICelPointSeCalc
{
    /// <summary>Calculate a single celestial point.</summary>
    /// <param name="celPoint">The celestial point that will be calculated.</param>
    /// <param name="jdnr">The Julian day number.</param>
    /// <param name="location">Location with coordinates.</param>
    /// <param name="flags">Flags that contain the settings for ecliptic or equatorial based calculations.</param>
    /// <returns>Array with position and speed for mainposition, deviation and distance, in that sequence. Typically: longitude, latitude, distance or right ascension, declination and distance.</returns>
    public PosSpeed[] CalculateCelPoint(ChartPoints celPoint, double jdnr, Location location, int flags);
}

/// <inheritdoc/>
public sealed class CelPointSeCalc : ICelPointSeCalc
{
    private readonly ICalcUtFacade _calcUtFacade;
    private readonly IChartPointsMapping _mapping;

    public CelPointSeCalc(ICalcUtFacade calcUtFacade, IChartPointsMapping chartPointsMapping)
    {
        _calcUtFacade = calcUtFacade;
        _mapping = chartPointsMapping;
    }

    /// <inheritdoc/>
    public PosSpeed[] CalculateCelPoint(ChartPoints celPoint, double jdnr, Location location, int flags)
    {
        int pointId = _mapping.SeIdForCelestialPoint(celPoint);
        double[] positions = _calcUtFacade.PositionFromSe(jdnr, pointId, flags);
        var mainPos = new PosSpeed(positions[0], positions[3]);
        var deviation = new PosSpeed(positions[1], positions[4]);
        var distance = new PosSpeed(positions[2], positions[5]);
        return new[] { mainPos, deviation, distance };
    }

}