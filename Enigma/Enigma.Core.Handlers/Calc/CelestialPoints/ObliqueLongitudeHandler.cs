﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.RequestResponse;
using Serilog;

namespace Enigma.Core.Handlers.Calc.CelestialPoints;

/// <inheritdoc/>
public sealed class ObliqueLongitudeHandler : IObliqueLongitudeHandler
{
    private readonly IObliqueLongitudeCalculator _calculator;


    public ObliqueLongitudeHandler(IObliqueLongitudeCalculator calculator)
    {
        _calculator = calculator;
    }

    /// <inheritdoc/>
    List<NamedEclipticLongitude> IObliqueLongitudeHandler.CalcObliqueLongitude(ObliqueLongitudeRequest request)
    {
        List<NamedEclipticLongitude> longitudes = new();
        try
        {
            longitudes = _calculator.CalcObliqueLongitudes(request);
        }
        catch (Exception ex)
        {
            Log.Error("ObliqueLongitudeHandler.CalcObliqueLOngitude(): " + ex.Message);
        }
        return longitudes;
    }
}