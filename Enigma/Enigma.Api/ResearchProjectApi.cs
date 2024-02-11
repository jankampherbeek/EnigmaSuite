// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Research;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Api;

/// <summary>Api for creation of a research project.</summary>
public interface IProjectCreationApi
{
    /// <summary>Create a research project and a controlgroup.</summary>
    /// <param name="project">Definition of the project to create.</param>
    /// <returns>Resultmessage with info about this action.</returns>
    public ResultMessage CreateProject(ResearchProject project);
}


/// <summary>Overview of available projects.</summary>
public interface IProjectsOverviewApi
{
    /// <summary>Get details for all available projects.</summary>
    /// <returns>List with projects.</returns>
    public List<ResearchProject> GetDetailsForAllProjects();
}




/// <inheritdoc/>
public sealed class ProjectCreationApi : IProjectCreationApi
{
    private readonly IProjectCreationHandler _projectCreationHandler;

    public ProjectCreationApi(IProjectCreationHandler projectCreationHandler) => _projectCreationHandler = projectCreationHandler;

    /// <inheritdoc/>
    public ResultMessage CreateProject(ResearchProject project)
    {
        Guard.Against.Null(project);
        Log.Information("ProjectCreationApi CreateProject: about to create project {Name}", project.Name);
        bool success = _projectCreationHandler.CreateProject(project, out int errorCode);
        string msg = "Project created";
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
}

/// <inheritdoc/>
public sealed class ProjectsOverviewApi : IProjectsOverviewApi
{
    private readonly IProjectsOverviewHandler _projectsOverviewHandler;

    public ProjectsOverviewApi(IProjectsOverviewHandler projectsOverviewHandler) => _projectsOverviewHandler = projectsOverviewHandler;

    /// <inheritdoc/>
    public List<ResearchProject> GetDetailsForAllProjects()
    {
        Log.Information("ProjectsOverviewApi.GetDetailsForAllProjects(). Returning list of projects");
        return _projectsOverviewHandler.ReadAllProjectDetails();
    }
}


