// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Enigma.Core.Calc.Util;
using Enigma.Domain.CalcVars;
using Enigma.Domain.Positional;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.PresentationFactories;
using Enigma.Frontend.UiDomain;

namespace Enigma.Frontend.Charts.Graphics;

public partial class ChartsWheel : Window
{
    private ChartsWheelController _controller;
    private ChartsWheelMetrics _metrics;
    private ISortedGraphicSolSysPointsFactory _sortedGraphicSolSysPointsFactory;
    private ISolarSystemPointSpecifications _solarSystemPointSpecifications;
    private IDoubleToDmsConversions _doubleToDmsConversions;

    private double _offsetAsc;
    private double _ascendant;
    private double _mc;
    private Point _centerPoint;
   

    public ChartsWheel(ChartsWheelController controller, 
                       ChartsWheelMetrics metrics, 
                       ISortedGraphicSolSysPointsFactory sortedGraphicsolSysPointFactory, 
                       ISolarSystemPointSpecifications solarSystemPointSpecifications,
                       IDoubleToDmsConversions doubleToDmsConversions)
    {
        InitializeComponent();
        _controller = controller;
        _metrics = metrics;
        _sortedGraphicSolSysPointsFactory = sortedGraphicsolSysPointFactory;
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
        _doubleToDmsConversions = doubleToDmsConversions;

    }

    public void DrawChart()
    {
        _ascendant = _controller.GetAscendantLongitude();
        _offsetAsc = _ascendant % 30.0;
        _mc = _controller.GetMcLongitude();
        _centerPoint = new Point(_metrics.GridSize / 2, _metrics.GridSize / 2);
        wheelCanvas.Children.Clear();
        DrawCircles();
        DrawSigns();
        DrawDegreeIndications();
        DrawCusps();
        DrawSolSysPoints();
    }


    private void DrawCircles()
    {
        Ellipse outerCircle = CreateCircle(_metrics.OuterCircle, 0.0, Colors.AliceBlue, Colors.White);
        Ellipse outerSignsCircle = CreateCircle(_metrics.OuterSignCircle, _metrics.StrokeSize, Colors.PaleTurquoise, Colors.CornflowerBlue);
        Ellipse outerHouseCircle = CreateCircle(_metrics.OuterHouseCircle, _metrics.StrokeSize, Colors.AntiqueWhite, Colors.CornflowerBlue);
        Ellipse outerAspectCircle = CreateCircle(_metrics.OuterAspectCircle, _metrics.StrokeSize, Colors.AliceBlue, Colors.CornflowerBlue);

        wheelCanvas.Children.Add(outerCircle);
        wheelCanvas.Children.Add(outerSignsCircle);
        wheelCanvas.Children.Add(outerHouseCircle);
        wheelCanvas.Children.Add(outerAspectCircle);
    }

