﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Calc.Celestialpoints;

public sealed class ChartAllPositionsHandler : IChartAllPositionsHandler
{

    private readonly ICelPointsHandler _celPointsHandler;
    private readonly IHousesHandler _housesHandler;


    public ChartAllPositionsHandler(ICelPointsHandler celPointsHandler, IHousesHandler housesHandler)
    {
        _celPointsHandler = celPointsHandler;
        _housesHandler = housesHandler;
    }

    public ChartAllPositionsResponse CalcFullChart(CelPointsRequest request)
    {
        CelPointsResponse celPointsResponse = _celPointsHandler.CalcCelPoints(request);
        FullHousesPosRequest housesRequest = new(request.JulianDayUt, request.Location, request.CalculationPreferences.ActualHouseSystem);
        FullHousesPositions housesResponse = _housesHandler.CalcHouses(housesRequest);
        return new ChartAllPositionsResponse(celPointsResponse.CelPointPositions, housesResponse);
    }

}
