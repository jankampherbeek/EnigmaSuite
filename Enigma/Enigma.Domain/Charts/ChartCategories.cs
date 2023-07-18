// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Charts;

/// <summary>Types of charts. Describes the application of a chart.</summary>
public enum ChartCategories
{
    Unknown = 0, Female = 1, Male = 2, Event = 3, Horary = 4, Election = 5
}

/// <summary>Details for the Category of a chart.</summary>
/// <param name="Category">The Category of the chart.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record ChartCategoryDetails(ChartCategories Category, string TextId);


/// <summary>Extension class for enum ChartCategories.</summary>
public static class ChartCategoriesExtensions
{
    /// <summary>Retrieve details for chart categories.</summary>
    /// <returns>Details for the chart category.</returns>
    public static ChartCategoryDetails GetDetails(this ChartCategories cat)
    {
        return cat switch
        {
            ChartCategories.Female => new ChartCategoryDetails(cat, "ref.enum.chartcategories.female"),
            ChartCategories.Male => new ChartCategoryDetails(cat, "ref.enum.chartcategories.male"),
            ChartCategories.Event => new ChartCategoryDetails(cat, "ref.enum.chartcategories.event"),
            ChartCategories.Horary => new ChartCategoryDetails(cat, "ref.enum.chartcategories.horary"),
            ChartCategories.Election => new ChartCategoryDetails(cat, "ref.enum.chartcategories.election"),
            ChartCategories.Unknown => new ChartCategoryDetails(cat, "ref.enum.chartcategories.unknown"),
            _ => throw new ArgumentException("CelPointCats unknown : " + cat)
        };
    }

    /// <summary>Retrieve details for items in the enum ChartCategories.</summary>
    /// <param name="_">The chart category, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<ChartCategoryDetails> AllDetails(this ChartCategories _)
    {
        return (from ChartCategories chartCat in Enum.GetValues(typeof(ChartCategories)) select chartCat.GetDetails()).ToList();
    }

    /// <summary>Find chart category for an index.</summary>
    /// <param name="index">The index to look for.</param>
    /// <returns>The chart category for the index.</returns>
    /// <exception cref="ArgumentException">Thrown if no chart category exists for given index.</exception>
    public static ChartCategories ChartCategoryForIndex(this ChartCategories _, int index)
    {
        foreach (ChartCategories chartCat in Enum.GetValues(typeof(ChartCategories)))
        {
            if ((int)chartCat == index) return chartCat;
        }
        string errorText = "ChartCategories.ChartCategoryForIndex(): Could not find chart category for index : " + index;
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }

}


