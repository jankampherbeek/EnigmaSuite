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

/// <summary>ViewModel for showing radix aspects</summary>
public partial class RadixAspectsViewModel: ObservableObject
{
    [ObservableProperty] private ObservableCollection<PresentableAspects> _actualAspects;
    [ObservableProperty] private string _chartId;
    [ObservableProperty] private string _description;
    
    public RadixAspectsViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<RadixAspectsModel>();
        _actualAspects =  new ObservableCollection<PresentableAspects>(model.GetPresentableAspectsForChartPoints());
        _chartId = model.GetChartIdName();
        _description = model.DescriptiveText();
    }
    
    [RelayCommand]
    private static void Help()
    {
        ShowHelp();
    }
    
    private static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "RadixAspects";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }
    
}