// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;


/// <summary>Positions of celestial points to be shown in a datagrid.</summary>
public record PresentableCommonPositions
{
    public char PointGlyph { get; set; }
    public string LongText { get; set; }
    public char SignGlyph { get; set; }
    public string LongSpeedText { get; set; }
    public string LatText { get; set; }
    public string LatSpeedText { get; set; }
    public string RightAscText { get; set; }
    public string RightAscSpeedText { get; set; }
    public string DeclText { get; set; }
    public string DeclSpeedText { get; set; }
    public string DistanceText { get; set; }
    public string DistanceSpeedText { get; set; }
    public string AzimuthText { get; set; }
    public string AltitudeText { get; set; }

    /// <summary>Construct a record with positions for celestial points to be shown in a datagrid.</summary>
    /// <param name="pointGlyph">Glyph for celestial point.</param>
    /// <param name="eclipticalLong">Tuple with sexagesimal longitude in sign, glyph for the sign and sexagesimal speed in longitude.</param>
    /// <param name="eclipticalLat">Tuple with sexagesimal latitude and speed in latitude.</param>
    /// <param name="equatorialRa">Tuple with right ascension in degrees (sexagesimal) and speed in ra.</param>
    /// <param name="equatorialDecl">Tuple with sexagesimal declination and speed in declination. Uses -sign for south.</param>
    /// <param name="distance">Tuple with decimal distance and speed in distance.</param>
    /// <param name="azimuth">Sexagesimal azimuth.</param>
    /// <param name="altitude">Sexagesimal altitude, positive or negative.</param>
    public PresentableCommonPositions(char pointGlyph,
        Tuple<string, char, string> eclipticalLong,
        Tuple<string, string> eclipticalLat,
        Tuple<string, string> equatorialRa,
        Tuple<string, string> equatorialDecl,
        Tuple<string, string> distance,
        string azimuth,
        string altitude)
    {
        PointGlyph = pointGlyph;
        LongText = eclipticalLong.Item1;
        SignGlyph = eclipticalLong.Item2;
        LongSpeedText = eclipticalLong.Item3;
        LatText = eclipticalLat.Item1;
        LatSpeedText = eclipticalLat.Item2;
        RightAscText = equatorialRa.Item1;
        RightAscSpeedText = equatorialRa.Item2;
        DeclText = equatorialDecl.Item1;
        DeclSpeedText = equatorialDecl.Item2;
        DistanceText = distance.Item1;
        DistanceSpeedText = distance.Item2;
        AzimuthText = azimuth;
        AltitudeText = altitude;
    }

}

