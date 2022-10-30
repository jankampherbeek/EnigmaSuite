// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
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
    public void AddChart(CalculatedChart newChart, bool isPrimary, bool isSecondary);
    public void RemoveChart(int chartId);

}