// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;

/// <summary>Longitude Equivalents to be shown in a data grid.</summary>
/// <param name="PointText">Name of chartpoint.</param>
/// <param name="PointGlyph">Glyph for chartpoint, if any.</param>
/// <param name="Longitude">Radix longitude for chartpoint.</param>
/// <param name="LongitudeGlyph">Glyph for sign.</param>
/// <param name="Declination">Radix declination.</param>
/// <param name="CoDeclination">Co-declination if chartpoint is OOB, otherwise empty.</param>
/// <param name="LongitudeEquivalent">Longitude for longitude equivalent.</param>
/// <param name="LeGlyph">Glyph for sign longitude equivalent.</param>
public record PresentableLongitudeEquivalent(string PointText, char PointGlyph, string Longitude, char LongitudeGlyph,
    string Declination, string CoDeclination, string LongitudeEquivalent, char LeGlyph);


