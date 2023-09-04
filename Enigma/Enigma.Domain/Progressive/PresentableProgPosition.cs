// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Progressive;

/// <summary>Presentable progressive positions.</summary>
/// <param name="pointGlyph">Glyph for the pogressive point.</param>
/// <param name="longitude">Sexagesimal longitude - within the sign - for the progressive point.</param>
/// <param name="longSpeed">Sexagesimal speel in longitude.</param>
/// <param name="signGlyph">Glyph for the sign of the progressive point.</param>
/// <param name="declination">Sexagesimal declination.</param>
/// <param name="declSpeed">Sexagesimal speed in declination.</param>
public record PresentableProgPosition(char pointGlyph, string longitude, char signGlyph, string longSpeed, 
    string declination, string declSpeed);
