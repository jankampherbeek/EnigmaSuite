// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

/// <summary>Handle calculation for a full chart with all positions.</summary>
public interface IChartAllPositionsHandler
{
    public Dictionary<ChartPoints, FullPointPos> CalcFullChart(CelPointsRequest request);
}

public sealed class ChartAllPositionsHandler(
    ICelPointsHandler celPointsHandler,
    IHousesHandler housesHandler,
    IZodiacPointsCalc zodiacPointsCalc,
    IObliquityHandler obliquityHandler,
    IAyanamshaFacade ayanamshaFacade,
    ILotsCalculator lotsCalculator)
    : IChartAllPositionsHandler
{
    public Dictionary<ChartPoints, FullPointPos> CalcFullChart(CelPointsRequest celPointsRequest)
    {
        var jdUt = celPointsRequest.JulianDayUt;
        var prefs = celPointsRequest.CalculationPreferences;
        var location = celPointsRequest.Location;
        var obliquity = CalcObliquity(jdUt);
        var ayanamshaOffset = PrepareAyanamsha(celPointsRequest);

        FullHousesPosRequest housesRequest = new(jdUt, location, prefs);
        var mundanePositions = housesHandler.CalcHouses(housesRequest);
        var armc = mundanePositions[ChartPoints.Mc].Equatorial.MainPosSpeed.Position;

        var commonPositions = celPointsHandler.CalcCommonPoints(jdUt, obliquity, ayanamshaOffset, armc, location, prefs);
        var lots = lotsCalculator.CalculateAllLots(commonPositions, mundanePositions, prefs, jdUt, obliquity, location);
        var zodiacPoints = zodiacPointsCalc.CalculateAllZodiacalPoints(prefs, jdUt, obliquity, location);

        List<Dictionary<ChartPoints, FullPointPos>> dictionaries =
        [
            commonPositions,
            mundanePositions,
            lots,
            zodiacPoints
        ];
        return MergeDirectories(dictionaries);
    }

    private static Dictionary<ChartPoints, FullPointPos> MergeDirectories(IEnumerable<Dictionary<ChartPoints, FullPointPos>> dictionaries)
    {
        return dictionaries.SelectMany(x => x).ToDictionary(x => x.Key, y => y.Value);
    }


    private double CalcObliquity(double jdUt)
    {
        ObliquityRequest obliquityRequest = new(jdUt, true);
        return obliquityHandler.CalcObliquity(obliquityRequest);
    }

    private double PrepareAyanamsha(CelPointsRequest request)
    {
        var ayanamshaOffset = 0.0;
        if (request.CalculationPreferences.ActualZodiacType != ZodiacTypes.Sidereal) return ayanamshaOffset;
        SeInitializer.SetAyanamsha(request.CalculationPreferences.ActualAyanamsha.GetDetails().SeId);
        ayanamshaOffset = ayanamshaFacade.GetAyanamshaOffset(request.JulianDayUt);
        return ayanamshaOffset;
    }
}

