// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.RequestResponse;
using Enigma.Domain.Research;
using Serilog;

namespace Enigma.Api.Research;


public class ProjectCreationApi : IProjectCreationApi
{
    private readonly IProjectCreationHandler _projectCreationHandler;

    public ProjectCreationApi(IProjectCreationHandler projectCreationHandler) => _projectCreationHandler = projectCreationHandler;

    public ResultMessage CreateProject(ResearchProject project)
    {
        Guard.Against.Null(project);
        Log.Information("ProjectCreationApi CreateProject: about to create project {name}.", project.Name);
        bool success = _projectCreationHandler.CreateProject(project, out int errorCode);
        string msg = "Project created";
        if (success)
        {
            Log.Information("Project {name} successfully created.", project.Name);
        }
        else
        {
            msg = "An error occurred";
            Log.Error("An error occurred when creating project {name}, the errorCode is: {code}.", project.Name, errorCode);
        }
        return new ResultMessage(errorCode, msg);
    }
}

public class ProjectsOverviewApi : IProjectsOverviewApi
{
    private readonly IProjectsOverviewHandler _projectsOverviewHandler;

    public ProjectsOverviewApi(IProjectsOverviewHandler projectsOverviewHandler) => _projectsOverviewHandler = projectsOverviewHandler;

    public List<ResearchProject> GetDetailsForAllProjects()
    {
        Log.Information("ProjectsOverviewApi GetDetailsForAllProjects.");
        return _projectsOverviewHandler.ReadAllProjectDetails();
    }
}


