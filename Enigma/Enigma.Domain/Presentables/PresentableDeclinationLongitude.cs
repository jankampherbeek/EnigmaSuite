// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.Presentables;

/// <summary>COmbination of longitude and latitude to be shown in a datagrid.</summary>
/// <param name="PointGlyph">Glyph for the chart point.</param>
/// <param name="Longitude">Text for the longitude.</param>
/// <param name="SignGlyph">Glyph for the sign.</param>
/// <param name="Declination">Text for the declination.</param>
public record PresentableDeclinationLongitude(char PointGlyph, string Longitude, char SignGlyph, string Declination);