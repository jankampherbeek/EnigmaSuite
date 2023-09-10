// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.RequestResponse;
using Serilog;

namespace Enigma.Api.Calc;


/// <inheritdoc/>
public sealed class HousesApi : IHousesApi
{
    private readonly IHousesHandler _housesHandler;

    /// <param name="housesHandler">Handler for the calculation of the houses.</param>
    public HousesApi(IHousesHandler housesHandler) => _housesHandler = housesHandler;

    /// <inheritdoc/>
    public Dictionary<ChartPoints, FullPointPos> GetHouses(FullHousesPosRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.ChartLocation);
        Log.Information("HousesApi GetHouses using house system {Hs}", request.CalcPrefs.ActualHouseSystem);
        return _housesHandler.CalcHouses(request);
    }

}