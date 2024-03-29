﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Domain.Presentables;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>Create presentable versions for chart data to be used in a datagrid.</summary>
public interface IChartDataForDataGridFactory
{
    /// <summary>Create list with PresentableChartData from list with CalculatedChart.</summary>
    /// <param name="charts">The charts for which to create presentable versions.</param>
    /// <returns>The presentable versions of the charts data.</returns>
    public List<PresentableChartData> CreateChartDataForDataGrid(IEnumerable<CalculatedChart> charts);

    /// <summary>Create list with PresentableChartData from list with PersistableChartData.</summary>
    /// <param name="charts">The charts for which to create presentable versions.</param>
    /// <returns>The presentable versions of the charts data.</returns>
    public List<PresentableChartData> CreateChartDataForDataGrid(List<PersistableChartData>? charts);

    /// <summary>Create instance of PresentableChartData from CalculatedChart.</summary>
    /// <param name="chart">The chart for which to create a presentable version.</param>
    /// <returns>TRhe presentable version of the chart data</returns>
    public PresentableChartData? CreatePresentableChartData(CalculatedChart chart);
}

/// <inherritdoc/>
public sealed class ChartDataForDataGridFactory : IChartDataForDataGridFactory
{

    /// <inherritdoc/>
    public List<PresentableChartData> CreateChartDataForDataGrid(IEnumerable<CalculatedChart> charts)
    {
        var x = (from chart in charts 
            let id = chart.InputtedChartData.Id.ToString() 
            let name = chart.InputtedChartData.MetaData.Name 
            let description = chart.InputtedChartData.MetaData.Description 
            select new PresentableChartData(id, name, description)).ToList();
        return x;
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
            let id = chart.Identification.Id.ToString() 
            let name = chart.Identification.Name 
            let description = chart.Identification.Description 
            select new PresentableChartData(id, name, description));
        return chartData;
    }
}