// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Slices;
using Enigma.Core.Slices.VenusStarPoint;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for Venus Star Point window</summary>
public class VspModel
{
    private readonly VenusStarPointService _venusStarPointService;
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;

    public VspModel(VenusStarPointService venusStarPointService)
    {
        _venusStarPointService = venusStarPointService;
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
    /// Get VSP positions for the current chart
    /// </summary>
    /// <param name="isPrenatal">Whether to include prenatal positions</param>
    /// <returns>List of presentable VSP positions</returns>
    public List<PresentableVspPosition> GetVspPositions(bool isPrenatal)
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        if (currentChart == null)
        {
            Log.Warning("VspModel.GetVspPositions: No current chart available");
            return new List<PresentableVspPosition>();
        }

        try
        {
            var birthJd = currentChart.InputtedChartData.FullDateTime.JulianDayForEt;
            var ayanamsha = GetAyanamshaFromConfiguration();
            Log.Information($"VspModel.GetVspPositions: Birth JD: {birthJd}, Ayanamsha: {ayanamsha}, Prenatal: {isPrenatal}");
            
            var request = new VenusStarPointRequest(birthJd, isPrenatal, ayanamsha);
            var vspPositions = _venusStarPointService.VenusStarPointCalculation(request);
            
            Log.Information($"VspModel.GetVspPositions: Calculated {vspPositions.Count} VSP positions");
            
            var result = vspPositions.Select(pos => new PresentableVspPosition(
                pos,
                FormatDate(pos.Jd),
                FormatTime(pos.Jd)
            )).ToList();
            
            Log.Information($"VspModel.GetVspPositions: Created {result.Count} presentable VSP positions");
            return result;
        }
        catch (System.Exception ex)
        {
            Log.Error(ex, "VspModel.GetVspPositions: Error calculating VSP positions");
            return new List<PresentableVspPosition>();
        }
    }
    
    /// <summary>Get the ayanamsha from the current configuration</summary>
    /// <returns>Ayanamsha setting</returns>
    private Ayanamshas GetAyanamshaFromConfiguration()
    {
        // Get the current configuration to determine if we should use sidereal
        var config = CurrentConfig.Instance.GetConfig();
        return config.Ayanamsha;
    }
    
    /// <summary>Format Julian Day to date string</summary>
    /// <param name="jd">Julian Day</param>
    /// <returns>Formatted date string</returns>
    private string FormatDate(double jd)
    {
        // Simple date formatting - in a real implementation you'd use a proper date conversion
        return $"JD {jd:F1}";
    }
    
    /// <summary>Format Julian Day to time string</summary>
    /// <param name="jd">Julian Day</param>
    /// <returns>Formatted time string</returns>
    private string FormatTime(double jd)
    {
        // Simple time formatting - in a real implementation you'd use a proper time conversion
        return $"Time {jd:F4}";
    }
}
