// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Research.DataFiles;
using Enigma.Frontend.Ui.Settings;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.Research;

public class ResearchMainController
{
    private IProjectsOverviewApi _projectsOverviewApi;
    private ProjectUsageWindow? _projectUsageWindow;
    private List<ResearchProject> _researchProjects = new();
    private List<Window> _openWindows = new();

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

        ResearchProject currentProject = null;
        foreach (var project in _researchProjects)
        {
            if (project.Name.Equals(projectItem.ProjectName) && (currentProject is null))  // check for null to avoid adding multiple projects to usage window
            {
                _projectUsageWindow = App.ServiceProvider.GetRequiredService<ProjectUsageWindow>();
                _openWindows.Add(_projectUsageWindow);
                currentProject = project;
                _projectUsageWindow.SetProject(currentProject);
                _projectUsageWindow.Show();
            }
        }
    }

    public void HandleClose()
    {
        foreach (Window window in _openWindows)
        {
            window.Close();
        }
    }

    public void ShowAppSettings()
    {
        AppSettingsWindow appSettingsWindow = new();
        appSettingsWindow.ShowDialog();
    }

    public void ShowAstroConfig()
    {
        AstroConfigWindow astroConfigWindow = new();
        astroConfigWindow.ShowDialog();
    }

    public void NewProject()
    {
        ProjectInputWindow projectInputWindow = new();
        projectInputWindow.ShowDialog();
    }

    public void ShowDataOverview()
    {
        DataFilesOverviewWindow dataFilesOverviewWindow = new();
        dataFilesOverviewWindow.ShowDialog();
    }


    public void ShowDataImport()
    {
        DataFilesImportWindow dataFilesImportWindow = new();
        dataFilesImportWindow.ShowDialog();
    }

    public void ShowAbout()
    {
        AboutWindow aboutWindow = new();  
        aboutWindow.ShowDialog();
    }

    public void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("TestwithProject");
        helpWindow.ShowDialog();
    }



}

public class ProjectItem
{
    public string ProjectName { get; set; }
    public string ProjectDescription { get; set; }
}