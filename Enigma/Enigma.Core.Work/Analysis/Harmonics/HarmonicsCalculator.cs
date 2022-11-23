// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Work.Analysis.Harmonics;

/// <inheritdoc/>
public class HarmonicsCalculator : IHarmonicsCalculator
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

    private static double InRange360(double originalValue)
    {
        double inRangeValue = originalValue;
        while (inRangeValue >= 360.0) inRangeValue -= 360.0;
        return inRangeValue;
    }


}