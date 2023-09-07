// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ConfigProgViewModel:ObservableObject
{
    [NotifyPropertyChangedFor(nameof(OrbPrimDirValid))]
    [ObservableProperty] private string _orbPrimDirText;
    [ObservableProperty] private bool _includeConverseDirections;
    [ObservableProperty] private ObservableCollection<string> _allPrimDirMethods;
    [ObservableProperty] private ObservableCollection<string> _allPrimDirKeys;
    [ObservableProperty] private ObservableCollection<SelectableChartPointDetails> _allSignificators;
    
    private double _orbPrimDirValue;

    public SolidColorBrush OrbPrimDirValid => CheckOrbPrimDir() ? Brushes.White : Brushes.Yellow;
    
    public ConfigProgViewModel()
    {
        AllPrimDirMethods = new ObservableCollection<string>(ConfigProgModel.AllPrimDirMethods());
        AllPrimDirKeys = new ObservableCollection<string>(ConfigProgModel.AllPrimDirKeys());
        AllSignificators = new ObservableCollection<SelectableChartPointDetails>(ConfigProgModel.AllSignificators());
        _orbPrimDirText = "1.0";   // TODO read from new version of config
        _orbPrimDirValue = 1.0;    // TODO read from new version of config
        
        
        
    }
    
    private bool CheckOrbPrimDir()
    {
        return double.TryParse(OrbPrimDirText.Replace(',', '.'), out _orbPrimDirValue);
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "ConfigurationsProg";   // TODO create help file
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }
    
}