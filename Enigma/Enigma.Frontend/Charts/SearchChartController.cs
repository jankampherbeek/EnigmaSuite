// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Charts;
using Enigma.Domain.Persistency;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using System;
using System.Collections.Generic;

namespace Enigma.Frontend.Uit.Charts;

public class SearchChartController
{
    private readonly IChartDataPersistencyApi _chartDataPersistencyApi;
    private readonly IChartDataForDataGridFactory _chartDataForDataGridFactory;
    private readonly IChartCalculation _chartCalculation;
    private readonly IChartDataConverter _chartDataConverter;
    private readonly DataVault _dataVault = DataVault.Instance;

    private List<PersistableChartData> chartsFound = new();
    public SearchChartController(IChartDataPersistencyApi chartDataPersistencyApi, IChartDataForDataGridFactory chartDataForDataGridFactory, IChartCalculation chartCalculation, IChartDataConverter chartDataConverter)
    {
        _chartDataPersistencyApi = chartDataPersistencyApi;
        _chartDataForDataGridFactory = chartDataForDataGridFactory;
        _chartCalculation = chartCalculation;
        _chartDataConverter = chartDataConverter;
    }

    public void PerformSearch(string searchArgument)
    {
        if (string.IsNullOrEmpty(searchArgument)) chartsFound = _chartDataPersistencyApi.ReadAllChartData();
        else chartsFound = _chartDataPersistencyApi.SearchChartData(searchArgument);
    }

    public List<PresentableChartData> SearchedChartData()
    {
        return _chartDataForDataGridFactory.CreateChartDataForDataGrid(chartsFound); 
    }

    public void AddFoundChartToDataVault(PresentableChartData presentableChartData)
    {
        int id = Int32.Parse(presentableChartData.Id);
        PersistableChartData persistableChartData = _chartDataPersistencyApi.ReadChartData(id);
        if (persistableChartData != null)
        {
            ChartData chartData = _chartDataConverter.FromPersistableChartData(persistableChartData);
            CalculatedChart calcChart = _chartCalculation.CalculateChart(chartData);
            _dataVault.AddNewChart(calcChart);
        }
    }

}