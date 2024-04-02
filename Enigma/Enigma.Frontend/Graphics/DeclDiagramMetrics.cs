// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.
// namespace Enigma.Frontend.Ui.Graphics;

using System;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace Enigma.Frontend.Ui.Graphics;

/// <summary>Metrics used in drawing a declination diagram.</summary>
public class DeclDiagramMetrics
{

    private const int LONGITUDE_DEGREES_COUNT = 180;     // Counted for 6 signs only.
    private const double BASE_WIDTH = 800.0;
    private const double BASE_HEIGHT = 640.0;
    private const double DIAGRAM_OFFSET_LEFT_FACTOR = 0.08;
    private const double DIAGRAM_OFFSET_RIGHT_FACTOR = 0.08;
    private const double DIAGRAM_HALF_HEIGHT_FACTOR = 0.47;
    private const double DIAGRAM_WIDTH_FACTOR = 0.84;
    private const double DEGREE_SIZE_LARGE_FACTOR = 0.012;
    private const double DEGREE_SIZE_SMALL_FACTOR = 0.006;
    private const double DEGREE_TEXT_SIZE_FACTOR = 0.02;
    private const double CELPOINT_GLYPH_SIZE_FACTOR = 0.03;
    private const double DECL_DEGREE_CHARACTER_OFFSET_LEFT_FACTOR = 0.03;
    private const double DECL_DEGREE_CHARACTER_OFFSET_RIGHT_FACTOR = 0.005;
    private const double DECL_DEGREE_TOP_OFFSET_FACTOR = 0.07;
    private const double DECL_DEGREE_BOTTOM_OFFSET_FACTOR = 0.07;
    private const double DECL_DEGREE_LEFT_OFFSET_FACTOR = 0.03;
    private const double DECL_DEGREE_RIGHT_OFFSET_FACTOR = 0.03;
    private const double LONG_DEGREE_TOP_OFFSET_FACTOR = 0.04;
    private const double LONG_DEGREE_BOTTOM_OFFSET_FACTOR = 0.04;
    private const double SIGN_GLYPH_SIZE_FACTOR = 0.04;
    private const double SIGN_WIDTH_FACTOR = 0.14;
    private const double POSITION_MARKER_SIZE_FACTOR = 0.01;
    public double HeightFactor { get; private set; }
    public double WidthFactor { get; private set; }
    public double CanvasWidth { get; private set; }
    public double CanvasHeight { get; private set; }
    public int DeclDegreesCount { get; set; } = 30;
    public double DiagramOffsetLeft { get; private set; }
    public double DiagramOffsetRight { get; private set; }
    public double DiagramHalfHeight { get; private set; }
    public double DiagramWidth { get; private set; }
    public double DegreeSizeLarge { get; private set; }
    public double DegreeSizeSmall { get; private set; }
    public double DeclDegreeCharacterLeftOffset { get; private set; }
    public double DeclDegreeCharacterRightOffset { get; private set; }
    public double DeclDegreeTopOffset { get; private set; }
    public double DeclDegreeBottomOffset { get; private set; }
    public double DeclDegreeLeftOffset { get; private set; }
    public double DeclDegreeRightOffset { get; private set; }
    public double LongDegreeTopOffset { get; private set; }
    public double LongDegreeBottomOffset { get; private set; }
    public double SignWidth { get; private set; }
    public double DeclinationDegreeWidth { get; private set; }  // todo remove
    public double LongitudeDegreeWidth { get; private set; } 
    public double DeclinationBarHeight { get; private set; }
    public double PositionMarkerSize { get; private set; }
    public double CelPointGlyphSize { get; private set; }
    
    public double PositionLineStrokeSize { get; private set; }
    public double PositionLineOpacity { get; } = 0.7;
    
    
    // Fonts and colors.
    public FontFamily DegreeTextsFontFamily { get; } = new ("Calibri");
    public FontFamily GlyphsFontFamily { get; } = new ("EnigmaAstrology");
    public double DegreeTextSize { get; private set; }
    
    public double DegreeTextOpacity = 1.0;
    public double SignGlyphSize { get; private set; }

    public Color CelPointGlyphColor { get; } = Colors.DarkSlateBlue;
    public Color SignGlyphColor { get; } = Colors.DarkSlateBlue;
    public Color DegreeTextColor { get; } = Colors.DarkSlateBlue;
   
    
    
    public DeclDiagramMetrics()
    {
        HeightFactor = 1.0;
        WidthFactor = 1.0;
        DefineSizes();
    }
    
    public void SetSizeFactors(double newHeightFactor, double newWidthFactor)
    {
        HeightFactor = newHeightFactor;
        WidthFactor = newWidthFactor;
        DefineSizes();
    }
    private void DefineSizes()
    {
        CanvasWidth = BASE_WIDTH * WidthFactor;
        CanvasHeight = BASE_HEIGHT * HeightFactor;
        DiagramOffsetLeft = DIAGRAM_OFFSET_LEFT_FACTOR * CanvasWidth;
        DiagramOffsetRight = DIAGRAM_OFFSET_RIGHT_FACTOR * CanvasWidth;
        DiagramHalfHeight = DIAGRAM_HALF_HEIGHT_FACTOR * CanvasHeight;
        DiagramWidth = DIAGRAM_WIDTH_FACTOR * CanvasWidth;
        DegreeSizeLarge = DEGREE_SIZE_LARGE_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        DegreeSizeSmall = DEGREE_SIZE_SMALL_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        DeclDegreeCharacterLeftOffset = DECL_DEGREE_CHARACTER_OFFSET_LEFT_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        DeclDegreeCharacterRightOffset = DECL_DEGREE_CHARACTER_OFFSET_RIGHT_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        DeclDegreeTopOffset = DECL_DEGREE_TOP_OFFSET_FACTOR * CanvasHeight;
        DeclDegreeBottomOffset = DECL_DEGREE_BOTTOM_OFFSET_FACTOR * CanvasHeight;
        DeclDegreeLeftOffset = DECL_DEGREE_LEFT_OFFSET_FACTOR * CanvasWidth;
        DeclDegreeRightOffset = DECL_DEGREE_RIGHT_OFFSET_FACTOR * CanvasWidth;
        LongDegreeTopOffset = LONG_DEGREE_TOP_OFFSET_FACTOR * CanvasHeight;
        LongDegreeBottomOffset = LONG_DEGREE_BOTTOM_OFFSET_FACTOR * CanvasHeight;
        SignWidth = SIGN_WIDTH_FACTOR * CanvasWidth;
        PositionMarkerSize = POSITION_MARKER_SIZE_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        DeclinationBarHeight = CanvasHeight - DeclDegreeTopOffset - DeclDegreeBottomOffset;
        DeclinationDegreeWidth = DiagramHalfHeight / DeclDegreesCount;
        LongitudeDegreeWidth = DiagramWidth / LONGITUDE_DEGREES_COUNT;
        PositionLineStrokeSize = 0.5 * Math.Max(HeightFactor, WidthFactor);
        if (PositionLineStrokeSize < 0.5) PositionLineStrokeSize = 0.5;
        if (PositionLineStrokeSize > 2.0) PositionLineStrokeSize = 2.0;
        
        // Fonts and colors
        DegreeTextSize = DEGREE_TEXT_SIZE_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        SignGlyphSize = SIGN_GLYPH_SIZE_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        CelPointGlyphSize = CELPOINT_GLYPH_SIZE_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        


    }

}