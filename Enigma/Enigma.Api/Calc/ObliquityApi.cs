﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.Specials;
using Serilog;

namespace Enigma.Api.Astron;

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
        Log.Information("ObliquityApi.GetObliquity() for julian day UT {jd}", request.JdUt);
        return _obliquityHandler.CalcObliquity(request);
    }

}