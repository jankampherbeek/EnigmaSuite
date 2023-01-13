// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.Charts;


/// <summary>Positions of houses to be shown in a datagrid.</summary>
/// <param name="PointName">Textual identifier of mundane point.</param>
/// <param name="LongText">Sexagesimal longitude within sign.</param>
/// <param name="SignGlyph">Glyph for the sign the belongs to the longitude.</param>
/// <param name="RightAscText">Sexagesimal right ascension in degrees.</param>
/// <param name="DeclText">Sexagesimal declination, positive or negative.</param>
/// <param name="AzimuthText">Sexagesimal azimuth.</param>
/// <param name="AltitudeText">Sexagesimal altitude, positive or negative.</param> 
public record PresentableHousePositions(string PointName, string LongText, char SignGlyph, string RightAscText, string DeclText, string AzimuthText, string AltitudeText);
