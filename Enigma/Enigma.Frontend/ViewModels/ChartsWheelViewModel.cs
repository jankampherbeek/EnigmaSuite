using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.WindowsFlow;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ChartsWheelViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.RADIX_POSITIONS;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
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
       //     descText = _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData);
       descText = _descriptiveChartText.FullDescriptiveText(config, chart.InputtedChartData);
        }
        return descText;
    }
    
    [RelayCommand]
    private void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseNonDlgMessage(VM_IDENTIFICATION, _windowId ));
    }
    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    
}