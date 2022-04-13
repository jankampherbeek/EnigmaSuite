// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using E4C.api.handlers;
using E4C.core.astron.coordinateconversion;
using E4C.core.astron.obliquity;
using E4C.domain.shared.reqresp;
using E4C.shared.reqresp;

namespace E4C.core.api;

/// <summary>API for astronomical calculations.</summary>
public interface IAstronApi
{
    /// <summary>Api call to convert ecliptical coordinates into equatorial coordinates.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>/// 
    /// <returns>Equaatorial coordinates that correspond to the ecliptic coordinates from the request, using the obliquity from the request.</returns>
    public CoordinateConversionResponse getEquatorialFromEcliptic(CoordinateConversionRequest request);

    /// <summary>Api call to retrieve obliquity.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Value for the obliquity of the earth's axis.</returns>
    public ObliquityResponse getObliquity(ObliquityRequest request);

}


/// <inheritdoc/>
public class AstronApi : IAstronApi
{
    private readonly ICoordinateConversionHandler _coordConversionHandler;
    private readonly IObliquityHandler _obliquityHandler;


    /// <param name="obliquityHandler">Handler for the calculation of the obliquity of the earth's axis.</param>
    public AstronApi(ICoordinateConversionHandler coordConvHandler, IObliquityHandler obliquityHandler)
    {
        _coordConversionHandler = coordConvHandler;
        _obliquityHandler = obliquityHandler;
    }

    public CoordinateConversionResponse getEquatorialFromEcliptic(CoordinateConversionRequest request)
    {
        Guard.Against.Null(request);
        return _coordConversionHandler.HandleConversion(request);
    }

    public ObliquityResponse getObliquity(ObliquityRequest request)
    {
        Guard.Against.Null(request);
        return _obliquityHandler.CalcObliquity(request);
    }

}