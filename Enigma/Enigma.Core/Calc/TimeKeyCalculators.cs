// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Calc;

/// <inheritdoc/>
public sealed class SolarArcCalculator: ISolarArcCalculator
{
    private readonly ICelPointSeCalc _calculator;

    public  SolarArcCalculator(ICelPointSeCalc calc)
    {
        _calculator = calc;
    }

    /// <inheritdoc/>
    public double CalcSolarArcForTimespan(double jdRadix, double timespan, Location location, int flags)
    {
        PosSpeed[] sunStart = _calculator.CalculateCelPoint(ChartPoints.Sun, jdRadix, location, flags);
        PosSpeed[] sunEnd = _calculator.CalculateCelPoint(ChartPoints.Sun, jdRadix + timespan, location, flags);
        return sunEnd[0].Position - sunStart[0].Position;
    }
}