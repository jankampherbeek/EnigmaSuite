// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Domain.RequestResponse;
using Serilog;

namespace Enigma.Api.Astron;

/// <inheritdoc/>
public class ObliquityApi : IObliquityApi
{
    private readonly IObliquityHandler _obliquityHandler;

    /// <param name="obliquityHandler">Handler for the calculation of the obliquity of the earth's axis.</param>
    public ObliquityApi(IObliquityHandler obliquityHandler) => _obliquityHandler = obliquityHandler;

    /// <inheritdoc/>
    public ObliquityResponse GetObliquity(ObliquityRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("ObliquityApi GetObliquity fpr julian day UT {jd}", request.JdUt);
        return _obliquityHandler.CalcObliquity(request);
    }

}