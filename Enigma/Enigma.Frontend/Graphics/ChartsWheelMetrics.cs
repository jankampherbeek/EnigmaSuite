// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows.Media;

namespace Enigma.Frontend.Ui.Graphics;

/// <summary>
/// Metrics used in drawing a graphic wheel.
/// </summary>
public sealed class ChartsWheelMetrics
{
    // Fixed values
    public static double MinDistance => 6.0;

    // Fonts
    public FontFamily GlyphsFontFamily { get; } = new ("EnigmaAstrology");
    public FontFamily PositionTextsFontFamily { get; } = new ("Calibri");
    // Colors
    public Color CuspLineColor { get; } = Colors.SteelBlue;
    public Color CuspTextColor { get; } = Colors.SaddleBrown;
    public Color CelPointColor { get; } = Colors.DarkSlateBlue;
    public Color CelPointConnectLineColor { get; } = Colors.DarkSlateBlue;
    public Color CelPointTextColor { get; } = Colors.DarkSlateBlue;
    public Color HardAspectsColor { get; } = Colors.Red;
    public Color SoftAspectsColor { get; } = Colors.Green;
    public Color MinorAspectsColor { get; } = Colors.Gray;

    // Opacities
    public double CuspLineOpacity => 0.5;
    public double CuspTextOpacity => 1.0;
    public static double CelPointConnectLineOpacity => 0.25;
    public static double AspectOpacity => 0.4;

    // Circles and radiuses
    private double OuterCircle { get; set; }
    public double OuterRadius { get; private set; }
    private double OuterSignCircle { get; set; }
    public double OuterSignRadius { get; private set; }
    private double OuterHouseCircle { get; set; }
    public double OuterHouseRadius { get; private set; }
    private double OuterAspectCircle { get; set; }
    public double OuterAspectRadius { get; private set; }
    private double SignGlyphCircle { get; set; }
    public double SignGlyphRadius { get; private set; }
    private double CuspTextCircle { get; set; }
    public double CuspTextRadius { get; private set; }
    private double CelPointGlyphCircle { get; set; }
    public double CelPointGlyphRadius { get; private set; }
    private double OuterConnectionCircle { get; set; }
    public double OuterConnectionRadius { get; private set; }
    private double DegreesCircle { get; set; }
    public double DegreesRadius { get; private set; }
    private double Degrees5Circle { get; set; }
    public double Degrees5Radius { get; private set; }
    private double CelPointTextCircle { get; set; }
    public double CelPointTextRadius { get; private set; }
    private double CardinalIndicatorCircle { get; set; }
    public double CardinalIndicatorRadius { get; private set; }


    private const double OUTER_CIRCLE_INITIAL = 0.98;
    private const double CARDINAL_INDICATOR_CIRCLE_INITIAL = 0.93;
    private const double OUTER_SIGN_CIRCLE_INITIAL = 0.89;
    private const double OUTER_HOUSE_CIRCLE_INITIAL = 0.79;
    private const double OUTER_ASPECT_CIRCLE_INITIAL = 0.44;
    private const double CUSP_TEXT_CIRCLE_INITIAL = 0.76;
    private const double SIGN_GLYPH_CIRCLE_INITIAL = 0.84;
    private const double CEL_POINT_TEXT_CIRCLE_INITIAL = 0.64;
    private const double OUTER_CONNECTION_CIRCLE_INITIAL = 0.48;
    private const double DEGREES_CIRCLE_INITIAL = 0.775;
    private const double DEGREES5_CIRCLE_INITIAL = 0.76;
    private const double CEL_POINT_GLYPH_CIRCLE_INITIAL = 0.54;


    // --------------------------------------------------
    public double BaseSize { get; } = 700.0;

    private const double BASE_STROKE_SIZE = 2.0;
    private const double BASE_CONNECT_LINE_SIZE = 1.0;
    private const double BASE_ASPECT_LINE_SIZE = 6.0;

    public double SizeFactor { get; private set; }


    public double SignGlyphSize { get; private set; }
    public double CelPointGlyphSize { get; private set; }
    public double CardinalFontSize { get; private set; }
    public double CelPointTextEastOffset { get; private set; }
    public double CelPointTextWestOffset { get; private set; }
    public double PositionTextSize { get; private set; }
    public double GlyphXOffset { get; private set; }
    public double GlyphYOffset { get; private set; }
    public double GridSize { get; private set; }
    public double StrokeSize { get; private set; }
    public double StrokeSizeDouble { get; private set; }
    public double ConnectLineSize { get; private set; }
    public double AspectLineSize { get; private set; }


