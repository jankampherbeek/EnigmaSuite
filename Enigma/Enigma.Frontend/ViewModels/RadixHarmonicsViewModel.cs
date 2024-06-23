// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Constants;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for harmonics in radix</summary>
public partial class RadixHarmonicsViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.RADIX_HARMONICS;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    [ObservableProperty] private ObservableCollection<PresentableHarmonic> _actualHarmonics;
    [ObservableProperty] private string _chartId;
    [ObservableProperty] private string _description;
    [NotifyPropertyChangedFor(nameof(HarmonicValid))]
    [NotifyCanExecuteChangedFor(nameof(ReCalculateCommand))]    
    [ObservableProperty] private double _harmonicNumber;
    [ObservableProperty] private string _harmonicText;
    private readonly RadixHarmonicsModel _model = App.ServiceProvider.GetRequiredService<RadixHarmonicsModel>();
    
    public SolidColorBrush HarmonicValid => IsHarmonicValid() ? Brushes.Gray : Brushes.Red;
    
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
        if (IsHarmonicValid())
        {
            ActualHarmonics = new ObservableCollection<PresentableHarmonic>(_model.RetrieveAndFormatHarmonics(HarmonicNumber));
            HarmonicText = "Effective harmonic: " + HarmonicNumber;
        }
        else
        {
            MessageBox.Show(StandardTexts.ERROR_HARMONIC_NR, StandardTexts.TITLE_ERROR);    
        }
    }
    
    private bool IsHarmonicValid()
    {
        return HarmonicNumber is > 1 and < 100_000;
    }
    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseNonDlgMessage(VM_IDENTIFICATION, _windowId ));
    }

}