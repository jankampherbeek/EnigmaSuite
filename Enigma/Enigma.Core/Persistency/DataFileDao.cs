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

/// <summary>Data Transfer Object for DataFiles table.</summary>
public class DataFileDto
{
    /// <summary>Name of the data file.</summary>
    public string Name { get; set; } = string.Empty;
        
    /// <summary>Location of the data file.</summary>
    public string Location { get; set; } = string.Empty;
}


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

    /// <summary>Inserts a new project into the database.</summary>
    /// <param name="project">The project to insert.</param>
    /// <returns>The ID of the newly inserted project, or -1 if the insert failed.</returns>
    public int InsertProject(ProjectDto project);

    /// <summary>Deletes a project from the database</summary>
    /// <param name="id">The id of the project</param>
    /// <returns>True if the record was deleted, false if the record was not found or could not be deleted</returns>
    public bool DeleteProject(int id);
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
    public int InsertProject(ProjectDto project)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            const string insertQuery = """
                                       INSERT INTO Projects(name, description, location, multiFactor, created, datafile)
                                       VALUES(@Name, @Description, @Location, @MultiFactor, @Created, @DataFile);
                                       SELECT last_insert_rowid();
                                       """;

            var id = dbConnection.Query<int>(insertQuery, project).FirstOrDefault();
            Log.Information("Inserted project {Name} at {Location} with ID {Id}", 
                project.Name, project.Location, id);
            return id;
        }
        catch (Exception e)
        {
            Log.Error("Error inserting project {Name} at {Location}. Exception: {Msg}", 
                project.Name, project.Location, e.Message);
            return -1;
        }
    }

    /// <inheritdoc/>
    public bool DeleteProject(int id)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            
            const string deleteQuery = """
                DELETE FROM Projects 
                WHERE id = @Id
                """;
            var affectedRows = dbConnection.Execute(deleteQuery, new { Id = id });
            var success = affectedRows > 0;
            if (success)
            {
                Log.Information("Successfully deleted project with ID {Id}", id);
            }
            else
            {
                Log.Warning("No project found with ID {Id}", id);
            }
            return success;
        }
        catch (Exception e)
        {
            Log.Error("Error deleting project with ID {Id}. Exception: {Msg}", id, e.Message);
            return false;
        }
    }
}
