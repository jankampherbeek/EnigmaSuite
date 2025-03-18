// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Calc;

/// <summary>Calculation of solar arcs for progressive techniques.</summary>
public interface ISolarArcCalculator
{
    /// <summary>Calculate the solar arc for a startmoment and a given timespan.</summary>
    /// <param name="jdRadix">Julian Day Number for the start moment (typically the radix).</param>
    /// <param name="timespan">The timespan in days.</param>
    /// <param name="location">The location is im,portant if the flags indicate the use of parallax, otherwise it is ignored.</param>
    /// <param name="flags">Combined value for the flags toa ccess the Swiss Ephemjeris.</param>
    /// <returns>The calculated solar arc.</returns>
    public double CalcSolarArcForTimespan(double jdRadix, double timespan, Location? location, int flags);
}

/// <inheritdoc/>
public sealed class SolarArcCalculator: ISolarArcCalculator
{
    private readonly ICelPointSeCalc _calculator;

    public  SolarArcCalculator(ICelPointSeCalc calc)
    {
        _calculator = calc;
    }

    /// <inheritdoc/>
    public double CalcSolarArcForTimespan(double jdRadix, double timespan, Location? location, int flags)
    {
        PosSpeed[] sunStart = _calculator.CalculateCelPoint((int)ChartPoints.Sun, jdRadix, location, flags);
        PosSpeed[] sunEnd = _calculator.CalculateCelPoint((int)ChartPoints.Sun, jdRadix + timespan, location, flags);
        return sunEnd[0].Position - sunStart[0].Position;
    }
}