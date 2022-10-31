// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Calc.Api.DateTime;


/// <inheritdoc/>
public class CheckDateTimeApi : ICheckDateTimeApi
{
    private readonly ICheckDateTimeHandler _checkDateTimeHandler;

    public CheckDateTimeApi(ICheckDateTimeHandler checkDateTimeHandler) => _checkDateTimeHandler = checkDateTimeHandler;

    public CheckDateTimeResponse CheckDateTime(CheckDateTimeRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.DateTime);
        return _checkDateTimeHandler.CheckDateTime(request);
    }

}