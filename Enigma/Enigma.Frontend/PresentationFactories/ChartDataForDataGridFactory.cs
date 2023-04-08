// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Domain.Persistency;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <inherritdoc/>
public sealed class ChartDataForDataGridFactory : IChartDataForDataGridFactory
{

    /// <inherritdoc/>
    public List<PresentableChartData> CreateChartDataForDataGrid(List<CalculatedChart> charts)
    {
        List<PresentableChartData> chartData = new();
        foreach (var chart in charts)
        {
            string id = chart.InputtedChartData.Id.ToString();
            string name = chart.InputtedChartData.MetaData.Name;
            string description = chart.InputtedChartData.MetaData.Description;
            chartData.Add(new(id, name, description));
        }
        return chartData;
    }


    /// <inherritdoc/>
    public List<PresentableChartData> CreateChartDataForDataGrid(List<PersistableChartData> charts)
    {
        List<PresentableChartData> chartData = new();
        foreach (var chart in charts)
        {
            string id = chart.Id.ToString();
            string name = chart.Name;
            string description = chart.Description;
            chartData.Add(new(id, name, description));
        }
        return chartData;
    }
}