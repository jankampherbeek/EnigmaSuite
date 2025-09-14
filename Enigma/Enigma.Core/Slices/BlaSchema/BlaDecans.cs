// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Decans for BLA schema calculations
/// </summary>
//
public static class BlaDecans
{

    /// <summary>
    /// Define decans for chartpoints and their longitudes.
    /// </summary>
    /// <param name="longitudes">ChartPoints and longitudes</param>
    /// <returns>Dictionary with ChartPoints and index of decan 0..6</returns>
    public static Dictionary<ChartPoints, int> DefineDecans(Dictionary<ChartPoints, double> longitudes)
    {
        var decans = new Dictionary<ChartPoints, int>();
        foreach (var point in longitudes)
        {
            var decan = (int)Math.Truncate(point.Value / 10.0) + 1;
            while (decan > 7) decan -= 7;
            decans.Add(point.Key, decan);
        }
        return decans;   
    }
}