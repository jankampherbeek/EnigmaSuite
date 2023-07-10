// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Charts;
using Enigma.Frontend.Ui.Research;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Enigma.Frontend.Ui;

/// <summary>Shows the initial dashboard and offers the posibility to start the different modules of Enigma.</summary>
public class MainController
{

    public CurrentCharts AllCurrentCharts { get; set; }         // Todo 0.2 move to ChartsMainController


    public MainController()
    {
        AllCurrentCharts = new CurrentCharts();   // move to maincharts
    }

    /// <summary>Show module for research.</summary>
    public void ShowResearchMain()
    {
        ResearchMainWindow researchMainWindow = new();
        researchMainWindow.ShowDialog();
    }

    /// <summary>Show module for charts.</summary>
    public void ShowChartsMain()
    {
        ChartsMainWindow chartsMainWindow = new();
        chartsMainWindow.ShowDialog();
    }

    public static void HelpClick()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("MainWindow");
        helpWindow.ShowDialog();
    }

}