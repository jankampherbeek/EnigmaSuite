// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;

/// <summary>Presentable progressive positions.</summary>
/// <param name="PointGlyph">Glyph for the pogressive point.</param>
/// <param name="Longitude">Sexagesimal longitude - within the sign - for the progressive point.</param>
/// <param name="SignGlyph">Glyph for the sign of the progressive point.</param>
/// <param name="Latitude">Sexagesimal latitude.</param>
/// <param name="Ra">Sexagesimal right ascension</param>
/// <param name="Declination">Sexagesimal declination.</param>
public record PresentableProgPosition(char PointGlyph, string Longitude, char SignGlyph, string Latitude, 
    string Ra, string Declination);
