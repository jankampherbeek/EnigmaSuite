// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.VenusStarPoint;

/// <summary>
/// Handle all activiteites for the calculation of a Venus Star Point
/// </summary>
public class VenusStarPointOrchestrator(DefineJdRange defineJdRange, ICalcUtFacade calcUtFacade){

    
    /// <summary>
    /// Calculate Venus Star Point
    /// </summary>
    /// <remarks>
    /// Calculations for oblique longitude (School of Ram) are not yet supported.
    /// </remarks>
    /// <param name="request">Request with data and settings for calculation</param>
    /// <returns>List with resulting values</returns>
    public List<VenusStarPointPosition> CalculateVenusStarPoint(VenusStarPointRequest request)
    {
        var flags = 2 + 256;                     // use SE + speed
        if (request.Ayanamsha != Ayanamshas.None)   // Handle sidereal positions
        {
            flags += (64 * 1024);                  // use sidereal zodiac
            SeInitializer.SetAyanamsha(request.Ayanamsha.GetDetails().SeId);  // prepare SE for selected Ayanamsha
        }
        // Calculate the exact JDs for the two pehnomena: Superior conjunction and Inferior conjunction
        var allPhenJds = defineJdRange.JdRange(request.Jd);
        var sequence = 1;
        return allPhenJds.Select(phenJd => CalcPosition(sequence++, phenJd, flags)).ToList();    
    }

    private VenusStarPointPosition CalcPosition(int sequence, Tuple<double, VenusPhenomena> phenJd, int flags)
    {
        // For Venus Star Point, Sun and Venus are at the same longitude during conjunction
        var positionSun = calcUtFacade.PositionFromSe(phenJd.Item1, ChartPoints.Sun.GetDetails().CalcId, flags)[0];
        return new VenusStarPointPosition(sequence, phenJd.Item1, phenJd.Item2, positionSun);
    }
    
    
}