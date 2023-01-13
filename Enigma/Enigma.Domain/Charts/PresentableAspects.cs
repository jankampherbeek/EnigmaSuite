// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Charts;

/// <summary>Aspects to be shown in a data grid.</summary>
/// <param name="Point1TextGlyph">Glyph or test for the first point. A glyph for celestial points and a text for mundane points.</param>
/// <param name="AspectGlyph">Glyph for the aspect.</param>
/// <param name="Point2Glyph">Glyph for the second point.</param>
/// <param name="OrbText">Text for the acual orb.</param>
/// <param name="ExactnessText">Text indicating the exactness of the aspect as a percentage.</param> 
public record PresentableAspects(char Point1TextGlyph, char AspectGlyph, char Point2Glyph, string OrbText, string ExactnessText);
