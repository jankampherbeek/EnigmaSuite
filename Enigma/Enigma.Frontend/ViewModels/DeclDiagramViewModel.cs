// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Frontend.Ui.WindowsFlow;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class DeclDiagramViewModel:ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.DECL_DIAGRAM;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;

    [ObservableProperty] private string _descriptionOfChart;
    [ObservableProperty] private string _obliquityText;
    [ObservableProperty] private bool _applyHidingPositionLines;
    public DeclDiagramViewModel()
    {
        _descriptiveChartText = new DescriptiveChartText();
        DescriptionOfChart = DescriptiveText();
        _doubleToDmsConversions = new DoubleToDmsConversions();
        ObliquityText = "Obliquity: " + ObliquityValueText();
    }
    
    
    private string DescriptiveText()
    {
        string descText = "";
        var chart = DataVaultCharts.Instance.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        if (chart != null) descText = _descriptiveChartText.FullDescriptiveText(config, chart.InputtedChartData);
        return descText;
    }

    private string ObliquityValueText()
    {
        double obliquity = DataVaultCharts.Instance.GetCurrentChart().Obliquity;
        return _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(obliquity);
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