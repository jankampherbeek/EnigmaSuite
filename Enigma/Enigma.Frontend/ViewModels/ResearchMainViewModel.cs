// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for main research page</summary>
public partial class ResearchMainViewModel: ObservableObject
{
    private readonly ResearchMainModel _model;
    [ObservableProperty] private ObservableCollection<ProjectItem> _availableProjects;   
    [NotifyCanExecuteChangedFor(nameof(OpenProjectCommand))]
    [ObservableProperty] private int _projectIndex = -1;
    public ResearchMainViewModel()
    {
        _model = App.ServiceProvider.GetRequiredService<ResearchMainModel>();
        AvailableProjects = new ObservableCollection<ProjectItem>(_model.GetAllProjectItems());
    }
    
    [RelayCommand]
    private static void AppSettings()
    {
        AppSettingsWindow appSettingsWindow = new();
        appSettingsWindow.ShowDialog();
    }
    
    [RelayCommand]
    private static void Configuration()
    {
        ConfigurationWindow configWindow = new();
        configWindow.ShowDialog();
    }

    [RelayCommand]
    private void NewProject()
    {
        ProjectInputWindow projectInputWindow = new();
        projectInputWindow.ShowDialog();
        AvailableProjects = new ObservableCollection<ProjectItem>(_model.GetAllProjectItems());
    }

    [RelayCommand(CanExecute = nameof(IsProjectSelected))]
    private void OpenProject()
    {
        ResearchProject project = _model.ResearchProjects[ProjectIndex];
        DataVault.Instance.CurrentProject = project;
        new ProjectUsageWindow().ShowDialog();
    }

    private bool IsProjectSelected()
    {
        return ProjectIndex >= 0;
    }
   
    
    [RelayCommand]
    private static void DataOverview()
    {
        DatafileOverviewWindow dataFileOverviewWindow = new();
        dataFileOverviewWindow.ShowDialog();
    }

    [RelayCommand]
    private static void DataImport()
    {
        DatafileImportWindow dataFilesImportWindow = new();
        dataFilesImportWindow.ShowDialog();
    }
    
    [RelayCommand]
    private static void About()
    {
        DataVault.Instance.CurrentViewBase = "AboutResearch";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }
    
    [RelayCommand]
    private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "ResearchMain";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }

    
    
}