// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
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
    /// <param name="seId">The SE Id for the celestial point that will be calculated.</param>
    /// <param name="jd">The Julian day number.</param>
    /// <param name="flags">Flags that contain the settings for ecliptic or equatorial based calculations.</param>
    /// <returns>Array with position and speed for mainposition, deviation and distance, in that sequence. Typically: longitude, latitude, distance or right ascension, declination and distance.</returns>
    public PosSpeed[] CalculateCelPoint(int seId, double jd, int flags);

    /// <summary>Calculate a single celestial point for only one coordinate.</summary>
    /// <param name="seId">The SE Id for the celestial point that will be calculated.</param>
    /// <param name="jd">The Julian day number.</param>
    /// <param name="flags">Flags that contain the settings for ecliptic or equatorial based calculations.</param>
    /// <param name="mainPos">True for main position (longitude, ra), false otherwise (latitude, declination)</param>
    /// <returns>Position for given coordinate.</returns>
    public double CalculatePosForSingleCoord(int seId, double jd, int flags, bool mainPos);
    
    
    
}

/// <inheritdoc/>
public sealed class CelPointSeCalc(ICalcUtFacade calcUtFacade, IChartPointsMapping chartPointsMapping)
    : ICelPointSeCalc
{
    private readonly IChartPointsMapping _mapping = chartPointsMapping;

    /// <inheritdoc/>
    public PosSpeed[] CalculateCelPoint(int seId, double jd, int flags)
    {
        var positions = calcUtFacade.PositionFromSe(jd, seId, flags);
        var mainPos = new PosSpeed(positions[0], positions[3]);
        var deviation = new PosSpeed(positions[1], positions[4]);
        var distance = new PosSpeed(positions[2], positions[5]);
        return [mainPos, deviation, distance];
    }

    /// <inheritdoc/>
    public double CalculatePosForSingleCoord(int seId, double jd, int flags, bool mainPos)
    {
        var positions = calcUtFacade.PositionFromSe(jd, seId, flags);
        return mainPos ? positions[0] : positions[1];
    }
}