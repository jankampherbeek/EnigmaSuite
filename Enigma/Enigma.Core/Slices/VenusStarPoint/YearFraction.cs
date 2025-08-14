// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.VenusStarPoint;

/// <summary>
/// Calculate the year and a fraction of that year for a given Julian day
/// </summary>
/// <remarks>
/// This is used in a formula by Jean Meeus, see Astronomical Algorithms p. 249
/// </remarks>
public static class YearFraction
{
    private const double YEAR_LENGTH = 365.25;
    /// <summary>
    /// Calculate the year as a decimal number with a fraction
    /// </summary>
    /// <param name="year">The year</param>
    /// <param name="jd">The julian day of the actual date</param>
    /// <param name="jdNewYear">The julian day of January 1 of the same year</param>
    /// <returns>The year as a double, including a fraction</returns>
    public static double CalcYearAndFraction(int year, double jd, double jdNewYear)
    {
        var fraction = (jd - jdNewYear) / YEAR_LENGTH;
        return (year + fraction);
    }
    
}