    private const double SIGN_GLYPH_SIZE_INITIAL = 28.0;
    private const double CEL_POINT_GLYPH_SIZE_INITIAL = 24.0;
    private const double CARDINAL_FONT_SIZE_INITIAL = 16.0;
    private const double CEL_POINT_TEXT_EAST_OFFSET_INITIAL = 8.0;
    private const double CEL_POINT_TEXT_WEST_OFFSET_INITIAL = -20.0;
    private const double POSITION_TEXT_SIZE_INITIAL = 10.0;
    private const double GLYPH_X_OFFSET_INITIAL = 0.0;
    private const double GLYPH_Y_OFFSET_INITIAL = 0.0;

    public ChartsWheelMetrics()
    {
        GridSize = 700.0;
        SizeFactor = 1.0;
        DefineSizes();
    }

    public void SetSizeFactor(double newFactor)
    {
        SizeFactor = newFactor;
        DefineSizes();
    }

    private void DefineSizes()
    {
        DefineCircleSizes();

        GridSize = BaseSize * SizeFactor;
        StrokeSize = BASE_STROKE_SIZE * SizeFactor;
        StrokeSizeDouble = StrokeSize * 2.0;
        ConnectLineSize = BASE_CONNECT_LINE_SIZE * SizeFactor;
        AspectLineSize = BASE_ASPECT_LINE_SIZE * SizeFactor;
        GlyphXOffset = GLYPH_X_OFFSET_INITIAL * GridSize;
        GlyphYOffset = GLYPH_Y_OFFSET_INITIAL * GridSize;
        SignGlyphSize = SIGN_GLYPH_SIZE_INITIAL * (GridSize / 700.0);
        CelPointGlyphSize = CEL_POINT_GLYPH_SIZE_INITIAL * (GridSize / 700.0);
        CardinalFontSize = CARDINAL_FONT_SIZE_INITIAL * (GridSize / 700.0);
        CelPointTextEastOffset = CEL_POINT_TEXT_EAST_OFFSET_INITIAL * SizeFactor;
        CelPointTextWestOffset = CEL_POINT_TEXT_WEST_OFFSET_INITIAL * SizeFactor;
        PositionTextSize = POSITION_TEXT_SIZE_INITIAL * (GridSize / 700.0);

    }

    private void DefineCircleSizes()
    {
        OuterCircle = OUTER_CIRCLE_INITIAL * GridSize;
        OuterRadius = OuterCircle / 2;

        OuterSignCircle = OUTER_SIGN_CIRCLE_INITIAL * GridSize;
        OuterSignRadius = OuterSignCircle / 2;

        OuterHouseCircle = OUTER_HOUSE_CIRCLE_INITIAL * GridSize;
        OuterHouseRadius = OuterHouseCircle / 2;

        OuterAspectCircle = OUTER_ASPECT_CIRCLE_INITIAL * GridSize;
        OuterAspectRadius = OuterAspectCircle / 2;

        SignGlyphCircle = SIGN_GLYPH_CIRCLE_INITIAL * GridSize;
        SignGlyphRadius = SignGlyphCircle / 2;

        CuspTextCircle = CUSP_TEXT_CIRCLE_INITIAL * GridSize;
        CuspTextRadius = CuspTextCircle / 2;

        CelPointGlyphCircle = CEL_POINT_GLYPH_CIRCLE_INITIAL * GridSize;
        CelPointGlyphRadius = CelPointGlyphCircle / 2;

        OuterConnectionCircle = OUTER_CONNECTION_CIRCLE_INITIAL * GridSize;
        OuterConnectionRadius = OuterConnectionCircle / 2;

        DegreesCircle = DEGREES_CIRCLE_INITIAL * GridSize;
        DegreesRadius = DegreesCircle / 2;

        Degrees5Circle = DEGREES5_CIRCLE_INITIAL * GridSize;
        Degrees5Radius = Degrees5Circle / 2;

        CelPointTextCircle = CEL_POINT_TEXT_CIRCLE_INITIAL * GridSize;
        CelPointTextRadius = CelPointTextCircle / 2;

        CardinalIndicatorCircle = CARDINAL_INDICATOR_CIRCLE_INITIAL * GridSize;
        CardinalIndicatorRadius = CardinalIndicatorCircle / 2;
    }

}