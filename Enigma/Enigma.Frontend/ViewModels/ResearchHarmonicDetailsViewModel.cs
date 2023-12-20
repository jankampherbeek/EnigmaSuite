// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for harmonic details in research. Collects user input about org and harmonic number.
/// Sends messages: HarmonicDetailsMessage, CompletedMessage, CancelMessage and HelpMessage.</summary>
public partial class ResearchHarmonicDetailsViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = "ResearchHarmonicDetails";    // identification of ViewModel
    private bool _continueClicked;
    [NotifyPropertyChangedFor(nameof(HarmonicNrValid))]
    [ObservableProperty] private string _harmonicNumber = "2";
    [NotifyPropertyChangedFor(nameof(OrbDegreeValid))]
    [ObservableProperty] private string _orbDegrees = "1";
    [NotifyPropertyChangedFor(nameof(OrbMinuteValid))]
    [ObservableProperty] private string _orbMinutes = "0";
    
    private int OrbDegreeValue { get; set; }
    private int OrbMinuteValue { get; set; }
    private double HarmonicValue { get; set; }
    public SolidColorBrush HarmonicNrValid => IsHarmonicNrValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush OrbDegreeValid => IsOrbDegreeValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush OrbMinuteValid => IsOrbMinuteValid() ? Brushes.Gray : Brushes.Red;

    
    [RelayCommand]
    private void Continue()
    {
        _continueClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            HarmonicDetailsSelection selection = new(HarmonicValue, OrbDegreeValue + OrbMinuteValue / 60.0);
            DataVaultResearch.Instance.CurrentHarmonicDetailsSelection = selection;
            WeakReferenceMessenger.Default.Send(new CompletedMessage(VM_IDENTIFICATION));
        }
        else
        {
            MessageBox.Show(errors, StandardTexts.TITLE_ERROR);
        }
    }

    [RelayCommand]
    private static void Cancel()
    {
        WeakReferenceMessenger.Default.Send(new CancelMessage(VM_IDENTIFICATION));
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    private string FindErrors()
    {
        StringBuilder errorsText = new();
        if (!IsHarmonicNrValid())
            errorsText.Append(StandardTexts.ERROR_HARMONIC_NR + EnigmaConstants.NEW_LINE);
        if (!IsOrbDegreeValid())
            errorsText.Append(StandardTexts.ERROR_DEGREE + EnigmaConstants.NEW_LINE);
        if (!IsOrbMinuteValid())
            errorsText.Append(StandardTexts.ERROR_MINUTE + EnigmaConstants.NEW_LINE);
        return errorsText.ToString();
    }

    private bool IsHarmonicNrValid()
    {
        if (string.IsNullOrEmpty(HarmonicNumber) && !_continueClicked) return true;         
        bool isValid = false;        
        bool isNumeric = double.TryParse(HarmonicNumber, out double value);
        if (isNumeric) isValid = value is > 1.0 and <= 100_000;
        if (isValid) HarmonicValue = value;
        return isValid;
    }

    private bool IsOrbDegreeValid()
    {
        if (string.IsNullOrEmpty(OrbDegrees) && !_continueClicked) return true;          
        bool isValid = false;
        bool isNumeric = int.TryParse(OrbDegrees, out int value);
        if (isNumeric) isValid = value  > 0 && value < ResearchHarmonicDetailsModel.MaxOrbHarmonic;
        if (isValid) OrbDegreeValue = value;
        return isValid;
    }
    
    private bool IsOrbMinuteValid()
    {
        if (string.IsNullOrEmpty(OrbMinutes) && !_continueClicked) return true;          
        bool isValid = false;
        bool isNumeric = int.TryParse(OrbMinutes, out int value);
        if (isNumeric) isValid = value is >= 0 and <= 59;
        if (isValid) OrbMinuteValue = value;
        return isValid;
    }

}