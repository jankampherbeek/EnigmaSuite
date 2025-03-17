// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

/// <summary>Calculate apsides</summary>
public interface IApsideSeCalc
{
    /// <summary>Calculate an apside</summary>
    /// <param name="celPoint">The celestial point that will be calculated</param>
    /// <param name="jdnr">Julian day number</param>
    /// <param name="method">Indicates mean or oscillating apside</param>
    /// <param name="flags">Flags that contain the settings for ecliptic or equatorial based calculations</param>
    /// <returns>Array with position and speed for mainposition, deviation and distance, in that sequence. Typically: longitude, latitude, distance or right ascension, declination and distance.</returns>
    public PosSpeed[] CalculateApside(ChartPoints celPoint, double jdnr, ApsidesMethods method, int flags);
}

/// <inheritdoc/>
public class ApsideSeCalc(ICalcApsidesFacade calcApsidesFacade, IChartPointsMapping chartPointsMapping)
    : IApsideSeCalc
{
    /// <inheritdoc/>
    public PosSpeed[] CalculateApside(ChartPoints celPoint, double jdnr, ApsidesMethods method, int flags)
    {
        int pointId = chartPointsMapping.SeIdForCelestialPoint(celPoint);
        double[][] positions = calcApsidesFacade.ApsidesFromSe(jdnr, pointId, method, flags);
        PosSpeed mainPos;
        PosSpeed deviation;
        PosSpeed distance;

        if (celPoint == ChartPoints.Diamond)
        {
            mainPos = new PosSpeed(positions[0][0], positions[0][3]);
            deviation = new PosSpeed(positions[0][1], positions[0][4]);
            distance = new PosSpeed(positions[0][2], positions[0][5]);
            return [mainPos, deviation, distance];
        }
        // celPoint is BlackSun
        mainPos = new PosSpeed(positions[1][0], positions[1][3]);
        deviation = new PosSpeed(positions[1][1], positions[1][4]);
        distance = new PosSpeed(positions[1][2], positions[1][5]);
        
        return [mainPos, deviation, distance];
    }
}


/*

/// <inheritdoc/>
public sealed class CelPointSeCalc : ICelPointSeCalc
{
    /// <inheritdoc/>
    public PosSpeed[] CalculateCelPoint(ChartPoints celPoint, double jdnr, Location? location, int flags)
    {
        int pointId = _mapping.SeIdForCelestialPoint(celPoint);
        double[] positions = _calcUtFacade.PositionFromSe(jdnr, pointId, flags);
        var mainPos = new PosSpeed(positions[0], positions[3]);
        var deviation = new PosSpeed(positions[1], positions[4]);
        var distance = new PosSpeed(positions[2], positions[5]);
        return new[] { mainPos, deviation, distance };
    }

}
*/