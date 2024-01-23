// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>Metadata for a chart.</summary>
/// <param Name="Name">Name for the chart.</param>
/// <param Name="Description">A descriptive text, possibly an empty string.</param>
/// <param Name="Source">An indication for the Source, possibly an empty string.</param>
/// <param name="LocationName">Descriptive name for location.</param>
/// <param Name="ChartCategory">The category for the chart, contains id as used in database.</param>
/// <param Name="RoddenRating">The Rodden Rating, contains id as used in database.</param>
public record MetaData(string Name, string Description, string Source, string LocationName, long ChartCategory, 
    long RoddenRating);
