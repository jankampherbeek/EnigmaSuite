// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for main research page</summary>
public partial class ResearchMainViewModel: ObservableObject, IRecipient<CompletedMessage>
{
    private readonly ResearchMainModel _model;
    private readonly IMsgAgent _researchMsgAgent;
    [ObservableProperty] private ObservableCollection<ProjectItem> _availableProjects;   
    [NotifyCanExecuteChangedFor(nameof(OpenProjectCommand))]
    [ObservableProperty] private int _projectIndex = -1;
    public ResearchMainViewModel()
    {
      //  _researchMsgAgent = researchMsgAgent;
      _researchMsgAgent = App.ServiceProvider.GetRequiredService<IResearchMsgAgent>();
        _model = App.ServiceProvider.GetRequiredService<ResearchMainModel>();
        AvailableProjects = new ObservableCollection<ProjectItem>(_model.GetAllProjectItems());
        WeakReferenceMessenger.Default.Register<CompletedMessage>(this);        
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
        WeakReferenceMessenger.Default.Send(new OpenMessage("ResearchMain", "ProjectInput")); 
        
        /*ProjectInputWindow projectInputWindow = new();
        projectInputWindow.ShowDialog();
        AvailableProjects = new ObservableCollection<ProjectItem>(_model.GetAllProjectItems());*/
    }

    [RelayCommand(CanExecute = nameof(IsProjectSelected))]
    private void OpenProject()
    {
        ResearchProject project = _model.ResearchProjects[ProjectIndex];
        DataVaultResearch.Instance.CurrentProject = project;
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
        DataVaultGeneral.Instance.CurrentViewBase = "AboutResearch";
        new HelpWindow().ShowDialog();
    }
    
    [RelayCommand]
    private static void UserManual()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "UserManual";
        new HelpWindow().ShowDialog();
    }
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "ResearchMain";
        new HelpWindow().ShowDialog();
    }


    public void Receive(CompletedMessage message)
    {
        if (message.Value == "ProjectInput")
        {
            AvailableProjects = new ObservableCollection<ProjectItem>(_model.GetAllProjectItems());            
        }
    }
}