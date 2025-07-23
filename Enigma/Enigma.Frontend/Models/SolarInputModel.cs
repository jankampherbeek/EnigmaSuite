// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Calc;
using Enigma.Api.Slices;
using Enigma.Core.Slices.Solar;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.State;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for solar input window</summary>
public sealed class SolarInputModel(SolarService solarService, ICalculationPreferencesCreator prefsCreator,
    IChartAllPositionsApi chartAllPositionsApi, IObliquityApi obliquityApi)
{
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;

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
    /// Validate if a chart is currently selected
    /// </summary>
    /// <returns>True if a chart is selected, false otherwise</returns>
    public bool IsChartSelected()
    {
        return _dataVaultCharts.GetCurrentChart() != null;
    }

    /// <summary>
    /// Calculate solar return (placeholder for future implementation)
    /// </summary>
    /// <param name="age">Age for the solar return</param>
    /// <param name="siderealReturn">Whether to use sidereal return</param>
    /// <param name="relocate">Whether to relocate</param>
    /// <param name="longitude">Longitude for relocation</param>
    /// <param name="latitude">Latitude for relocation</param>
    /// <returns>True if calculation was successful</returns>
    public bool CalculateSolarReturn(int age, bool siderealReturn, bool relocate, 
                                   double longitude = 0, double latitude = 0)
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        if (currentChart == null)
        {
            Log.Warning("SolarInputModel.CalculateSolarReturn: No current chart available");
            return false;
        }
        Log.Information("SolarInputModel.CalculateSolarReturn: Age={Age}, Sidereal={SiderealReturn}, Relocate={Relocate}", age, siderealReturn, relocate);
        var jd = currentChart.InputtedChartData.FullDateTime.JulianDayForEt;
        var radixLoc = currentChart.InputtedChartData.Location ?? new Location("Unknown", 0, 0);
        var relocationLoc = new Location("Relocated", longitude, latitude);
        var request = CreateSolarRequest(jd, age, siderealReturn, relocate, radixLoc, relocationLoc);
        // define jd for solar
        var jdSolar = solarService.CalculateJdForSolar(request);
        var solarObliquity = obliquityApi.GetObliquity(new ObliquityRequest(jdSolar, true));
        var config = CurrentConfig.Instance.GetConfig();
        var celPointsRequest = new CelPointsRequest(jdSolar, relocationLoc, prefsCreator.CreatePrefs(config, CoordinateSystems.Ecliptical));
        var positions = chartAllPositionsApi.GetChart(celPointsRequest);
        
        var fullDateTime = new FullDateTime("", "", jdSolar);
        var chartData = new ChartData(-1, null, relocationLoc, fullDateTime);        
        var calculatedSolar = new CalculatedChart(positions, chartData, solarObliquity); 
        DataVaultProg.Instance.SetCurrentSolar(calculatedSolar);
        return true;
    }

    private SolarRequest CreateSolarRequest(double jd, int age, bool siderealReturn, bool relocate, Location radixLoc, Location relocationLoc)
    {
        var calcPref = CreateCalculationPreferences(siderealReturn);
        return new SolarRequest(jd, age, siderealReturn, calcPref, radixLoc, relocationLoc);
    }

    private CalculationPreferences CreateCalculationPreferences(bool isSidereal)
    {
        var config = CurrentConfig.Instance.GetConfig();
     //   return prefsCreator.CreatePrefs(config, CoordinateSystems.Ecliptical);
        var zodiacType = isSidereal ? ZodiacTypes.Sidereal : config.ZodiacType;
        var ayanamsha = isSidereal ? Ayanamshas.Fagan : Ayanamshas.None;
        var coordSys = CoordinateSystems.Ecliptical;
        List<ChartPoints> actualChartPoints = new() { ChartPoints.Sun };
        return new CalculationPreferences(actualChartPoints, zodiacType, ayanamsha,
            coordSys, config.ObserverPosition, config.ProjectionType, config.HouseSystem, config.ApogeeType, config.OscillateNodes);
    }
} 