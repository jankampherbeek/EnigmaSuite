// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Frontend.UiDomain;


/// <summary>Positions of houses to be shown in a datagrid.</summary>
public record PresentableHousePositions
{
    public string PointName { get; }
    public string Longtext { get; }
    public string SignGlyph { get; }
    public string RightAscText{ get; }
    public string Decltext{ get; }
    public string AzimuthText { get; }
    public string AltitudeText { get; }



    /// <summary>Construct a record with mundane positions to be shown in a datagrid.</summary>
    /// <param name="pointName">Textual identifier of mundane point.</param>
    /// <param name="longText">Sexagesimal longitude within sign.</param>
    /// <param name="signGlyph">Glyph for the sign the belongs to the longitude.</param>
    /// <param name="rightAscText">Sexagesimal right ascension in degrees.</param>
    /// <param name="declText">Sexagesimal declination, positive or negative.</param>
    /// <param name="azimuthText">Sexagesimal azimuth.</param>
    /// <param name="altitudeText">Sexagesimal altitude, positive or negative.</param>
    public PresentableHousePositions(string pointName, string longText, string signGlyph, string rightAscText, string declText, string azimuthText, string altitudeText)
    {
        PointName = pointName;
        Longtext = longText;
        SignGlyph = signGlyph;
        RightAscText = rightAscText;
        Decltext = declText;
        AzimuthText = azimuthText;
        AltitudeText = altitudeText;
    }

}