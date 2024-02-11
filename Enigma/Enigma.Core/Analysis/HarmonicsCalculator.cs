// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Analysis;

/// <summary>Calculator for harmonics.</summary>
public interface IHarmonicsCalculator
{
    /// <summary>Calculate harmonics for a list of positions using a specified harmonic number.</summary>
    /// <param name="originalPositions">List of original positions.</param>
    /// <param name="harmonicNumber">The multiplication factor for the harmonic to calculate.</param>
    /// <returns>List with harmonic positions in the same sequence as the original positions.</returns>
    public List<double> CalculateHarmonics(IEnumerable<double> originalPositions, double harmonicNumber);

    /// <summary>Calculate harmonics for a single position using a specified harmonic number.</summary>
    /// <param name="originalPosition">OriginalPosition.</param>
    /// <param name="harmonicNumber">The multiplication factor for the harmonic to calculate.</param>
    /// <returns>Value for the harmonic position in the range 0 lt;= value lt; 360.0.</returns>
    public double CalculateHarmonic(double originalPosition, double harmonicNumber);
}

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