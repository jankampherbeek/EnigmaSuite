// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.UiDomain;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Charts;


public class ChartsStartController
{

    public CurrentCharts AllCurrentCharts { get; set; }


    public ChartsStartController()
    {
        AllCurrentCharts = new CurrentCharts();
    }

    public void AddCalculatedChart(CalculatedChart newChart)
    {
        AllCurrentCharts.AddChart(newChart, true, false);
        ChartPositions? chartPositions = App.ServiceProvider.GetService<ChartPositions>();
        if (chartPositions != null)
        {
            chartPositions.ActiveChart = newChart;
            chartPositions.PopulateHouses();
            chartPositions.Show();
        }
    }

}