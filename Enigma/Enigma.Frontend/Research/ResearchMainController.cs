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
    private ProjectUsageWindow? _projectUsageWindow;
    private List<ResearchProject> _researchProjects = new();
    private readonly List<Window> _openWindows = new();
    private readonly AppSettingsWindow _appSettingsWindow = new();
    private readonly AstroConfigWindow _astroConfigWindow = new();
    private readonly ProjectInputWindow _projectInputWindow = new();
    private readonly DataFilesOverviewWindow _dataFilesOverviewWindow = new();
    private readonly DataFilesImportWindow _dataFilesImportWindow = new();
    private readonly AboutWindow _aboutWindow = new();
    private readonly HelpWindow _helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();

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
        _appSettingsWindow.ShowDialog();
    }

    public void ShowAstroConfig()
    {
        _astroConfigWindow.ShowDialog();
    }

    public void NewProject()
    {
        _projectInputWindow.ShowDialog();
    }

    public void ShowDataOverview()
    {
        _dataFilesOverviewWindow.ShowDialog();
    }


    public void ShowDataImport()
    {
        _dataFilesImportWindow.ShowDialog();
    }

    public void ShowAbout()
    {
        _aboutWindow.ShowDialog();
    }

    public void ShowHelp()
    {
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