// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for midpoint details in research</summary>
public partial class ResearchMidpointDetailsViewModel: ObservableObject
{
    [ObservableProperty] private int _dialIndex;
    [NotifyCanExecuteChangedFor(nameof(ContinueCommand))]
    [NotifyPropertyChangedFor(nameof(OrbDegreeValid))]
    [ObservableProperty] private string _orbDegrees = "1";
    [NotifyCanExecuteChangedFor(nameof(ContinueCommand))]
    [NotifyPropertyChangedFor(nameof(OrbMinuteValid))]
    [ObservableProperty] private string _orbMinutes = "0";

    [ObservableProperty] private ObservableCollection<string> _dialSizes;
    private int OrbDegreeValue { get; set; }
    private int OrbMinuteValue { get; set; }

    
    public SolidColorBrush OrbDegreeValid => IsOrbDegreeValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush OrbMinuteValid => IsOrbMinuteValid() ? Brushes.White : Brushes.Yellow;

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
    
    
    [RelayCommand(CanExecute = nameof(IsInputOk))]
    private void Continue()
    {
        DataVaultResearch.Instance.ResearchCanceled = false;
        double orbValue = OrbDegreeValue + OrbMinuteValue / 60.0;        
        ResearchMidpointDetailsModel.SaveOrbMidpoints(orbValue);
        int dialDivision = DialIndex switch
        {
            0 => 1,
            1 => 4,
            2 => 8,
            _ => 1
        };
        ResearchMidpointDetailsModel.SaveDialDivision(dialDivision);
    }

    [RelayCommand]
    private static void Cancel()
    {
        DataVaultResearch.Instance.ResearchCanceled = true;
    }
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "ResearchMidpointDetails";
        new HelpWindow().ShowDialog();
    }
    
    private bool IsOrbDegreeValid()
    {
        bool isValid = false;
        bool isNumeric = int.TryParse(OrbDegrees, out int value);
        if (isNumeric) isValid = value  > 0 && value < ResearchMidpointDetailsModel.MaxOrbMidpoints;
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
    
    private bool IsInputOk()
    {
        return IsOrbDegreeValid() && IsOrbMinuteValid();
    }
    
}