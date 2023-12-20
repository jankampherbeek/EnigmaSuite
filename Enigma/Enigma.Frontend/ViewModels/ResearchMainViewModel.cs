// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for main research page. Startpage for any action in the Research Module.
/// Sends messages: CompletedMessage, OpenMessage (for ProjectInput, Datafile overview, and Datafile Import)
/// and HelpMessage. Receives Messages: Completed (for ProjectInput).</summary>
public partial class ResearchMainViewModel: ObservableObject, IRecipient<CompletedMessage>
{
    private const string VM_IDENTIFICATION = GeneralWindowsFlow.RESEARCH_MAIN;
    private const string ABOUT_RESEARCH = "AboutResearch";
    private const string USER_MANUAL = "UserManual";
    private readonly ResearchMainModel _model;
    // ReSharper disable once NotAccessedField.Local  An instance of ResearchWindowsFlow must be instantiated so it can
    // handle incoming messages.
    private readonly ResearchWindowsFlow _researchWindowsFlow;
    [ObservableProperty] private ObservableCollection<ProjectItem> _availableProjects;   
    [NotifyCanExecuteChangedFor(nameof(OpenProjectCommand))]
    [ObservableProperty] private int _projectIndex = -1;
    
    public ResearchMainViewModel()
    {
        _researchWindowsFlow = App.ServiceProvider.GetRequiredService<ResearchWindowsFlow>();
        _model = App.ServiceProvider.GetRequiredService<ResearchMainModel>();
        AvailableProjects = new ObservableCollection<ProjectItem>(_model.GetAllProjectItems());
        WeakReferenceMessenger.Default.Register(this);        
    }
    
    [RelayCommand]
    private static void AppSettings()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION,GeneralWindowsFlow.APP_SETTINGS));
    }
    
    [RelayCommand]
    private static void Configuration()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, GeneralWindowsFlow.CONFIGURATION));
    }

    [RelayCommand]
    private static void NewProject()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.PROJECT_INPUT)); 
    }

    [RelayCommand(CanExecute = nameof(IsProjectSelected))]
    private void OpenProject()
    {
        ResearchProject project = _model.ResearchProjects[ProjectIndex];
        DataVaultResearch.Instance.CurrentProject = project;
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.PROJECT_USAGE));
    }

    private bool IsProjectSelected()
    {
        return ProjectIndex >= 0;
    }
   
    
    [RelayCommand]
    private static void DataOverview()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.DATAFILE_OVERVIEW)); 
    }

    [RelayCommand]
    private static void DataImport()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.DATAFILE_IMPORT)); 
    }

    [RelayCommand]
    private static void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private static void About()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(ABOUT_RESEARCH));
    }
    
    [RelayCommand]
    private static void UserManual()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(USER_MANUAL));
    }
    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }


    public void Receive(CompletedMessage message)
    {
        if (message.Value == ResearchWindowsFlow.PROJECT_INPUT)
        {
            AvailableProjects = new ObservableCollection<ProjectItem>(_model.GetAllProjectItems());            
        }
    }
}