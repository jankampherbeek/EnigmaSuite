// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Api;

/// <summary>API for the calculation of horizontal coordinates.</summary>
public interface IHorizontalApi
{
    /// <summary>Api call to retrieve horizontal coordinates (azimuth and altitude).</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if either the request is null or the request contains a location that is null or eclipticalcoordinates that are null.</remarks>
    /// <returns>Values for azimuth and altitude.</returns>
    public HorizontalCoordinates GetHorizontal(HorizontalRequest request);
}

/// <inheritdoc/>
public sealed class HorizontalApi : IHorizontalApi
{
    private readonly IHorizontalHandler _horizontalHandler;

    /// <param name="horizontalHandler">Handler for the calculation of horizontal coordinates.</param>
    public HorizontalApi(IHorizontalHandler horizontalHandler) => _horizontalHandler = horizontalHandler;

    /// <inheritdoc/>
    public HorizontalCoordinates GetHorizontal(HorizontalRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.EquCoordinates);
        Guard.Against.Null(request.Location);
        Log.Information("HorizontalApi GetHorizontal");
        return _horizontalHandler.CalcHorizontal(request);
    }

}
