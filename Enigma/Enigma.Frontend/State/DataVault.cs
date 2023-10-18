// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Research;
using Enigma.Domain.Responses;

namespace Enigma.Frontend.Ui.State;


/// <summary>Central vault for calculated charts and other data.</summary>
/// <remarks>Implemented as singleton, based on code by Jon Skeet: https://csharpindepth.com/articles/singleton .</remarks>
public sealed class DataVault
{
    // TODO Too many global vars, restrict to a minimum.
    
    
    private static readonly DataVault instance = new();

    private readonly List<CalculatedChart> _allCharts = new();
    private CalculatedChart? _currentChart;
    public ProgresMethods CurrentProgresMethod { get; set; } = ProgresMethods.Undefined;
    public ProgEvent? CurrentProgEvent { get; set; }
    public ProgPeriod? CurrentProgPeriod { get; set; }
    public ResearchProject? CurrentProject { get; set; }
    public ResearchMethods ResearchMethod { get; set; } 
    public ResearchPointsSelection? CurrentPointsSelection { get; set; }
    public bool ResearchCanceled { get; set; } 
    public bool ResearchIncludeCusps { get; set; }
    public MethodResponse? ResponseTest { get; set; }
    public MethodResponse? ResponseCg { get; set; }
    
    public double ResearchHarmonicValue { get; set; }
    public double ResearchHarmonicOrb { get; set; }

    public int ResearchMidpointDialDivision { get; set; }
    public double ResearchMidpointOrb { get; set; }
    
    
    private bool _newChartAdded;

    /// <summary>Base name for the current view (without the 'View' part)</summary>
    public string CurrentViewBase { get; set; } = string.Empty;

    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static DataVault()
    {
    }

    private DataVault()
    {
    }

    // ReSharper disable once ConvertToAutoProperty
    public static DataVault Instance => instance;   // instance is a singleton

    public void ClearExistingCharts()
    {
        _allCharts.Clear();
        _currentChart = null;
        _newChartAdded = false;
    }


    public void AddNewChart(CalculatedChart newChart)
    {
        CalculatedChart? chartToRemove = null;
        foreach (CalculatedChart chart in _allCharts.Where(
                     chart => chart.InputtedChartData.Id == newChart.InputtedChartData.Id))
        {
            chartToRemove = chart;
        }
        if (chartToRemove != null)
        {
            _allCharts.Remove(chartToRemove);
        }
        _allCharts.Add(newChart);
        _currentChart = newChart;
    }


    public CalculatedChart? GetCurrentChart()
    {
        if (_currentChart != null) return _currentChart;
        Log.Error("No chart available while using GetCurrentChart() in DataVault");
        return null;
    }

    public void SetCurrentChart(int id)
    {
        foreach (CalculatedChart chart in _allCharts.Where(chart => chart.InputtedChartData.Id == id))
        {
            _currentChart = chart;
        }
    }

    public void SetIndexCurrentChart(int index)
    {
        if (_allCharts.Count > index) _currentChart = _allCharts[index];
    }
    
    public IEnumerable<CalculatedChart> GetAllCharts()
    {
        return _allCharts;
    }

    public void SetNewChartAdded(bool newStatus)
    {
        _newChartAdded = newStatus;
    }

    public bool GetNewChartAdded()
    {
        return _newChartAdded;
    }


    
}