// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Points;
using System.Globalization;
using Enigma.Domain.References;

namespace Enigma.Domain.Calc.Specials;

/// <summary>Calculated data for a chart for a specific research item (person, event).</summary>
/// <param name="Id">The id for the item.</param>
/// <param name="Positions">All positions.</param>
public record FullChartForResearchItem(string Id, Dictionary<ChartPoints, FullPointPos> Positions);

/// <summary>Inputdata for the calculation of  a single item that is is used in a Charts Range.</summary>
/// <param name="Id">Id for the item.</param>
/// <param name="Location">Location with coordinates.</param>
/// <param name="DateTime">Date and time.</param>
/// <param name="Cal">Calendar.</param>
public record DataForCalculationOfRange(string Id, Location Location, SimpleDateTime DateTime, Calendar Cal);

/// <summary>Request for the calculation of a range of charts.</summary>
/// <param name="CalcData">Inputdata for a set of single charts.</param>
/// <param name="Preferences">Preferences for the calculation.</param>
public record ChartsRangeRequest(List<DataForCalculationOfRange> CalcData, CalculationPreferences Preferences);




