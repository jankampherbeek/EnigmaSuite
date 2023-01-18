// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Charts;
using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.Research;

namespace Enigma.Frontend.Ui;


public class MainController
{
    private readonly ResearchMainWindow _researchMainWindow = new();
    private readonly ChartsMainWindow _chartsMainWindow = new();
    public CurrentCharts AllCurrentCharts { get; set; }         // Todo move to ChartsMainController


    public MainController()
    {
        AllCurrentCharts = new CurrentCharts();   // move to maincharts
    }


    public void ShowResearchMain()
    {
        _researchMainWindow.ShowDialog();
    }

    public void ShowChartsMain()
    {
        _chartsMainWindow.ShowDialog();
    }

}