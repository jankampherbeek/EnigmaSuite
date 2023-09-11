// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Domain.Persistency;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Presentables;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <inherritdoc/>
public sealed class ChartDataForDataGridFactory : IChartDataForDataGridFactory
{

    /// <inherritdoc/>
    public List<PresentableChartData> CreateChartDataForDataGrid(IEnumerable<CalculatedChart> charts)
    {
        return (from chart in charts 
            let id = chart.InputtedChartData.Id.ToString() 
            let name = chart.InputtedChartData.MetaData.Name 
            let description = chart.InputtedChartData.MetaData.Description 
            select new PresentableChartData(id, name, description)).ToList();
    }

    /// <inherritdoc/>
    public PresentableChartData CreatePresentableChartData(CalculatedChart chart)
    {
        string id = chart.InputtedChartData.Id.ToString();
        string name = chart.InputtedChartData.MetaData.Name;
        string description = chart.InputtedChartData.MetaData.Description; 
        return new PresentableChartData(id, name, description);
    }


    /// <inherritdoc/>
    public List<PresentableChartData> CreateChartDataForDataGrid(List<PersistableChartData>? charts)
    {
        List<PresentableChartData> chartData = new();
        if (charts != null) chartData.AddRange(
            from chart in charts 
            let id = chart.Id.ToString() 
            let name = chart.Name 
            let description = chart.Description 
            select new PresentableChartData(id, name, description));
        return chartData;
    }
}