// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for main charts screen</summary>
public sealed class ChartsMainModel
{
    private readonly DataVaultCharts _dataVaultCharts;
    private readonly IChartDataConverter _chartDataConverter;
    private readonly IChartDataPersistencyApi _chartDataPersistencyApi;
    private readonly IChartDataForDataGridFactory _chartDataForDataGridFactory;
    
    public ChartsMainModel(IChartDataConverter chartDataConverter,
                           IChartDataPersistencyApi chartDataPersistencyApi,
                           IChartDataForDataGridFactory chartDataForDataGridFactory)
    {
        _chartDataConverter = chartDataConverter;
        _chartDataPersistencyApi = chartDataPersistencyApi;
        _chartDataForDataGridFactory = chartDataForDataGridFactory;
        _dataVaultCharts = DataVaultCharts.Instance;
    }

    public List<PresentableChartData> AvailableCharts()
    {
        return _chartDataForDataGridFactory.CreateChartDataForDataGrid(_dataVaultCharts.GetAllCharts());
    }
    
    public long SaveCurrentChart()
    {
        long newIndex = -1;
        var currentChart = _dataVaultCharts.GetCurrentChart();
        if (currentChart == null) return newIndex;
        ChartData chartData = currentChart.InputtedChartData;
        PersistableChartData persistableChartData = _chartDataConverter.ToPersistableChartData(chartData);
        Log.Information("ChartsMainModel.SaveCurrentChart(): calls ChartDataPersistencyApi.AddChartData()");
        newIndex = _chartDataPersistencyApi.AddChartData(persistableChartData);
        return newIndex;
    }
    
    public bool DeleteCurrentChart()
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        if (currentChart == null) return false;
        long id = currentChart.InputtedChartData.Id;
        bool deleteOk = _chartDataPersistencyApi.DeleteChartData(id);
        if (deleteOk) _dataVaultCharts.RemoveDeletedChart();
        return deleteOk;
    }

    public long CountPersistedCharts()
    {
        Log.Information("ChartsMainModel.CountPersistedCharts(): requesting number of records from chartdata persistency api");
        return _chartDataPersistencyApi.NumberOfRecords();
    }
    
    public string MostRecentChart()
    {
        long highestIndex = _chartDataPersistencyApi.HighestIndex();
        if (highestIndex == 0) return "-";
        PersistableChartData? chartData = _chartDataPersistencyApi.ReadChartData(highestIndex);
        return chartData != null ? chartData.Identification.Name : "-";
    }
    
    public string CurrentChartName()
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        return currentChart != null ? currentChart.InputtedChartData.MetaData.Name : "";
    }

    public PresentableChartData? GetCurrentChart()
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        return currentChart != null ? _chartDataForDataGridFactory.CreatePresentableChartData(currentChart) : null;
    }

    public void SetCurrentChartId(int id)
    {
        _dataVaultCharts.SetCurrentChart(id);
        _dataVaultCharts.SetIndexCurrentChart(id);
    }

    public int IndexCurrentChart()
    {
        return _dataVaultCharts.GetIndexCurrentChart();
    }

}