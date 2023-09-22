// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Api;


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
