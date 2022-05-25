// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Calc.Obliquity;
using Enigma.Core.Calc.ReqResp;

namespace Enigma.Core.Calc.Api.Astron;

/// <summary>API for calculation of the obliquity of the earth's axis.</summary>
public interface IObliquityApi
{
    /// <summary>Api call to retrieve obliquity.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Value for the obliquity of the earth's axis.</returns>
    public ObliquityResponse getObliquity(ObliquityRequest request);
}


/// <inheritdoc/>
public class ObliquityApi : IObliquityApi
{
    private readonly IObliquityHandler _obliquityHandler;

    /// <param name="obliquityHandler">Handler for the calculation of the obliquity of the earth's axis.</param>
    public ObliquityApi(IObliquityHandler obliquityHandler) => _obliquityHandler = obliquityHandler;

    public ObliquityResponse getObliquity(ObliquityRequest request)
    {
        Guard.Against.Null(request);
        return _obliquityHandler.CalcObliquity(request);
    }

}