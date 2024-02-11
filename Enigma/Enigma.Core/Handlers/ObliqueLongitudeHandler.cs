// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Core.Handlers;

/// <summary>Start calculations for oblique longitude.</summary>
/// <remarks>Oblique longitude is the 'True place' according to the School of Ram.</remarks>
public interface IObliqueLongitudeHandler
{
    /// <summary>Calculate oblique longitude for one point.</summary>
    /// <param name="request"></param>
    /// <returns>Calculated positions in oblique longitude.</returns>
    public List<NamedEclipticLongitude> CalcObliqueLongitude(ObliqueLongitudeRequest request);
}

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
            Log.Error("ObliqueLongitudeHandler.CalcObliqueLOngitude(): {Msg}", ex.Message);
        }
        return longitudes;
    }
}