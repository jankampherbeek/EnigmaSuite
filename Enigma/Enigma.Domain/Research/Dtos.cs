// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Persistency;

namespace Enigma.Research.Domain;


/// <summary>Abstract definition for a point that can be the result of a research test.</summary>
/// <param name="Id">Id that identifies the point.</param>
public abstract record ResearchPoint(int Id, string Name);


/// <summary>Define a celestial point that can be the result of a research test.</summary>
/// <param name="Id">Id that identifies the point.</param>
/// <param name="CelPoint">The celestial point.</param>
public record ResearchCelPoint(int Id, CelPoints CelPoint) : ResearchPoint(Id, CelPoint.ToString());


/// <summary>Define a mundane point that can be the result of a research test.</summary>
/// <param name="Id">Id that identifies the point.</param>
/// <param name="MundanePoint">The mundane point.</param>
public record ResearchMundanePoint(int Id, MundanePoints MundanePoint) : ResearchPoint(Id, MundanePoint.ToString());


/// <summary>Define a cusp that can be the result of a research test.</summary>
/// <param name="Id">Number for the cusp.</param>
/// <param name="Name">Name for the cusp, e.g. 'Cusp 1'.</param>
public record ResearchCuspPoint(int Id, string Name) : ResearchPoint(Id, Name);

/// <summary>Define a zodiacal point that can be the result of a research test.</summary>
/// <param name="Id">Id that identifies the point.</param>
/// <param name="ZodiacalPoint">The zodiacal point.</param>
public record ResearchZodiacalPoint(int Id, ZodiacalPoints ZodiacalPoint) : ResearchPoint(Id, ZodiacalPoint.ToString());




/// <summary>Selection of points to use in research.</summary>
/// <param name="SelectedCelPoints">Selected celestial points.</param>
/// <param name="SelectedMundanePoints">Selected mundane points.</param>
/// <param name="IncludeCusps">True if all cusps are used, otherwise false.</param>
public record ResearchPointsSelection(List<CelPoints> SelectedCelPoints, List<MundanePoints> SelectedMundanePoints, bool IncludeCusps);


// TODO check if CelPointPerSign is still required.
public record CelPointPerSign(CelPoints CelPoint, int[] PositionsPerSign);


// TODO check if SignPerCelPoint is still required.
public record SignPerCelPoint(int SignIndex, int[] CelPointIndexes);


/// <summary>Inputdata for a chart in a research project.</summary>
/// <param name="CelPointPositions">All relevant positions for celstial points.</param>
/// <param name="FullHousePositions">All relevant mundane positions including cusps.</param>
/// <param name="InputItem"></param>
public record CalculatedResearchChart(List<FullCelPointPos> CelPointPositions, FullHousesPositions FullHousePositions, StandardInputItem InputItem);