    private void DrawSigns()
    {
        double angle = 0.0;
        double glyphAngle = 0.0;
        Point point1;
        Point point2;
        double hypothenusa1 = _metrics.OuterHouseCircle/2;
        double hypothenusa2 = _metrics.OuterSignCircle/2;
        double hypothenusa3 = _metrics.SignGlyphCircle/2;
        double fontSize = _metrics.SignGlyphSize;
        string[] glyphs = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "="};
        int indexFirstGlyph = (int)(_ascendant / 30.0 + 1);
        int glyphIndex = indexFirstGlyph;
        for (int i = 0; i < 12; i++)
        {

            angle = (i * 30 + _offsetAsc) + 90.0;
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;
            point1 = DefinePoint(angle, hypothenusa1);
            point2 = DefinePoint(angle, hypothenusa2);
            wheelCanvas.Children.Add(CreateLine(point1, point2, _metrics.StrokeSize, Colors.SlateBlue));

            glyphAngle = angle + 15.0;
            if (glyphAngle < 0.0) glyphAngle += 360.0;
            if (glyphAngle >= 360.0) glyphAngle -= 360.0;
            point1 = DefinePoint(glyphAngle, hypothenusa3);

            TextBlock glyph = new TextBlock();
            glyph.Text = glyphs[glyphIndex];
            glyph.Opacity = 0.7;
            glyphIndex++;
            if (glyphIndex > 11) glyphIndex = 0;

            glyph.FontFamily = new FontFamily("EnigmaAstrology");
            glyph.FontSize = fontSize;
            glyph.Foreground = new SolidColorBrush(Colors.SlateBlue);
         
            Canvas.SetLeft(glyph, point1.X - fontSize/3);
            Canvas.SetTop(glyph, point1.Y - fontSize/1.8);

            wheelCanvas.Children.Add(glyph);

        }
    }

    private void DrawDegreeIndications()
    {
        double angle = _offsetAsc;
        double hypothenusa1 = _metrics.Degrees5Circle / 2;
        double hypothenusa2 = _metrics.DegreesCircle / 2;
        double hypothenusa3 = _metrics.OuterHouseCircle / 2;

        Point point1;
        Point point2;
        for (int i = 0; i < 360; i++)
        {
            if (i % 5 == 0)
            {
                point1 = DefinePoint(angle, hypothenusa1); 
            }
            else
            {
                point1 = DefinePoint(angle, hypothenusa2);
            }
            point2 = DefinePoint(angle, hypothenusa3);
            wheelCanvas.Children.Add(CreateLine(point1, point2, 1, Colors.SlateBlue));
            angle += 1.0;
            if (angle >= 360.0) angle -= 360.0;
        }
    }

    private void DrawCusps()
    {
        double hypothenusa1 = _metrics.OuterAspectCircle / 2;
        double hypothenusa2 = _metrics.OuterHouseCircle / 2;
        double hypothenusa3 = _metrics.CuspTextCircle / 2;
        double strokeSizeSmall = _metrics.StrokeSize;
        double strokeSizeDouble = _metrics.StrokeSize * 2;
        Point point1;
        Point point2;
        double angle = 0.0;
        List<double> housePositions = _controller.GetHouseLongitudesCurrentChart();
        for (int i = 0; i < housePositions.Count; i++)
        {
            angle = housePositions[i] - _ascendant + 90.0;      // TODO check
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;
            point1 = DefinePoint(angle, hypothenusa1);
            point2 = DefinePoint(angle, hypothenusa2);
            double width = ((i % 3) == 0) ? strokeSizeDouble : strokeSizeSmall;
            Line cuspLine = CreateLine(point1, point2, width, Colors.Gray);
            cuspLine.Stroke.Opacity = 0.5;
            wheelCanvas.Children.Add(cuspLine);


            // show textual position

            TextBlock posText = new TextBlock();
            posText.Text = _doubleToDmsConversions.ConvertDoubleToLongInSignNoGlyph(housePositions[i]);
            posText.FontFamily = new FontFamily("Calibri");
            posText.FontSize = _metrics.PositionTextSize;
            posText.Foreground = new SolidColorBrush(Colors.SaddleBrown);

            RotateTransform rotateTransform = new RotateTransform();
            double rotateAngle = 0.0;
            double swapAngle = 0.0;
            double yOffset;     // TODO move to metrics
            double textOffsetDegrees;  
            if (angle <= 90.0 || angle > 270.0)
            {
                rotateAngle = angle - 90.0;
                yOffset = 0.0;
                textOffsetDegrees = 3.0;
            }
            else
            {
                rotateAngle = angle - 270.0;
                yOffset = -10.0;
                textOffsetDegrees = -3.0;
            }
            point1 = DefinePoint(angle + textOffsetDegrees, hypothenusa3 + yOffset);

            swapAngle = 90.0 - rotateAngle;
            rotateAngle = 180.0 + swapAngle;
            if (rotateAngle < 0.0) rotateAngle += 360.0;

            rotateTransform.Angle = rotateAngle;
            posText.RenderTransform = rotateTransform;
            Canvas.SetLeft(posText, point1.X);
            Canvas.SetTop(posText, point1.Y);
            wheelCanvas.Children.Add(posText);


        }
        angle = 90.0;
        hypothenusa1 = _metrics.OuterSignCircle / 2.0;
        hypothenusa2 = _metrics.OuterCircle / 2.0;
        point1 = DefinePoint(angle, hypothenusa1);
        point2 = DefinePoint(angle, hypothenusa2);
        Line ascLine = CreateLine(point1, point2, _metrics.StrokeSize * 2, Colors.Gray);
        ascLine.Stroke.Opacity = 0.5;
        wheelCanvas.Children.Add(ascLine);
        angle += 180.0;
        if (angle >= 360.0) angle -= 360.0;
        point1 = DefinePoint(angle, hypothenusa1);
        point2 = DefinePoint(angle, hypothenusa2);
        Line descLine = CreateLine(point1, point2, _metrics.StrokeSize * 2, Colors.Gray);
        descLine.Stroke.Opacity = 0.5;
        wheelCanvas.Children.Add(descLine);
        angle = _mc - _ascendant + 90.0;
        if (angle < 0.0) angle += 360.0;
        if (angle >= 360.0) angle -= 360.0;
        point1 = DefinePoint(angle, hypothenusa1);
        point2 = DefinePoint(angle, hypothenusa2);
        Line mcLine = CreateLine(point1, point2, _metrics.StrokeSize * 2, Colors.Gray);
        mcLine.Stroke.Opacity = 0.5;
        wheelCanvas.Children.Add(mcLine);
        angle += 180.0;
        if (angle >= 360.0) angle -= 360.0;
        point1 = DefinePoint(angle, hypothenusa1);
        point2 = DefinePoint(angle, hypothenusa2);
        Line icLine = CreateLine(point1, point2, _metrics.StrokeSize * 2, Colors.Gray);
        icLine.Stroke.Opacity = 0.5;
        wheelCanvas.Children.Add(icLine);
    }

    private void DrawSolSysPoints()
    {
        double minDistance = 6.0;
        List<FullSolSysPointPos> solSysPoints = _controller.GetSolSysPointsCurrentChart();
        List<GraphicSolSysPointPositions> graphicSolSysPointsPositions = _sortedGraphicSolSysPointsFactory.CreateSortedList(solSysPoints, _ascendant, minDistance);

        double hypothenusa1 = _metrics.SolSysPointGlyphCircle / 2;
        double hypothenusa2 = _metrics.OuterConnectionCircle / 2;
        double hypothenusa3 = _metrics.OuterAspectCircle / 2;
        double hypothenusa4 = _metrics.SolSysPointTextCircle / 2;
        double angle = 0.0;
        Point point1 = new Point(0, 0);
        Point point2 = new Point(0, 0);
        double fontSize = _metrics.SolSysPointGlyphSize;
        double offsetX = _metrics.GlyphXOffset;
        double offsetY = _metrics.GlyphYOffset;
        foreach (var graphPoint in graphicSolSysPointsPositions)
        {
            angle = graphPoint.PlotPos;
            point1 = DefinePoint(angle, hypothenusa1);

            TextBlock glyph = new TextBlock();
            glyph.Text = _solarSystemPointSpecifications.DetailsForPoint(graphPoint.SolSysPoint).DefaultGlyph;

            glyph.FontFamily = new FontFamily("EnigmaAstrology");
            glyph.FontSize = fontSize;
            glyph.Foreground = new SolidColorBrush(Colors.DarkSlateBlue);

            Canvas.SetLeft(glyph, point1.X - fontSize / 3);
            Canvas.SetTop(glyph, point1.Y - fontSize / 1.8);

            wheelCanvas.Children.Add(glyph);

            // Draw connectionLine
           
            point1 = DefinePoint(angle, hypothenusa2);
            angle = graphPoint.MundanePos;
            point2 = DefinePoint(angle, hypothenusa3);
            Line connectionLine = CreateLine(point1, point2, _metrics.ConnectLineSize, Colors.DarkSlateBlue);
            connectionLine.Stroke.Opacity = 0.25;
            wheelCanvas.Children.Add(connectionLine);

            // Show text for position
            angle = graphPoint.PlotPos;
            double hypothenusaEast = hypothenusa4 + 20.0;
            double hypothenusaWest = hypothenusa4 - 20.0;
            if (angle < 180.0) point1 = DefinePoint(angle, hypothenusaEast);
            else point1 = DefinePoint(angle, hypothenusaWest);

            TextBlock posText = new TextBlock();
            posText.Text = graphPoint.LongitudeText;
            posText.FontFamily = new FontFamily("Calibri");
            posText.FontSize = _metrics.PositionTextSize;
            posText.Foreground = new SolidColorBrush(Colors.DarkSlateBlue);

            RotateTransform rotateTransform = new RotateTransform();
            double rotateAngle = 0.0;
            double swapAngle = 0.0;
            if (graphPoint.PlotPos < 180.0)
            {
                rotateAngle = graphPoint.PlotPos - 90.0;
            }
            else
            {
                rotateAngle = graphPoint.PlotPos - 270.0;
            }
            swapAngle = 90.0 - rotateAngle;
            rotateAngle = 270.0 + swapAngle;
            if (rotateAngle < 0.0) rotateAngle += 360.0;

            rotateTransform.Angle = rotateAngle;
            posText.RenderTransform = rotateTransform;
            Canvas.SetLeft(posText, point1.X - offsetX );
            Canvas.SetTop(posText, point1.Y - offsetY);
            wheelCanvas.Children.Add(posText);
        }
    


    }



    private Point DefinePoint(double angle, double hypothenusa)
    {
        double x = _centerPoint.X - (Math.Sin(MathExtra.DegToRad(angle)) * hypothenusa);
        double y = _centerPoint.Y - (Math.Cos(MathExtra.DegToRad(angle)) * hypothenusa);
        return new Point(x, y);

    }

    private Line CreateLine(Point point1, Point point2, double lineWidth, Color lineColor)
    {
        Line line = new Line();
        line.X1 = point1.X;
        line.X2 = point2.X;
        line.Y1 = point1.Y;
        line.Y2 = point2.Y;
        line.Stroke = new SolidColorBrush(lineColor);
        line.StrokeThickness = lineWidth;
        return line;
    }


    private Ellipse CreateCircle(double circleSize, double strokeThickness, Color fillColor, Color strokeColor)
    {
        Ellipse circle = new Ellipse();
        
        circle.Margin = new Thickness((350 * _metrics.SizeFactor) - circleSize/2);
        circle.Width = circleSize;
        circle.Height = circleSize;
        circle.StrokeThickness = strokeThickness;
        circle.Fill = new SolidColorBrush(fillColor);
        circle.Stroke = new SolidColorBrush(strokeColor);
        return circle;
    }


    private void wheelGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var ah = ActualHeight;
        var aw = ActualWidth;
        var h = Height;
        var w = Width;
        double minSize = Math.Min(h, w);
        _metrics.SetSizeFactor(minSize/740.0);
        wheelCanvas.Height = _metrics.GridSize;
        wheelCanvas.Width = _metrics.GridSize;


        DrawChart();
    }

}
