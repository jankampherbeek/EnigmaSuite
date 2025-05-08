// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Data.SQLite;
using Dapper;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Core.Persistency;

/// <summary>DAO for research data files and projects</summary>
public interface IDataFileDao
{
    /// <summary>Inserts a new data file into the database.</summary>
    /// <param name="dataFile">The data file to insert.</param>
    /// <returns>The ID of the newly inserted data file, or -1 if the insert failed.</returns>
    public int InsertDataFile(DataFileDto dataFile);

    /// <summary>Deletes a datafile from the database</summary>
    /// <param name="id">The id of the data file</param>
    /// <returns>True if the record was deleted, false if the record was not found or could not be deleted</returns>
    public bool DeleteDataFile(int id);

    /// <summary>Checks if a data name is available in the database.</summary>
    /// <param name="name">Name of the data to check.</param>
    /// <returns>True if the name is available (not in use), otherwise false.</returns>
    public bool IsDataNameAvailable(string name);

    /// <summary>Gets all data files from the database.</summary>
    /// <returns>List of all data files.</returns>
    public List<DataFileDto> AllDataFiles();

    /// <summary>Reads a data file from the database by its ID.</summary>
    /// <param name="id">The ID of the data file to read.</param>
    /// <returns>The data file, or null if not found.</returns>
    public DataFileDto? ReadDataFile(int id);

    /// <summary>Reads a data file from the database by its name.</summary>
    /// <param name="name">The name of the data file to read.</param>
    /// <returns>The data file, or null if not found.</returns>
    public DataFileDto? ReadDataFile(string name);

    /// <summary>Returns all names of projects that use this datafile</summary>
    /// <param name="name">Name of the datafile</param>
    /// <returns>List of project names</returns>
    public List<string> ReadProjectsForDataFile(string name);
}

/// <inheritdoc/>
public class DataFileDao: IDataFileDao
{
    /// <inheritdoc/>
    public int InsertDataFile(DataFileDto dataFile)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            const string insertQuery = """
                                       INSERT INTO DataFiles(name, location)
                                       VALUES(@Name, @Location);
                                       SELECT last_insert_rowid();
                                       """;

            var id = dbConnection.Query<int>(insertQuery, dataFile).FirstOrDefault();
            Log.Information("Inserted data file {Name} at {Location} with ID {Id}", 
                dataFile.Name, dataFile.Location, id);
            return id;
        }
        catch (Exception e)
        {
            Log.Error("Error inserting data file {Name} at {Location}. Exception: {Msg}", 
                dataFile.Name, dataFile.Location, e.Message);
            return -1;
        }
    }

    /// <inheritdoc/>
    public bool DeleteDataFile(int id)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            
            // First check if the datafile is used by any project
            const string checkUsageQuery = """
                SELECT COUNT(*) 
                FROM Projects 
                WHERE datafile = @Id
                """;
            var usageCount = dbConnection.Query<int>(checkUsageQuery, new { Id = id }).FirstOrDefault();
            if (usageCount > 0)
            {
                Log.Warning("Cannot delete data file with ID {Id} because it is used by {Count} projects", 
                    id, usageCount);
                return false;
            }
            // If not used, proceed with deletion
            const string deleteQuery = """
                DELETE FROM DataFiles 
                WHERE id = @Id
                """;
            var affectedRows = dbConnection.Execute(deleteQuery, new { Id = id });
            var success = affectedRows > 0;
            if (success)
            {
                Log.Information("Successfully deleted data file with ID {Id}", id);
            }
            else
            {
                Log.Warning("No data file found with ID {Id}", id);
            }
            return success;
        }
        catch (Exception e)
        {
            Log.Error("Error deleting data file with ID {Id}. Exception: {Msg}", id, e.Message);
            return false;
        }
    }

    /// <inheritdoc/>
    public bool IsDataNameAvailable(string name)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();

            const string query = "SELECT COUNT(*) FROM DataFiles WHERE name = @Name";
            var count = dbConnection.ExecuteScalar<int>(query, new { Name = name });
            
            Log.Information("Checked if data name {Name} is available. Result: {Available}", 
                name, count == 0);
            
            return count == 0;
        }
        catch (Exception e)
        {
            Log.Error("Error checking data name availability for {Name}. Exception: {Msg}", 
                name, e.Message);
            return false;
        }
    }

    /// <inheritdoc/>
    public List<DataFileDto> AllDataFiles()
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();

            const string query = "SELECT id, name, location FROM DataFiles ORDER BY name";
            var dataFiles = dbConnection.Query<DataFileDto>(query).ToList();
            
            Log.Information("Retrieved {Count} data files from database", dataFiles.Count);
            return dataFiles;
        }
        catch (Exception e)
        {
            Log.Error("Error reading data files from database: {Message}", e.Message);
            return new List<DataFileDto>();
        }
    }

    /// <inheritdoc/>
    public DataFileDto? ReadDataFile(int id)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();

            const string query = "SELECT id, name, location FROM DataFiles WHERE id = @Id";
            var dataFile = dbConnection.QueryFirstOrDefault<DataFileDto>(query, new { Id = id });
            
            if (dataFile == null)
            {
                Log.Warning("No data file found with ID {Id}", id);
            }
            else
            {
                Log.Information("Read data file with ID {Id}, name {Name}, location {Location}", 
                    id, dataFile.Name, dataFile.Location);
            }
            
            return dataFile;
        }
        catch (Exception e)
        {
            Log.Error("Error reading data file with ID {Id}. Exception: {Msg}", 
                id, e.Message);
            return null;
        }
    }

    /// <inheritdoc/>
    public DataFileDto? ReadDataFile(string name)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();

            const string query = "SELECT id, name, location FROM DataFiles WHERE name = @Name";
            var dataFile = dbConnection.QueryFirstOrDefault<DataFileDto>(query, new { Name = name });
            
            if (dataFile == null)
            {
                Log.Warning("No data file found with name {Name}", name);
            }
            else
            {
                Log.Information("Read data file with name {Name}", name);
            }
            
            return dataFile;
        }
        catch (Exception e)
        {
            Log.Error("Error reading data file with name {Name}. Exception: {Msg}", 
                name, e.Message);
            return null;
        }
    }

    /// <inheritdoc/>
    public List<string> ReadProjectsForDataFile(string name)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();

            const string query = """
                SELECT p.name 
                FROM Projects p
                INNER JOIN DataFiles d ON p.datafile = d.id
                WHERE d.name = @Name
                ORDER BY p.name
                """;
            
            var projectNames = dbConnection.Query<string>(query, new { Name = name }).ToList();

            Log.Information($"Found {projectNames.Count} projects using data file {name}");
            return projectNames;
        }
        catch (Exception e)
        {
            Log.Error($"Error reading projects for data file {name}. Exception: {e.Message}");
            return [];
        }
    }
}
