// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Configuration.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Configuration;
using Enigma.Domain.Persistency;
using Enigma.Domain.Points;
using Enigma.Research.Domain;
using Serilog;

namespace Enigma.Core.Handlers.Research.Helpers;

/// <inheritdoc/>
public sealed class CalculatedResearchPositions : ICalculatedResearchPositions
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
            Dictionary<ChartPoints, FullPointPos> chartPositions = _chartAllPositionsHandler.CalcFullChart(cpRequest);
            calculatedCharts.Add(new CalculatedResearchChart(chartPositions, inputItem));
        }
        Log.Information("CalculatedResearchPositions: Calculation completed.");
        return calculatedCharts;
    }

    private double CalcJdUt(StandardInputItem inputItem)
    {
        PersistableTime time = inputItem.Time!;
        PersistableDate date = inputItem.Date!;

        double ut = time.Hour + time.Minute / 60.0 + time.Second / 3600.0 - time.Dst - time.ZoneOffset;
        // TODO 0.2 check for overflow
        Calendars cal = date.Calendar == "G" ? Calendars.Gregorian : Calendars.Julian;
        SimpleDateTime simpleDateTime = new(date.Year, date.Month, date.Day, ut, cal);
        return _julDayHandler.CalcJulDay(simpleDateTime).JulDayUt;
    }


    private CalculationPreferences DefinePreferences()
    {
        AstroConfig config = _configurationHandler.ReadConfig();
        Dictionary<ChartPoints, ChartPointConfigSpecs> cpSpecs = config.ChartPoints;
        List<ChartPoints> celPoints = new();
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> cpSpec in cpSpecs)
        {
            if (cpSpec.Value.IsUsed)
            {
                PointCats pointCat = cpSpec.Key.GetDetails().PointCat;
                if (pointCat == PointCats.Common)
                {
                    celPoints.Add(cpSpec.Key);
                }
            }
        }
        return new CalculationPreferences(celPoints, config.ZodiacType, config.Ayanamsha, CoordinateSystems.Ecliptical, config.ObserverPosition, config.ProjectionType, config.HouseSystem);
    }

}