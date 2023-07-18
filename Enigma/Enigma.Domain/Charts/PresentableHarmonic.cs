// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Charts;

/// <summary>Harmonics in a presentable format. </summary>
/// <param name="PointTextGlyph">Glyph or text fopr a point</param>
/// <param name="RadixPosText">Radix position as text</param>
/// <param name="RadixSignGlyph">Glyph for radix sign</param>
/// <param name="HarmonicPosText">Harmonic position as text</param>
/// <param name="HarmonicPosGlyph">Glyph for harmonic sign</param>
public record PresentableHarmonic(char PointTextGlyph, string RadixPosText, char RadixSignGlyph, string HarmonicPosText, char HarmonicPosGlyph);
