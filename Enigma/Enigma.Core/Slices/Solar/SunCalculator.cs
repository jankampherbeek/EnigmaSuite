// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.Solar;

/// <summary>
/// Calculate longitude of Sun 
/// </summary>
/// <remarks>Utility function for the calculation of solar returns</remarks>
public interface ISunCalculator
{
    /// <summary>
    /// Calculate the longitude of the Sun
    /// </summary>
    /// <param name="jd">Julian Day</param>
    /// <param name="flags">Calculation flags</param>
    /// <returns>Longitude of the Sun</returns>
    double CalcPositionSun(double jd, int flags);
}

/// <inheritdoc/>
public class SunCalculator(ICalcUtFacade calcUtFacade) : ISunCalculator
{
    /// <inheritdoc/>
    public double CalcPositionSun(double jd, int flags)
    {
        var seId = ChartPoints.Sun.GetDetails().CalcId;
        var result = calcUtFacade.PositionFromSe(jd, seId, flags);
        return result[0];
    }
}