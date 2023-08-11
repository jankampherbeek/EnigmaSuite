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

/// <summary>ViewModel for radix positions</summary>
public partial class RadixPositionsViewModel: ObservableObject
{
    [ObservableProperty] private ObservableCollection<PresentableHousePositions> _actualHousePositions;
    [ObservableProperty] private ObservableCollection<PresentableCommonPositions> _actualPointPositions;
    [ObservableProperty] private string _chartId;
    [ObservableProperty] private string _details;
    
    

    public RadixPositionsViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<RadixPositionsModel>();
        _chartId = model.GetIdName();
        _details = model.DescriptiveText();
        _actualHousePositions = new ObservableCollection<PresentableHousePositions>(model.GetHousePositionsCurrentChart());
        _actualPointPositions =
            new ObservableCollection<PresentableCommonPositions>(model.GetCelPointPositionsCurrentChart());
    }
    
    [RelayCommand]
    private static void Help()
    {
        ShowHelp();
    }
    
    private static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "RadixPositions";
        new HelpWindow().ShowDialog();
    }

}