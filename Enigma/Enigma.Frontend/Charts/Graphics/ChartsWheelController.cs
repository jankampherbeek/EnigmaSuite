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
    private Point _centerPoint;

    private readonly ChartsWheelMetrics _metrics;
    private readonly DataVault _dataVault;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly ISortedGraphicSolSysPointsFactory _sortedGraphicSolSysPointsFactory;
    private readonly ISolarSystemPointSpecifications _solarSystemPointSpecifications;
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
        DimDegreeIndications dimDegreeIndications = new(_centerPoint, offsetAsc, _metrics.OuterHouseRadius, _metrics.DegreesRadius, _metrics.Degrees5Radius);
        return dimDegreeIndications.CreateDegreeIndications();
    }

    public List<Line> CreateSignSeparators()
    {
        List<Line> allSeparators = new();
        double offsetAsc = 30.0 - (GetAscendantLongitude() % 30.0);
        DimLine dimLine = new();
        DimPoint dimPoint = new(_centerPoint);
        Point point1;
        Point point2;
        double angle;
        double hypothenusa1 = _metrics.OuterHouseRadius;
        double hypothenusa2 = _metrics.OuterSignRadius;
        for (int i = 0; i < 12; i++)
        {
            angle = InRange360(i * 30 + offsetAsc) + 90.0;
            point1 = dimPoint.CreatePoint(angle, hypothenusa1);
            point2 = dimPoint.CreatePoint(angle, hypothenusa2);
            allSeparators.Add(dimLine.CreateLine(point1, point2, _metrics.StrokeSize, Colors.SlateBlue, 1.0));
        }
        return allSeparators;
    }

    public List<TextBlock> CreateSignGlyphs()
    {
        Point point1;
        double offsetAsc = 30.0 - (GetAscendantLongitude() % 30.0);
        double hypothenusa = _metrics.SignGlyphRadius;
        double fontSize = _metrics.SignGlyphSize;
        string[] glyphs = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };
        int indexFirstGlyph = (int)(GetAscendantLongitude() / 30.0 + 1);
        int glyphIndex = indexFirstGlyph;
        DimPoint dimPoint = new(_centerPoint);
        DimTextBlock dimTextBlock = new(_metrics.GlyphsFontFamily, fontSize, 0.7, Colors.SlateBlue);
        List<TextBlock> glyphList = new();
        for (int i = 0; i < 12; i++)
        {
            double angle = InRange360((i * 30) + offsetAsc + 90.0 + 15.0);
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
        List<double> housePositions = GetHouseLongitudesCurrentChart();
        for (int i = 0; i < housePositions.Count; i++)
        {
            double angle = InRange360(housePositions[i] - GetAscendantLongitude() + 90.0);
            double width = ((i % 3) == 0) ? _metrics.StrokeSizeDouble : _metrics.StrokeSize;
            cuspLines.Add(CreateSingleCuspLine(angle, _metrics.OuterAspectRadius, _metrics.OuterHouseRadius, width));
        }
        return cuspLines;
    }

    public List<Line> CreateCardinalLines()
    {
        List<Line> cardinalLines = new();
        double angle = 90.0;
        double hypothenusa1 = _metrics.OuterSignRadius;
        double hypothenusa2 = _metrics.OuterRadius;
        cardinalLines.Add(CreateSingleCuspLine(angle, hypothenusa1, hypothenusa2, _metrics.StrokeSizeDouble));
        angle = InRange360(angle + 180.0);
        cardinalLines.Add(CreateSingleCuspLine(angle, hypothenusa1, hypothenusa2, _metrics.StrokeSizeDouble));
        angle = InRange360(GetMcLongitude() - GetAscendantLongitude() + 90.0);
        cardinalLines.Add(CreateSingleCuspLine(angle, hypothenusa1, hypothenusa2, _metrics.StrokeSizeDouble));
        angle = InRange360(angle + 180.0);
        cardinalLines.Add(CreateSingleCuspLine(angle, hypothenusa1, hypothenusa2, _metrics.StrokeSizeDouble));
        return cardinalLines;
    }

    private Line CreateSingleCuspLine(double angle, double hypothenusa1, double hypothenusa2, double strokeSize)
    {
        DimPoint dimPoint = new(_centerPoint);
        DimLine dimLine = new();
        Point point1 = dimPoint.CreatePoint(angle, hypothenusa1);
        Point point2 = dimPoint.CreatePoint(angle, hypothenusa2);
        return dimLine.CreateLine(point1, point2, strokeSize, _metrics.CuspLineColor, _metrics.CuspLineOpacity);
    }

    public List<TextBlock> CreateCuspTexts()
    {
        List<TextBlock> cuspTexts = new();
        DimPoint dimPoint = new(_centerPoint);
        DimTextBlock cuspsDimTextBlock = new(_metrics.PositionTextsFontFamily, _metrics.PositionTextSize, _metrics.CuspTextOpacity, _metrics.CuspTextColor);     
        List<double> housePositions = GetHouseLongitudesCurrentChart();
        for (int i = 0; i < housePositions.Count; i++)
        {
            double angle = InRange360(housePositions[i] - GetAscendantLongitude() + 90.0);
            RotateTransform rotateTransform = new();
            double rotateAngle;
            double swapAngle;
            double yOffset;    
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
            Point point1 = dimPoint.CreatePoint(angle + textOffsetDegrees, _metrics.CuspTextRadius + yOffset);
            swapAngle = 90.0 - rotateAngle;
            rotateTransform.Angle = InRange360(180.0 + swapAngle); 
            string text = _doubleToDmsConversions.ConvertDoubleToLongInSignNoGlyph(housePositions[i]);
            TextBlock posText = cuspsDimTextBlock.CreateTextBlock(text, point1.X, point1.Y, rotateTransform);
            cuspTexts.Add(posText);
        }
        return cuspTexts;
    }

 

    public List<TextBlock> CreateSolSysPointGlyphs()
    {
        List<TextBlock> glyphs = new();

        List<FullSolSysPointPos> solSysPoints = GetSolSysPointsCurrentChart();
        List<GraphicSolSysPointPositions> graphicSolSysPointsPositions = _sortedGraphicSolSysPointsFactory.CreateSortedList(solSysPoints, GetAscendantLongitude(), _metrics.MinDistance);
        DimPoint dimPoint = new(_centerPoint);
        double fontSize = _metrics.SolSysPointGlyphSize;
        foreach (var graphPoint in graphicSolSysPointsPositions)
        {
            double angle = graphPoint.PlotPos;
            Point point1 = dimPoint.CreatePoint(angle, _metrics.SolSysPointGlyphRadius);
            TextBlock glyph = new()
            {
                Text = _solarSystemPointSpecifications.DetailsForPoint(graphPoint.SolSysPoint).DefaultGlyph,
                FontFamily = _metrics.GlyphsFontFamily,
                FontSize = fontSize,
                Foreground = new SolidColorBrush(_metrics.SolSysPointColor)
            };

            Canvas.SetLeft(glyph, point1.X - fontSize / 3);
            Canvas.SetTop(glyph, point1.Y - fontSize / 1.8);
            glyphs.Add(glyph);
        }
        return glyphs;
    }

    public List<Line> CreateSolSysPointConnectLines()
    {
        List<Line> connectLines = new();
        List<FullSolSysPointPos> solSysPoints = GetSolSysPointsCurrentChart();
        List<GraphicSolSysPointPositions> graphicSolSysPointsPositions = _sortedGraphicSolSysPointsFactory.CreateSortedList(solSysPoints, GetAscendantLongitude(), _metrics.MinDistance);
        DimLine dimLine = new();
        DimPoint dimPoint = new(_centerPoint);
        foreach (var graphPoint in graphicSolSysPointsPositions)
        {
            Point point1 = dimPoint.CreatePoint(graphPoint.PlotPos, _metrics.OuterConnectionRadius);
            Point point2 = dimPoint.CreatePoint(graphPoint.MundanePos, _metrics.OuterAspectRadius);
            Line connectionLine = dimLine.CreateLine(point1, point2, _metrics.ConnectLineSize, _metrics.SolSysPointConnectLineColor, _metrics.SolSysPointConnectLineOpacity);
            connectLines.Add(connectionLine);
        }
        return connectLines;
    }

    public List<TextBlock> CreateSolSysPointTexts()
    {
        List<TextBlock> texts = new();
        List<FullSolSysPointPos> solSysPoints = GetSolSysPointsCurrentChart();
        List<GraphicSolSysPointPositions> graphicSolSysPointsPositions = _sortedGraphicSolSysPointsFactory.CreateSortedList(solSysPoints, GetAscendantLongitude(), _metrics.MinDistance);
        DimPoint dimPoint = new(_centerPoint);
        foreach (var graphPoint in graphicSolSysPointsPositions)
        {
            double angle = graphPoint.PlotPos;
            Point point1 = angle < 180.0 ? dimPoint.CreatePoint(angle, _metrics.SolSysPointTextRadius + 20.0) : dimPoint.CreatePoint(angle, _metrics.SolSysPointTextRadius - 20.0);
            TextBlock posText = new()
            {
                Text = graphPoint.LongitudeText,
                FontFamily = _metrics.PositionTextsFontFamily,
                FontSize = _metrics.PositionTextSize,
                Foreground = new SolidColorBrush(_metrics.SolSysPointTextColor)
            };
            RotateTransform rotateTransform = new();
            double rotateAngle = graphPoint.PlotPos < 180.0 ? graphPoint.PlotPos - 90.0 : graphPoint.PlotPos - 270.0;
            double swapAngle = 90.0 - rotateAngle;
            rotateTransform.Angle = InRange360(270.0 + swapAngle);
            posText.RenderTransform = rotateTransform;
            Canvas.SetLeft(posText, point1.X - _metrics.GlyphXOffset);
            Canvas.SetTop(posText, point1.Y - _metrics.GlyphYOffset);
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
        List<double> longitudes = new();
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
        _centerPoint = new(_metrics.GridSize / 2, _metrics.GridSize / 2);
    }

    private double InRange360(double angle)
    {
        double angleInRange = angle;
        while (angleInRange < 0.0)
        {
            angleInRange += 360.0;
        }
        while (angleInRange >= 360.0)
        {
            angleInRange -= 360.0;
        }
        return angleInRange;
    }

}