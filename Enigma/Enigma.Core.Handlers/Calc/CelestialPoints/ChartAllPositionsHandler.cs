// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.Specials;
using Enigma.Domain.Points;
using Enigma.Domain.RequestResponse;
using Enigma.Facades.Interfaces;
using Enigma.Facades.Se;

namespace Enigma.Core.Handlers.Calc.Celestialpoints;

public sealed class ChartAllPositionsHandler : IChartAllPositionsHandler
{

    private readonly ICelPointsHandler _celPointsHandler;
    private readonly IHousesHandler _housesHandler;
    private readonly IZodiacPointsCalc _zodiacPointCalc;
    private readonly IObliquityHandler _obliquityHandler;
    private readonly IAyanamshaFacade _ayanamshaFacade;
    private readonly ILotsCalculator _lotsCalculator;


    public ChartAllPositionsHandler(ICelPointsHandler celPointsHandler, IHousesHandler housesHandler, IZodiacPointsCalc zodiacPointsCalc, IObliquityHandler obliquityHandler, IAyanamshaFacade ayanamshaFacade, ILotsCalculator lotsCalculator)
    {
        _celPointsHandler = celPointsHandler;
        _housesHandler = housesHandler;
        _zodiacPointCalc = zodiacPointsCalc;
        _obliquityHandler = obliquityHandler;
        _ayanamshaFacade = ayanamshaFacade;
        _lotsCalculator = lotsCalculator;
    }

    public Dictionary<ChartPoints, FullPointPos> CalcFullChart(CelPointsRequest celPointsRequest)
    {
        double jdUt = celPointsRequest.JulianDayUt;
        CalculationPreferences prefs = celPointsRequest.CalculationPreferences;
        Location location = celPointsRequest.Location;
        double obliquity = CalcObliquity(jdUt);
        double ayanamshaOffset = PrepareAyanamsha(celPointsRequest);

        FullHousesPosRequest housesRequest = new(jdUt, location, prefs);
        Dictionary<ChartPoints, FullPointPos> mundanePositions = _housesHandler.CalcHouses(housesRequest);
        double armc = mundanePositions[ChartPoints.Mc].Equatorial.MainPosSpeed.Position;

        Dictionary<ChartPoints, FullPointPos> commonPositions = _celPointsHandler.CalcCommonPoints(jdUt, obliquity, ayanamshaOffset, armc, location, prefs);
        Dictionary<ChartPoints, FullPointPos> lots = _lotsCalculator.CalculateAllLots(commonPositions, mundanePositions, prefs, jdUt, obliquity, location);
        Dictionary<ChartPoints, FullPointPos> zodiacPoints = _zodiacPointCalc.CalculateAllZodiacalPoints(prefs, jdUt, obliquity, location);

        // TODO backlog Add calculation of fixstars.

        List<Dictionary<ChartPoints, FullPointPos>> dictionaries = new()
        {
            commonPositions,
            mundanePositions,
            lots,
            zodiacPoints
        };
        return MergeDirectories(dictionaries);
    }

    private static Dictionary<ChartPoints, FullPointPos> MergeDirectories(IEnumerable<Dictionary<ChartPoints, FullPointPos>> dictionaries)
    {
        return dictionaries.SelectMany(x => x).ToDictionary(x => x.Key, y => y.Value);
    }


    private double CalcObliquity(double jdUt)
    {
        ObliquityRequest obliquityRequest = new(jdUt, true);
        return _obliquityHandler.CalcObliquity(obliquityRequest);
    }

    private double PrepareAyanamsha(CelPointsRequest request)
    {
        double ayanamshaOffset = 0.0;
        if (request.CalculationPreferences.ActualZodiacType != ZodiacTypes.Sidereal) return ayanamshaOffset;
        SeInitializer.SetAyanamsha(request.CalculationPreferences.ActualAyanamsha.GetDetails().SeId);
        ayanamshaOffset = _ayanamshaFacade.GetAyanamshaOffset(request.JulianDayUt);
        return ayanamshaOffset;
    }
}

