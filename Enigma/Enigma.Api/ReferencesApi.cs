// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;

namespace Enigma.Api;

/// <summary>Api for retrieving references/look-up values.</summary>
public interface IReferencesApi
{
    /// <summary>Retrieve all ratings.</summary>
    /// <returns>Dictionary with indexes and names of ratings.</returns>
    public Dictionary<long, string> ReadAllRatings();

    /// <summary>Retrieve the name for a specific rating.</summary>
    /// <param name="index">The id of the rating.</param>
    /// <returns>If found, the name of the rating. Otherwise an empty string.</returns>
    public string ReadNameForeRating(int index);

    /// <summary>Retrieve all chart categories.</summary>
    /// <returns>Dictionary with indexes and names of chart categories.</returns>
    public Dictionary<long, string> ReadAllChartCategories();

    /// <summary>Retrieve the name for a specific chart category.</summary>
    /// <param name="index">The id of the category.</param>
    /// <returns>If found, the name of the category. Otherwise an empty string.</returns>
    public string ReadNameForChartCategory(int index);

    /// <summary>Retrieve all file formats.</summary>
    /// <returns>Dictionary with indexes and names of file formats.</returns>
    public Dictionary<long, string> ReadAllFileFormats();

    /// <summary>Retrieve the name for a specific file format.</summary>
    /// <param name="index">The id of the file format.</param>
    /// <returns>If found, the name of the category. Otherwise an empty string.</returns>
    public string ReadNameForFileFormat(int index);
}


/// <inheritdoc/>
public class ReferencesApi(IReferencesDao refDao) : IReferencesApi
{
    /// <inheritdoc/>
    public Dictionary<long, string> ReadAllRatings()
    {
        return refDao.ReadAllRatings();
    }

    /// <inheritdoc/>
    public string ReadNameForeRating(int index)
    {
        return refDao.ReadNameForRating(index);
    }

    /// <inheritdoc/>
    public Dictionary<long, string> ReadAllChartCategories()
    {
        return refDao.ReadAllChartCategories();
    }

    /// <inheritdoc/>
    public string ReadNameForChartCategory(int index)
    {
        return refDao.ReadNameForChartCategory(index);
    }

    public Dictionary<long, string> ReadAllFileFormats()
    {
        return refDao.ReadAllFileFormats();
    }

    public string ReadNameForFileFormat(int index)
    {
        return refDao.ReadNameForFileFormat(index);
    }
}