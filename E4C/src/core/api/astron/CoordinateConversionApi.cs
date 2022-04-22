// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using E4C.Core.Astron.CoordinateConversion;
using E4C.Shared.ReqResp;

namespace E4C.Core.Api.Astron;

/// <summary>API for conversion between coordinates.</summary>
public interface ICoordinateConversionApi
{
    /// <summary>Api call to convert ecliptical coordinates into equatorial coordinates.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Equatorial coordinates that correspond to the ecliptic coordinates from the request, using the obliquity from the request.</returns>
    public CoordinateConversionResponse getEquatorialFromEcliptic(CoordinateConversionRequest request);

}


/// <inheritdoc/>
public class CoordinateConversionApi : ICoordinateConversionApi
{
    private readonly ICoordinateConversionHandler _coordConvHandler;


    /// <param name="coordConvHandler">Handler for the conversion of coordinates.</param>
    public CoordinateConversionApi(ICoordinateConversionHandler coordConvHandler)
    {
        _coordConvHandler = coordConvHandler;
    }

    public CoordinateConversionResponse getEquatorialFromEcliptic(CoordinateConversionRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.EclCoord);
        return _coordConvHandler.HandleConversion(request);
    }

}