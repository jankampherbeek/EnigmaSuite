// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Research;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.ResearchProjects;

public class ProjectsOverviewController
{
    private IProjectsOverviewApi _projectsOverviewApi;
    private ResearchMethodInputWindow? _researchMethodInputWindow;
    private List<Window> _openWindows = new();

    public ProjectsOverviewController(IProjectsOverviewApi projectsOverviewApi)
    {
        _projectsOverviewApi = projectsOverviewApi;
    }

    public List<ProjectItem> GetAllProjectItems()
    {
        List<ProjectItem> projectItems = new();
        List<ResearchProject> allProjects = _projectsOverviewApi.GetDetailsForAllProjects();
        foreach (var project in allProjects)
        {
            projectItems.Add(new ProjectItem() { ProjectName = project.Name, ProjectDescription = project.Description });
        }
        return projectItems;
    }

    public void OpenProject(ProjectItem projectItem)
    {
        _researchMethodInputWindow = App.ServiceProvider.GetRequiredService <ResearchMethodInputWindow>();
        _openWindows.Add(_researchMethodInputWindow);
        _researchMethodInputWindow.Show();
        _researchMethodInputWindow.PopulateAll(projectItem);
    }

    public void HandleClose()
    {
        foreach (Window window in _openWindows)
        {
            window.Close();
        }
    }

}

public class ProjectItem
{
    public string ProjectName { get; set; }
    public string ProjectDescription { get; set; }
}