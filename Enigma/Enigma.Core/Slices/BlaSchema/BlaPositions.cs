// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Positions for Black Lights Astrology calculations
/// </summary>
/// <param name="Point">The chart point</param>
/// <param name="Sign">Nr of the sign, 1 = Aries,.. 12 = Pisces</param>
/// <param name="Decan">Nr of the decans: 1 = Mars, 2 = Sun, 3 Venus, 4 = Mercury, 5 = Moon 6 = Saturn, 7 = Jupiter</param>
public record BlaPositions(ChartPoints Point, int Sign, int Decan);

/// <summary>
/// Factory for the creation of BlaPositions
/// </summary>
public class BlaPositionsFactory()
{
    /// <summary>
    /// Create a record BlaPositions
    /// </summary>
    /// <param name="Point">The Chartpoint</param>
    /// <param name="longitude">Ecliptical lonigutde</param>
    /// <returns>BlaPositions</returns>
    public BlaPositions CreateBlaPositions(ChartPoints Point, double longitude)
    {
        var sign = (int)Math.Truncate(longitude / 30.0) + 1;
        var decan = (int)Math.Truncate(longitude / 10.0) + 1;
        while (decan > 7) decan -= 7; 
        return new BlaPositions(Point, sign, decan);
    }
    
}