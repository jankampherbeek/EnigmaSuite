// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>
/// Record for a full definition of geographic Latitude.
/// </summary>
/// <param name="DegreeMinuteSecond">Texts for degree, minute and second in that sequence.</param>
/// <param name="Latitude">Value of geographic Latitude.</param>
/// <param name="Direction">Direction for geographic Latitude: north or south.</param>
/// <param name="GeoLatFullText">Text for the geographic Latitude.</param>
public record FullGeoLatitude(int[] DegreeMinuteSecond, double Latitude, Directions4GeoLat Direction, string GeoLatFullText);
