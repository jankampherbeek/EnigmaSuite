// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Charts;

/// <summary>Chart data to be shown in a data grid.</summary>
/// <param name="Id">Unique id for the chart.</param>
/// <param name="Name">Name or id for the chart owner / chart event.</param>
/// <param name="Description">Descriptive text, can be empty.</param>
public record PresentableChartData(string Id, string Name, string Description);