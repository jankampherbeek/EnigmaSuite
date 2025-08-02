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
/// <remarks>Utility function for the alculation of solar returns</remarks>
/// <param name="calcUtFacade"></param>
public class SunCalculator(ICalcUtFacade calcUtFacade)
{
    /// <summary>
    /// Calculate the longitude of the Sun
    /// </summary>
    /// <param name="jd"></param>
    /// <param name="flags"></param>
    /// <returns></returns>
    public double CalcPositionSun(double jd, int flags)
    {
        var seId = ChartPoints.Sun.GetDetails().CalcId;
        var result = calcUtFacade.PositionFromSe(jd, seId, flags);
        return result[0];
    }

}