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
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for main research page. Startpage for any action in the Research Module.
/// Sends messages: CompletedMessage, OpenMessage (for ProjectInput, Datafile overview, and Datafile Import)
/// and HelpMessage. Receives Messages: Completed (for ProjectInput).</summary>
public partial class ResearchMainViewModel: ObservableObject, IRecipient<CompletedMessage>
{
    private const string VM_IDENTIFICATION = GeneralWindowsFlow.RESEARCH_MAIN;
    private const string ABOUT_RESEARCH = "AboutResearch";
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
        Log.Information("ResearchMainViewModel.AppSettings(): send OpenMessage");  
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION,GeneralWindowsFlow.APP_SETTINGS));
    }
    
    [RelayCommand]
    private static void Configuration()
    {
        Log.Information("ResearchMainViewModel.Configuration(): send OpenMessage"); 
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, GeneralWindowsFlow.CONFIGURATION));
    }

    [RelayCommand]
    private static void NewProject()
    {
        Log.Information("ResearchMainViewModel.NewProject(): send OpenMessage"); 
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.PROJECT_INPUT)); 
    }

    [RelayCommand(CanExecute = nameof(IsProjectSelected))]
    private void OpenProject()
    {
        ResearchProject project = _model.ResearchProjects[ProjectIndex];
        DataVaultResearch.Instance.CurrentProject = project;
        Log.Information("ResearchMainViewModel.OpenProject(): send OpenMessage"); 
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.PROJECT_USAGE));
    }

    private bool IsProjectSelected()
    {
        return ProjectIndex >= 0;
    }
   
    
    [RelayCommand]
    private static void DataOverview()
    {
        Log.Information("ResearchMainViewModel.DataOverview(): send OpenMessage"); 
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.DATAFILE_OVERVIEW)); 
    }

    [RelayCommand]
    private static void DataImport()
    {
        Log.Information("ResearchMainViewModel.DataImport(): send OpenMessage"); 
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.DATAFILE_IMPORT)); 
    }

    [RelayCommand]
    private static void Close()
    {
        Log.Information("ResearchMainViewModel.Close(): send CloseMessage"); 
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private static void About()
    {
        Log.Information("ResearchMainViewModel.About(): send HelpMessage"); 
        WeakReferenceMessenger.Default.Send(new HelpMessage(ABOUT_RESEARCH));
    }
    
    [RelayCommand]
    private static void UserManual()
    {
        UserManual userManual = new();
        userManual.ShowUserManual();
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("ResearchMainViewModel.Help(): send HelpMessage"); 
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }


    public void Receive(CompletedMessage message)
    {
        Log.Information("ResearchMainViewModel.Receive(CompletedMessage) with value {Value}", message.Value);
        if (message.Value == ResearchWindowsFlow.PROJECT_INPUT)
        {
            AvailableProjects = new ObservableCollection<ProjectItem>(_model.GetAllProjectItems());            
        }
    }
}