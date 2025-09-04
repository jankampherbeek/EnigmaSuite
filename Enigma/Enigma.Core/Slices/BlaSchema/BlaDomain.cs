// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Solar;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;


/// <summary>
/// Details for a chart that are relevant for the BLA schema calculations
/// </summary>
public record BlaChartDetails(
    List<BlaPositions> SignsDecansHouses, 
    List<int> InterceptedSigns, 
    List<int> ClampedHouses,
    Dictionary<ChartPoints, int> Houses,
    Dictionary<int, int> QuadrantCounts,
    Dictionary<int, int> SignCounts,
    Dictionary<int, int> HouseCounts,
    List<RulerPair> SignRulers,
    Dictionary<int, List<RulerPair>> HouseRulers);


/// <summary>
/// Positions for Black Lights Astrology calculations
/// </summary>
/// <param name="Longitude">Ecliptical longitude</param>
/// <param name="Point">The chart point</param>
/// <param name="Sign">Nr of the sign, 1 = Aries,.. 12 = Pisces</param>
/// <param name="Decan">Nr of the decans: 1 = Mars, 2 = Sun, 3 Venus, 4 = Mercury, 5 = Moon 6 = Saturn, 7 = Jupiter</param>
/// /// <param name="House">Nr of the house, 1..12</param>
public record BlaPositions(ChartPoints Point, double Longitude, int Sign, int Decan, int House);


/// <summary>
/// Combination of ruler and subruler
/// </summary>
/// <param name="SignIndex">Index for the sign: 1..12</param>
/// <param name="MainRuler">ChartPoint for the ruler</param>
/// <param name="SubRuler">ChartPoint for the subruler</param>
public record RulerPair(int SignIndex, ChartPoints MainRuler, ChartPoints SubRuler);


/// <summary>
/// Domain for Black Lights Astrology
/// </summary>
public static class BlaDomain
{

    public static List<RulerPair> SignRulers()
    {
        var rulers = new List<RulerPair>
        {
            new(1,ChartPoints.Mars, ChartPoints.Pluto),
            new(2,ChartPoints.Venus, ChartPoints.PersephoneCarteret),
            new(3,ChartPoints.Mercury, ChartPoints.VulcanusCarteret),
            new(4,ChartPoints.Moon, ChartPoints.Priapus),
            new(5, ChartPoints.Sun, ChartPoints.ApogeeMean),
            new(6, ChartPoints.VulcanusCarteret, ChartPoints.Mercury),
            new(7, ChartPoints.PersephoneCarteret, ChartPoints.Venus),
            new(8, ChartPoints.Pluto, ChartPoints.Mars),
            new(9, ChartPoints.Jupiter, ChartPoints.Neptune),
            new(10, ChartPoints.ApogeeMean, ChartPoints.Sun),
            new(11, ChartPoints.Priapus, ChartPoints.Moon),
            new(12, ChartPoints.Neptune, ChartPoints.Jupiter)
        };
        return rulers;
    }
    
 
    
}
