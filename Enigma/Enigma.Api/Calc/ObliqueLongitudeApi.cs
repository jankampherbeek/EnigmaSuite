// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Api.Calc;

/// <summary>API for calculation of oblique longitude (True place for the WvA)</summary>
public interface IObliqueLongitudeApi
{
    /// <summary>Calculate oblique longitude.</summary>
    public List<NamedEclipticLongitude> GetObliqueLongitude(ObliqueLongitudeRequest request);

}

/// <inheritdoc/>
public sealed class ObliqueLongitudeApi(IObliqueLongitudeHandler handler) : IObliqueLongitudeApi
{
    /// <inheritdoc/>
    public List<NamedEclipticLongitude> GetObliqueLongitude(ObliqueLongitudeRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.NullOrEmpty(request.CelPointCoordinates);
        Log.Information("ObliqueLongitudeApi GetObliqueLongitude");
        return handler.CalcObliqueLongitude(request);
    }
}