// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Frontend.UiDomain;

/// <summary>Aspects to be shown in a data grid.</summary>
public record PresentableAspects
{
    public string Point1TextGlyph { get; }
    public string AspectGlyph { get; }
    public string Point2Glyph { get; }
    public string OrbText { get; }
    public string ExactnessText { get; }

    /// <summary>Construct a record with data for aspects to be shown in a datagrid.</summary>
    /// <param name="point1TextGlyph">Glyph or test for the first point. A glyph for solar system points and a text for mundane points.</param>
    /// <param name="aspectGlyph">Glyph for the aspect.</param>
    /// <param name="point2Glyph">Glyph for the second point.</param>
    /// <param name="orbText">Text for the acual orb.</param>
    /// <param name="exactnessText">Text indicating the exactness of the aspect as a percentage.</param>
    public PresentableAspects(string point1TextGlyph, string aspectGlyph, string point2Glyph, string orbText, string exactnessText)
    {
        Point1TextGlyph = point1TextGlyph;
        AspectGlyph = aspectGlyph;
        Point2Glyph = point2Glyph;
        OrbText = orbText;
        ExactnessText = exactnessText;
    }



}