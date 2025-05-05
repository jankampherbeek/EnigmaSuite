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


/// <summary>DAO for research projects</summary>
public interface IProjectDao
{
    /// <summary>Inserts a new project into the database.</summary>
    public int InsertProject(ProjectDto project);
    /// <summary>Deletes project from the database.</summary>
    public bool DeleteProject(int id);

    /// <summary>Gets all projects with their associated data file names.</summary>
    /// <returns>List of projects with data file information.</returns>
    public List<ProjectDto> GetAllProjectsWithDataFiles();

    /// <summary>Reads a project from the database by its name.</summary>
    /// <param name="name">The name of the project to read.</param>
    /// <returns>The project, or null if not found.</returns>
    public ProjectDto? ReadProject(string name);
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
                                       INSERT INTO Projects(name, description, location, multiFactor, created, datafile, controlgrouptype)
                                       VALUES(@Name, @Description, @Location, @MultiFactor, @Created, @DataFile, @ControlGroupType);
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

    /// <inheritdoc/>
    public List<ProjectDto> GetAllProjectsWithDataFiles()
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();

            const string query = """
                SELECT p.id, p.name, p.description, p.multiFactor, p.created, p.datafile, d.name as dataFileName
                FROM Projects p
                JOIN DataFiles d ON p.datafile = d.id
                """;

            var projects = dbConnection.Query<ProjectDto>(query).ToList();
            Log.Information("Retrieved {Count} projects from database", projects.Count);
            return projects;
        }
        catch (Exception e)
        {
            Log.Error("Error reading projects from database: {Message}", e.Message);
            return new List<ProjectDto>();
        }
    }

    /// <inheritdoc/>
    public ProjectDto? ReadProject(string name)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();

            const string query = """
                SELECT p.id, p.name, p.description, p.location, p.multiFactor, p.created, p.datafile, p.controlgrouptype
                FROM Projects p
                WHERE p.name = @Name
                """;

            var project = dbConnection.QueryFirstOrDefault<ProjectDto>(query, new { Name = name });
            if (project != null)
            {
                Log.Information("Retrieved project {Name} from database", name);
            }
            else
            {
                Log.Warning("Project {Name} not found in database", name);
            }
            return project;
        }
        catch (Exception e)
        {
            Log.Error("Error reading project {Name} from database: {Message}", name, e.Message);
            return null;
        }
    }
}