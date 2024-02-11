// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Api;

/// <summary>API for calculation of house cusps and other mundane points.</summary>
public interface IHousesApi
{
    /// <summary>Api call to calculate house cusps and other mundane points.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Instance of FullHousesPosResponse with all coordinates for cusps, MC, Ascendant, EastPoint and Vertex.</returns>
    public Dictionary<ChartPoints, FullPointPos> GetHouses(FullHousesPosRequest request);
}

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