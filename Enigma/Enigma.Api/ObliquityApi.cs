// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Api;

/// <summary>API for calculation of the obliquity of the earth's axis.</summary>
public interface IObliquityApi
{
    /// <summary>Api call to retrieve obliquity.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Value for the obliquity of the earth's axis.</returns>
    public double GetObliquity(ObliquityRequest request);
}

/// <inheritdoc/>
public sealed class ObliquityApi : IObliquityApi
{
    private readonly IObliquityHandler _obliquityHandler;

    /// <param name="obliquityHandler">Handler for the calculation of the obliquity of the earth's axis.</param>
    public ObliquityApi(IObliquityHandler obliquityHandler) => _obliquityHandler = obliquityHandler;

    /// <inheritdoc/>
    public double GetObliquity(ObliquityRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("ObliquityApi.GetObliquity() for julian day UT {Jd}", request.JdUt);
        return _obliquityHandler.CalcObliquity(request);
    }

}