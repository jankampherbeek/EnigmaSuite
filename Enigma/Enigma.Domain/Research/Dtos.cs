// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Domain.Persistency;
using Enigma.Domain.Research;

namespace Enigma.Research.Domain;


/// <summary>Abstract definition for a point that can be the result of a research test.</summary>
/// <param name="Id">Id that identifies the point.</param>
public abstract record ResearchPoint(int Id);


/// <summary>Define a celestial point that can be the result of a research test.</summary>
/// <param name="Id">Id that identifies the point.</param>
/// <param name="CelPoint">The celestial point.</param>
public record ResearchCelPoint(int Id, CelPoints CelPoint): ResearchPoint(Id);


/// <summary>Define a mundane point that can be the result of a research test.</summary>
/// <param name="Id">Id that identifies the point.</param>
/// <param name="MundanePoint">The mundane point.</param>
public record ResearchMundanePoint(int Id, MundanePoints MundanePoint): ResearchPoint(Id);

/// <summary>Define a zodiacal point that can be the result of a research test.</summary>
/// <param name="Id">Id that identifies the point.</param>
/// <param name="ZodiacalPoint">The zodiacal point.</param>
public record ResearchZodiacalPoint(int Id, ZodiacalPoints ZodiacalPoint) : ResearchPoint(Id);




/// <summary>Selection of points to use in research.</summary>
/// <param name="SelectedCelPoints">Selected celestial points.</param>
/// <param name="SelectedMundanePoints">Selected mundane points.</param>
/// <param name="SelectedZodiacalPoints">Selected zodiacal points.</param>
/// <param name="IncludeCusps">True if all cusps are used, otherwise false.</param>
public record ResearchPointsSelection(List<CelPoints> SelectedCelPoints, List<MundanePoints> SelectedMundanePoints, List<ZodiacalPoints> SelectedZodiacalPoints, bool IncludeCusps);


// TODO check if CelPointPerSign is still required.
public record CelPointPerSign(CelPoints celPoint, int[] positionsPerSign);


// TODO check if SignPerCelPoint is still required.
public record SignPerCelPoint(int signIndex, int[] celPointIndexes);


/// <summary>Inputdata for a chart in a research project.</summary>
/// <param name="CelPointPositions">All relevant positions for celstial points.</param>
/// <param name="FullHousePositions">All relevant mundane positions including cusps.</param>
/// <param name="InputItem"></param>
public record CalculatedResearchChart(List<FullCelPointPos> CelPointPositions, FullHousesPositions FullHousePositions, StandardInputItem InputItem);




/// <summary>Resulting counts from a test.</summary>
/// <param name="Point">The research point for which the counting has been performed.</param>
/// <param name="Counts">The totals in the sequence of the elements that have been checked.</param>
public record ResearchPointCounts(ResearchPoint Point, List<int> Counts);   // TODO check if ResearchPointCounts is still required.
