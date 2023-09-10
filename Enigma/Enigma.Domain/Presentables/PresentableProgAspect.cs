// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;

/// <summary>Presentable progressive aspect.</summary>
/// <param name="RadixGlyph">Glyph for radix point.</param>
/// <param name="RadixName">Text for radix point.</param>
/// <param name="AspectGlyph">Glyph for aspect.</param>
/// <param name="AspectName">Text for aspect.</param>
/// <param name="ProgGlyph">Glyph for progressive point.</param>
/// <param name="ProgName">Text for progressive point.</param>
/// <param name="OrbText">Sexagesimal presentation of orb.</param>
public record PresentableProgAspect(char RadixGlyph, string RadixName, char AspectGlyph, string AspectName, 
    char ProgGlyph, string ProgName, string OrbText);