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
/// <param name="MoonInHouse">The house of the Moon</param>
public record BlaDetailsData(
    int SisterSignAsc,
    List<int> ClampedHouses,
    List<int> InterceptedSigns,
    List<int> GroundNote,
    List<int> LordAscInHouses,
    int MoonInHouse);

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

public record Reinforcements(    
    Dictionary<ChartPoints, int> PointsInOwnSign,
    Dictionary<ChartPoints, int> PointsInOwnHouse,
    Dictionary<ChartPoints, int> PointsInOwnMundaneHouse,
    Dictionary<ChartPoints, int> RulersInHouseAsSign,
    List<FactorPairAnalogHouseSign> FactorPairs,
    List<Reception> ReceptionsInSigns,
    List<Reception> ReceptionsInHouses,
    List<Reception> ReceptionsInMundaneHouses);

/// <summary>
/// Factor pair in analog house and sign
/// </summary>
/// <param name="PointInSign">The point in a sign</param>
/// <param name="Sign">Index of the sign 1..12</param>
/// <param name="PointInHouse">The point in an analog house</param>
/// <param name="House">Index of the house 1..12</param>
public record FactorPairAnalogHouseSign(
    ChartPoints PointInSign,
    int Sign,
    ChartPoints PointInHouse,
    int House);

