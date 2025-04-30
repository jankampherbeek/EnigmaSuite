// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
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
    /// <param name="standardInputItems">Contains the positions.</param>
    /// <returns>The calculated charts.</returns>
    public List<CalculatedResearchChart> CalculatePositions(List<StandardInputItem> standardInputItems);
}


/// <inheritdoc/>
public sealed class CalculatedResearchPositions(
    IConfigurationHandler configurationHandler,
    IChartAllPositionsHandler chartAllPositionsHandler,
    IJulDayHandler julDayHandler,
    IObliquityHandler obliquityHandler)
    : ICalculatedResearchPositions
{
    public List<CalculatedResearchChart> CalculatePositions(List<StandardInputItem> standardInputItems)
    {
        return Calculate(standardInputItems);
    }


    private List<CalculatedResearchChart> Calculate(List<StandardInputItem> standardInputItems)
    {
   //     Log.Information("CalculatedResearchPositions: Start of calculation");
        var calcPref = DefinePreferences();
        List<CalculatedResearchChart> calculatedCharts = (from inputItem in standardInputItems 
            let location = new Location("", inputItem.GeoLongitude, inputItem.GeoLatitude) 
            let jdUt = CalcJdUt(inputItem) 
            let obliquity = CalcObliquity(jdUt)
            let cpRequest = new CelPointsRequest(jdUt, location, calcPref) 
            let chartPositions = chartAllPositionsHandler.CalcFullChart(cpRequest) 
            select new CalculatedResearchChart(chartPositions, obliquity, inputItem)).ToList();
    //    Log.Information("CalculatedResearchPositions: Calculation completed");
        return calculatedCharts;
    }

    private double CalcJdUt(StandardInputItem inputItem)
    {
        var ut = inputItem.Hour + inputItem.Minute / 60.0 + inputItem.Second / 3600.0 - inputItem.Dst - inputItem.ZoneOffset;
        var cal = inputItem.Calendar == "G" ? Calendars.Gregorian : Calendars.Julian;
        SimpleDateTime simpleDateTime = new(inputItem.Year, inputItem.Month, inputItem.Day, ut, cal);
        return julDayHandler.CalcJulDay(simpleDateTime).JulDayUt;
    }

    private double CalcObliquity(double jdNr)
    {
        ObliquityRequest request = new(jdNr, true);
        return obliquityHandler.CalcObliquity(request);
    }

    private CalculationPreferences DefinePreferences()
    {
        var config = configurationHandler.ReadCurrentConfig();
        var cpSpecs = config.ChartPoints;
        var celPoints = new List<ChartPoints>();
        
        foreach (var cpSpec in cpSpecs)
        {
            if (cpSpec.Value.IsUsed)
            {
                var pointCat = cpSpec.Key.GetDetails().PointCat;
                if (pointCat != PointCats.Cusp)
                {
                    celPoints.Add(cpSpec.Key);
                }
            }
        }
        
        return new CalculationPreferences(celPoints, config.ZodiacType, config.Ayanamsha, CoordinateSystems.Ecliptical, 
            config.ObserverPosition, config.ProjectionType, config.HouseSystem, ApogeeTypes.Corrected, false);
    }

}