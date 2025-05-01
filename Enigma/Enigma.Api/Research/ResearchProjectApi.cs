// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Research;
using Enigma.Domain.Responses;
using Serilog;
using System.Globalization;
using Enigma.Core.Persistency;
using Enigma.Domain.References;

namespace Enigma.Api.Research;

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

/// <summary>Api for reading a specific project.</summary>
public interface IProjectApi
{
    /// <summary>Read a project by its name.</summary>
    /// <param name="name">The name of the project to read.</param>
    /// <returns>The project, or null if not found.</returns>
    public ResearchProject? ReadProject(string name);
}

/// <inheritdoc/>
public sealed class ProjectCreationApi(IProjectCreationHandler projectCreationHandler) : IProjectCreationApi
{
    /// <inheritdoc/>
    public ResultMessage CreateProject(ResearchProject project)
    {
        Guard.Against.Null(project);
        Log.Information("ProjectCreationApi CreateProject: about to create project {Name}", project.Name);
        var success = projectCreationHandler.CreateProject(project, out int errorCode);
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
}

/// <inheritdoc/>
public sealed class ProjectsOverviewApi(IProjectsOverviewHandler projectsOverviewHandler) : IProjectsOverviewApi
{
    /// <inheritdoc/>
    public List<ResearchProject> GetDetailsForAllProjects()
    {
        Log.Information("ProjectsOverviewApi.GetDetailsForAllProjects(). Returning list of projects");
        return projectsOverviewHandler.ReadAllProjectDetails();
    }
}

/// <inheritdoc/>
public sealed class ProjectApi(IProjectDao projectDao) : IProjectApi
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
            projectDto.Name,
            projectDto.Description,
            projectDto.DataFile,
            projectDto.Created.ToString(CultureInfo.InvariantCulture),
            projectDto.ControlGroupType,
            projectDto.MultiFactor);

        Log.Information("Successfully read project {Name}", name);
        return researchProject;
    }
}