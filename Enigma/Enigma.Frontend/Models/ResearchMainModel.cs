// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Domain.Research;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for main research page</summary>
public class ResearchMainModel
{
    private readonly IProjectsOverviewApi _projectsOverviewApi;
    public List<ResearchProject> ResearchProjects = new();

    public ResearchMainModel(IProjectsOverviewApi projectsOverviewApi)
    {
        _projectsOverviewApi = projectsOverviewApi;
    }


    public List<ProjectItem> GetAllProjectItems()
    {
        List<ProjectItem> projectItems = new();
        ResearchProjects = new List<ResearchProject>();
        List<ResearchProject> allProjects = _projectsOverviewApi.GetDetailsForAllProjects();
        foreach (var project in allProjects)
        {
            ResearchProjects.Add(project);
            projectItems.Add(new ProjectItem { ProjectName = project.Name, ProjectDescription = project.Description });
        }

        return projectItems;
    }


    // TODO remove outcommented method OpenProject from ResearchMainModel
    /*   public void OpenProject()
       {
           
           ResearchProject? currentProject = null;
           // check for null to avoid adding multiple projects to usage window
           foreach (ResearchProject project in _researchProjects.Where(project => project.Name.Equals(projectItem.ProjectName) && (currentProject is null)))
           {
               currentProject = project;
               ProjectUsageWindow projectUsageWindow = new ProjectUsageWindow();       // todo 0.2 move navigation out of model
               projectUsageWindow.ShowDialog();
           }
       }
       
   }*/
}


public class ProjectItem
{
    public string? ProjectName { get; init; }
    public string? ProjectDescription { get; init; }
}