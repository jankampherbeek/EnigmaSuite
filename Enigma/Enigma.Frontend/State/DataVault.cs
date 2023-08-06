// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Research;

namespace Enigma.Frontend.Ui.State;


/// <summary>Central vault for calculated charts and other data.</summary>
/// <remarks>Implemented as singleton, based on code by Jon Skeet: https://csharpindepth.com/articles/singleton .</remarks>
public sealed class DataVault
{
    private static readonly DataVault instance = new();

    private readonly List<CalculatedChart> _allCharts = new();
    private CalculatedChart? _currentChart;
    public ResearchProject? CurrentProject { get; set; }
    public ResearchMethods ResearchMethod { get; set; } = ResearchMethods.None;
    public ResearchPointsSelection? CurrentPointsSelection { get; set; }
    public bool ResearchIncludeCusps { get; set; }
    public MethodResponse? ResponseTest { get; set; }
    public MethodResponse? ResponseCg { get; set; }
    
    public double ResearchHarmonicValue { get; set; }
    public double ResearchHarmonicOrb { get; set; }
    
    
    private bool NewChartAdded;

    /// <summary>Base name for the current view (without the 'View' part)</summary>
    public string CurrentViewBase { get; set; }

    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static DataVault()
    {
    }

    private DataVault()
    {
    }

    public static DataVault Instance => instance;

    public void ClearExistingCharts()
    {
        _allCharts.Clear();
        _currentChart = null;
        NewChartAdded = false;
    }


    public void AddNewChart(CalculatedChart newChart)
    {
        CalculatedChart? chartToRemove = null;
        foreach (var chart in _allCharts)
        {
            if (chart.InputtedChartData.Id == newChart.InputtedChartData.Id)
            {
                chartToRemove = chart;
            }
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

    public List<CalculatedChart> GetAllCharts()
    {
        return _allCharts;
    }

    public void SetNewChartAdded(bool newStatus)
    {
        NewChartAdded = newStatus;
    }

    public bool GetNewChartAdded()
    {
        return NewChartAdded;
    }
}