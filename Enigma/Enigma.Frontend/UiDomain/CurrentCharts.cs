// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;

namespace Enigma.Frontend.UiDomain;


/// <summary>Collection of available charts for the UI.</summary>
public interface ICurrentCharts
{

    /// <summary>Id for the chart that is currently active.</summary>
    public int IdPrimaryChart { get; set; }

    /// <summary>Id for a chart that is compared with the primary chart.</summary>
    public int IdSecondaryChart { get; set; }
    public void AddChart(CalculatedChart newChart, bool isPrimary, bool isSecondary);
    public void RemoveChart(int chartId);

}


/// <inhertidoc/>
public class CurrentCharts: ICurrentCharts
{
    public int IdPrimaryChart { get; set; }
    public int IdSecondaryChart { get; set; }

    public List<CalculatedChart> AllCurrentCharts { get; set; }

    public CurrentCharts() {
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
            if (chart.InputtedChartData.Id == chartId) {
                AllCurrentCharts.Remove(chart);
            }
        }
    }

}