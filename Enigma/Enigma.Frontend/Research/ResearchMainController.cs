// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Configuration;
using Enigma.Frontend.Ui.Research.DataFiles;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.Research;

public class ResearchMainController
{
    private readonly IProjectsOverviewApi _projectsOverviewApi;
    private List<ResearchProject> _researchProjects = new();

    public ResearchMainController(IProjectsOverviewApi projectsOverviewApi)
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
                ProjectUsageWindow projectUsageWindow = new ProjectUsageWindow();
                projectUsageWindow.SetProject(currentProject);
                projectUsageWindow.ShowDialog();
            }
        }
    }

    public static void ShowAppSettings()
    {
        AppSettingsWindow appSettingsWindow = new();
        appSettingsWindow.ShowDialog();
    }

    public static void ShowAstroConfig()
    {
        AstroConfigWindow astroConfigWindow = new();
        astroConfigWindow.ShowDialog();
    }

    public static void NewProject()
    {
        ProjectInputWindow projectInputWindow = new();
        projectInputWindow.ShowDialog();
    }

    public static void ShowDataOverview()
    {
        DatafileOverviewWindow dataFileOverviewWindow = new();
        dataFileOverviewWindow.ShowDialog();
    }


    public static void ShowDataImport()
    {
        DatafileImportWindow dataFilesImportWindow = new();
        dataFilesImportWindow.ShowDialog();
    }

    public static void ShowAbout()
    {
        DataVault.Instance.CurrentViewBase = "AboutResearch";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }

    public static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "TestWithProject";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }
}

public class ProjectItem
{
    public string? ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
}