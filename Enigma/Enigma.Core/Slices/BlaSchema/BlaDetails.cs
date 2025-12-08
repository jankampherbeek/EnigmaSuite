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
        var ascRulers = BlaDomain.RulerPairs()[asc-1];
        var subRulerAsc = ascRulers.SubRuler;
        var sisterSignAsc = 0;
        foreach (var rulerPair in BlaDomain.RulerPairs())
        {
            if (rulerPair.MainRuler == subRulerAsc)
            {
                sisterSignAsc = rulerPair.SignIndex;
            }
        }    
        var clampedHouses = InterceptedClamped.DefineClampedHouses(chart);
        var interceptedSigns = InterceptedClamped.DefineInterceptedSigns(chart);
        var groundNote = new List<int>();  // Groundnote: asc, mundaneHouseAsc,
        groundNote.Add(1); // start with ascendant as cusp
        groundNote.Add(asc);    // number of sign on ascendant
        // check sister signs   
        foreach (var rulerPair in BlaDomain.RulerPairs())
        {
            if (rulerPair.MainRuler == subRulerAsc)
            {
                var signIndex = rulerPair.SignIndex;
                foreach (var cuspSign in signsOnCusps)
                {
                    if (cuspSign.Value == signIndex)
                    {
                       groundNote.Add(cuspSign.Key);
                    }
                }
            }
        }
        // add houses with same ruler as ascendant
        foreach (var sign in signsOnCusps)
        {
            if (sign.Key != 1 && sign.Value == asc)
            {
                groundNote.Add(sign.Key);
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