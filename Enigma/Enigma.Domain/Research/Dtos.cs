// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Persistency;
using Enigma.Domain.Points;

namespace Enigma.Research.Domain;


// TODO 0.1 Analysis

/*
/// <summary>Abstract definition for a point that can be the result of a research test.</summary>
/// <param name="Id">Id that identifies the point.</param>
public abstract record ResearchPoint(int Id, string Name);


/// <summary>Define a celestial point that can be the result of a research test.</summary>
/// <param name="Id">Id that identifies the point.</param>
/// <param name="Point">The celestial point.</param>
public record ResearchCelPoint(int Id, ChartPoints Point) : ResearchPoint(Id, Point.ToString());


/// <summary>Define a mundane point that can be the result of a research test.</summary>
/// <param name="Id">Id that identifies the point.</param>
/// <param name="MundanePoint">The mundane point.</param>
public record ResearchMundanePoint(int Id, ChartPoints MundanePoint) : ResearchPoint(Id, MundanePoint.ToString());


/// <summary>Define a cusp that can be the result of a research test.</summary>
/// <param name="Id">Number for the cusp.</param>
/// <param name="Name">Name for the cusp, e.g. 'Cusp 1'.</param>
public record ResearchCuspPoint(int Id, string Name) : ResearchPoint(Id, Name);
*/




/// <summary>Selection of points to use in research.</summary>
/// <param name="SelectedPoints">Selected celestial points.</param>
/// <param name="SelectedMundanePoints">Selected mundane points.</param>
/// <param name="IncludeCusps">True if all cusps are used, otherwise false.</param>
public record ResearchPointsSelection(List<ChartPoints> SelectedPoints, List<ChartPoints> SelectedMundanePoints, bool IncludeCusps);


/// <summary>Inputdata for a chart in a research project.</summary>
/// <param name="CelPointPositions">All relevant positions for celstial points.</param>
/// <param name="FullHousePositions">All relevant mundane positions including cusps.</param>
/// <param name="InputItem"></param>
public record CalculatedResearchChart(List<FullChartPointPos> CelPointPositions, FullHousesPositions FullHousePositions, StandardInputItem InputItem);


/// <summary>Instance of ResearchPoint with position.</summary>
/// <param name="Point">The research point.</param>
/// <param name="Position">The position.</param>
public record PositionedResearchPoint(ChartPoints Point, double Position);