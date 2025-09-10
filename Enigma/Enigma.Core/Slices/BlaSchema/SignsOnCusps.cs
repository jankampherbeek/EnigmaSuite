// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Ecliptical signs o houses cusps
/// </summary>
public static class SignsOnCusps
{

    /// <summary>
    /// Define the signs that are on all housecusps (intercepted signs are ignored)
    /// </summary>
    /// <param name="houseLongitudes">Housecusps and longitudes</param>
    /// <returns>Dictionary with for the cusp (1..12) and for the sign (1..12)</returns>
    public static Dictionary<int, int> DefineSignsOnCusps(Dictionary<int, double> houseLongitudes)
    {
        var signsOnCusps = new Dictionary<int, int>();
        foreach (var (house, longitude) in houseLongitudes)
        {
            var sign = (int)Math.Truncate(longitude / 30.0) + 1;
            signsOnCusps.Add(house, sign);
        }
        return signsOnCusps;
    }
    
}