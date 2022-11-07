// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.RequestResponse;
using Enigma.Domain.Research;

namespace Engima.Api.Research;


public class ProjectCreationApi : IProjectCreationApi
{
    private readonly IProjectCreationHandler _projectCreationHandler;

    public ProjectCreationApi(IProjectCreationHandler projectCreationHandler)
    {
        _projectCreationHandler = projectCreationHandler;
    }

    public ResultMessage CreateProject(ResearchProject project)
    {
        Guard.Against.Null(project);
        bool success = _projectCreationHandler.CreateProject(project, out int errorCode);
        string msg = success ? "Project created." : "An error occurred.";
        return new ResultMessage(errorCode, msg);
    }
}

public class ProjectsOverviewApi: IProjectsOverviewApi
{
    private readonly IProjectsOverviewHandler _projectsOverviewHandler;

    public ProjectsOverviewApi(IProjectsOverviewHandler projectsOverviewHandler)
    {
        _projectsOverviewHandler = projectsOverviewHandler;
    }

    public List<ResearchProject> GetDetailsForAllProjects()
    {
        return _projectsOverviewHandler.ReadAllProjectDetails();

    }
}