// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;


/// <summary>Details for an actual parallel and its forming partners.</summary>
/// <param name="Point1">The first point, in progressions the prog point.</param>
/// <param name="Point2">The second point, in progressions the radix point.</param>
/// <param name="OppParallel">True for opposition parallel, otherwise false.</param>
/// <param name="MaxOrb">Max allowed orb.</param>
/// <param name="ActualOrb">Actual orb.</param>
public record DefinedParallel(ChartPoints Point1, ChartPoints Point2, bool OppParallel, double MaxOrb, double ActualOrb);


/// <summary>List of parallels for a specific chart.</summary>
/// <remarks>Main usage is for research projects that involve the counting of aspects.</remarks>
/// <param name="ChartId">Unique id for the chart (unique within the existing dataset).</param>
/// <param name="DefinedParallels">The aspects that are effective for this chart.</param>
public record ParallelsPerChart(string ChartId, List<DefinedParallel> DefinedParallels);


/// <summary>Totals of parallel counts.</summary>
/// <param name="Points">All celestial points that have been used.</param>
/// <param name="MundanePoints">All mundane points that have been used, if any.</param>
/// <param name="Cusps">All cusps that have been used, if any.</param>
/// <param name="Totals">A 2d array with the counts.</param>
/// <remarks>The first index relates to the Points, MundanePoints and Cusps (in that sequence). </remarks>
/// <example>Note that all indexes start at zero.</example>
public record ParallelTotals(List<ChartPoints> Points, List<ChartPoints> MundanePoints, List<int> Cusps, int[,] Totals);