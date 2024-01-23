// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Data.SQLite;
using Dapper;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Core.Persistency;

/// <summary>DAO for references/lookup values. Only supports read-access.</summary>
public interface IReferencesDao
{
    /// <summary>Read all ratings.</summary>
    /// <returns>Dictionary with id and name for ratings.</returns>
    Dictionary<long, string> ReadAllRatings();
    
    /// <summary>Read name for a specific rating.</summary>
    /// <remarks>Logs an error if rating was not found.</remarks>
    /// <param name="index">The id of the rating.</param>
    /// <returns>If found: the name of the rating, otherwise an empty string.</returns>
    string ReadNameForRating(int index);
    
    /// <summary>Read all chart categories.</summary>
    /// <returns>Dictionary with id and name for chart categories.</returns>
    Dictionary<long, string> ReadAllChartCategories();
    
    /// <summary>Read name for a specific chart category.</summary>
    /// <remarks>Logs an error if chart category was not found.</remarks>
    /// <param name="index">The id of the chart category.</param>
    /// <returns>If found: the name of the chart category, otherwise an empty string.</returns>
    string ReadNameForChartCategory(int index);

    /// <summary>Read all file formats.</summary>
    /// <returns>Dictionary with id and name for file formats.</returns>
    Dictionary<long, string> ReadAllFileFormats();

    /// <summary>Read name for a specific file format</summary>
    /// <remarks>Logs an error if the file format was not found.</remarks>/// 
    /// <param name="index">The id of te file format.</param>
    /// <returns>If found: the name of the file format, otherwise an empty string.</returns>
    string ReadNameForFileFormat(int index);

}


public class ReferencesDao : IReferencesDao
{
    private const string DATA_SOURCE_PREFIX = "Data Source=";
    
    public Dictionary<long, string> ReadAllRatings()
    {
        string fullPath = ApplicationSettings.LocationDatabase + EnigmaConstants.RDBMS_NAME;
        SQLiteConnection dbConnection = new(DATA_SOURCE_PREFIX + fullPath);
        const string sqlQuery = "SELECT * FROM Ratings";
        using var cnn = dbConnection;
        var ratings = cnn.Query(sqlQuery).ToList();
        return ratings.ToDictionary<dynamic, long, string>(rating => rating.id, rating => rating.name);
    }

    public string ReadNameForRating(int index)
    {
        string ratingNameResult = "";
        string fullPath = ApplicationSettings.LocationDatabase + EnigmaConstants.RDBMS_NAME;
        SQLiteConnection dbConnection = new(DATA_SOURCE_PREFIX + fullPath);
        const string sqlQuery = "SELECT name FROM Ratings where id = @ratingIndex";
        using var cnn = dbConnection;
        var names = cnn.Query(sqlQuery, new { ratingIndex = index }).ToList();
        if (names.Count > 0) ratingNameResult = names[0].name;
        else Log.Error("ReferencesDao.ReadNameForRating: could not find rating for {Index}", index);
        return ratingNameResult;
    }

    public Dictionary<long, string> ReadAllChartCategories()
    {
        string fullPath = ApplicationSettings.LocationDatabase + EnigmaConstants.RDBMS_NAME;
        SQLiteConnection dbConnection = new(DATA_SOURCE_PREFIX + fullPath);
        const string sqlQuery = "SELECT * FROM ChartCategories";
        using var cnn = dbConnection;
        var categories = cnn.Query(sqlQuery).ToList();
        return categories.ToDictionary<dynamic, long, string>(category => category.id, category => category.name);
    }

    public string ReadNameForChartCategory(int index)
    {
        string categoryNameResult = "";
        string fullPath = ApplicationSettings.LocationDatabase + EnigmaConstants.RDBMS_NAME;
        SQLiteConnection dbConnection = new(DATA_SOURCE_PREFIX + fullPath);
        const string sqlQuery = "SELECT name FROM ChartCategories where id = @catIndex";
        using var cnn = dbConnection;
        var names = cnn.Query(sqlQuery, new { catIndex = index }).ToList();
        if (names.Count > 0) categoryNameResult = names[0].name;
        else Log.Error("ReferencesDao.ReadNameForChartCategory: could not find category for {Index}", index);
        return categoryNameResult;
    }

    public Dictionary<long, string> ReadAllFileFormats()
    {
        string fullPath = ApplicationSettings.LocationDatabase + EnigmaConstants.RDBMS_NAME;
        SQLiteConnection dbConnection = new(DATA_SOURCE_PREFIX + fullPath);
        const string sqlQuery = "SELECT * FROM FileFormats";
        using var cnn = dbConnection;
        var fileFormats = cnn.Query(sqlQuery).ToList();
        return fileFormats.ToDictionary<dynamic, long, string>(fileFormat => fileFormat.id, fileFormat => fileFormat.name);
    }

    public string ReadNameForFileFormat(int index)
    {
        string fileFormatNameResult = "";
        string fullPath = ApplicationSettings.LocationDatabase + EnigmaConstants.RDBMS_NAME;
        SQLiteConnection dbConnection = new(DATA_SOURCE_PREFIX + fullPath);
        const string sqlQuery = "SELECT name FROM FileFormats where id = @fileFormatIndex";
        using var cnn = dbConnection;
        var names = cnn.Query(sqlQuery, new { fileFormatIndex = index }).ToList();
        if (names.Count > 0) fileFormatNameResult = names[0].name;
        else Log.Error("ReferencesDao.ReadNameForFileFormat: could not find file format for {Index}", index);
        return fileFormatNameResult;
    }
}