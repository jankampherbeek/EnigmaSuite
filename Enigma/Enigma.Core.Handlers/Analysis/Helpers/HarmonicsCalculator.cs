// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;

namespace Enigma.Core.Handlers.Analysis.Helpers;

/// <inheritdoc/>
public sealed class HarmonicsCalculator : IHarmonicsCalculator
{
    /// <inheritdoc/>
    public List<double> CalculateHarmonics(List<double> originalPositions, double harmonicNumber)
    {
        List<double> harmonicPositions = new();
        foreach (var originalPosition in originalPositions)
        {
            harmonicPositions.Add(InRange360(originalPosition * harmonicNumber));
        }
        return harmonicPositions;
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