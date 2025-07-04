// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.Enneagram;

/// <summary>
/// Calcuylate the strengt of Enneagram types
/// </summary>
public class EnneagramCalc
{
    /// <summary>
    /// Calculate the strengt for 9 Enneagramtypes.
    /// </summary>
    /// <remarks>
    /// Prompt: Calculate the relative strengts of the 9 Enneagramtypes.
    /// For all chartPoints, define the sign based on the longitude (the double in the keyvalue pair), use integers for
    /// the signs: great than 0.0 and smaller than 30.0 is 1, greater than 30.0 and smalleer than 60.0 is 2 etc.
    /// For each enneagramtype make a list of the factors in signsData. Check for the id of the planet and the sign.
    /// The id for the planet is the value in the enum ChartPoints.
    /// Do the same for the all cusps, but use housesData and houses. For houses the index is the id for the cusp,
    /// so houses[1] is cusp 1 etc. houses[0] is ignored. Add the factors to the same list as for signs.
    /// Also perform a calculation for MC and ascendant the same way as used for chartpoints. Use the longitude from
    /// houses[1] for the ascendant and houses[10] for the MC. The id for the ascendant is 1001 and for the MC it is 1002.
    /// Add the results to the existing list of chartfactors.
    /// Calculate for each enneagramtype 1..9 a total factor by multiplying all factors in the list for the
    /// respective enneagram types. Return the results.
    /// If the parameter timeIsKnown is false, ignore the calculation of houses, ascendant and MC.
    /// If the parameter plutoDouble is true, count all factors with Pluto twice
    /// </remarks>
    /// <param name="signsData">Positions in signs</param>
    /// <param name="housesData">Positions in houses</param>
    /// <param name="chartPoints">Calculated chart points</param>
    /// <param name="houses">Calculated houses</param>
    /// <param name="timeIsKnown">True if time is known</param>
    /// <param name="plutoDouble">Count factors for Pluto twice</param>
    /// <returns>List with id for Enneagramtype (1..9) and relative strength</returns>
    public List<KeyValuePair<int, double>> CalcEnneagramStrengths(
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
        
        // Dictionary to store factors for each Enneagram type (1-9)
        var enneagramFactors = new Dictionary<int, List<double>>();
        for (var i = 1; i <= 9; i++)
        {
            enneagramFactors[i] = new List<double>();
        }
        
        // Process chart points (planets, asteroids, etc.)
        foreach (var (key, longitude) in chartPoints)
        {
            var pointId = (int)key;

            // Calculate sign (1-12) based on longitude
            var sign = CalculateSign(longitude);
            
            // Find factors for this planet and sign in signsData
            var factors = FindFactors(signsData, pointId, sign);
            if (factors == null) continue;
            // Add factors to each Enneagram type
            for (var i = 0; i < Math.Min(factors.Length, 9); i++)
            {
                enneagramFactors[i + 1].Add(factors[i]);
            }
                
            // If plutoDouble is true and this is Pluto, add factors again
            if (!plutoDouble || key != ChartPoints.Pluto) continue;
            {
                for (var i = 0; i < Math.Min(factors.Length, 9); i++)
                {
                    enneagramFactors[i + 1].Add(factors[i]);
                }
            }
        }
        
        // Process house cusps (only if time is known)
        if (timeIsKnown)
        {
            // Process cusps 1-12
            for (var cuspIndex = 1; cuspIndex <= 12; cuspIndex++)
            {
                if (cuspIndex < houses.Length)
                {
                    var cuspLongitude = houses[cuspIndex];
                    var cuspSign = CalculateSign(cuspLongitude);
                    var cuspId = 2000 + cuspIndex; // Cusp IDs are 2001-2012
                    
                    var factors = FindFactors(housesData, cuspId, cuspSign);
                    if (factors == null) continue;
                    for (var i = 0; i < Math.Min(factors.Length, 9); i++)
                    {
                        enneagramFactors[i + 1].Add(factors[i]);
                    }
                }
            }
            
            // Process Ascendant (houses[1]) - ID 1001
            if (houses.Length > 1)
            {
                var ascendantLongitude = houses[1];
                var ascendantSign = CalculateSign(ascendantLongitude);
                var ascendantFactors = FindFactors(signsData, 1001, ascendantSign);
                if (ascendantFactors != null)
                {
                    for (var i = 0; i < Math.Min(ascendantFactors.Length, 9); i++)
                    {
                        enneagramFactors[i + 1].Add(ascendantFactors[i]);
                    }
                }
            }
            
            // Process MC (houses[10]) - ID 1002
            if (houses.Length > 10)
            {
                var mcLongitude = houses[10];
                var mcSign = CalculateSign(mcLongitude);
                var mcFactors = FindFactors(signsData, 1002, mcSign);
                if (mcFactors != null)
                {
                    for (var i = 0; i < Math.Min(mcFactors.Length, 9); i++)
                    {
                        enneagramFactors[i + 1].Add(mcFactors[i]);
                    }
                }
            }
        }
        
        // Calculate total strength for each Enneagram type by multiplying all factors
        var result = new List<KeyValuePair<int, double>>();
        for (var enneagramType = 1; enneagramType <= 9; enneagramType++)
        {
            var factors = enneagramFactors[enneagramType];
            var totalStrength = factors.Aggregate(1.0, (current, factor) => current * factor); // Start with 1 for multiplication

            result.Add(new KeyValuePair<int, double>(enneagramType, totalStrength));
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