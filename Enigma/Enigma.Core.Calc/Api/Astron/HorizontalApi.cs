// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Calc.Api.Astron;


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
