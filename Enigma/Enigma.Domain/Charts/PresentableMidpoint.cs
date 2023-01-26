// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Charts;


/// <summary>Midpoint to be presented in a data grid.</summary>
public record PresentableMidpoint
{
    public char Point1Glyph { get; }
    public string Separator { get; } = "/";
    public char Point2Glyph { get; }
    public string Position { get; }
    public char SignGlyph { get; }

    /// <param name="Point1Glyph">Glyph of first point.</param>
    /// <param name="Point2Glyph">Glyph of second point.</param>
    /// <param name="SignGlyph">Glyph of sign.</param>
    /// <param name="Position">Positions as text.</param>
    public PresentableMidpoint(char point1Glyph, char point2Glyph, string position, char signGlyph)
    {
        Point1Glyph = point1Glyph;
        Point2Glyph = point2Glyph;
        SignGlyph = signGlyph;
        Position = position;
    }

}



/// <summary>Occupied midpoints to be shown in a datagrid.</summary>
public record PresentableOccupiedMidpoint
{
    public char Point1Glyph { get; }
    public string Separator { get; } = "/";
    public char Point2Glyph { get; }
    public string IsSign { get; } = "=";
    public char PointOccGlyph { get; }
    public string OrbText { get; }
    public string ExactnessText { get; }

    /// <summary>Construct a record with data for occupied midpoints to be shown in a datagrid.</summary>
    /// <param name="point1Glyph">Glyph for the first point.</param>
    /// <param name="point2Glyph">Glyph for the second point.</param>
    /// <param name="pointOccGlyph">Glyph for the occupying point.</param>
    /// <param name="orbText">Text with the orb.</param>
    /// <param name="exactnessText">text with the exactness as percentage.</param>
    public PresentableOccupiedMidpoint(char point1Glyph, char point2Glyph, char pointOccGlyph, string orbText, string exactnessText)
    {
        Point1Glyph = point1Glyph;
        Point2Glyph = point2Glyph;
        PointOccGlyph = pointOccGlyph;
        OrbText = orbText;
        ExactnessText = exactnessText;
    }


}