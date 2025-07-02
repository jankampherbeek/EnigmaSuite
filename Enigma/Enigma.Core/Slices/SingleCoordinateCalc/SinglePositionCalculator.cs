// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.SingleCoordinateCalc;

/// <summary>
/// Calculate a single coordinate for a single chartpoint
/// </summary>
public class SinglePositionCalculator
{
    private readonly ICalcUtFacade _calcUtfacade;

    public SinglePositionCalculator(ICalcUtFacade calcUtFacade)
    {
        _calcUtfacade = calcUtFacade ?? throw new ArgumentNullException(nameof(calcUtFacade));
    }

    /// <summary>
    /// Calculate main position (longitude, right ascension) or deviation (latitude, declination) for a single position
    /// </summary>
    /// <param name="jd">Julian day number</param>
    /// <param name="pointId">The id for the SE calculation</param>
    /// <param name="flags">Calculation flags</param>
    /// <param name="isMainPos">If true, uses mainPos, otherwise uses deviation</param>
    /// <returns>Longitude/ra or latitude/declination</returns>
    public double CalcSinglePosition(double jd, int pointId, int flags, bool isMainPos)
    {
        var positions = _calcUtfacade.PositionFromSe(jd, pointId, flags);
        return isMainPos ? positions[0] : positions[1];
    }
}