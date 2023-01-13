// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Charts;

/// <summary>Aspects to be shown in a data grid.</summary>
/// <param name="point1TextGlyph">Glyph or test for the first point. A glyph for celestial points and a text for mundane points.</param>
/// <param name="aspectGlyph">Glyph for the aspect.</param>
/// <param name="point2Glyph">Glyph for the second point.</param>
/// <param name="orbText">Text for the acual orb.</param>
/// <param name="exactnessText">Text indicating the exactness of the aspect as a percentage.</param>
public record PresentableHarmonic(char PointTextGlyph, string RadixPosTest, char RadixSignGlyph, string HarmonicPosText, char HarmonicPosGlyph);
