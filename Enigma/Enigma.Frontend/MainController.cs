// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.Research;

namespace Enigma.Frontend.Ui;


public class MainController
{
    private readonly Rosetta _rosetta = Rosetta.Instance;



    public CurrentCharts AllCurrentCharts { get; set; }         // Todo move to ChartsMainController


    public MainController(ChartsWheel chartsWheel)
    {
        AllCurrentCharts = new CurrentCharts();   // move to maincharts
    }


    public void ShowResearchMain()
    {
        ResearchMainWindow researchMainWindow = new();
        researchMainWindow.ShowDialog();
    }

    public void ShowChartsMain()
    {
        ChartsMainWindow chartsMainWindow = new();
        chartsMainWindow.ShowDialog();
    }

}