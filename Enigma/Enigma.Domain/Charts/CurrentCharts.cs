// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Charts;


/// <inheritdoc/>
public sealed class CurrentCharts : ICurrentCharts
{
    /// <inheritdoc/>
    public int IdPrimaryChart { get; set; }
    /// <inheritdoc/>
    public int IdSecondaryChart { get; set; }

    public List<CalculatedChart> AllCurrentCharts { get; set; }

    public CurrentCharts()
    {
        IdPrimaryChart = -1;
        IdSecondaryChart = -1;
        AllCurrentCharts = new List<CalculatedChart>();
    }

    /// <inheritdoc/>
    public void AddChart(CalculatedChart newChart, bool isPrimary, bool isSecondary)
    {
        AllCurrentCharts.Add(newChart);
        if (isPrimary) IdPrimaryChart = newChart.InputtedChartData.Id;
        if (isSecondary) IdSecondaryChart = newChart.InputtedChartData.Id;
    }

    /// <inheritdoc/>
    public void RemoveChart(int chartId)
    {
        foreach (CalculatedChart chart in AllCurrentCharts.Where(chart => chart.InputtedChartData.Id == chartId))
        {
            AllCurrentCharts.Remove(chart);
        }
    }

}