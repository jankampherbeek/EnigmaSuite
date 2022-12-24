// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
using Enigma.Domain.Persistency;
using Enigma.Domain.RequestResponse;
using Enigma.Research.Domain;
using Serilog;

namespace Enigma.Core.Handlers.Research.Helpers;

/// <inheritdoc/>
public class CalculatedResearchPositions : ICalculatedResearchPositions
{

    private readonly IConfigurationHandler _configurationHandler;
    private readonly IChartAllPositionsHandler _chartAllPositionsHandler;
    private readonly IJulDayHandler _julDayHandler;

    /// <inheritdoc/>
    public CalculatedResearchPositions(IConfigurationHandler configurationHandler,
        IChartAllPositionsHandler chartAllPositionsHandler,
        IJulDayHandler julDayHandler)
    {
        _configurationHandler = configurationHandler;
        _chartAllPositionsHandler = chartAllPositionsHandler;
        _julDayHandler = julDayHandler;
    }

    public List<CalculatedResearchChart> CalculatePositions(StandardInput standardInput)
    {
        return Calculate(standardInput);
    }


    private List<CalculatedResearchChart> Calculate(StandardInput standardInput)
    {
        Log.Information("CalculatedResearchPositions: Start of calculation.");
        List<CalculatedResearchChart> calculatedCharts = new();
        CalculationPreferences calcPref = DefinePreferences();
        foreach (StandardInputItem inputItem in standardInput.ChartData)
        {
            Location location = new("", inputItem.GeoLongitude, inputItem.GeoLatitude);
            double jdUt = CalcJdUt(inputItem);
            CelPointsRequest cpRequest = new(jdUt, location, calcPref);
            ChartAllPositionsRequest chartAllPosRequest = new(cpRequest, calcPref.ActualHouseSystem);
            ChartAllPositionsResponse response = _chartAllPositionsHandler.CalcFullChart(chartAllPosRequest);
            // TODO check for null for MundanePositions.

            calculatedCharts.Add(new CalculatedResearchChart(response.CelPointPositions, response.MundanePositions, inputItem));
        }
        Log.Information("CalculatedResearchPositions: Calculation completed.");
        return calculatedCharts;
    }

    private double CalcJdUt(StandardInputItem inputItem)
    {
        double ut = inputItem.Time.Hour + inputItem.Time.Minute * 60.0 + inputItem.Time.Second * 3600.0 + inputItem.Time.Dst + inputItem.Time.ZoneOffset;
        // TODO check for overflow
        Calendars cal = inputItem.Date.Calendar == "G" ? Calendars.Gregorian : Calendars.Julian;
        SimpleDateTime simpleDateTime = new(inputItem.Date.Year, inputItem.Date.Month, inputItem.Date.Day, ut, cal);
        JulianDayRequest jdRequest = new(simpleDateTime);
        return _julDayHandler.CalcJulDay(jdRequest).JulDayUt;
    }


    private CalculationPreferences DefinePreferences()
    {
        AstroConfig config = _configurationHandler.ReadConfig();
        List<CelPointSpecs> cpSpecs = config.CelPoints;
        List<CelPoints> celPoints = new();
        foreach (CelPointSpecs cpSpec in cpSpecs)
        {
            if (cpSpec.IsUsed)
            {
                celPoints.Add(cpSpec.CelPoint);
            }

        }
        return new CalculationPreferences(celPoints, config.ZodiacType, config.Ayanamsha, CoordinateSystems.Ecliptical, config.ObserverPosition, config.ProjectionType, config.HouseSystem);
    }

}