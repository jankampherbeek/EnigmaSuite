// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

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


/// <summary>Parent for a response from a performed test.</summary>
/// <param name="Request">The original request.</param>
public abstract record MethodResponse(GeneralCountRequest Request);


/// <summary>Response for counting of parts.</summary>
/// <param name="Request">The original request.</param>
/// <param name="Counts">All counted values.</param>
/// <param name="Totals">Totals of all positions per sign.</param>
public record CountOfPartsResponse(GeneralCountRequest Request, List<CountOfParts> Counts, List<int> Totals) : MethodResponse(Request);





/// <summary>Response for counting unaspected points.</summary>
/// <param name="Request">The original request.</param>
/// <param name="Counts">All counted values.</param>
public record CountOfUnaspectedResponse(GeneralCountRequest Request, List<SimpleCount> Counts) : MethodResponse(Request);

