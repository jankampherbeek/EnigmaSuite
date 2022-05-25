// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.CalcVars;

/// <summary>
/// Types of charts. Describes the application of a chart.
/// </summary>
public enum ChartCategories
{
    Unknown = 0, Female = 1, Male = 2, Event = 3, Horary = 4, Election = 5
}

/// <summary>
/// Details for the Category of a chart.
/// </summary>
public record ChartCategoryDetails
{
    readonly public ChartCategories Category;
    readonly public string TextId;

    /// <summary>
    /// Construct details for a Chart category.
    /// </summary>
    /// <param name="category">The category of the chart.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public ChartCategoryDetails(ChartCategories category, string textId)
    {
        Category = category;
        TextId = textId;
    }
}

/// <summary>
/// Specifications for a Chart category.
/// </summary>
public interface IChartCategorySpecifications
{
    /// <summary>
    /// Returns the details for a Chart category.
    /// </summary>
    /// <param name="category">The category, from the enum ChartCategories.</param>
    /// <returns>A record ChartCategoryDetails with the specifications.</returns>
    public ChartCategoryDetails DetailsForCategory(ChartCategories category);

    /// <summary>
    /// Returns a value from the enum ChartCategories that corresponds with an index.
    /// </summary>
    /// <param name="chartCategoryIndex">The index for the requested item from ChartCategories. 
    /// Throws an exception if no ChartCategories for the given index does exist.</param>
    /// <returns>Instance from enum ChartCategories that corresponds with the given index.</returns>
    public ChartCategories ChartCategoryForIndex(int chartCategoryIndex);

}


public class ChartCategorySpecifications : IChartCategorySpecifications
{
    /// <exception cref="ArgumentException">Is thrown if the category was not recognized.</exception>
    public ChartCategoryDetails DetailsForCategory(ChartCategories category)
    {
        return category switch
        {
            ChartCategories.Female => new ChartCategoryDetails(category, "ref.enum.chartcategories.female"),
            ChartCategories.Male => new ChartCategoryDetails(category, "ref.enum.chartcategories.male"),
            ChartCategories.Event => new ChartCategoryDetails(category, "ref.enum.chartcategories.event"),
            ChartCategories.Horary => new ChartCategoryDetails(category, "ref.enum.chartcategories.horary"),
            ChartCategories.Election => new ChartCategoryDetails(category, "ref.enum.chartcategories.election"),
            ChartCategories.Unknown => new ChartCategoryDetails(category, "ref.enum.chartcategories.unknown"),
            _ => throw new ArgumentException("SolSysPointCats unknown : " + category.ToString())
        };
    }

    public ChartCategories ChartCategoryForIndex(int chartCategoryIndex)
    {
        foreach (ChartCategories category in Enum.GetValues(typeof(ChartCategories)))
        {
            if ((int)category == chartCategoryIndex) return category;
        }
        throw new ArgumentException("Could not find ChartCategories for index : " + chartCategoryIndex);
    }

}