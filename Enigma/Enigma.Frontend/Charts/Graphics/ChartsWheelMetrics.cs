// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows.Media;

namespace Enigma.Frontend.Ui.Charts.Graphics;

/// <summary>
/// Metrics used in drawing a graphic wheel.
/// </summary>
public class ChartsWheelMetrics
{
    // Fixed values
    public double MinDistance { get; } = 6.0;
    // Fonts
    public FontFamily GlyphsFontFamily { get; } = new FontFamily("EnigmaAstrology");
    public FontFamily PositionTextsFontFamily { get; } = new FontFamily("Calibri");
    // Colors
    public Color CuspLineColor { get; } = Colors.Gray;
    public Color CuspTextColor { get; } = Colors.SaddleBrown;
    public Color SolSysPointColor { get; } = Colors.DarkSlateBlue;
    public Color SolSysPointConnectLineColor { get; } = Colors.DarkSlateBlue;
    public Color SolSysPointTextColor { get; } = Colors.DarkSlateBlue;
    public Color HardAspectsColor { get; } = Colors.Red;
    public Color SoftAspectsColor { get; } = Colors.Green;
    public Color MinorAspectsColor { get; } = Colors.Gray;

    // Opacities
    public double CuspLineOpacity { get; } = 0.5;
    public double CuspTextOpacity { get; } = 1.0;
    public double SolSysPointConnectLineOpacity { get; } = 0.25;

    // Circles and radiuses
    public double OuterCircle { get; private set; }
    public double OuterRadius { get; private set; }
    public double OuterSignCircle { get; private set; }
    public double OuterSignRadius { get; private set; }
    public double OuterHouseCircle { get; private set; }
    public double OuterHouseRadius { get; private set; }
    public double OuterAspectCircle { get; private set; }
    public double OuterAspectRadius { get; private set; }
    public double SignGlyphCircle { get; private set; }
    public double SignGlyphRadius { get; private set; }
    public double CuspTextCircle { get; private set; }
    public double CuspTextRadius { get; private set; }
    public double SolSysPointGlyphCircle { get; private set; }
    public double SolSysPointGlyphRadius { get; private set; }
    public double OuterConnectionCircle { get; private set; }
    public double OuterConnectionRadius { get; private set; }
    public double DegreesCircle { get; private set; }
    public double DegreesRadius { get; private set; }
    public double Degrees5Circle { get; private set; }
    public double Degrees5Radius { get; private set; }
    public double SolSysPointTextCircle { get; private set; }
    public double SolSysPointTextRadius { get; private set; }
    public double CardinalIndicatorCircle { get; private set; }
    public double CardinalIndicatorRadius { get; private set; }


    private readonly double OuterCircleInitial = 0.98;
    private readonly double CardinalIndicatorCircleInitial = 0.93;
    private readonly double OuterSignCircleInitial = 0.89;
    private readonly double OuterHouseCircleInitial = 0.79;
    private readonly double OuterAspectCircleInitial = 0.44;
    private readonly double CuspTextCircleInitial = 0.76;
    private readonly double SignGlyphCircleInitial = 0.84;
    private readonly double SolSysPointTextCircleInitial = 0.64;
    private readonly double OuterConnectionCircleInitial = 0.48;
    private readonly double DegreesCircleInitial = 0.775;
    private readonly double Degrees5CircleInitial = 0.76;
    private readonly double SolSysPointGlyphCircleInitial = 0.54;


    // --------------------------------------------------
    public double BaseSize { get; private set; } = 700.0;

    private readonly double _baseStrokeSize = 2.0;
    private readonly double _baseConnectLineSize = 1.0;
    private readonly double _baseAspectLineSize = 6.0;

    public double SizeFactor { get; private set; }


    public double SignGlyphSize { get; private set; }
    public double SolSysPointGlyphSize { get; private set; }
    public double CardinalFontSize { get; private set; }
    public double SolSysPointTextEastOffset { get; private set; }
    public double SolSysPointTextWestOffset { get; private set; }
    public double PositionTextSize { get; private set; }
    public double GlyphXOffset { get; private set; }
    public double GlyphYOffset { get; private set; }
    public double GridSize { get; private set; }
    public double StrokeSize { get; private set; }
    public double StrokeSizeDouble { get; private set; }
    public double ConnectLineSize { get; private set; }
    public double AspectLineSize { get; private set; }



    private readonly double SignGlyphSizeInitial = 28.0;
    private readonly double SolSysPointGlyphSizeInitial = 24.0;
    private readonly double CardinalFontSizeInitial = 16.0;
    private readonly double SolSysPointTextEastOffsetInitial = 8.0;
    private readonly double SolSysPointTextWestOffsetInitial = -20.0;
    private readonly double PositionTextSizeInitial = 10.0;
    private readonly double GlyphXOffsetInitial = 0.0;
    private readonly double GlyphYOffsetInitial = 0.0;

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
        StrokeSize = _baseStrokeSize * SizeFactor;
        StrokeSizeDouble = StrokeSize * 2.0;
        ConnectLineSize = _baseConnectLineSize * SizeFactor;
        AspectLineSize = _baseAspectLineSize * SizeFactor;
        GlyphXOffset = GlyphXOffsetInitial * GridSize;
        GlyphYOffset = GlyphYOffsetInitial * GridSize;
        SignGlyphSize = SignGlyphSizeInitial * (GridSize / 700.0);
        SolSysPointGlyphSize = SolSysPointGlyphSizeInitial * (GridSize / 700.0);
        CardinalFontSize = CardinalFontSizeInitial * (GridSize / 700.0);
        SolSysPointTextEastOffset = SolSysPointTextEastOffsetInitial * SizeFactor;
        SolSysPointTextWestOffset = SolSysPointTextWestOffsetInitial * SizeFactor;
        PositionTextSize = PositionTextSizeInitial * (GridSize / 700.0);

    }

    private void DefineCircleSizes()
    {
        OuterCircle = OuterCircleInitial * GridSize;
        OuterRadius = OuterCircle / 2;

        OuterSignCircle = OuterSignCircleInitial * GridSize;
        OuterSignRadius = OuterSignCircle / 2;

        OuterHouseCircle = OuterHouseCircleInitial * GridSize;
        OuterHouseRadius = OuterHouseCircle / 2;

        OuterAspectCircle = OuterAspectCircleInitial * GridSize;
        OuterAspectRadius = OuterAspectCircle / 2;

        SignGlyphCircle = SignGlyphCircleInitial * GridSize;
        SignGlyphRadius = SignGlyphCircle / 2;

        CuspTextCircle = CuspTextCircleInitial * GridSize;
        CuspTextRadius = CuspTextCircle / 2;

        SolSysPointGlyphCircle = SolSysPointGlyphCircleInitial * GridSize;
        SolSysPointGlyphRadius = SolSysPointGlyphCircle / 2;

        OuterConnectionCircle = OuterConnectionCircleInitial * GridSize;
        OuterConnectionRadius = OuterConnectionCircle / 2;

        DegreesCircle = DegreesCircleInitial * GridSize;
        DegreesRadius = DegreesCircle / 2;

        Degrees5Circle = Degrees5CircleInitial * GridSize;
        Degrees5Radius = Degrees5Circle / 2;

        SolSysPointTextCircle = SolSysPointTextCircleInitial * GridSize;
        SolSysPointTextRadius = SolSysPointTextCircle / 2;

        CardinalIndicatorCircle = CardinalIndicatorCircleInitial * GridSize;
        CardinalIndicatorRadius = CardinalIndicatorCircle / 2;
    }

}