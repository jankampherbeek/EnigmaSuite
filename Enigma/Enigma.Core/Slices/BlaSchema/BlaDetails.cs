// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Details for BLA Schema
/// </summary>
public static class BlaDetails
{
    /// <summary>
    /// Create the details for the BLA schema.
    /// </summary>
    /// <param name="chart">Positions in longitude</param>
    /// <param name="signsOnCusps">Signs on cusps</param>
    /// <param name="planetsInHouses">Planets in houses</param>   
    /// <returns>Populated BlaDetailsData</returns>
    public static BlaDetailsData CreateDetails(ChartLongitudes chart, 
        Dictionary<int, int> signsOnCusps,
        Dictionary<ChartPoints, int> planetsInHouses)
    {
        
        var asc = signsOnCusps[1];
        var ascRulers = BlaDomain.RulerPairs()[asc];
        var sisterRulerAsc = ascRulers.SubRuler;
        var sisterSignAsc = (int)Math.Truncate(chart.Points[sisterRulerAsc] / 30.0) + 1;
        var clampedHouses = InterceptedClamped.DefineClampedHouses(chart);
        var interceptedSigns = InterceptedClamped.DefineInterceptedSigns(chart);
        var groundNote = new List<int>();
        groundNote.Add(asc);
        var mundaneHouseAsc = (int)Math.Truncate(asc / 30.0) + 1;
        groundNote.Add(mundaneHouseAsc);
        var sisterSignCusp = 0;
        foreach (var rulerPair in BlaDomain.RulerPairs())
        {
            if (rulerPair.MainRuler == sisterRulerAsc)
            {
                sisterSignCusp = rulerPair.SignIndex;
            }
        }
        groundNote.Add(sisterSignCusp);
        foreach (var cuspSign in signsOnCusps)
        {
            if (cuspSign.Value == asc && cuspSign.Key != 1)
            {
                groundNote.Add(cuspSign.Key);
            }
        }
        var lordAscInHouses = new List<int>
        {
            planetsInHouses[ascRulers.MainRuler],
            planetsInHouses[ascRulers.SubRuler]
        };

        var moonInHouse = planetsInHouses[ChartPoints.Moon];
        
        return new BlaDetailsData(sisterSignAsc, clampedHouses, interceptedSigns, groundNote, lordAscInHouses, moonInHouse);

    }

}