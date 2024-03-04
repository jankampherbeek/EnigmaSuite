// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for main cycles page. Startpage for any action in the Cycles Module.</summary>
public partial class CyclesMainViewModel: ObservableObject, IRecipient<CompletedMessage>
{
    private const string VM_IDENTIFICATION = GeneralWindowsFlow.CYCLES_MAIN;
    private const string ABOUT_CYCLES = "AboutCycles";
    private readonly CyclesMainModel _model;
    // ReSharper disable once NotAccessedField.Local  An instance of ResearchWindowsFlow must be instantiated so it can
    // handle incoming messages.
    //private readonly CyclesWindowsFlow _cyclesWindowsFlow;
    [ObservableProperty] private ObservableCollection<CycleItem> _availableCycles;   
    [NotifyCanExecuteChangedFor(nameof(OpenCycleCommand))]
    [ObservableProperty] private int _cycleIndex = -1;
    
    public CyclesMainViewModel()
    {
        //_researchWindowsFlow = App.ServiceProvider.GetRequiredService<ResearchWindowsFlow>();
        _model = App.ServiceProvider.GetRequiredService<CyclesMainModel>();
        AvailableCycles = new ObservableCollection<CycleItem>(_model.GetAllCycleItems());
        WeakReferenceMessenger.Default.Register(this);        
    }
    
    [RelayCommand]
    private static void NewCycle()
    {
        // TODO open dialog window for cycle input

    }

    [RelayCommand(CanExecute = nameof(IsCycleSelected))]
    private void OpenCycle()
    {
      
        //DataVaultResearch.Instance.CurrentCycle = cycle;
        //Log.Information("ResearchMainViewModel.OpenProject(): send OpenMessage"); 
        //WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.PROJECT_USAGE));
        // TODO open dialog Cycle window
    }

    private bool IsCycleSelected()
    {
        return CycleIndex >= 0;
    }
   
    [RelayCommand]
    private static void Close()
    {
        // TODO replace with close method in View
        //        Log.Information("ResearchMainViewModel.Close(): send CloseMessage"); 
//        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private static void About()
    {
        // TODO open About window as dialog
//        Log.Information("ResearchMainViewModel.About(): send HelpMessage"); 
//        WeakReferenceMessenger.Default.Send(new HelpMessage(ABOUT_RESEARCH));
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
  //          AvailableProjects = new ObservableCollection<ProjectItem>(_model.GetAllProjectItems());            
        }
    }
}