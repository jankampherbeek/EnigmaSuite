// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistency;
using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Domain.Research;

/// <summary>Selection of points to use in research.</summary>
/// <param name="SelectedPoints">Selected chart points.</param>
/// <param name="IncludeCusps">True if all cusps are used, otherwise false.</param>
public record ResearchPointsSelection(List<ChartPoints> SelectedPoints, bool IncludeCusps);


/// <summary>Positions and inputdata for a chart in a research project.</summary>
/// <param name="Positions">All relevant positions for celestial points.</param>
/// <param name="InputItem">Inputted data.</param>
public record CalculatedResearchChart(Dictionary<ChartPoints, FullPointPos> Positions, StandardInputItem InputItem);


/// <summary>Instance of ResearchPoint with position.</summary>
/// <param name="Point">The research point.</param>
/// <param name="Position">The position.</param>
public record PositionedResearchPoint(ChartPoints Point, double Position);

/// <summary>Definition of points that should be excluded when performing a research action.</summary>
/// <remarks>One of these records should be used to specify the exclusions when using a specific research method.</remarks>
/// <param name="ExcludedPoints">List of ChartPoints to exclude.</param>
/// <param name="ExcludeCusps">True if cusps should be excluded. Angles are defined as part of the excludedpoints.</param>
public record PointsToExclude(List<ChartPoints> ExcludedPoints, bool ExcludeCusps);