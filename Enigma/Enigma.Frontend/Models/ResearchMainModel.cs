// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Research;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for main research page</summary>
public class ResearchMainModel
{
    private readonly IProjectsOverviewApi _projectsOverviewApi;
    private List<ResearchProject> _researchProjects = new();

    public ResearchMainModel(IProjectsOverviewApi projectsOverviewApi)
    {
        _projectsOverviewApi = projectsOverviewApi;
    }
    
    
    public List<ProjectItem> GetAllProjectItems()
    {
        List<ProjectItem> projectItems = new();
        _researchProjects = new();
        List<ResearchProject> allProjects = _projectsOverviewApi.GetDetailsForAllProjects();
        foreach (var project in allProjects)
        {
            _researchProjects.Add(project);
            projectItems.Add(new ProjectItem() { ProjectName = project.Name, ProjectDescription = project.Description });
        }
        return projectItems;
    }
    
    
    public void OpenProject(ProjectItem projectItem)
    {

        ResearchProject? currentProject = null;
        foreach (var project in _researchProjects)
        {
            if (project.Name.Equals(projectItem.ProjectName) && (currentProject is null))  // check for null to avoid adding multiple projects to usage window
            {
                currentProject = project;
                ProjectUsageWindow projectUsageWindow = new ProjectUsageWindow();       // todo 0.2 move navigation out of model
                projectUsageWindow.SetProject(currentProject);
                projectUsageWindow.ShowDialog();
            }
        }
    }
    
}



public class ProjectItem
{
    public string? ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
}