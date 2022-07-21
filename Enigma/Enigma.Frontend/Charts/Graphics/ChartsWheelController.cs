// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;
using Enigma.Domain.Positional;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.PresentationFactories;
using Enigma.Frontend.State;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Enigma.Frontend.Charts.Graphics;

public class ChartsWheelController
{
    public double CanvasSize{ get; private set; }

    private readonly ChartsWheelMetrics _metrics;
    private readonly DataVault _dataVault;
    private IDoubleToDmsConversions _doubleToDmsConversions;
    private ISortedGraphicSolSysPointsFactory _sortedGraphicSolSysPointsFactory;
    private ISolarSystemPointSpecifications _solarSystemPointSpecifications;
    private CalculatedChart? _currentChart;

    public ChartsWheelController(ChartsWheelMetrics metrics, 
        IDoubleToDmsConversions doubleToDmsConversions, 
        ISortedGraphicSolSysPointsFactory sortedGraphicSolSysPointsFactory,
        ISolarSystemPointSpecifications solarSystemPointSpecifications)
    {
        _dataVault = DataVault.Instance;
        _doubleToDmsConversions = doubleToDmsConversions;
        _sortedGraphicSolSysPointsFactory = sortedGraphicSolSysPointsFactory;
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
        _metrics = metrics;
    }

    public List<Ellipse> GetAllCircles()
    {
        DimCircle dimCircle = new(_metrics.BaseSize, _metrics.SizeFactor);
        List<Ellipse> allCircles = new()
        {
            dimCircle.CreateCircle(_metrics.OuterRadius, 0.0, Colors.AliceBlue, Colors.White),
            dimCircle.CreateCircle(_metrics.OuterSignRadius, _metrics.StrokeSize, Colors.PaleTurquoise, Colors.CornflowerBlue),
            dimCircle.CreateCircle(_metrics.OuterHouseRadius, _metrics.StrokeSize, Colors.AntiqueWhite, Colors.CornflowerBlue),
            dimCircle.CreateCircle(_metrics.OuterAspectRadius, _metrics.StrokeSize, Colors.AliceBlue, Colors.CornflowerBlue)
        };
        return allCircles;
    }

    public List<Line> GetAllDegreeIndications()
    {
        double offsetAsc = 30.0 - (GetAscendantLongitude() % 30.0);
        Point centerPoint = new(_metrics.GridSize / 2, _metrics.GridSize / 2);
        DimDegreeIndications dimDegreeIndications = new(centerPoint, offsetAsc, _metrics.OuterHouseCircle/2, _metrics.DegreesCircle/2, _metrics.Degrees5Circle/2);
        return dimDegreeIndications.CreateDegreeIndications();
    }

    public List<Line> CreateSignSeparators()
    {
        List<Line> allSeparators = new();
        double offsetAsc = 30.0 - (GetAscendantLongitude() % 30.0);
        Point centerPoint = new(_metrics.GridSize / 2, _metrics.GridSize / 2);
        DimLine dimLine = new();
        DimPoint dimPoint = new(centerPoint);
        Point point1;
        Point point2;
        double angle;
        double hypothenusa1 = _metrics.OuterHouseCircle / 2;
        double hypothenusa2 = _metrics.OuterSignCircle / 2;
        for (int i = 0; i < 12; i++)
        {
            angle = (i * 30 + offsetAsc) + 90.0;
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;
            point1 = dimPoint.CreatePoint(angle, hypothenusa1);
            point2 = dimPoint.CreatePoint(angle, hypothenusa2);
            allSeparators.Add(dimLine.CreateLine(point1, point2, _metrics.StrokeSize, Colors.SlateBlue, 1.0));
        }
        return allSeparators;
    }

