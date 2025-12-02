// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Graphics;


namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>Controller for charts wheel view</summary>
/// <remarks>This view uses MVC instead of MVVM</remarks>
public class ChartsWheelCanvasController(
    ChartsWheelMetrics metrics,
    IGraphicCelPoints graphicCelPoints,
    IChartsWheelSigns chartsWheelSigns,
    IChartsWheelCusps chartsWheelCusps,
    IChartsWheelCircles chartsWheelCircles,
    IChartsWheelAspects chartsWheelAspects)
{

    public bool NoTime { get; set; }
    public bool NoAspects { get; set; }
    public bool ShowSignBackgroundColors { get; set; } = true;
    public List<Line> SignSeparators { get; private set; } = new();
    public List<TextBlock> SignGlyphs { get; private set; } = new();
    public List<Polygon> SignBackgroundSectors { get; private set; } = new();
    public List<Line> CuspLines { get; private set; } = new();
    public List<Line> CuspCardinalLines { get; private set; } = new();
    public List<TextBlock> CuspCardinalIndicators { get; private set; } = new();
    public List<TextBlock> CuspTexts { get; private set; } = new();
    public List<Line> CelPointConnectLines { get; private set; } = new();
    public List<TextBlock> CelPointTexts { get; private set; } = new();
    public List<TextBlock> CelPointGlyphs { get; private set; } = new();
    public List<TextBlock> VspTexts { get; private set; } = new();
    public List<Ellipse> WheelCircles { get; private set; } = new();
    public List<Line> DegreeLines { get; private set; } = new();
    public List<Line> AspectLines { get; private set; } = new();
    public Dictionary<ChartPoints, FullPointPos>? AllPositions { get; set; }

    public double LeftAnchorPoint { get; set; } = -1.0;
    
    public double CanvasSize { get; private set; }
    public ChartsWheelMetrics Metrics => metrics;
    private Point _centerPoint;


    private void HandleCircles()
    {
        WheelCircles = chartsWheelCircles.CreateCircles(metrics);
        DegreeLines = chartsWheelCircles.CreateDegreeLines(metrics, _centerPoint, GetAscendantLongitude());
    }

    private void HandleSigns()
    {
        var leftStartPoint = GetLeftStartPoint();
        SignSeparators = chartsWheelSigns.CreateSignSeparators(metrics, _centerPoint, leftStartPoint);
        SignGlyphs = chartsWheelSigns.CreateSignGlyphs(metrics, _centerPoint, leftStartPoint);
        if (ShowSignBackgroundColors)
        {
            SignBackgroundSectors = chartsWheelSigns.CreateSignBackgroundSectors(metrics, _centerPoint, leftStartPoint);
        }
        else
        {
            SignBackgroundSectors.Clear();
        }
    }

    private void HandleCusps()
    {
        var leftStartPoint = GetLeftStartPoint();
        if (NoTime)
        {
            CuspLines.Clear();
            CuspCardinalLines.Clear();
            CuspCardinalIndicators.Clear();
            CuspTexts.Clear();
            return;
        }
        CuspLines = chartsWheelCusps.CreateCuspLines(metrics, _centerPoint, GetHouseLongitudesCurrentChart(),
            leftStartPoint);
        CuspCardinalLines =
            chartsWheelCusps.CreateCardinalLines(metrics, _centerPoint, GetAscendantLongitude(), GetMcLongitude(), LeftAnchorPoint);
        CuspCardinalIndicators =
            chartsWheelCusps.CreateCardinalIndicators(metrics, _centerPoint, GetAscendantLongitude(),
                GetMcLongitude(), LeftAnchorPoint);
        CuspTexts = chartsWheelCusps.CreateCuspTexts(metrics, _centerPoint, GetHouseLongitudesCurrentChart(),
            leftStartPoint);

    }

    private void HandleCelPoints()
    {
        var points = GetCommonPointsCurrentChart();
        var leftStartPoint = GetLeftStartPoint();
        // var al = GetAscendantLongitude();
        CelPointGlyphs = graphicCelPoints.CreateCelPointGlyphsForWheel(metrics, points, _centerPoint, leftStartPoint);
        CelPointConnectLines = graphicCelPoints.CreateCelPointConnectLines(metrics, points, _centerPoint, leftStartPoint);
        CelPointTexts = graphicCelPoints.CreateCelPointTextsForWheel(metrics, points, _centerPoint, leftStartPoint);
    }

    private void HandleVspPoints()
    {
        VspTexts = new List<TextBlock>(); // Will be populated by VspWindow
    }

    private void HandleAspects()
    {
      AspectLines = chartsWheelAspects.CreateAspectLines(AllPositions!, metrics, _centerPoint, NoTime);
    }

    private double GetAscendantLongitude()
    {
        if (NoTime) return 0.0;
        return AllPositions != null
            ? AllPositions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position
            : 0.0;
    }

    private double GetMcLongitude()
    {
        return AllPositions != null ? AllPositions[ChartPoints.Mc].Ecliptical.MainPosSpeed.Position : 0.0;
    }


    private List<double> GetHouseLongitudesCurrentChart()
    {
        List<double> longitudes = new();
        if (AllPositions == null) return longitudes;
        longitudes.AddRange(from cusp in AllPositions
            where cusp.Key.GetDetails().PointCat == PointCats.Cusp
            select cusp.Value.Ecliptical.MainPosSpeed.Position);
        return longitudes;
    }

    private Dictionary<ChartPoints, FullPointPos> GetCommonPointsCurrentChart()
    {
        return AllPositions != null
            ? AllPositions.Where(item => item.Key.GetDetails().PointCat == PointCats.Common 
                                                    || item.Key.GetDetails().PointCat == PointCats.Lots)
                .ToDictionary(item => item.Key, item => item.Value)
            : new Dictionary<ChartPoints, FullPointPos>();
    }

    public void Resize(double minSize)
    {
        metrics.SetSizeFactor(minSize / 740.0);
        CanvasSize = metrics.GridSize;
        _centerPoint = new Point(metrics.GridSize / 2, metrics.GridSize / 2);
        PrepareDraw();
    }

    public void PrepareDraw()
    {
        HandleCircles();
        HandleSigns();
        HandleCusps();
        HandleCelPoints();
        HandleVspPoints();
        HandleAspects();
    }

    private double GetLeftStartPoint()
    {
        var ascLongitude = GetAscendantLongitude();
        var leftStartPoint = LeftAnchorPoint < 0.0 ? ascLongitude : LeftAnchorPoint;
        leftStartPoint = RangeUtil.ValueToRange(leftStartPoint, 0.0, 360.0);
        return leftStartPoint;
    }
}