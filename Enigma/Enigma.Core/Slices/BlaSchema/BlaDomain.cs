// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;


/// <summary>
/// Fully specified data sheet for BLA schema calculations
/// </summary>
/// <param name="CrossesSignHouseCounts">Counts of points in crosses, first dictionary for signs, the second for houses</param>
/// <param name="ElementsSignHouseCounts">Counts of points in elements, first dictionary for signs, the second for houses</param>
/// <param name="QuadrantPositions">Counts of points per quadrant</param>
/// <param name="Dispositors">Dispositors for the BLA schema</param>
/// <param name="Decans">Decans</param>
/// <param name="DetailsData">Details for the BLA schema</param>
/// <param name="CyclesData">House cycles, separate for all elements and all crosses</param>
/// <param name="ShortenedCyclesData">Shortened house cycles, separate for all elements and all crosses</param>
/// <param name="Reinforcements">Several reinforcements</param>
public record BlaSchemaDataSheet( 
  
    Dictionary<int, BlaSignHouseCountLine> CrossesSignHouseCounts,
    Dictionary<int, BlaSignHouseCountLine> ElementsSignHouseCounts,
    Dictionary<int, int> QuadrantPositions,
    List<BlaDispositorLine> Dispositors,
    Dictionary<ChartPoints, int> Decans,
    BlaDetailsData DetailsData,
    BlaCyclesData CyclesData,
    BlaCyclesData ShortenedCyclesData,
    Reinforcements Reinforcements
);

/// <summary>
/// Several details for the BLA Schema that are combined in one visual rectangle
/// </summary>
/// <param name="SisterSignAsc">The sign that is ruled by the other point in the same ruler pair</param>
/// <param name="ClampedHouses">Houses that that not contain the beginning of a new sign</param>
/// <param name="InterceptedSigns">Signs that do not contain a house cusp</param>
/// <param name="GroundNote">Houses related to the ascendant, respectively the ascendant, mundane house ascendant
///     (same index for house as for sign on the as: Libra is 7), house with sister sign on cusp, house ruled by same
///     sign as ascendant (if any)</param>
/// <param name="LordAscInHouses">The house(s) where rulers of the ascendant are located</param>
/// <param name="MoonInSign">The ecliptical sign of the Moon</param>
public record BlaDetailsData(
    int SisterSignAsc,
    List<int> ClampedHouses,
    List<int> InterceptedSigns,
    List<int> GroundNote,
    List<int> LordAscInHouses,
    int MoonInSign);

/// <summary>
/// Cyclic connections in a BLA schema. Each connection conists of two houses (1..12). The ruler of the first house
/// is located in the second house.
/// </summary>
/// <param name="Cardinal">Cycles in cardinal houses</param>
/// <param name="Fixed">Cycles in fixed houses</param>
/// <param name="Mutable">Cycles in mutable houses</param>
/// <param name="Fire">Cycles in fire houses</param>
/// <param name="Earth">Cycles in earth houses</param>
/// <param name="Air">Cycles in air houses</param>
/// <param name="Water">Cycles in water houses</param>
public record BlaCyclesData(
    List<(int, int)> Cardinal,
    List<(int, int)> Fixed,
    List<(int, int)> Mutable,
    List<(int, int)> Fire,
    List<(int, int)> Earth,
    List<(int, int)> Air,
    List<(int, int)> Water
);

public record Reinforcements(    // todo augment
    Dictionary<ChartPoints, int> pointsInOwnSign,
    Dictionary<ChartPoints, int> pointsInOwnHouse,
    Dictionary<ChartPoints, int> pointsInOwnMundaneHouse
    );



/// <summary>
/// Counts for a sign, house and cusp in a BLA schema
/// </summary>
/// <param name="Sign">Count for the points in signs</param>
/// <param name="House">Count for the points in houses</param>
/// <param name="Sum">Sum of Sign and House</param>
/// <param name="HCusp"></param>
/// <param name="Total"></param>
public record BlaSignHouseCountLine(int Sign, int House, int Sum, int HCusp, int Total);

/// <summary>
/// Full specs of a BLA data line for dispositors (specs for a ruler pair)
/// </summary>
/// <param name="MainRuler">The main ruler in a ruler pair</param>
/// <param name="SubRuler">The sub ruler in a ruler pair</param>
/// <param name="MainRulerSignCount">Count of points in sign ruled by main ruler</param>
/// <param name="SubRulerSignCount">Count of points in sign ruler by sub ruler</param>
/// <param name="SumRulerSignCount">Sum of MainRulerSignCount and SubRulerSignCount</param>
/// <param name="IndirectRulerSignCount">Count of unique points in signs via indirect rulership</param>
/// <param name="TotalRulerSignCount">Total of IndirectRulerSignCount and SumRulerSignCount</param>
/// <param name="SumRulerHouseCount">Count of points in a house ruler by main ruler</param>
/// <param name="IndirectRulerHouseCount">Count of unique points in houses via indirect rulership</param>
/// <param name="TotalRulerHouseCount">Sum of SumRulerHouseCount and IndirectRulerHoouseCount</param>
/// <param name="Total">Sum of TotalRulerSignCount and TotalRulerHouseCount</param>
public record BlaDispositorLine(
    ChartPoints MainRuler,
    ChartPoints SubRuler,
    int MainRulerSignCount,
    int SubRulerSignCount,
    int SumRulerSignCount,
    int IndirectRulerSignCount,
    int TotalRulerSignCount,
    int SumRulerHouseCount,
    int IndirectRulerHouseCount,
    int TotalRulerHouseCount,
    int Total
);


/// <summary>
/// Details for a chart that are relevant for the BLA schema calculations
/// </summary>
/// TODO remove obsolete
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
/// TODO remove, obsolete
public record BlaPositions(ChartPoints Point, double Longitude, int Sign, int Decan, int House);


/// <summary>
/// Details for a chart point that are relevant for the BLA schema calculations
/// </summary>
/// <param name="Point">The chart point</param>
/// <param name="Longitude">Longitude in degrees </param>
/// <param name="Sign">Index for the ecliptical sign: 1..12</param>
/// <param name="House">Index for the house 1..12</param>
/// <param name="MainRuledSign">Sign that is ruled by the chart point as a main ruler</param>
/// <param name="SubRuledSign">Sign that is ruled by the chart point as a sub ruler</param>
/// <param name="MainRuledHouses">List with houses that are ruled by the chart point as main ruler</param>
/// <param name="SubRuledHouses">List with houses that are ruled by the chart point as sub ruler</param>
public record BlaPointDetails(ChartPoints Point, double Longitude, int Sign, int House, int MainRuledSign, int SubRuledSign, List<int> MainRuledHouses, List<int> SubRuledHouses);

/// <summary>
/// Details for a house that are relevant for the BLA schema calculations
/// </summary>
/// <param name="HouseNr">Index for the house 1..12</param>
/// <param name="SignOnCusp">Index for the sign on the cusp 1..12</param>
/// <param name="MainRuler">Main ruler of the house</param>
/// <param name="SubRuler">Sub ruler of the house</param>
/// <param name="PointsInHouse">Chart points that are positioned in the house</param>
public record BlaHouseDetails(int HouseNr, int SignOnCusp, ChartPoints MainRuler, ChartPoints SubRuler, List<ChartPoints> PointsInHouse );


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

    public static List<RulerPair> RulerPairs()
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
