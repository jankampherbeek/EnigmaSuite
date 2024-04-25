// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;


/// <summary>Midpoint to be presented in a data grid.</summary>
public record PresentableMidpoint
{
    public char Point1Glyph { get; set; }
    public string Separator { get; set; } = "/";
    public char Point2Glyph { get; set; }
    public string Position { get; set; }
    public char SignGlyph { get; set; }

    /// <param name="point1Glyph">Glyph of first point.</param>
    /// <param name="point2Glyph">Glyph of second point.</param>
    /// <param name="signGlyph">Glyph of sign.</param>
    /// <param name="position">Positions as text.</param>
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
    public char Point1Glyph { get; set; }
    public string Separator { get; set; } = "/";
    public char Point2Glyph { get; set; }
    public string IsSign { get; set; } = "=";
    public char PointOccGlyph { get; set; }
    public string OrbText { get; set;  }
    public double OrbExactness { get; set;  }

    /// <summary>Construct a record with data for occupied midpoints to be shown in a datagrid.</summary>
    /// <param name="point1Glyph">Glyph for the first point.</param>
    /// <param name="point2Glyph">Glyph for the second point.</param>
    /// <param name="pointOccGlyph">Glyph for the occupying point.</param>
    /// <param name="orbText">Text with the orb.</param>
    /// <param name="orbExactness">Value with the exactness as percentage.</param>
    public PresentableOccupiedMidpoint(char point1Glyph, char point2Glyph, char pointOccGlyph, string orbText, double orbExactness)
    {
        Point1Glyph = point1Glyph;
        Point2Glyph = point2Glyph;
        PointOccGlyph = pointOccGlyph;
        OrbText = orbText;
        OrbExactness = orbExactness;
    }


}