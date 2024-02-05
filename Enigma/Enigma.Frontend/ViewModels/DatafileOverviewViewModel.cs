// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for an overview of data files.
/// Shows names of datafiles and takes no further action. Messages: CloseMessage and HelpMessage.</summary>
public partial class DatafileOverviewViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ResearchWindowsFlow.DATAFILE_OVERVIEW;
    [ObservableProperty] private ObservableCollection<string> _dataNames;    

    public DatafileOverviewViewModel()
    {
        DatafileOverviewModel model = App.ServiceProvider.GetRequiredService<DatafileOverviewModel>();
        _dataNames = new ObservableCollection<string>(model.GetDataNames());
    }

    [RelayCommand]
    private static void Close()
    {
        Log.Information("DatafileOverviewViewModel.Close(): send CloseMessage");   
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION)); 
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("DatafileOverviewViewModel.Help(): send HelpMessage"); 
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

}
