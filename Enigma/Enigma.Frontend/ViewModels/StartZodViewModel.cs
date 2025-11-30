// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for Startpoint Zodiac window</summary>
public partial class StartZodViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.START_ZOD;
    private readonly StartZodModel _model = App.ServiceProvider.GetRequiredService<StartZodModel>();
    
    [ObservableProperty] private string _chartName = "Chart Name";
    [ObservableProperty] private ObservableCollection<SelectableChartPointDetails> _allChartPointDetails = [];
    [ObservableProperty] private SelectableChartPointDetails? _selectedChartPoint;

    public StartZodViewModel()
    {
        ChartName = _model.GetCurrentChartName();
        AllChartPointDetails = new ObservableCollection<SelectableChartPointDetails>(_model.GetAllCelPointDetails());
        // Default to first ChartPoint in collection
        SelectedChartPoint = AllChartPointDetails[0];
        Populate();
    }

    partial void OnSelectedChartPointChanged(SelectableChartPointDetails? value)
    {
        if (value != null)
        {
            Populate();
        }
    }
    
    private void Populate()
    {
        if (SelectedChartPoint != null)
        {
            _model.DefineAltChart(SelectedChartPoint.ChartPoint);
        }
    }

    [RelayCommand]
    private void Help()
    {
        Log.Information("StartZodViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private void Close()
    {
        Log.Information("StartZodViewModel.Close(): send CloseMessage");
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
}
