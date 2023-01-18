// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Points;

namespace Enigma.Domain.Research;


/// <summary>Parent for count results from a performed test.</summary>
/// <param name="Point">The research point.</param>
public abstract record MethodCount(ChartPoints Point);


/// <summary>Count result for parts (signs, houses etc.)</summary>
/// <param name="Point">The research point.</param>
/// <param name="Counts">List with results in the same sequence as the parts, always starting with zero.</param>
public record CountOfParts(ChartPoints Point, List<int> Counts) : MethodCount(Point);


/// <summary>Count for a single research point.</summary>
/// <param name="Point">The research point.</param>
/// <param name="Count">The counted result.</param>
public record SimpleCount(ChartPoints Point, int Count) : MethodCount(Point);


/// <summary>Count result for two research points.</summary>
/// <param name="Point">The first research point.</param>
/// <param name="Point2">The second research point.</param>
/// <param name="Count">The counted result.</param>
public record TwoPointCount(ChartPoints Point, ChartPoints Point2, int Count) : MethodCount(Point);


/// <summary>Count result for three research points.</summary>
/// <param name="Point">The first research point.</param>
/// <param name="Point2">The second research point.</param>
/// <param name="Point3">The third research point.</param>
/// <param name="Count">The counted result.</param>
public record ThreePointCount(ChartPoints Point, ChartPoints Point2, ChartPoints Point3, int Count) : MethodCount(Point);


/// <summary>Response with totals for counts of aspects.</summary>
/// <param name="Request">The original request.</param>
/// <param name="AllCounts">Three dimension array with counts. Dimensions: 1. ChartPoints, 2. Chartpoints ossibly including cusps, 3. Aspects.</param>
/// <param name="TotalsPerPointCombi">Totals for a specific combination of points.</param>
/// <param name="TotalsPerAspect">Totals for a specific aspect.</param>
/// <param name="PointsUsed">The supported points.</param>
/// <param name="AspectsUsed">The supported aspects.</param>
/// <remarks>The sequence of ChartPoints in PointUsed is the same as in the first two dimensions of AllCounts. The sequence of Aspects in AspectsUsed is the same as in the thrid dimension of AllCounts.</remarks>
public record CountOfAspectsResponse(GeneralResearchRequest Request, int[,,] AllCounts, int[,] TotalsPerPointCombi, int[] TotalsPerAspect, List<ChartPoints> PointsUsed, List<AspectTypes> AspectsUsed);

/// <summary>Parent for a response from a performed test.</summary>
/// <param name="Request">The original request.</param>
public abstract record MethodResponse(GeneralResearchRequest Request);


/// <summary>Response for counting of parts.</summary>
/// <param name="Request">The original request.</param>
/// <param name="Counts">All counted values.</param>
/// <param name="Totals">Totals of all positions per sign.</param>
public record CountOfPartsResponse(GeneralResearchRequest Request, List<CountOfParts> Counts, List<int> Totals) : MethodResponse(Request);





/// <summary>Response for counting unaspected points.</summary>
/// <param name="Request">The original request.</param>
/// <param name="Counts">All counted values.</param>
public record CountOfUnaspectedResponse(GeneralResearchRequest Request, List<SimpleCount> Counts) : MethodResponse(Request);

