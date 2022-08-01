// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain;
using Enigma.Frontend.Charts.Graphics;
using Enigma.Frontend.UiDomain;

namespace Enigma.Frontend.Charts;


public class MainController
{

    private ChartDataInputWindow _chartDataInputWindow;
    private ChartPositionsWindow _chartPositionsWindow;
    private ChartsWheel _chartsWheel;

    public CurrentCharts AllCurrentCharts { get; set; }


    public MainController(ChartDataInputWindow chartDataInputWindow, ChartPositionsWindow chartPositionsWindow, ChartsWheel chartsWheel)
    {
        _chartDataInputWindow = chartDataInputWindow;
        _chartPositionsWindow = chartPositionsWindow;
        _chartsWheel = chartsWheel;
        AllCurrentCharts = new CurrentCharts();
    }

    public void NewChart()
    {
        _chartDataInputWindow.ShowDialog();
        ShowWheel();
        ShowPositions();

    }


    public void AddCalculatedChart(CalculatedChart newChart)
    {
        AllCurrentCharts.AddChart(newChart, true, false);
    }

    private void ShowWheel()
    {
        _chartsWheel.Show();
        _chartsWheel.DrawChart();
    }
    private void ShowPositions()
    {
        _chartPositionsWindow.Show();
        _chartPositionsWindow.PopulateAll();
    }
}