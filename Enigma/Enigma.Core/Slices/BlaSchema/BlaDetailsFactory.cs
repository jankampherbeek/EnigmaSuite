// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Serilog;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Factory for ChartDetails
/// </summary>
public static class BlaDetailsFactory
{

    /// <summary>
    /// Define details for a chartpoint in a BLA schema</summary>
    /// <param name="point">The chart point</param>
    /// <param name="chart">Longitudes of points and cusps</param>
    /// <returns>A populated record with the details</returns>
    public static BlaPointDetails CreateBlaPointDetails(ChartPoints point, ChartLongitudes chart)
    {
        var longitude = 0.0;
        foreach (var p in chart.Points)
        {
            if (p.Key == point) longitude = p.Value;
        }

        var sign = (int)Math.Truncate(longitude / 30.0) + 1;
        var house = HousePositions.FindSingleHousePosition(chart, longitude);
        var (mainRuledSign, subRuledSign) = FindSignsForRuler(point);
        var (mainRuledHouses, subRuledHouses) = FindHousesForRuler(mainRuledSign, subRuledSign, chart.Cusps);
        return new BlaPointDetails(point, longitude, sign, house, mainRuledSign, subRuledSign, mainRuledHouses,
            subRuledHouses);
    }

    /// <summary>
    /// Define details for a house in a BLA schema.
    /// </summary>
    /// <param name="houseNr">The number of the house</param>
    /// <param name="chart">Longitudes of points and cusps</param>
    /// <returns>A populated record with the details</returns>
    public static BlaHouseDetails CreateBlaHouseDetails(int houseNr, ChartLongitudes chart)
    {
        var signOnCusp = FindSignOnCusp(houseNr, chart.Cusps);
        var (mainRuler, subRuler) = FindRulersForSign(signOnCusp);
        var pointsInHouse = new List<ChartPoints>();
        var allPointsInHouses = HousePositions.DefineHousePositions(chart);
        foreach (var point in allPointsInHouses)
        {
            if (point.Value == houseNr) pointsInHouse.Add(point.Key);
        }

        return new BlaHouseDetails(houseNr, signOnCusp, mainRuler, subRuler, pointsInHouse);
    }


    // Return signs that are ruled by a given point, main and sub, in that sequence
    private static (int, int) FindSignsForRuler(ChartPoints point)
    {
        var signMain = 0;
        var signSub = 0;
        foreach (var rulers in BlaDomain.RulerPairs())
        {
            if (rulers.MainRuler == point)
            {
                signMain = rulers.SignIndex;
            }
            else if (rulers.SubRuler == point)
            {
                signSub = rulers.SignIndex;
            }
        }

        return (signMain, signSub);
    }

    // Return rulers for a given sign, first main ruler, then sub ruler
    private static (ChartPoints, ChartPoints) FindRulersForSign(int sign)
    {
        if (sign is < 1 or > 12) throw new ArgumentOutOfRangeException(nameof(sign), "Sign must be between 1 and 12");
        foreach (var rulersPair in BlaDomain.RulerPairs())
        {
            if (rulersPair.SignIndex == sign)
            {
                return (rulersPair.MainRuler, rulersPair.SubRuler);
            }
        }

        throw new Exception("Could not find rulers for sign " + sign);
    }


    // Return houses that are ruled by a given sign, main and sub, in that sequence
    private static (List<int>, List<int>) FindHousesForRuler(int mainSign, int subSign, Dictionary<int, double> houses)
    {
        var housesMain = new List<int>();
        var housesSub = new List<int>();
        foreach (var (house, longitude) in houses)
        {
            var sign = (int)Math.Truncate(longitude / 30.0) + 1;
            if (sign == mainSign) housesMain.Add(house);
            if (sign == subSign) housesSub.Add(house);
        }

        return (housesMain, housesSub);
    }

    // Return sign (1..12) on cusp for a given house
    private static int FindSignOnCusp(int houseNr, Dictionary<int, double> houses)
    {
        var signOnCusp = 0;
        foreach (var (house, longitude) in houses)
        {
            if (house == houseNr)
            {
                signOnCusp = (int)Math.Truncate(longitude / 30.0) + 1;
            }
        }

        return signOnCusp;
    }

}
