// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

public class RadixSearchModel
{
    public List<PersistableChartData>? ChartsFound { get; private set; }
    private readonly IChartDataPersistencyApi _chartDataPersistencyApi;
    private readonly IChartCalculation _chartCalculation;
    private readonly IChartDataConverter _chartDataConverter;
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    

    public RadixSearchModel(IChartDataPersistencyApi chartDataPersistencyApi, 
        IChartCalculation chartCalculation, 
        IChartDataConverter chartDataConverter)
    {
        _chartDataPersistencyApi = chartDataPersistencyApi;
        _chartCalculation = chartCalculation;
        _chartDataConverter = chartDataConverter;
    }
    
    public void PerformSearch(string? searchArgument)
    {
        ChartsFound = string.IsNullOrEmpty(searchArgument) ? _chartDataPersistencyApi.ReadAllChartData() 
            : _chartDataPersistencyApi.SearchChartData(searchArgument);
    }
    
    public void AddFoundChartToDataVault(int chartId)
    {
        if (ChartsFound == null) return;
        PersistableChartData persistableChartData = ChartsFound[chartId];
        ChartData chartData = _chartDataConverter.FromPersistableChartData(persistableChartData);
        CalculatedChart calcChart = _chartCalculation.CalculateChart(chartData);
        _dataVaultCharts.AddNewChart(calcChart);
        _dataVaultCharts.SetCurrentChart(calcChart.InputtedChartData.Id);
    }
    
}