// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Domain.RequestResponse;
using Serilog;

namespace Enigma.Api.Astron;


/// <inheritdoc/>
public class HorizontalApi : IHorizontalApi
{
    private readonly IHorizontalHandler _horizontalHandler;

    /// <param name="horizontalHandler">Handler for the calculation of horizontal coordinates.</param>
    public HorizontalApi(IHorizontalHandler horizontalHandler) => _horizontalHandler = horizontalHandler;

    /// <inheritdoc/>
    public HorizontalResponse GetHorizontal(HorizontalRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.EclCoord);
        Guard.Against.Null(request.ChartLocation);
        Log.Information("HorizontalApi GetHorizontal");
        return _horizontalHandler.CalcHorizontal(request);
    }

}
