// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Domain.Charts;

/// <summary>Metadata for a chart.</summary>
/// <param Name="Name">Name for the chart.</param>
/// <param Name="Description">A descriptive text, possibly an empty string.</param>
/// <param Name="Source">An indication for the Source, possibly an empty string.</param>
/// <param Name="ChartCategory">The category for teh chart, from enum ChartCategories.</param>
/// <param Name="RoddenRating">The Rodden Rating, from the enum RoddenRatings.</param>
public record MetaData(string Name, string Description, string Source, ChartCategories ChartCategory, RoddenRatings RoddenRating);
