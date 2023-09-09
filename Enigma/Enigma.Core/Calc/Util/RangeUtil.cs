// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Calc.Util;

/// <summary>Utility that handles coformance of a value to a given range.</summary>
public static class RangeUtil
{
    /// <summary>Forces a value to be within a given range.</summary>
    /// <remarks>Adds or subtracts the size of the range until the value falls within the limits of the range.</remarks>
    /// <param name="testValue">The value to check/adapt.</param>
    /// <param name="lowerLimit">Lower limit of the range (inclusive).</param>
    /// <param name="upperLimit">Upper limit (exclusive).</param>
    /// <returns>The - if necessary corrected - value.</returns>
    /// <exception cref="ArgumentException">IS thrown if the lowerLimit is not smaller than the upperLimit.</exception>
    public static double ValueToRange(double testValue, double lowerLimit, double upperLimit)
    {
        if (upperLimit <= lowerLimit)
        {
            throw new ArgumentException("UpperRange: " + upperLimit + " <+ lowerLimit: " + lowerLimit);
        }
        return ForceToRange(testValue, lowerLimit, upperLimit);
    }

    private static double ForceToRange(double testValue, double lowerLimit, double upperLimit)
    {
        double rangeSize = upperLimit - lowerLimit;
        double checkedForLowerLimit = ForceLowerLimit(lowerLimit, rangeSize, testValue);
        return ForceUpperLimit(upperLimit, rangeSize, checkedForLowerLimit);
    }

    private static double ForceLowerLimit(double lowerLimit, double rangeSize, double toCheck)
    {
        double checkedForUpperLimit = toCheck;
        while (checkedForUpperLimit < lowerLimit)
        {
            checkedForUpperLimit += rangeSize;
        }
        return checkedForUpperLimit;
    }

    private static double ForceUpperLimit(double upperLimit, double rangeSize, double toCheck)
    {
        double checkedForLowerLimit = toCheck;
        while (checkedForLowerLimit >= upperLimit)
        {
            checkedForLowerLimit -= rangeSize;
        }
        return checkedForLowerLimit;
    }
}