/// <summary>
/// Reception of 2 points, either in signs or in  houses
/// </summary>
/// <param name="Point1">First point</param>
/// <param name="SignOrHouse1">Index of sign or of house of first point</param>
/// <param name="Point2">Second point</param>
/// <param name="SignOrHouse2">Index of sign of or house of second point</param>
public record Reception(
    ChartPoints Point1,
    int SignOrHouse1,
    ChartPoints Point2,
    int SignOrHouse2);

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
/// <param name="DirectRulerHouseCount">Count of points in a house ruler by main ruler</param>
/// <param name="IndirectRulerHouseCount">Count of unique points in houses via indirect rulership</param>
/// <param name="SumRulerHouseCount">Sum of SumRulerHouseCount and IndirectRulerHoouseCount</param>
/// <param name="DirectRulerDecanataCount">Count of points in a decanate ruler by main ruler</param>
/// <param name="Total">Sum of TotalRulerSignCount and TotalRulerHouseCount</param>
public record BlaDispositorLine(
    ChartPoints MainRuler,
    ChartPoints SubRuler,
    int MainRulerSignCount,
    int SubRulerSignCount,
    int SumRulerSignCount,
    int IndirectRulerSignCount,
    int TotalRulerSignCount,
    int DirectRulerHouseCount,
    int IndirectRulerHouseCount,
    int SumRulerHouseCount,
    int DirectRulerDecanataCount,
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
/// <param name="Decanate">Index for the decanate: 1..7</param>
/// <param name="House">Index for the house 1..12</param>
/// <param name="MainRuledSign">Sign that is ruled by the chart point as a main ruler</param>
/// <param name="SubRuledSign">Sign that is ruled by the chart point as a sub ruler</param>
/// <param name="MainRuledHouses">List with houses that are ruled by the chart point as main ruler</param>
/// <param name="SubRuledHouses">List with houses that are ruled by the chart point as sub ruler</param>
public record BlaPointDetails(ChartPoints Point, double Longitude, int Sign, int Decanate, int House, int MainRuledSign, 
    int SubRuledSign, List<int> MainRuledHouses, List<int> SubRuledHouses);

/// <summary>
/// Details for a house that are relevant for the BLA schema calculations
/// </summary>
/// <param name="HouseNr">Index for the house 1..12</param>
/// <param name="SignOnCusp">Index for the sign on the cusp 1..12</param>
/// <param name="Decanate">Index for the decanate: 1..7</param>
/// <param name="Longitude">Longitude in degrees</param>
/// <param name="MainRuler">Main ruler of the house</param>
/// <param name="SubRuler">Sub ruler of the house</param>
/// <param name="PointsInHouse">Chart points that are positioned in the house</param>
public record BlaHouseDetails(int HouseNr, int SignOnCusp, int Decanate, double Longitude, ChartPoints MainRuler, ChartPoints SubRuler, List<ChartPoints> PointsInHouse );


/// <summary>
/// Combination of ruler and subruler
/// </summary>
/// <param name="SignIndex">Index for the sign: 1..12</param>
/// <param name="MainRuler">ChartPoint for the ruler</param>
/// <param name="SubRuler">ChartPoint for the subruler</param>
public record RulerPair(int SignIndex, ChartPoints MainRuler, ChartPoints SubRuler);

/// <summary>
/// Ruler and the signs it rules
/// </summary>
/// <param name="Ruler">The ruler</param>
/// <param name="MainSign">The sign where the ruler is the mainruler</param>
/// <param name="SubSign">The sign where the ruler is the subruler</param>
public record RulerAndSigns(ChartPoints Ruler, int MainSign, int SubSign);


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

    /// <summary>
    /// Define a list with all rulers and the signs they rule
    /// </summary>
    /// <returns>The list with the rulers</returns>
    public static List<RulerAndSigns> AllRulerAndSigns()
    {
        var rulerSigns = new List<RulerAndSigns>()
        {
            new(ChartPoints.Sun, 5, 10),
            new(ChartPoints.Moon, 4, 11),
            new(ChartPoints.Mercury, 3, 6),
            new(ChartPoints.Venus, 2, 7),
            new(ChartPoints.Mars, 1, 8),
            new(ChartPoints.Jupiter, 9, 12),
            new(ChartPoints.Neptune, 12, 9),
            new(ChartPoints.Pluto, 8, 1),
            new(ChartPoints.PersephoneCarteret, 7, 2),
            new(ChartPoints.VulcanusCarteret, 6, 3),
            new(ChartPoints.ApogeeMean, 10, 5),
            new(ChartPoints.Priapus, 11, 4)
        };
        return rulerSigns;   
    }
    

    /// <summary>
    /// Create logical pairs of factors
    /// </summary>
    /// <returns>The create factors</returns>
    public static List<(ChartPoints, ChartPoints)> FactorPairs()
    {
        var pairs = new List<(ChartPoints, ChartPoints)>
        {
            (ChartPoints.Mars, ChartPoints.Pluto),
            (ChartPoints.Venus, ChartPoints.PersephoneCarteret),
            (ChartPoints.Mercury, ChartPoints.VulcanusCarteret),
            (ChartPoints.Moon, ChartPoints.Priapus),
            (ChartPoints.Sun, ChartPoints.ApogeeMean),
            (ChartPoints.Jupiter, ChartPoints.Neptune),
            (ChartPoints.Saturn, ChartPoints.Uranus),
            (ChartPoints.NorthNode, ChartPoints.SouthNode),
            (ChartPoints.NorthNode, ChartPoints.Beast),
            (ChartPoints.NorthNode, ChartPoints.Dragon),
            (ChartPoints.SouthNode, ChartPoints.Beast),
            (ChartPoints.SouthNode, ChartPoints.Dragon),
            (ChartPoints.Beast, ChartPoints.Dragon)
        };
        return pairs;
    }

    
    public record DecanateRuler(ChartPoints Ruler, int Decan);
    
    public static List<DecanateRuler> DecanateRulers()
    {
        var decRulers = new List<DecanateRuler>
        {
            { new(ChartPoints.Mars, 1) },
            { new(ChartPoints.Sun, 2) },
            { new(ChartPoints.Venus, 3) },
            { new(ChartPoints.Mercury, 4) },
            { new(ChartPoints.Moon, 5) },
            { new(ChartPoints.Saturn, 6) },
            { new(ChartPoints.Jupiter, 7) },
        };
        return decRulers;   
    }
}
