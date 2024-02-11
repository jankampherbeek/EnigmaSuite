// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>Collection of available charts for the UI.</summary>
public interface ICurrentCharts
{

    /// <summary>Id for the chart that is currently active.</summary>
    public long IdPrimaryChart { get; set; }

    /// <summary>Id for a chart that is compared with the primary chart.</summary>
    public long IdSecondaryChart { get; set; }

    /// <summary>Add a chart to the set of current charts.</summary>
    /// <param name="newChart">The chart.</param>
    /// <param name="isPrimary">True if this is the primary chart.</param>
    /// <param name="isSecondary">True if this is the secondary chart.</param>
    public void AddChart(CalculatedChart newChart, bool isPrimary, bool isSecondary);

    /// <summary>Remove chart from the set of current charts.</summary>
    /// <param name="chartId">Id of the chart.</param>
    public void RemoveChart(long chartId);

}

/// <inheritdoc/>
public sealed class CurrentCharts : ICurrentCharts
{
    /// <inheritdoc/>
    public long IdPrimaryChart { get; set; } = -1L;

    /// <inheritdoc/>
    public long IdSecondaryChart { get; set; } = -1L;

    private List<CalculatedChart> AllCurrentCharts { get; } = new();

    /// <inheritdoc/>
    public void AddChart(CalculatedChart newChart, bool isPrimary, bool isSecondary)
    {
        AllCurrentCharts.Add(newChart);
        if (isPrimary) IdPrimaryChart = newChart.InputtedChartData.Id;
        if (isSecondary) IdSecondaryChart = newChart.InputtedChartData.Id;
    }

    /// <inheritdoc/>
    public void RemoveChart(long chartId)
    {
        foreach (CalculatedChart chart in AllCurrentCharts.Where(chart => chart.InputtedChartData.Id == chartId))
        {
            AllCurrentCharts.Remove(chart);
        }
    }

}