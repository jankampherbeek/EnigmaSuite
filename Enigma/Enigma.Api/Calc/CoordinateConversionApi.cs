// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.RequestResponse;
using Serilog;

namespace Enigma.Api.Astron;


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
        Log.Information("CoordinateConversionApi: GetEquatorialFromEcliptic() using longitude {lon}, latitude {lat} and obliquity {obl}.", request.EclCoord.Longitude, request.EclCoord.Latitude, request.Obliquity);
        return _coordConvHandler.HandleConversion(request);
    }

}