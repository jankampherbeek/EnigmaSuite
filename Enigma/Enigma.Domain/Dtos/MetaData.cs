// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

/// <summary>Metadata for a chart.</summary>
/// <param Name="Name">Name for the chart.</param>
/// <param Name="Description">A descriptive text, possibly an empty string.</param>
/// <param Name="Source">An indication for the Source, possibly an empty string.</param>
/// <param name="LocationName">Descriptive name for location.</param>
/// <param Name="ChartCategory">The category for the chart, from enum ChartCategories.</param>
/// <param Name="RoddenRating">The Rodden Rating, from the enum RoddenRatings.</param>
public record MetaData(string Name, string Description, string Source, string LocationName, ChartCategories ChartCategory, RoddenRatings RoddenRating);
