﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;

namespace Enigma.Frontend.Ui.State;


/// <summary>Central vault for calculated charts and other data.</summary>
/// <remarks>Implemented as singleton, based on code by Jon Skeet: https://csharpindepth.com/articles/singleton .</remarks>
public sealed class DataVaultCharts
{

    public int LastWindowId { get; set; }
    private static readonly DataVaultCharts instance = new();

    private readonly List<CalculatedChart> _allCharts = new();
    private CalculatedChart? _currentChart;
    private bool _newChartAdded;
    private int _indexCurrentChart;



    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static DataVaultCharts()
    {
    }

    private DataVaultCharts()
    {
    }

    // ReSharper disable once ConvertToAutoProperty
    public static DataVaultCharts Instance => instance;   // instance is a singleton

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

    public void RemoveDeletedChart()
    {
        _allCharts.Remove(GetCurrentChart());
        _currentChart = null;
    }
    
    
    public CalculatedChart? GetCurrentChart()
    {
        if (_currentChart != null) return _currentChart;
        Log.Error("No chart available while using GetCurrentChart() in DataVault");
        return null;
    }

    public void SetCurrentChart(long id)
    {
        foreach (CalculatedChart chart in _allCharts.Where(chart => chart.InputtedChartData.Id == id))
        {
            _currentChart = chart;
        }
    }

    public void SetIndexCurrentChart(int index)
    {
        if (_allCharts.Count > index) _currentChart = _allCharts[index];
        _indexCurrentChart = index;
    }

    public int GetIndexCurrentChart()
    {
        if (_currentChart is null) return -1;
        int index = -1;
        for (int i = 0; i < _allCharts.Count; i++)
        {
            if (_allCharts[i].InputtedChartData.Id == _currentChart.InputtedChartData.Id)
            {
                index = i;
            }
        }
        return index;
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