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

namespace Enigma.Frontend.Ui.Research;

public class ResearchMainController
{
    private readonly IProjectsOverviewApi _projectsOverviewApi;
    private List<ResearchProject> _researchProjects = new();
    private readonly ProjectUsageWindow _projectUsageWindow = new();

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
                _projectUsageWindow.SetProject(currentProject);
                _projectUsageWindow.ShowDialog();
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
        DataFilesOverviewWindow dataFilesOverviewWindow = new();
        dataFilesOverviewWindow.ShowDialog();
    }


    public static void ShowDataImport()
    {
        DataFilesImportWindow dataFilesImportWindow = new();
        dataFilesImportWindow.ShowDialog();
    }

    public static void ShowAbout()
    {
        AboutWindow aboutWindow = new AboutWindow();
        aboutWindow.ShowDialog();
    }

    public static void ShowHelp()
    {
        HelpWindow _helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        _helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _helpWindow.SetHelpPage("TestwithProject");
        _helpWindow.ShowDialog();
    }



}

public class ProjectItem
{
    public string? ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
}