// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Windows;
using Enigma.Api.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Domain.Persistency;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Charts;

public sealed class SearchChartController
{
    private readonly IChartDataPersistencyApi _chartDataPersistencyApi;
    private readonly IChartDataForDataGridFactory _chartDataForDataGridFactory;
    private readonly IChartCalculation _chartCalculation;
    private readonly IChartDataConverter _chartDataConverter;
    private readonly DataVault _dataVault = DataVault.Instance;

    private List<PersistableChartData>? _chartsFound = new();
    public SearchChartController(IChartDataPersistencyApi chartDataPersistencyApi, 
        IChartDataForDataGridFactory chartDataForDataGridFactory, 
        IChartCalculation chartCalculation, 
        IChartDataConverter chartDataConverter)
    {
        _chartDataPersistencyApi = chartDataPersistencyApi;
        _chartDataForDataGridFactory = chartDataForDataGridFactory;
        _chartCalculation = chartCalculation;
        _chartDataConverter = chartDataConverter;
    }

    public void PerformSearch(string? searchArgument)
    {
        _chartsFound = string.IsNullOrEmpty(searchArgument) ? _chartDataPersistencyApi.ReadAllChartData() : _chartDataPersistencyApi.SearchChartData(searchArgument);
    }

    public IEnumerable<PresentableChartData> SearchedChartData()
    {
        return _chartDataForDataGridFactory.CreateChartDataForDataGrid(_chartsFound);
    }

    public void AddFoundChartToDataVault(PresentableChartData presentableChartData)
    {
        int id = int.Parse(presentableChartData.Id);
        PersistableChartData? persistableChartData = _chartDataPersistencyApi.ReadChartData(id);
        if (persistableChartData == null) return;
        ChartData chartData = _chartDataConverter.FromPersistableChartData(persistableChartData);
        CalculatedChart calcChart = _chartCalculation.CalculateChart(chartData);
        _dataVault.AddNewChart(calcChart);
    }

    public static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "ChartsSearch";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }

}