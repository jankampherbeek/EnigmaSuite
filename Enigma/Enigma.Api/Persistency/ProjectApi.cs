// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Domain.Research;
using Enigma.Domain.Responses;
using Serilog;
using System.Globalization;
using Enigma.Core.Persistency;
using Enigma.Core.Research;

namespace Enigma.Api.Persistency;


/// <summary>Api for persistency of projects</summary>
public interface IProjectApi
{
    /// <summary>Read a project by its name.</summary>
    /// <param name="name">The name of the project to read.</param>
    /// <returns>The project, or null if not found.</returns>
    public ResearchProject? ReadProject(string name);
    
    /// <summary>Create a research project and a controlgroup.</summary>
    /// <param name="project">Definition of the project to create.</param>
    /// <returns>Resultmessage with info about this action.</returns>
    public ResultMessage CreateProject(ResearchProject project);
    
    /// <summary>Get details for all available projects.</summary>
    /// <returns>List with projects.</returns>
    public List<ResearchProject> GetDetailsForAllProjects();

    /// <summary>Delete project form the database and from the filesystem</summary>
    /// <param name="id">The id of the project</param>
    /// <returns>True if the project was deleted, otherwise false</returns>
    public bool DeleteProject(int id);

}


/// <inheritdoc/>
public sealed class ProjectApi(IProjectDao projectDao, 
    IProjectCreationHandler projectCreationHandler,
    IProjectsOverviewHandler projectsOverviewHandler,
    IProjectPersistencyHandler progPersHandler) : IProjectApi
{
    /// <inheritdoc/>
    public ResearchProject? ReadProject(string name)
    {
        Guard.Against.NullOrEmpty(name);
        Log.Information("ProjectApi: Reading project {Name}", name);
        
        var projectDto = projectDao.ReadProject(name);
        if (projectDto == null)
        {
            Log.Warning("Project {Name} not found", name);
            return null;
        }

        var researchProject = new ResearchProject(
            projectDto.Id,
            projectDto.Name,
            projectDto.Description,
            projectDto.DataFile,
            projectDto.Created.ToString(CultureInfo.InvariantCulture),
            projectDto.ControlGroupType,
            projectDto.MultiFactor);

        Log.Information("Successfully read project {Name}", name);
        return researchProject;
    }
    
    /// <inheritdoc/>
    public ResultMessage CreateProject(ResearchProject project)
    {
        Guard.Against.Null(project);
        Log.Information("ProjectCreationApi CreateProject: about to create project {Name}", project.Name);
        var success = projectCreationHandler.CreateProject(project, out var errorCode);
        var msg = "Project created";
        if (success)
        {
            Log.Information("ProjectCreationApi.CreateProject(): Project {Name} successfully created", project.Name);
        }
        else
        {
            msg = "An error occurred when trying to create a project.";
            Log.Error("ProjectCreationApi.CreateProject(): An error occurred when creating project {Name}, the errorCode is: {Code}", project.Name, errorCode);
        }
        return new ResultMessage(errorCode, msg);
    }
    
    /// <inheritdoc/>
    public List<ResearchProject> GetDetailsForAllProjects()
    {
        Log.Information("ProjectsOverviewApi.GetDetailsForAllProjects(). Returning list of projects");
        return projectsOverviewHandler.ReadAllProjectDetails();
    }

    /// <inheritdoc/>
    public bool DeleteProject(int id)
    {
        Log.Information($"Deleting project and files for id {id}");
        return progPersHandler.DeleteProjectAndFiles(id);
    }
}