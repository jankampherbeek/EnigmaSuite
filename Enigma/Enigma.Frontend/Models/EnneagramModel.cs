// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Slices;
using Enigma.Core.Slices.Enneagram;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

/// <summary>
/// Model for Enneagram calculations and data management
/// </summary>
public class EnneagramModel
{
    private readonly EnneagramService _enneagramService;
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;

    public EnneagramModel(EnneagramService enneagramService)
    {
        _enneagramService = enneagramService;
    }

    /// <summary>
    /// Get the current chart name
    /// </summary>
    /// <returns>Name of the current chart or empty string if no chart</returns>
    public string GetCurrentChartName()
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        return currentChart?.InputtedChartData.MetaData.Name ?? "";
    }

    /// <summary>
    /// Calculate Enneagram strengths for the current chart
    /// </summary>
    /// <param name="selectedPoints">Selected chart points</param>
    /// <param name="includeHouses">Whether to include houses in calculation</param>
    /// <param name="countPlutoTwice">Whether to count Pluto twice</param>
    /// <returns>List of Enneagram strengths sorted by strength (high to low)</returns>
    public List<KeyValuePair<int, double>> CalculateEnneagramStrengths(
        List<ChartPoints> selectedPoints, 
        bool includeHouses, 
        bool countPlutoTwice)
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        if (currentChart == null)
        {
            Log.Warning("EnneagramModel.CalculateEnneagramStrengths: No current chart available");
            return new List<KeyValuePair<int, double>>();
        }

        var chartData = currentChart.InputtedChartData;
        
        // Create EnneagramRequest
        var request = new EnneagramRequest(
            chartData.FullDateTime.JulianDayForEt,
            chartData.Location.GeoLong,
            chartData.Location.GeoLat,
            selectedPoints,
            includeHouses,
            countPlutoTwice
        );

        // Calculate strengths
        var strengths = _enneagramService.DefineEnneagramStrengths(request);
        
        // Sort by strength (high to low)
        return strengths.OrderByDescending(kvp => kvp.Value).ToList();
    }

    /// <summary>
    /// Get the default list of chart points for Enneagram calculation
    /// </summary>
    /// <returns>List of default chart points</returns>
    public List<ChartPoints> GetDefaultChartPoints()
    {
        return new List<ChartPoints>
        {
            ChartPoints.Sun,
            ChartPoints.Moon,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars,
            ChartPoints.Jupiter,
            ChartPoints.Saturn,
            ChartPoints.Uranus,
            ChartPoints.Neptune,
            ChartPoints.Pluto,
            ChartPoints.Chiron,
            ChartPoints.TrueNode,
            ChartPoints.ApogeeMean
        };
    }

    /// <summary>
    /// Get the name for an Enneagram type
    /// </summary>
    /// <param name="type">Enneagram type (1-9)</param>
    /// <returns>Name of the Enneagram type</returns>
    public string GetEnneagramTypeName(int type)
    {
        return type switch
        {
            1 => "Perfectionist",
            2 => "Helper",
            3 => "Winner",
            4 => "Feeler",
            5 => "Observer",
            6 => "Loyalist",
            7 => "Optimist",
            8 => "Leader",
            9 => "Peacemaker",
            _ => $"Type {type}"
        };
    }

    /// <summary>
    /// Check if a chart is currently loaded
    /// </summary>
    /// <returns>True if a chart is loaded, false otherwise</returns>
    public bool IsChartLoaded()
    {
        return _dataVaultCharts.GetCurrentChart() != null;
    }
} 