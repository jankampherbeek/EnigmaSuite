// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Calc;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Api;


/// <summary>API for conversion between coordinates.</summary>
public interface ICoordinateConversionApi
{
    /// <summary>Api call to convert ecliptical coordinates into equatorial coordinates.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Equatorial coordinates that correspond to the ecliptic coordinates from the request, using the obliquity from the request.</returns>
    public EquatorialCoordinates GetEquatorialFromEcliptic(CoordinateConversionRequest request);
}

/// <inheritdoc/>
public sealed class CoordinateConversionApi : ICoordinateConversionApi
{
    private readonly ICoordinateConversionHandler _coordConvHandler;


    /// <param name="coordConvHandler">Handler for the conversion of coordinates.</param>
    public CoordinateConversionApi(ICoordinateConversionHandler coordConvHandler)
    {
        _coordConvHandler = coordConvHandler;
    }

    /// <inheritdoc/>
    public EquatorialCoordinates GetEquatorialFromEcliptic(CoordinateConversionRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.EclCoord);
        Log.Information("CoordinateConversionApi: GetEquatorialFromEcliptic() using longitude {Lon}, latitude {Lat} and obliquity {Obl}", 
            request.EclCoord.Longitude, request.EclCoord.Latitude, request.Obliquity);
        return _coordConvHandler.HandleConversion(request);
    }

}