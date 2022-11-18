// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Charts;

/// <summary>Midpoints to be shown in a data grid.</summary>
public record PresentableMidpoint
{
    public string Point1Glyph { get; }
    public string Separator { get; } = "/";
    public string Point2Glyph { get; }
    public string Position { get; }
    public string SignGlyph { get; }    
   

    /// <summary>Construct a record with data for midpoints to be shown in a datagrid.</summary>
    /// <param name="point1Glyph">Glyph for the first point.</param>
    /// <param name="point2Glyph">Glyph for the second point.</param>

    public PresentableMidpoint(string point1Glyph, string point2Glyph, string signGlyph, string position)
    {
        Point1Glyph = point1Glyph;
        Point2Glyph = point2Glyph;
        Position = position;
        SignGlyph = signGlyph;
    }

    /// <param name="orbText">Text for the acual orb.</param>
    /// <param name="exactnessText">Text indicating the exactness of the aspect as a percentage.</param>

}

/// <summary>Occupied midpoints to be shown in a datagrid.</summary>
public record PresentableOccupiedMidpoint
{
    public string Point1Glyph { get; }
    public string Separator { get; } = "/";
    public string Point2Glyph { get; }
    public string IsSign { get; } = "=";
    public string PointOccGlyph { get; }
    public string OrbText { get; }
    public string ExactnessText { get; }

    /// <summary>Construct a record with data for occupied midpoints to be shown in a datagrid.</summary>
    /// <param name="point1Glyph">Glyph for the first point.</param>
    /// <param name="point2Glyph">Glyph for the second point.</param>
    /// <param name="pointOccGlyph">Glyph for the occupying point.</param>
    /// <param name="orbText">Text with the orb.</param>
    /// <param name="exactnessText">text with the exactness as percentage.</param>
    public PresentableOccupiedMidpoint(string point1Glyph, string point2Glyph, string pointOccGlyph, string orbText, string exactnessText)
    {
        Point1Glyph = point1Glyph;
        Point2Glyph = point2Glyph;
        PointOccGlyph = pointOccGlyph;
        OrbText= orbText;
        ExactnessText= exactnessText;
    }


}