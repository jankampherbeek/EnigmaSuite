﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Core.Research;


/// <summary>Calculates the positions for multiple charts to be used in research methods.</summary>
public interface ICalculatedResearchPositions
{
    /// <summary>Calculate the positions.</summary>
    /// <param name="standardInput">Contains the positions.</param>
    /// <returns>The calculated charts.</returns>
    public List<CalculatedResearchChart> CalculatePositions(StandardInput standardInput);
}


/// <inheritdoc/>
public sealed class CalculatedResearchPositions : ICalculatedResearchPositions
{
    private readonly IConfigurationHandler _configurationHandler;
    private readonly IChartAllPositionsHandler _chartAllPositionsHandler;
    private readonly IJulDayHandler _julDayHandler;
    private readonly IObliquityHandler _obliquityHandler;
    
    public CalculatedResearchPositions(IConfigurationHandler configurationHandler,
        IChartAllPositionsHandler chartAllPositionsHandler,
        IJulDayHandler julDayHandler,
        IObliquityHandler obliquityHandler)
    {
        _configurationHandler = configurationHandler;
        _chartAllPositionsHandler = chartAllPositionsHandler;
        _julDayHandler = julDayHandler;
        _obliquityHandler = obliquityHandler;
    }

    public List<CalculatedResearchChart> CalculatePositions(StandardInput standardInput)
    {
        return Calculate(standardInput);
    }


    private List<CalculatedResearchChart> Calculate(StandardInput standardInput)
    {
        Log.Information("CalculatedResearchPositions: Start of calculation");
        CalculationPreferences calcPref = DefinePreferences();
        List<CalculatedResearchChart> calculatedCharts = (from inputItem in standardInput.ChartData 
            let location = new Location("", inputItem.GeoLongitude, inputItem.GeoLatitude) 
            let jdUt = CalcJdUt(inputItem) 
            let obliquity = CalcObliquity(jdUt)
            let cpRequest = new CelPointsRequest(jdUt, location, calcPref) 
            let chartPositions = _chartAllPositionsHandler.CalcFullChart(cpRequest) 
            select new CalculatedResearchChart(chartPositions, obliquity, inputItem)).ToList();
        Log.Information("CalculatedResearchPositions: Calculation completed");
        return calculatedCharts;
    }

    private double CalcJdUt(StandardInputItem inputItem)
    {
        PersistableTime time = inputItem.Time!;
        PersistableDate date = inputItem.Date!;

        double ut = time.Hour + time.Minute / 60.0 + time.Second / 3600.0 - time.Dst - time.ZoneOffset;
        Calendars cal = date.Calendar == "G" ? Calendars.Gregorian : Calendars.Julian;
        SimpleDateTime simpleDateTime = new(date.Year, date.Month, date.Day, ut, cal);
        return _julDayHandler.CalcJulDay(simpleDateTime).JulDayUt;
    }

    private double CalcObliquity(double jdNr)
    {
        ObliquityRequest request = new(jdNr, true);
        return _obliquityHandler.CalcObliquity(request);
    }

    private CalculationPreferences DefinePreferences()
    {
        AstroConfig config = _configurationHandler.ReadCurrentConfig();
        Dictionary<ChartPoints, ChartPointConfigSpecs> cpSpecs = config.ChartPoints;
        List<ChartPoints> celPoints = (from cpSpec in cpSpecs 
            where cpSpec.Value.IsUsed 
            let pointCat = cpSpec.Key.GetDetails().PointCat 
            where pointCat == PointCats.Common 
            select cpSpec.Key).ToList();
        return new CalculationPreferences(celPoints, config.ZodiacType, config.Ayanamsha, CoordinateSystems.Ecliptical, 
            config.ObserverPosition, config.ProjectionType, config.HouseSystem, ApogeeTypes.Corrected,false);  // TODO define correct value for Oscillate and apogee type
    }

}