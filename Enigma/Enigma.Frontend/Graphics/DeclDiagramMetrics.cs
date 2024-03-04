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

    private const double POLYGON_WIDTH_FACTOR_INITIAL = 0.80;
    private const double MARGIN_X_AXIS = 0.1;
    private const double MARGIN_Y_AXIS = 0.1;
    private const double RASTER_FACTOR_HOR_VERT = 4.0;
    
    
    // Fixed values
    public static double MinDistance => 6.0;

    // Fonts
    public FontFamily GlyphsFontFamily { get; } = new ("EnigmaAstrology");
    public FontFamily PositionTextsFontFamily { get; } = new ("Calibri");
    public Color CelPointConnectLineColor { get; } = Colors.DarkSlateBlue;
    public Color CelPointTextColor { get; } = Colors.DarkSlateBlue;
    
    public double BaseSize { get; } = 700.0;
    public double SizeFactor { get; private set; }
    public double GridSize { get; private set; }
    public double RasterCellSizeHorizontal { get; private set; }
    public double RasterCellSizeVertical { get; private set; }
    public double MinOobRegionSize { get; private set; }
    
    public double PolygonWidth { get; set; }
    
    
    
    public DeclDiagramMetrics()
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

        GridSize = BaseSize * SizeFactor;
        RasterCellSizeHorizontal = (POLYGON_WIDTH_FACTOR_INITIAL * SizeFactor * GridSize) / 180.0;
        RasterCellSizeVertical = RasterCellSizeHorizontal * RASTER_FACTOR_HOR_VERT;
        MinOobRegionSize = RasterCellSizeVertical / 0.5;


        // show description
        // draw background in a color that indicates OOB
        // define x and y axes
        // draw axes
        // define polygon
        // draw polygon in a color that indicates in bound
        // plot glyphs for planets
        // show positions for planets in graph

    }

    public Tuple<List<Point>, List<Point>> GetPolygonPoints(double obliquity)
    {
        List<Point> PolygonPointsNorth = new List<Point>();
        List<Point> PolygonPointsSouth = new List<Point>();
        double degreeSize = RasterCellSizeHorizontal;
        for (int i = 0; i <= 180; i++)
        {
            double decl = Math.Asin(Math.Cos(MathExtra.DegToRad(i)) * Math.Cos(MathExtra.DegToRad(obliquity)));
            double offset = decl * RASTER_FACTOR_HOR_VERT;
            PolygonPointsNorth.Add(new Point(i * degreeSize, offset * degreeSize));
            PolygonPointsSouth.Add(new Point(i * degreeSize, -offset * degreeSize));
        }
        return new Tuple<List<Point>, List<Point>>(PolygonPointsNorth, PolygonPointsSouth);
    }
    
}