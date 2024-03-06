// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;


/// <summary>Parallels to be shown in a data grid.</summary>
/// <param name="Point1Text">Text for the first point.</param>
/// <param name="Point1Glyph">Glyph for the first point. Possibly empty string.</param>
/// <param name="TypeGlyph">Glyph for the aspect.</param>
/// <param name="Point2Text">Text for the second point.</param>
/// <param name="Point2Glyph">Glyph for the second point. Possibly empty string.</param>
/// <param name="OrbText">Text for the actual orb.</param>
/// <param name="ExactnessText">Text indicating the exactness of the aspect as a percentage.</param> 
public record PresentableParallels(string Point1Text, char Point1Glyph, char TypeGlyph, 
    string Point2Text, char Point2Glyph, string OrbText, string ExactnessText);