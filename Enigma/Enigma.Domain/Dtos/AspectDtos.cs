// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;


/// <summary>Details for an actual aspect and its forming partners.</summary>
/// <param name="Point1">The first point.</param>
/// <param name="Point2">The second point or a progressive point.</param>
/// <param name="Aspect">Details about the aspect.</param>
/// <param name="MaxOrb">Max allowed orb.</param>
/// <param name="ActualOrb">Actual orb.</param>
public record DefinedAspect(ChartPoints Point1, ChartPoints Point2, AspectDetails Aspect, double MaxOrb, double ActualOrb);


/// <summary>List of aspects for a specific chart.</summary>
/// <remarks>Main usage is for research projects that involve the counting of aspects.</remarks>
/// <param name="ChartId">Unique id for the chart (unique within the existing dataset).</param>
/// <param name="DefinedAspects">The aspects that are effective for this chart.</param>
public record AspectsPerChart(string ChartId, List<DefinedAspect> DefinedAspects);


/// <summary>Totals of aspect counts.</summary>
/// <param name="Points">All celestial points that have been used.</param>
/// <param name="MundanePoints">All mundane points that have been used, if any.</param>
/// <param name="Cusps">All cusps that have been used, if any.</param>
/// <param name="AspectTypes">All aspects that have been used.</param>
/// <param name="Totals">A 2d array with the counts.</param>
/// <remarks>The first index relates to the Points, MundanePoints and Cusps (in that sequence). The second index relates to the AspectTypes.</remarks>
/// <example>Note that all indexes start at zero. If the record contains 5 celestial points, no mundane points and 12 cusps, and 4 aspects, the position [7,2] refers to the third cusp and the third aspect.</example>
public record AspectTotals(List<ChartPoints> Points, List<ChartPoints> MundanePoints, List<int> Cusps, List<AspectTypes> AspectTypes, int[,] Totals);