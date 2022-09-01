// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain;
using Enigma.Frontend.Charts;
using Enigma.Frontend.Charts.Graphics;
using Enigma.Frontend.UiDomain;

namespace Enigma.Frontend;


public class MainController
{
    private AboutWindow _aboutWindow;
    private ChartDataInputWindow _chartDataInputWindow;
    private ChartPositionsWindow _chartPositionsWindow;
    private ChartsWheel _chartsWheel;

    public CurrentCharts AllCurrentCharts { get; set; }


    public MainController(AboutWindow aboutWindow, ChartDataInputWindow chartDataInputWindow, ChartPositionsWindow chartPositionsWindow, ChartsWheel chartsWheel)
    {
        _aboutWindow = aboutWindow;
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

    public void ShowAbout()
    {
        _aboutWindow.ShowDialog();
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