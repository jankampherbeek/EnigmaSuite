// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.
// namespace Enigma.Frontend.Ui.Graphics;

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Enigma.Core.Calc;
using Color = System.Windows.Media.Color;

namespace Enigma.Frontend.Ui.Graphics;

/// <summary>Metrics used in drawing a declination diagram.</summary>
public class DeclDiagramMetrics
{
    // TODO make decl degrees count in metrics a variable
    private const int DECL_DEGREES_COUNT = 30;         // Counted for north or south only.
    private const int LONGITUDE_DEGREES_COUNT = 180;     // Counted for 6 signs only.
    private const double BASE_WIDTH = 700.0;
    private const double BASE_HEIGHT = 600.0;
    private const double BASE_LINE_DIAGRAM_OFFSET_TOP_FACTOR = 0.5;
    private const double BASE_LINE_DIAGRAM_OFFSET_BOTTOM_FACTOR = 0.5;
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
    public double SizeFactor { get; private set; }    
    public double CanvasWidth { get; private set; }
    public double CanvasHeight { get; private set; }
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
        
    
    // Fonts and colors.
    public FontFamily DegreeTextsFontFamily { get; } = new ("Calibri");
    public FontFamily GlyphsFontFamily { get; } = new ("EnigmaAstrology");
    public double DegreeTextSize { get; private set; }
    
    public double DegreeTextOpacity = 1.0;
    public double SignGlyphSize { get; private set; }

    public Color PositionLineColor = Colors.Orchid;
 //   public Brushes PositionCrossBrush = SolidColorBrush(Colors.Crimson);
    public Color CelPointColor { get; } = Colors.DarkSlateBlue;
    public Color CelPointConnectLineColor { get; } = Colors.DarkSlateBlue;
    public Color DegreeTextColor { get; } = Colors.DarkSlateBlue;
    
    
// ========================================================================
    
    private const double POLYGON_WIDTH_FACTOR_INITIAL = 1.0;
    private const double RELATIVE_SIZE_OF_VERTICAL_GRID = 0.778;
    private const double DIAGRAM_HEIGHT_FACTOR = 0.92;
    private const double DIAGRAM_VERTICAL_OFFSET_FACTOR = 0.04;
    private const double DIAGRAM_HORIZONTAL_OFFSET_FACTOR = 0.06;
    private const double DEGREE_VERTICAL_OFFSET_FACTOR = 0.04;
    private const double DEGREE_HORIZONTAL_OFFSET_FACTOR = 0.02;    
    
    private const double MARGIN_X_AXIS = 0.1;
    private const double MARGIN_Y_AXIS = 0.1;
    private const double RASTER_FACTOR_HOR_VERT = 2.0;

    private const double POSITION_TEXT_SIZE_INITIAL = 10.0;

    
    
    // Fixed values
    public static double MinDistance => 6.0;

    // Fonts

    
    public Color CelPointTextColor { get; } = Colors.DarkSlateBlue;





    public double DiagramVerticalOffset { get; private set; }
    public double DiagramHeight { get; private set; }
    public double DegreeHorizontalOffset { get; private set; }

    
    public double RasterCellSizeHorizontal { get; private set; }
    public double RasterCellSizeVertical { get; private set; }
    public double MinOobRegionSize { get; private set; }
    

    
    
    
    public double PolygonWidth { get; set; }
    
    
    
    public DeclDiagramMetrics()
    {
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
        CanvasWidth = BASE_WIDTH * SizeFactor;
        CanvasHeight = BASE_HEIGHT * SizeFactor;
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
        DeclinationDegreeWidth = DiagramHalfHeight / DECL_DEGREES_COUNT;
        LongitudeDegreeWidth = DiagramWidth / LONGITUDE_DEGREES_COUNT;
        
        // Fonts and colors
        DegreeTextSize = DEGREE_TEXT_SIZE_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        SignGlyphSize = SIGN_GLYPH_SIZE_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        CelPointGlyphSize = CELPOINT_GLYPH_SIZE_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        
        // ==============================================
        /*GridCanvasSizeHorizontal = BaseSize * SizeFactor;
        GridCanvasSizeVertical = GridCanvasSizeHorizontal * RELATIVE_SIZE_OF_VERTICAL_GRID;
        DiagramVerticalOffset = GridCanvasSizeVertical * DIAGRAM_VERTICAL_OFFSET_FACTOR;
        DiagramOffsetLeft = GridCanvasSizeHorizontal * DIAGRAM_HORIZONTAL_OFFSET_FACTOR;
        DiagramWidth = GridCanvasSizeHorizontal * DIAGRAM_WIDTH_FACTOR;
        DiagramHeight = GridCanvasSizeVertical * DIAGRAM_HEIGHT_FACTOR;
        DegreeHorizontalOffset = GridCanvasSizeHorizontal * DEGREE_HORIZONTAL_OFFSET_FACTOR;
        
        
        RasterCellSizeHorizontal = (POLYGON_WIDTH_FACTOR_INITIAL * SizeFactor * GridCanvasSizeHorizontal) / 180.0;
        RasterCellSizeVertical = RasterCellSizeHorizontal * RASTER_FACTOR_HOR_VERT;
        MinOobRegionSize = RasterCellSizeVertical / 0.5;
        DegreeTextSize = POSITION_TEXT_SIZE_INITIAL * (GridCanvasSizeHorizontal / 700.0);*/

        // show description
        // draw background in a color that indicates OOB
        // define x and y axes
        // draw axes
        // define polygon
        // draw polygon in a color that indicates in bound
        // plot glyphs for planets
        // show positions for planets in graph

    }

}