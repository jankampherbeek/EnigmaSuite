// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Persistency;
using Enigma.Domain.Research;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for main research page.</summary>
public class ResearchMainModel(IProjectApi projectApi)
{
    public List<ResearchProject> ResearchProjects = [];
    
    public List<ProjectItem> GetAllProjectItems()
    {
        List<ProjectItem> projectItems = [];
        ResearchProjects = [];
        var allProjects = projectApi.GetDetailsForAllProjects();
        foreach (var project in allProjects)
        {
            ResearchProjects.Add(project);
            projectItems.Add(new ProjectItem { ProjectName = project.Name, ProjectDescription = project.Description });
        }
        return projectItems;
    }

    public bool DeleteProject(string name)
    {
        var proj = projectApi.ReadProject(name);
        return proj is not null && projectApi.DeleteProject(proj.id);
    } 
    
}


/// <summary>DTO for a single project item.</summary>
public class ProjectItem
{ public string? ProjectName { get; init; }
    public string? ProjectDescription { get; init; }
}