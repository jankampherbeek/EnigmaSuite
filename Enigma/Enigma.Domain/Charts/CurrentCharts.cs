// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Charts;


/// <inhertidoc/>
public class CurrentCharts : ICurrentCharts
{
    public int IdPrimaryChart { get; set; }
    public int IdSecondaryChart { get; set; }

    public List<CalculatedChart> AllCurrentCharts { get; set; }

    public CurrentCharts()
    {
        IdPrimaryChart = -1;
        IdSecondaryChart = -1;
        AllCurrentCharts = new List<CalculatedChart>();
    }

    public void AddChart(CalculatedChart newChart, bool isPrimary, bool isSecondary)
    {
        AllCurrentCharts.Add(newChart);
        if (isPrimary) IdPrimaryChart = newChart.InputtedChartData.Id;
        if (isSecondary) IdSecondaryChart = newChart.InputtedChartData.Id;
    }

    public void RemoveChart(int chartId)
    {
        foreach (var chart in AllCurrentCharts)
        {
            if (chart.InputtedChartData.Id == chartId)
            {
                AllCurrentCharts.Remove(chart);
            }
        }
    }

}