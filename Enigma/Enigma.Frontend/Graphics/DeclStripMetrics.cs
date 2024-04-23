// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Windows.Media;

namespace Enigma.Frontend.Ui.Graphics;

public class DeclStripMetrics
{
    private const double BASE_WIDTH = 360.0;
    private const double BASE_HEIGHT = 640.0;
    private const double DEGREE_TEXT_SIZE_FACTOR = 0.03;
    private const double CELPOINT_GLYPH_SIZE_FACTOR = 0.06;
    private const double BAR_HEIGHT_FACTOR = 0.94;
    private const double BAR_WIDTH_FACTOR = 0.12;
    private const double NORTH_SOUTH_BAR_FACTOR = 0.03;
    
    public double FullOpacity { get; }  = 1.0;

    private double HeightFactor { get; set; }
    private double WidthFactor { get; set; }
    public double CanvasWidth { get; private set; }
    public double CanvasHeight { get; private set; }
    public double BarHeight { get; private set; }
    public double BarWidth { get; private set; }
    public double BarOffsetLeft { get; private set; }
    public double BarOffsetRight { get; private set; }
    public double BarX { get; private set; }
    public double BarY { get; private set; }
    public double DegreeHeight { get; private set; }
    public double DegreesBottom { get; private set; }
    public double DegreeOffsetFromBarBorder { get; private set; }
    public double DegreeOffsetFromLine { get; private set; }
    public double NorthSouthBarHeight { get; private set; }
    public double NorthSouthBarX { get; private set; }
    public double NorthSouthBarY { get; private set; }
    public double PositionLineStrokeSize { get; private set; }
    public double CelPointNorthBaseXPos { get; private set; }
    public double CelPointSouthBaseXPos { get; private set; }
    public double CelPointMargin { get; private set; }
    public double LabelNorthXPos { get; private set; }
    public double LabelSouthXPos { get; private set; }
    public double LabelsYPos { get; private set; }

    
    public int DeclDegreesCount { get; set; } = 30;
    
    
    // Fonts and colors.
    public FontFamily DegreeTextsFontFamily { get; } = new ("Calibri");
    public FontFamily GlyphsFontFamily { get; } = new ("EnigmaAstrology");
    public double DegreeTextSize { get; private set; }
    public const double DEGREE_TEXT_OPACITY = 1.0;
    public double CelPointGlyphSize { get; private set; }
    public Color CelPointGlyphColor { get; } = Colors.DarkSlateBlue;
    public Color DegreeTextColor { get; } = Colors.DarkSlateBlue;
    
   public DeclStripMetrics()
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
      
        BarHeight = CanvasHeight * BAR_HEIGHT_FACTOR;
        BarWidth = CanvasWidth * BAR_WIDTH_FACTOR;
        BarOffsetLeft = (CanvasWidth - BarWidth) / 2.0;
        BarOffsetRight = BarOffsetLeft + BarWidth;
        BarX = BarOffsetLeft;
        BarY = CanvasHeight - BarHeight;
        BarOffsetLeft = (CanvasWidth - BarWidth) / 2.0;
        BarOffsetRight = BarOffsetLeft + BarWidth;
        NorthSouthBarHeight = CanvasHeight * NORTH_SOUTH_BAR_FACTOR;
        NorthSouthBarX = 0.0;
        NorthSouthBarY = CanvasHeight - NorthSouthBarHeight;
        DegreeHeight = CanvasWidth * 0.9 + NorthSouthBarHeight;
        DegreesBottom = NorthSouthBarHeight;
        DegreeHeight = (BarHeight - DegreesBottom) / DeclDegreesCount;
        DegreeOffsetFromLine = DegreeHeight * 0.2;
        DegreeOffsetFromBarBorder = BarWidth * 0.2;
        CelPointNorthBaseXPos = BarOffsetLeft * 0.9;
        CelPointSouthBaseXPos = BarOffsetRight * 1.1;
        CelPointMargin = BarOffsetLeft * 0.12;
        LabelNorthXPos = BarOffsetLeft * 0.8;
        LabelSouthXPos = BarOffsetRight * 1.02;
        LabelsYPos = CanvasHeight - NorthSouthBarHeight / 0.2;
        PositionLineStrokeSize = 0.75 * Math.Max(HeightFactor, WidthFactor);
        if (PositionLineStrokeSize < 0.75) PositionLineStrokeSize = 0.75;
        if (PositionLineStrokeSize > 2.0) PositionLineStrokeSize = 2.0;
        
        // Fonts
        DegreeTextSize = DEGREE_TEXT_SIZE_FACTOR * Math.Min(CanvasWidth, CanvasHeight);
        CelPointGlyphSize = CELPOINT_GLYPH_SIZE_FACTOR * Math.Min(CanvasWidth, CanvasHeight);

    }
    
}