// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for harmonic details in research</summary>
public partial class ResearchHarmonicDetailsViewModel: ObservableObject
{
    [NotifyCanExecuteChangedFor(nameof(ContinueCommand))]
    [NotifyPropertyChangedFor(nameof(HarmonicNrValid))]
    [ObservableProperty] private string _harmonicNumber = "2";
    [NotifyCanExecuteChangedFor(nameof(ContinueCommand))]
    [NotifyPropertyChangedFor(nameof(OrbDegreeValid))]
    [ObservableProperty] private string _orbDegrees = "1";
    [NotifyCanExecuteChangedFor(nameof(ContinueCommand))]
    [NotifyPropertyChangedFor(nameof(OrbMinuteValid))]
    [ObservableProperty] private string _orbMinutes = "0";
    
    private int OrbDegreeValue { get; set; }
    private int OrbMinuteValue { get; set; }
    private double HarmonicValue { get; set; }
    public SolidColorBrush HarmonicNrValid => IsHarmonicNrValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush OrbDegreeValid => IsOrbDegreeValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush OrbMinuteValid => IsOrbMinuteValid() ? Brushes.White : Brushes.Yellow;
    
    
    [RelayCommand(CanExecute = nameof(IsInputOk))]
    private void Continue()
    {
        double orbValue = OrbDegreeValue + OrbMinuteValue / 60.0;        
        ResearchHarmonicDetailsModel.SaveOrbHarmonics(orbValue);
        ResearchHarmonicDetailsModel.SaveHarmonicNr(HarmonicValue);
    }

    [RelayCommand]
    private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "ResearchHarmonicDetails";
        new HelpWindow().ShowDialog();
    }
    
    
    private bool IsInputOk()
    {
        return IsHarmonicNrValid() && IsOrbDegreeValid() && IsOrbMinuteValid();
    }
    
    private bool IsHarmonicNrValid()
    {
        bool isValid = false;
        bool isNumeric = double.TryParse(HarmonicNumber, out double value);
        if (isNumeric) isValid = value > 0.0;
        if (isValid) HarmonicValue = value;
        return isValid;
    }

    private bool IsOrbDegreeValid()
    {
        bool isValid = false;
        bool isNumeric = int.TryParse(OrbDegrees, out int value);
        if (isNumeric) isValid = value  > 0 && value < ResearchHarmonicDetailsModel.MaxOrbHarmonic;
        if (isValid) OrbDegreeValue = value;
        return isValid;
    }
    
    private bool IsOrbMinuteValid()
    {
        bool isValid = false;
        bool isNumeric = int.TryParse(OrbMinutes, out int value);
        if (isNumeric) isValid = value is >= 0 and <= 59;
        if (isValid) OrbMinuteValue = value;
        return isValid;
    }
    
}