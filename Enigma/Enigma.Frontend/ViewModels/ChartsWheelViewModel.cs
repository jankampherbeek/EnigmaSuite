using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ChartsWheelViewModel: ObservableObject
{
    private readonly IDescriptiveChartText _descriptiveChartText;

    [ObservableProperty] private string _descriptionOfChart;
    
    public ChartsWheelViewModel()
    {
        _descriptiveChartText = new DescriptiveChartText();
        DescriptionOfChart = DescriptiveText();
    }
    
    private string DescriptiveText()
    {
        string descText = "";
        var chart = DataVaultCharts.Instance.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        if (chart != null)
        {
            descText = _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData);
        }
        return descText;
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "ChartsWheel";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }
    
    
}