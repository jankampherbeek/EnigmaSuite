// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Charts;

namespace Enigma.Domain.Interfaces;

/// <summary>Collection of available charts for the UI.</summary>
public interface ICurrentCharts
{

    /// <summary>Id for the chart that is currently active.</summary>
    public int IdPrimaryChart { get; set; }

    /// <summary>Id for a chart that is compared with the primary chart.</summary>
    public int IdSecondaryChart { get; set; }

    /// <summary>Add a chart to the set of current charts.</summary>
    /// <param name="newChart">The chart.</param>
    /// <param name="isPrimary">True if this is the primary chart.</param>
    /// <param name="isSecondary">True if this is the secundary chart.</param>
    public void AddChart(CalculatedChart newChart, bool isPrimary, bool isSecondary);

    /// <summary>Remove chart from the set of current charts.</summary>
    /// <param name="chartId">Id of the chart.</param>
    public void RemoveChart(int chartId);

}