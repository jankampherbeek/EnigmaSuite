// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Domain.RequestResponse;
using Serilog;

namespace Enigma.Api.Astron;


/// <inheritdoc/>
public class ObliqueLongitudeApi : IObliqueLongitudeApi
{
    private readonly IObliqueLongitudeHandler _handler;

    public ObliqueLongitudeApi(IObliqueLongitudeHandler handler) => _handler = handler;

    /// <inheritdoc/>
    public ObliqueLongitudeResponse GetObliqueLongitude(ObliqueLongitudeRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.NullOrEmpty(request.CelPointCoordinates);
        Log.Information("ObliqueLongitudeApi GetObliqueLongitude.");
        return _handler.CalcObliqueLongitude(request);
    }
}