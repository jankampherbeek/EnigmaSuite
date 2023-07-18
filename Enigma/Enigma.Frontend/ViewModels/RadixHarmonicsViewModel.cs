// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for harmonics in radix</summary>
public partial class RadixHarmonicsViewModel: ObservableObject
{
    [ObservableProperty] private ObservableCollection<PresentableHarmonic> _actualHarmonics;
    [ObservableProperty] private string _chartId;
    [ObservableProperty] private string _description;
    [ObservableProperty] private double _harmonicNumber;
    [ObservableProperty] private string _harmonicText;
    private readonly RadixHarmonicsModel _model = App.ServiceProvider.GetRequiredService<RadixHarmonicsModel>();
    
    public RadixHarmonicsViewModel()
    {
        _harmonicNumber = 2.0;
        _harmonicText = "Effective harmonic: " + HarmonicNumber;
        _chartId = _model.RetrieveChartName();
        _description = _model.DescriptiveText();
        _actualHarmonics = new ObservableCollection<PresentableHarmonic>(_model.RetrieveAndFormatHarmonics(HarmonicNumber));
    }

    [RelayCommand]
    private void ReCalculate()
    {
        ActualHarmonics = new ObservableCollection<PresentableHarmonic>(_model.RetrieveAndFormatHarmonics(HarmonicNumber));
        HarmonicText = "Effective harmonic: " + HarmonicNumber;
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        ShowHelp();
    }
    
    private static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "RadixHarmonics";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }

}