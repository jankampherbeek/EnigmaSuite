// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Facades.Se;
using Enigma.Domain.Exceptions;

namespace Enigma.Core.Slices.Enneagram;

/// <summary>
/// Calculation of Placidus houses
/// </summary>
public class CalcHousesForEnneagram
{
    private readonly IHousesFacade _housesFacade;
    
    /// <summary>
    /// Initializes a new instance of the CalcPlacidusHouses class
    /// </summary>
    /// <param name="housesFacade">The houses facade for calculations</param>
    public CalcHousesForEnneagram(IHousesFacade housesFacade)
    {
        _housesFacade = housesFacade ?? throw new ArgumentNullException(nameof(housesFacade));
    }
    
    /// <summary>
    /// Calculate tropical Placidus houses
    /// </summary>
    /// <param name="julianDay">Julian day</param>
    /// <param name="geoLon">geographic longitude</param>
    /// <param name="geoLat">geograpic latitude</param>
    /// <returns>An array of 13 positions, the first position can be ignored, the cusps are in 1..12</returns>
    /// <exception cref="SwissEphException">Thrown when the Swiss Ephemeris calculation fails</exception>
    /// <exception cref="ArgumentException">Thrown when input parameters are invalid</exception>
    public double[] CalcHouses(double julianDay, double geoLon, double geoLat)
    {
        // Validate input parameters
        if (double.IsNaN(julianDay) || double.IsInfinity(julianDay))
        {
            throw new ArgumentException("Julian day must be a finite number", nameof(julianDay));
        }
        
        if (geoLat is <= -90 or >= 90)
        {
            throw new ArgumentException("Geographic latitude must be between -90 and 90 degrees (exclusive)", nameof(geoLat));
        }
        
        if (geoLon is < -180 or > 180)
        {
            throw new ArgumentException("Geographic longitude must be between -180 and 180 degrees", nameof(geoLon));
        }
        
        try
        {
            const char houseType = 'P';
            const int flags = 2;  // Use SE, no specifics
            return _housesFacade.RetrieveHouses(julianDay, flags, geoLat, geoLon, houseType)[0];
        }
        catch (SwissEphException)
        {
            // Re-throw SwissEphException as it contains specific error information
            throw;
        }
        catch (Exception ex) when (ex is not SwissEphException)
        {
            // Wrap unexpected exceptions with context
            throw new EnigmaException($"Unexpected error calculating Placidus houses: {ex.Message}");
        }
    }
}