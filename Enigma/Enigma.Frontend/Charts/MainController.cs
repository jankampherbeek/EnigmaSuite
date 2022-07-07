// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.UiDomain;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Charts;


public class MainController
{

    private ChartDataInputWindow _chartDataInputWindow;
    private ChartPositionsWindow _chartPositionsWindow;

    public CurrentCharts AllCurrentCharts { get; set; }


    public MainController(ChartDataInputWindow chartDataInputWindow, ChartPositionsWindow chartPositionsWindow)
    {
        _chartDataInputWindow = chartDataInputWindow;
        _chartPositionsWindow = chartPositionsWindow;
        AllCurrentCharts = new CurrentCharts();
    }

    public void NewChart()
    {
        _chartDataInputWindow.ShowDialog();
        ShowPositions();
    }


    public void AddCalculatedChart(CalculatedChart newChart)
    {
        AllCurrentCharts.AddChart(newChart, true, false);
    }


    private void ShowPositions()
    {
        _chartPositionsWindow.Show();
        _chartPositionsWindow.PopulateAll();
    }
}