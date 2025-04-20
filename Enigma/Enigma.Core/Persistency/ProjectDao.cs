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


/// <summary>Data Transfer Object for Projects table.</summary>
public class ProjectDto
{
    /// <summary>Name of the project.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>Description of the project.</summary>
    public string Description { get; set; } = string.Empty;
   
    /// <summary>Location of the project.</summary>
    public string Location { get; set; } = string.Empty;
    
    /// <summary>Multiplier factor for the project.</summary>
    public int MultiFactor { get; set; }
    
    /// <summary>Creation date of the project.</summary>
    public string Created { get; set; } = string.Empty;
    
    /// <summary>ID of the associated data file.</summary>
    public int DataFile { get; set; }
}


/// <summary>DAO for research projects</summary>
public interface IProjectDao
{
    /// <summary>Inserts a new project into the database.</summary>
    public int InsertProject(ProjectDto project);
    /// <summary>Deletes project from the database.</summary>
    public bool DeleteProject(int id);
}

public class ProjectDao: IProjectDao
{
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