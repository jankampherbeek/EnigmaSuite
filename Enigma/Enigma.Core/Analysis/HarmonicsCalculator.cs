// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Interfaces;

namespace Enigma.Core.Analysis.Helpers;

/// <inheritdoc/>
public sealed class HarmonicsCalculator : IHarmonicsCalculator
{
    /// <inheritdoc/>
    public List<double> CalculateHarmonics(IEnumerable<double> originalPositions, double harmonicNumber)
    {
        return originalPositions.Select(originalPosition 
            => InRange360(originalPosition * harmonicNumber)).ToList();
    }

    /// <inheritdoc/>
    public double CalculateHarmonic(double originalPosition, double harmonicNumber)
    {
        return InRange360(originalPosition * harmonicNumber);
    }

    private static double InRange360(double originalValue)
    {
        double inRangeValue = originalValue;
        while (inRangeValue >= 360.0) inRangeValue -= 360.0;
        return inRangeValue;
    }


}