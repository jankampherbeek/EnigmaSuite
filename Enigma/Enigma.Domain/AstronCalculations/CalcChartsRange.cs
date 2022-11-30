// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;
using System.Globalization;

namespace Enigma.Domain.CalcChartsRange;

/// <summary>Calculated data for a chart for a specific research item (person, event).</summary>
/// <param name="Id">The id for the item.</param>
/// <param name="CelPointPositions">Celpoints and positions.</param>
/// <param name="MundanePositions">Houses and other mundane positions.</param>
public record FullChartForResearchItem(string Id, List<FullCelPointPos> CelPointPositions, FullHousesPositions MundanePositions);


public record DataForCalculationOfRange(string Id, Location Location, SimpleDateTime DateTime, Calendar Cal);


public record ChartsRangeRequest(List<DataForCalculationOfRange> CalcData, CalculationPreferences Preferences);




