// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;



/// <summary>
/// Factory for the creation of BlaPositions
/// </summary>
public class BlaPositionsFactory()
{
    /// <summary>
    /// Create a record BlaPositions
    /// </summary>
    /// <param name="point">The Chartpoint</param>
    /// <param name="longitude">Ecliptical longitude</param>
    /// <param name="house">Number of the house 1..12</param>
    /// <returns>BlaPositions</returns>
    public BlaPositions CreateBlaPositions(ChartPoints point, double longitude, int house)
    {
        var sign = (int)Math.Truncate(longitude / 30.0) + 1;
        var decan = (int)Math.Truncate(longitude / 10.0) + 1;
        while (decan > 7) decan -= 7; 
        return new BlaPositions(point, longitude, sign, decan, house);
    }
    
}