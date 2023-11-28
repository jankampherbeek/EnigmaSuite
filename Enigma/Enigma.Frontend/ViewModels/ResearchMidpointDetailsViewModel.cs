// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Enigma.Frontend.Ui.WindowsFlow;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for midpoint details in research</summary>
public partial class ResearchMidpointDetailsViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ResearchWindowsFlow.RESEARCH_MIDPOINT_DETAILS;
    private bool _continueClicked;    
    [ObservableProperty] private int _dialIndex;
    [NotifyPropertyChangedFor(nameof(OrbDegreeValid))]
    [ObservableProperty] private string _orbDegrees = "1";
    [NotifyPropertyChangedFor(nameof(OrbMinuteValid))]
    [ObservableProperty] private string _orbMinutes = "0";

    [ObservableProperty] private ObservableCollection<string> _dialSizes;
    private int OrbDegreeValue { get; set; }
    private int OrbMinuteValue { get; set; }

    
    public SolidColorBrush OrbDegreeValid => IsOrbDegreeValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush OrbMinuteValid => IsOrbMinuteValid() ? Brushes.Gray : Brushes.Red;

    public ResearchMidpointDetailsViewModel()
    {
        DataVaultResearch.Instance.ResearchCanceled = true;
        List<string> dialSizeList = new()
        {
            "Dial 360\u00B0",
            "Dial 90\u00B0",
            "Dial 45\u00B0"
        };
        DialSizes = new ObservableCollection<string>(dialSizeList);
    }
    
    
    [RelayCommand]
    private void Continue()
    {
        _continueClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            int dialDivision = DialIndex switch
            {
                0 => 1,
                1 => 4,
                2 => 8,
                _ => 1
            };
            MidpointDetailsSelection selection = new(dialDivision, OrbDegreeValue + OrbMinuteValue / 60.0);
            WeakReferenceMessenger.Default.Send(new MidpointDetailsMessage(selection));
            WeakReferenceMessenger.Default.Send(new CompletedMessage(VM_IDENTIFICATION));
        }
        else
        {
            MessageBox.Show(errors, StandardTexts.TITLE_ERROR);
        }
    }
    
    private string FindErrors()
    {
        StringBuilder errorsText = new();
        if (!IsOrbDegreeValid())
            errorsText.Append(StandardTexts.ERROR_DEGREE + EnigmaConstants.NEW_LINE);
        if (!IsOrbMinuteValid())
            errorsText.Append(StandardTexts.ERROR_MINUTE + EnigmaConstants.NEW_LINE);
        return errorsText.ToString();
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
    
    private bool IsOrbDegreeValid()
    {
        if (string.IsNullOrEmpty(OrbDegrees) && !_continueClicked) return true;     
        bool isValid = false;
        bool isNumeric = int.TryParse(OrbDegrees, out int value);
        if (isNumeric) isValid = value  > 0 && value < ResearchMidpointDetailsModel.MaxOrbMidpoints;
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