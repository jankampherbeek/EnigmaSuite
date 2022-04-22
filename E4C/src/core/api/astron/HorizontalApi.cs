// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using E4C.Core.Astron.Horizontal;
using E4C.Shared.ReqResp;

namespace E4C.Core.Api.Astron;

/// <summary>API for the calculation of horizontal coordinates.</summary>
public interface IHorizontalApi
{

    /// <summary>Api call to retrieve horizontal coordinates (azimuth and altitude).</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if either the request is null or the request contains a location that is null or eclipticalcoordinates that are null.</remarks>
    /// <returns>Values for azimuth and altitude.</returns>
    public HorizontalResponse getHorizontal(HorizontalRequest request);
}


/// <inheritdoc/>
public class HorizontalApi : IHorizontalApi
{
    private readonly IHorizontalHandler _horizontalHandler;

    /// <param name="horizontalHandler">Handler for the calculation of horizontal coordinates.</param>
    public HorizontalApi(IHorizontalHandler horizontalHandler) => _horizontalHandler = horizontalHandler;

    public HorizontalResponse getHorizontal(HorizontalRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.EclCoord);
        Guard.Against.Null(request.ChartLocation);
        return _horizontalHandler.CalcHorizontal(request);
    }

}