    public List<TextBlock> CreateSignGlyphs()
    {
        double angle = 0.0;
        Point point1;
        double offsetAsc = 30.0 - (GetAscendantLongitude() % 30.0);
        Point centerPoint = new(_metrics.GridSize / 2, _metrics.GridSize / 2);
        double hypothenusa = _metrics.SignGlyphCircle / 2;
        double fontSize = _metrics.SignGlyphSize;
        string[] glyphs = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };
        int indexFirstGlyph = (int)(GetAscendantLongitude() / 30.0 + 1);
        int glyphIndex = indexFirstGlyph;
        DimPoint dimPoint = new(centerPoint);
        DimTextBlock dimTextBlock = new DimTextBlock(new FontFamily("EnigmaAstrology"), fontSize, 0.7, Colors.SlateBlue);
        List<TextBlock> glyphList = new();
        for (int i = 0; i < 12; i++)
        {
            angle = (i * 30) + offsetAsc + 90.0 + 15.0;
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;
            point1 = dimPoint.CreatePoint(angle, hypothenusa);
            glyphList.Add(dimTextBlock.CreateTextBlock(glyphs[glyphIndex], point1.X - fontSize / 3, point1.Y - fontSize / 1.8));
            glyphIndex++;
            if (glyphIndex > 11) glyphIndex = 0;
        }
        return glyphList;
    }

    public List<Line> CreateCuspLines()
    {
        List<Line> cuspLines = new();
        Point centerPoint = new(_metrics.GridSize / 2, _metrics.GridSize / 2);
        DimPoint dimPoint = new(centerPoint);
        double hypothenusa1 = _metrics.OuterAspectRadius;
        double hypothenusa2 = _metrics.OuterHouseRadius;
        double strokeSizeSmall = _metrics.StrokeSize;
        double strokeSizeDouble = _metrics.StrokeSize * 2;
        Point point1;
        Point point2;
        double angle = 0.0;
        DimLine dimLine = new();
        List<double> housePositions = GetHouseLongitudesCurrentChart();
        for (int i = 0; i < housePositions.Count; i++)
        {
            angle = housePositions[i] - GetAscendantLongitude() + 90.0;
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;
            point1 = dimPoint.CreatePoint(angle, hypothenusa1);
            point2 = dimPoint.CreatePoint(angle, hypothenusa2);
            double width = ((i % 3) == 0) ? strokeSizeDouble : strokeSizeSmall;
            Line cuspLine = dimLine.CreateLine(point1, point2, width, Colors.Gray, 0.5);
            cuspLines.Add(cuspLine);
        }
        return cuspLines;
    }

    public List<TextBlock> CreateCuspTexts()
    {
        List<TextBlock> cuspTexts = new();
        Point centerPoint = new(_metrics.GridSize / 2, _metrics.GridSize / 2);
        DimPoint dimPoint = new(centerPoint);
        DimTextBlock cuspsDimTextBlock = new DimTextBlock(new FontFamily("Calibri"), _metrics.PositionTextSize, 1.0, Colors.SaddleBrown);
        double hypothenusa3 = _metrics.CuspTextCircle / 2;
        Point point1;
        double angle = 0.0;
       
        List<double> housePositions = GetHouseLongitudesCurrentChart();
        for (int i = 0; i < housePositions.Count; i++)
        {
            angle = housePositions[i] - GetAscendantLongitude() + 90.0;
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;
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
            point1 = dimPoint.CreatePoint(angle + textOffsetDegrees, hypothenusa3 + yOffset);
            swapAngle = 90.0 - rotateAngle;
            rotateAngle = 180.0 + swapAngle;
            if (rotateAngle < 0.0) rotateAngle += 360.0;

            rotateTransform.Angle = rotateAngle;


            string text = _doubleToDmsConversions.ConvertDoubleToLongInSignNoGlyph(housePositions[i]);
            TextBlock posText = cuspsDimTextBlock.CreateTextBlock(text, point1.X, point1.Y, rotateTransform);
            cuspTexts.Add(posText);
        }
        return cuspTexts;
    }

    public List<Line> CreateCardinalLines()
    {
        List<Line> cardinalLines = new();
        Point centerPoint = new(_metrics.GridSize / 2, _metrics.GridSize / 2);
        DimPoint dimPoint = new(centerPoint);
        double hypothenusa1 = _metrics.OuterAspectRadius;
        double hypothenusa2 = _metrics.OuterHouseRadius;
        Point point1;
        Point point2;
        double angle = 0.0;
        DimLine dimLine = new();

        angle = 90.0;
        hypothenusa1 = _metrics.OuterSignRadius;
        hypothenusa2 = _metrics.OuterRadius;
        point1 = dimPoint.CreatePoint(angle, hypothenusa1);
        point2 = dimPoint.CreatePoint(angle, hypothenusa2);
        Line ascLine = dimLine.CreateLine(point1, point2, _metrics.StrokeSize * 2, Colors.Gray, 0.5);
        cardinalLines.Add(ascLine);
        angle += 180.0;
        if (angle >= 360.0) angle -= 360.0;
        point1 = dimPoint.CreatePoint(angle, hypothenusa1);
        point2 = dimPoint.CreatePoint(angle, hypothenusa2);
        Line descLine = dimLine.CreateLine(point1, point2, _metrics.StrokeSize * 2, Colors.Gray, 0.5);
        cardinalLines.Add(descLine);
        angle = GetMcLongitude() - GetAscendantLongitude() + 90.0;
        if (angle < 0.0) angle += 360.0;
        if (angle >= 360.0) angle -= 360.0;
        point1 = dimPoint.CreatePoint(angle, hypothenusa1);
        point2 = dimPoint.CreatePoint(angle, hypothenusa2);
        Line mcLine = dimLine.CreateLine(point1, point2, _metrics.StrokeSize * 2, Colors.Gray, 0.5);
        cardinalLines.Add(mcLine);
        angle += 180.0;
        if (angle >= 360.0) angle -= 360.0;
        point1 = dimPoint.CreatePoint(angle, hypothenusa1);
        point2 = dimPoint.CreatePoint(angle, hypothenusa2);
        Line icLine = dimLine.CreateLine(point1, point2, _metrics.StrokeSize * 2, Colors.Gray, 0.5);
        cardinalLines.Add(icLine);
        return cardinalLines;
    }

    public List<TextBlock> CreateSolSysPointGlyphs()
    {
        List<TextBlock> glyphs = new();
        Point centerPoint = new(_metrics.GridSize / 2, _metrics.GridSize / 2);
        double minDistance = 6.0;  // TODO move to metrics
        List<FullSolSysPointPos> solSysPoints = GetSolSysPointsCurrentChart();
        List<GraphicSolSysPointPositions> graphicSolSysPointsPositions = _sortedGraphicSolSysPointsFactory.CreateSortedList(solSysPoints, GetAscendantLongitude(), minDistance);
        DimPoint dimPoint = new(centerPoint);
        double hypothenusa1 = _metrics.SolSysPointGlyphCircle / 2;
        double angle = 0.0;
        Point point1;
        double fontSize = _metrics.SolSysPointGlyphSize;
        foreach (var graphPoint in graphicSolSysPointsPositions)
        {
            angle = graphPoint.PlotPos;
            point1 = dimPoint.CreatePoint(angle, hypothenusa1);
            TextBlock glyph = new TextBlock();
            glyph.Text = _solarSystemPointSpecifications.DetailsForPoint(graphPoint.SolSysPoint).DefaultGlyph;
            glyph.FontFamily = new FontFamily("EnigmaAstrology");
            glyph.FontSize = fontSize;
            glyph.Foreground = new SolidColorBrush(Colors.DarkSlateBlue);

            Canvas.SetLeft(glyph, point1.X - fontSize / 3);
            Canvas.SetTop(glyph, point1.Y - fontSize / 1.8);
            glyphs.Add(glyph);
        }
        return glyphs;
    }

    public List<Line> CreateSolSysPointConnectLines()
    {
        List<Line> connectLines = new();
        Point centerPoint = new(_metrics.GridSize / 2, _metrics.GridSize / 2);
        double minDistance = 6.0;  // TODO move to metrics
        List<FullSolSysPointPos> solSysPoints = GetSolSysPointsCurrentChart();
        List<GraphicSolSysPointPositions> graphicSolSysPointsPositions = _sortedGraphicSolSysPointsFactory.CreateSortedList(solSysPoints, GetAscendantLongitude(), minDistance);
        DimLine dimLine = new();
        DimPoint dimPoint = new(centerPoint);
        double hypothenusa2 = _metrics.OuterConnectionCircle / 2;
        double hypothenusa3 = _metrics.OuterAspectRadius;
        double angle = 0.0;
        Point point1;
        Point point2;
        foreach (var graphPoint in graphicSolSysPointsPositions)
        {
            angle = graphPoint.PlotPos;
            point1 = dimPoint.CreatePoint(angle, hypothenusa2);
            angle = graphPoint.MundanePos;
            point2 = dimPoint.CreatePoint(angle, hypothenusa3);
            Line connectionLine = dimLine.CreateLine(point1, point2, _metrics.ConnectLineSize, Colors.DarkSlateBlue, 0.25);
            connectLines.Add(connectionLine);
        }
        return connectLines;
    }

    public List<TextBlock> CreateSolSysPointTexts()
    {
        List<TextBlock> texts = new();
        Point centerPoint = new(_metrics.GridSize / 2, _metrics.GridSize / 2);
        double minDistance = 6.0;
        List<FullSolSysPointPos> solSysPoints = GetSolSysPointsCurrentChart();
        List<GraphicSolSysPointPositions> graphicSolSysPointsPositions = _sortedGraphicSolSysPointsFactory.CreateSortedList(solSysPoints, GetAscendantLongitude(), minDistance);
        DimPoint dimPoint = new(centerPoint);
        double hypothenusa4 = _metrics.SolSysPointTextCircle / 2;
        double angle = 0.0;
        Point point1;
        double offsetX = _metrics.GlyphXOffset;
        double offsetY = _metrics.GlyphYOffset;
        foreach (var graphPoint in graphicSolSysPointsPositions)
        {
            angle = graphPoint.PlotPos;
            double hypothenusaEast = hypothenusa4 + 20.0;
            double hypothenusaWest = hypothenusa4 - 20.0;

            if (angle < 180.0) point1 = dimPoint.CreatePoint(angle, hypothenusaEast);
            else point1 = dimPoint.CreatePoint(angle, hypothenusaWest);
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
            Canvas.SetLeft(posText, point1.X - offsetX);
            Canvas.SetTop(posText, point1.Y - offsetY);
            texts.Add(posText);
        }
        return texts;
    }


    public double GetAscendantLongitude()
    {
        double ascLong = -double.MaxValue;
        _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null) 
        {
            ascLong = _currentChart.FullHousePositions.Ascendant.Longitude;
        }
        return ascLong;
    }

    public double GetMcLongitude()
    {
        double mcLong = -double.MaxValue;
        _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null)
        {
            mcLong = _currentChart.FullHousePositions.Mc.Longitude;
        }
        return mcLong;
    }


    public List<double> GetHouseLongitudesCurrentChart()
    {
        List<double> longitudes = new List<double>();
        _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null)
        {
            foreach (var cusp in _currentChart.FullHousePositions.Cusps)
            {
                longitudes.Add(cusp.Longitude);
            }

        }
        return longitudes;
    }

    public List<FullSolSysPointPos> GetSolSysPointsCurrentChart()
    {
        _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null)
        {
            return _currentChart.SolSysPointPositions;
        }
        else
        {
            return new List<FullSolSysPointPos>();
        }   
    }

    public void Resize(double minSize)
    {
        _metrics.SetSizeFactor(minSize / 740.0);
        CanvasSize = _metrics.GridSize;
    }



}