// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Calc.Celestialpoints;

public sealed class ChartAllPositionsHandler : IChartAllPositionsHandler
{

    private readonly ICelPointsHandler _celPointsHandler;
    private readonly IHousesHandler _housesHandler;
    private readonly IZodiacPointsCalc _zodiacPointCalc;


    public ChartAllPositionsHandler(ICelPointsHandler celPointsHandler, IHousesHandler housesHandler, IZodiacPointsCalc zodiacPointsCalc)
    {
        _celPointsHandler = celPointsHandler;
        _housesHandler = housesHandler;
        _zodiacPointCalc = zodiacPointsCalc;
    }

    public CalculatedChartPositions CalcFullChart(CelPointsRequest request)
    {
        Dictionary<ChartPoints, FullPointPos> commonPositions = _celPointsHandler.CalcCommonPoints(request);
        FullHousesPosRequest housesRequest = new(request.JulianDayUt, request.Location, request.CalculationPreferences.ActualHouseSystem);
        FullHousesPositions mundane = _housesHandler.CalcHouses(housesRequest);
        Dictionary<ChartPoints, FullPointPos> zodiacPoints = new();         // TODO 0.1 Add zodiacal points to ChartAllPositionsHandler.
        Dictionary<ChartPoints, FullPointPos> arabicPoints = new();         // TODO 0.1 Add pars fortunae to ChartAllPositionsHandler.
        Dictionary<ChartPoints, FullPointPos> fixStars = new();             // TODO backlog Add FixStars for ChartAllPositionsHandler.
        return new CalculatedChartPositions(commonPositions, mundane.Angles, mundane.Cusps, zodiacPoints, arabicPoints, fixStars);
    }

    private Dictionary<ChartPoints, FullPointPos> CalcZodiacPoints(CelPointsRequest request)
    {
        Dictionary<ChartPoints, FullPointPos> zodiacPoints = new();
        if (request.CalculationPreferences.ActualZodiacType == ZodiacTypes.Tropical && request.CalculationPreferences.CoordinateSystem == CoordinateSystems.Ecliptical)
        {
            if (request.CalculationPreferences.ActualChartPoints.Contains(ChartPoints.ZeroAries))
            {
                zodiacPoints.Add(ChartPoints.ZeroAries, _zodiacPointCalc.DefineZeroAries(request));
            }
            if (request.CalculationPreferences.ActualChartPoints.Contains(ChartPoints.ZeroCancer))
            {
                zodiacPoints.Add(ChartPoints.ZeroAries, _zodiacPointCalc.DefineZeroCancer(request));
            }
        }
        return zodiacPoints;
    }
}

