// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>
/// Record for a full definition of geographic Longitude.
/// </summary>
/// <param name="DegreeMinuteSecond">Texts for degree, minute and second in that sequence.</param>
/// <param name="Longitude">Value of geographic Longitude.</param>
/// <param name="Direction">Direction for geographic Longitude: east or west.</param>
/// <param name="GeoLongFullText">Text for the geographic Longitude.</param>
public record FullGeoLongitude(int[] DegreeMinuteSecond, double Longitude, Directions4GeoLong Direction, string GeoLongFullText);
