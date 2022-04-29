// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;

namespace E4C.Core.Util;

public static class RangeUtil
{
    public static double ValueToRange(double testValue, double lowerLimit, double upperLimit)
    {
        if ((upperLimit <= lowerLimit))
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



