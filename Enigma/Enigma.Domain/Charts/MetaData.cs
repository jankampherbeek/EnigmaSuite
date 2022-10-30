// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;

namespace Enigma.Domain.Charts;

/// <summary>
/// Metadata for a chart.
/// </summary>
public record MetaData
{
    public readonly string Name;
    public readonly string Description;
    public readonly string Source;
    public readonly ChartCategories ChartCategory;
    public readonly RoddenRatings RoddenRating;

    /// <summary>
    /// Constructor for record MetaData.
    /// </summary>
    /// <param name="name">Name for the chart.</param>
    /// <param name="description">A descriptive text, possibly an empty string.</param>
    /// <param name="source">An indication for the source, possibly an empty string.</param>
    /// <param name="chartCategory">The category for teh chart, from enum ChartCategories.</param>
    /// <param name="roddenRating">The Rodden Rating, from the enum RoddenRatings.</param>
    public MetaData(string name, string description, string source, ChartCategories chartCategory, RoddenRatings roddenRating)
    {
        Name = name;
        Description = description;
        Source = source;
        ChartCategory = chartCategory;
        RoddenRating = roddenRating;
    }
}
