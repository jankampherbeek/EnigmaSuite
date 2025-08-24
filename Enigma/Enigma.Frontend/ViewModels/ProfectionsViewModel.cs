// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.WindowsFlow;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for Profections window</summary>
public partial class ProfectionsViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.PROFECTIONS;

    [ObservableProperty] private string _chartName = "Chart Name";

    [RelayCommand]
    private void Help()
    {
        Log.Information("ProfectionsViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private void Close()
    {
        Log.Information("ProfectionsViewModel.Close(): send CloseMessage");
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    public ProfectionsViewModel()
    {
        // Initialize data here when needed
    }
}
