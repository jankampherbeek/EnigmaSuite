// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for radix positions</summary>
public sealed class RadixPositionsModel
{
    private readonly IHousePosForDataGridFactory _housePosForDataGridFactory;
    private readonly ICelPointForDataGridFactory _celPointForDataGridFactory;
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly DataVault _dataVault;

    public RadixPositionsModel(IHousePosForDataGridFactory housePosForDataGridFactory,
        ICelPointForDataGridFactory celPointForDataGridFactory,
        IDescriptiveChartText descriptiveChartText)
    {
        _dataVault = DataVault.Instance;
        _housePosForDataGridFactory = housePosForDataGridFactory;
        _celPointForDataGridFactory = celPointForDataGridFactory;
        _descriptiveChartText = descriptiveChartText;
    }

    private ChartData? GetChartData()
    {
        CalculatedChart? currentChart = _dataVault.GetCurrentChart();
        return currentChart?.InputtedChartData;
    }

    public string GetIdName()
    {
        ChartData? chartData = GetChartData();
        return chartData != null ? chartData.MetaData.Name : "";
    }
    
    public string DescriptiveText()
    {
        var chart = _dataVault.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        return chart != null
            ? _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData)
            : "";
    }


    public List<PresentableHousePositions> GetHousePositionsCurrentChart()
    {
        CalculatedChart? currentChart = _dataVault.GetCurrentChart();
        return currentChart != null ? _housePosForDataGridFactory.CreateHousePosForDataGrid(currentChart.Positions) 
            : new List<PresentableHousePositions>();
    }

    public List<PresentableCommonPositions> GetCelPointPositionsCurrentChart()
    {
        CalculatedChart? currentChart = _dataVault.GetCurrentChart();
        return currentChart != null ? _celPointForDataGridFactory.CreateCelPointPosForDataGrid(currentChart.Positions) 
            : new List<PresentableCommonPositions>();
    }

    public CalculatedChart? GetCalculatedChart()
    {
        return _dataVault.GetCurrentChart();
    }
}