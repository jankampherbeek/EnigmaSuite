// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for main charts screen</summary>
public sealed class ChartsMainModel
{
    private readonly DataVault _dataVault;
    private readonly IChartDataConverter _chartDataConverter;
    private readonly IEventDataConverter _eventDataConverter;
    private readonly IChartDataPersistencyApi _chartDataPersistencyApi;
    private readonly IEventDataPersistencyApi _eventDataPersistencyApi;
    private readonly IChartDataForDataGridFactory _chartDataForDataGridFactory;
    
    public ChartsMainModel(IChartDataConverter chartDataConverter,
                           IEventDataConverter eventDataConverter, 
                           IChartDataPersistencyApi chartDataPersistencyApi,
                           IEventDataPersistencyApi eventDataPersistencyApi,
                           IChartDataForDataGridFactory chartDataForDataGridFactory)
    {
        _chartDataConverter = chartDataConverter;
        _eventDataConverter = eventDataConverter;
        _chartDataPersistencyApi = chartDataPersistencyApi;
        _eventDataPersistencyApi = eventDataPersistencyApi;
        _chartDataForDataGridFactory = chartDataForDataGridFactory;
        _dataVault = DataVault.Instance;
    }

    public List<PresentableChartData> AvailableCharts()
    {
        return _chartDataForDataGridFactory.CreateChartDataForDataGrid(_dataVault.GetAllCharts());
    }
    
    public int SaveCurrentChart()
    {
        int newIndex = -1;
        var currentChart = _dataVault.GetCurrentChart();
        if (currentChart == null) return newIndex;
        ChartData chartData = currentChart.InputtedChartData;
        PersistableChartData persistableChartData = _chartDataConverter.ToPersistableChartData(chartData);
        newIndex = _chartDataPersistencyApi.AddChartData(persistableChartData);
        return newIndex;
    }
    
    public bool DeleteCurrentChart()
    {
        var currentChart = _dataVault.GetCurrentChart();
        if (currentChart == null) return false;
        int id = currentChart.InputtedChartData.Id;
        return _chartDataPersistencyApi.DeleteChartData(id);
    }

    public int CountPersistedCharts()
    {
        return _chartDataPersistencyApi.NumberOfRecords();
    }
    
    public string MostRecentChart()
    {
        int highestIndex = _chartDataPersistencyApi.HighestIndex();
        if (highestIndex == 0) return "-";
        PersistableChartData? chartData = _chartDataPersistencyApi.ReadChartData(highestIndex);
        return chartData != null ? chartData.Name : "-";
    }
    
    public string CurrentChartName()
    {
        var currentChart = _dataVault.GetCurrentChart();
        return currentChart != null ? currentChart.InputtedChartData.MetaData.Name : "";
    }

    public PresentableChartData? CurrentChart()
    {
        var currentChart = _dataVault.GetCurrentChart();
        return currentChart != null ? _chartDataForDataGridFactory.CreatePresentableChartData(currentChart) : null;
    }
    
    

}