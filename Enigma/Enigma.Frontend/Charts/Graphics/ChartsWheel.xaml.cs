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

namespace Enigma.Frontend.Charts.Graphics;

public partial class ChartsWheel : Window
{
    private ChartsWheelController _controller;
    private ChartsWheelMetrics _metrics;

    private double _ascOffset;
   

    public ChartsWheel(ChartsWheelController controller, ChartsWheelMetrics metrics)
    {
        InitializeComponent();
        _controller = controller;
        _metrics = metrics;

    }

    public void DrawChart()
    {
        wheelCanvas.Children.Clear();
        DrawCircles();
        DrawSigns();
    }


    private void DrawCircles()
    {
        

        Ellipse outerCircle = CreateCircle(_metrics.OuterCircle, 0.0, Colors.AliceBlue, Colors.White);
        Ellipse outerSignsCircle = CreateCircle(_metrics.OuterSignCircle, _metrics.StrokeSize, Colors.PaleTurquoise, Colors.SlateBlue);
        Ellipse outerHouseCircle = CreateCircle(_metrics.OuterHouseCircle, _metrics.StrokeSize, Colors.AntiqueWhite, Colors.SlateBlue);
        Ellipse outerAspectCircle = CreateCircle(_metrics.OuterAspectCircle, _metrics.StrokeSize, Colors.White, Colors.SlateBlue);

   /*     Ellipse innerCircle = new Ellipse();
        innerCircle.Width = 400;
        innerCircle.Height = 400;
        innerCircle.StrokeThickness = 2;
        innerCircle.Stroke = new SolidColorBrush(Colors.Red);

        Line testLine = new Line();
        testLine.X1 = 20;
        testLine.Y1 = 20;
        testLine.X2 = 300;
        testLine.Y2 = 200;
        testLine.Stroke = Brushes.Red;
        testLine.StrokeThickness = 2;
   */
        wheelCanvas.Children.Add(outerCircle);
        wheelCanvas.Children.Add(outerSignsCircle);
        wheelCanvas.Children.Add(outerHouseCircle);
        wheelCanvas.Children.Add(outerAspectCircle);


    }

    private void DrawSigns()
    {
        double ascendant = _controller.GetAscendantLongitude();
        double offsetAsc = ascendant % 30.0;
        double angle = 0.0;
        double glyphAngle = 0.0;
        Point point1;
        Point point2;
        Point centerPoint = new Point(_metrics.GridSize/2, _metrics.GridSize/2);
        double hypothenusa1 = _metrics.OuterHouseCircle/2;
        double hypothenusa2 = _metrics.OuterSignCircle/2;
        double hypothenusa3 = _metrics.SignGlyphCircle/2;
        double fontSize = _metrics.SignGlyphSize;
        string[] glyphs = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "="};
        int indexFirstGlyph = (int)(ascendant / 30.0);
        int glyphIndex = indexFirstGlyph;
        for (int i = 0; i < 12; i++)
        {

            angle = (i * 30 + offsetAsc) + 90.0;
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;
            point1 = DefinePoint(centerPoint, angle, hypothenusa1);
            point2 = DefinePoint(centerPoint, angle, hypothenusa2);
            wheelCanvas.Children.Add(CreateLine(point1, point2, _metrics.StrokeSize, Colors.SlateBlue));

            glyphAngle = angle + 15.0;
            if (glyphAngle < 0.0) glyphAngle += 360.0;
            if (glyphAngle >= 360.0) glyphAngle -= 360.0;
            point1 = DefinePoint(centerPoint, glyphAngle, hypothenusa3);

            TextBlock glyph = new TextBlock();
            glyph.Text = glyphs[glyphIndex];
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

    private Point DefinePoint(Point centerPoint, double angle, double hypothenusa)
    {
        double x = centerPoint.X - (Math.Sin(MathExtra.DegToRad(angle)) * hypothenusa);
        double y = centerPoint.Y - (Math.Cos(MathExtra.DegToRad(angle)) * hypothenusa);
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
