// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.Enneagram;


/// <summary>
/// Define details for the Enneagram calculations
/// </summary>
public class EnneagramDetails
{
    /// <summary>
    /// Calculate the strength for 9 Enneagramtypes.
    /// </summary>
    /// <remarks>
    /// Prompt: Find the factors for Enneagram type strengths in a comparable way as in EnneagramCalc.
    /// Do not calculate the total strengths but create a EnneagramDetailsLine for each chart point in signs (including Ascendant and MC).
    /// Do not add entries for house cusps (2001–2012), as cusps are only boundaries and have no factors.
    /// </remarks>
    /// <param name="signsData">Positions in signs</param>
    /// <param name="housesData">Positions in houses (not used for cusps)</param>
    /// <param name="chartPoints">Calculated chart points</param>
    /// <param name="houses">Calculated houses</param>
    /// <param name="timeIsKnown">True if time is known</param>
    /// <param name="plutoDouble">Count factors for Pluto twice</param>
    /// <returns>List with id for Enneagramtype (1..9) and relative strength</returns>
    public List<EnneagramDetailsLine> CalcEnneagramStrengths(
        List<EnneagramData> signsData,
        List<EnneagramData> housesData,
        List<KeyValuePair<ChartPoints, double>> chartPoints,
        double[] houses,
        bool timeIsKnown,
        bool plutoDouble)
    {
        ArgumentNullException.ThrowIfNull(signsData);
        ArgumentNullException.ThrowIfNull(housesData);
        ArgumentNullException.ThrowIfNull(chartPoints);
        ArgumentNullException.ThrowIfNull(houses);

        if (houses.Length < 13)
        {
            throw new ArgumentException("Houses array must have at least 13 elements", nameof(houses));
        }

        var result = new List<EnneagramDetailsLine>();

        // Process chart points (planets, asteroids, etc.) - Signs data
        foreach (var (key, longitude) in chartPoints)
        {
            var pointId = (int)key;
            var sign = CalculateSign(longitude);
            
            // Find factors for this planet and sign in signsData
            var factors = FindFactors(signsData, pointId, sign);
            if (factors == null) continue;
            result.Add(new EnneagramDetailsLine(key, sign, true, factors));
                
            // If plutoDouble is true and this is Pluto, add the same line again
            if (plutoDouble && key == ChartPoints.Pluto)
            {
                result.Add(new EnneagramDetailsLine(key, sign, true, factors));
            }
        }

        // Add Ascendant (houses[1]) - ID 1001 - Use signs data (only when time is known)
        if (timeIsKnown && houses.Length > 1)
        {
            var ascendantLongitude = houses[1];
            var ascendantSign = CalculateSign(ascendantLongitude);
            var ascendantFactors = FindFactors(signsData, 1001, ascendantSign);
            if (ascendantFactors != null)
            {
                result.Add(new EnneagramDetailsLine(ChartPoints.Ascendant, ascendantSign, true, ascendantFactors));
            }
        }

        // Add MC (houses[10]) - ID 1002 - Use signs data (only when time is known)
        if (timeIsKnown && houses.Length > 10)
        {
            var mcLongitude = houses[10];
            var mcSign = CalculateSign(mcLongitude);
            var mcFactors = FindFactors(signsData, 1002, mcSign);
            if (mcFactors != null)
            {
                result.Add(new EnneagramDetailsLine(ChartPoints.Mc, mcSign, true, mcFactors));
            }
        }

        return result;
    }

    /// <summary>
    /// Calculate sign (1-12) based on longitude
    /// </summary>
    /// <param name="longitude">Longitude in degrees</param>
    /// <returns>Sign number (1-12)</returns>
    private static int CalculateSign(double longitude)
    {
        // Normalize longitude to 0-360 range
        var normalizedLongitude = longitude % 360.0;
        if (normalizedLongitude < 0)
        {
            normalizedLongitude += 360.0;
        }
        
        // Calculate sign: 0-30 = 1, 30-60 = 2, etc.
        var sign = (int)(normalizedLongitude / 30.0) + 1;
        
        // Ensure sign is in range 1-12
        if (sign > 12) sign = 12;
        if (sign < 1) sign = 1;
        
        return sign;
    }
    
    /// <summary>
    /// Find factors for a specific point and sign in the data
    /// </summary>
    /// <param name="data">The data to search in</param>
    /// <param name="pointId">The point ID</param>
    /// <param name="sign">The sign (1-12)</param>
    /// <returns>Array of factors or null if not found</returns>
    private static double[]? FindFactors(List<EnneagramData> data, int pointId, int sign)
    {
        var matchingData = data.FirstOrDefault(d => d.Point == pointId && d.Index == sign);
        return matchingData?.Factors;
    }
}