// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Graphics;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>Controller for charts wheel view</summary>
/// <remarks>This view uses MVC instead of MVVM</remarks>
public class ChartsWheelCanvasController
{

    public List<Line> SignSeparators { get; private set; } = new();
    public List<TextBlock> SignGlyphs { get; private set; } = new();
    public List<Line> CuspLines { get; private set; } = new();
    public List<Line> CuspCardinalLines { get; private set; } = new();
    public List<TextBlock> CuspCardinalIndicators { get; private set; } = new();
    public List<TextBlock> CuspTexts { get; private set; } = new();
    public List<Line> CelPointConnectLines { get; private set; } = new();
    public List<TextBlock> CelPointTexts { get; private set; } = new();
    public List<TextBlock> CelPointGlyphs { get; private set; } = new();
    public List<Ellipse> WheelCircles { get; private set; } = new();
    public List<Line> DegreeLines { get; private set; } = new();
    public List<Line> AspectLines { get; private set; } = new();

    public double CanvasSize { get; private set; }
    private Point _centerPoint;

    private readonly ChartsWheelMetrics _metrics;
    private readonly DataVaultCharts _dataVaultCharts;
    private readonly IGraphicCelPoints _graphicCelPoints;
    private readonly IChartsWheelSigns _chartsWheelSigns;
    private readonly IChartsWheelCusps _chartsWheelCusps;
    private readonly IChartsWheelCircles _chartsWheelCircles;
    private readonly IChartsWheelAspects _chartsWheelAspects;


    private CalculatedChart? _currentChart;

    public ChartsWheelCanvasController(ChartsWheelMetrics metrics,
        IGraphicCelPoints graphicCelPoints,
        IChartsWheelSigns chartsWheelSigns,
        IChartsWheelCusps chartsWheelCusps,
        IChartsWheelCircles chartsWheelCircles,
        IChartsWheelAspects chartsWheelAspects)
    {
        _dataVaultCharts = DataVaultCharts.Instance;
        _graphicCelPoints = graphicCelPoints;
        _metrics = metrics;
        _chartsWheelSigns = chartsWheelSigns;
        _chartsWheelCusps = chartsWheelCusps;
        _chartsWheelCircles = chartsWheelCircles;
        _chartsWheelAspects = chartsWheelAspects;
    }

    private void HandleCircles()
    {
        WheelCircles = _chartsWheelCircles.CreateCircles(_metrics);
        DegreeLines = _chartsWheelCircles.CreateDegreeLines(_metrics, _centerPoint, GetAscendantLongitude());
    }

    private void HandleSigns()
    {
        SignSeparators = _chartsWheelSigns.CreateSignSeparators(_metrics, _centerPoint, GetAscendantLongitude());
        SignGlyphs = _chartsWheelSigns.CreateSignGlyphs(_metrics, _centerPoint, GetAscendantLongitude());
    }

    private void HandleCusps()
    {
        CuspLines = _chartsWheelCusps.CreateCuspLines(_metrics, _centerPoint, GetHouseLongitudesCurrentChart(),
            GetAscendantLongitude());
        CuspCardinalLines =
            _chartsWheelCusps.CreateCardinalLines(_metrics, _centerPoint, GetAscendantLongitude(), GetMcLongitude());
        CuspCardinalIndicators =
            _chartsWheelCusps.CreateCardinalIndicators(_metrics, _centerPoint, GetAscendantLongitude(),
                GetMcLongitude());
        CuspTexts = _chartsWheelCusps.CreateCuspTexts(_metrics, _centerPoint, GetHouseLongitudesCurrentChart(),
            GetAscendantLongitude());

    }

    private void HandleCelPoints()
    {
        CelPointGlyphs = _graphicCelPoints.CreateCelPointGlyphsForWheel(_metrics, GetCommonPointsCurrentChart(),
            _centerPoint, GetAscendantLongitude());
        CelPointConnectLines = _graphicCelPoints.CreateCelPointConnectLines(_metrics, GetCommonPointsCurrentChart(),
            _centerPoint, GetAscendantLongitude());
        CelPointTexts = _graphicCelPoints.CreateCelPointTextsForWheel(_metrics, GetCommonPointsCurrentChart(), _centerPoint,
            GetAscendantLongitude());
    }

    private void HandleAspects()
    {
        AspectLines = _chartsWheelAspects.CreateAspectLines(_dataVaultCharts.GetCurrentChart()!, _metrics, _centerPoint);
    }

    private double GetAscendantLongitude()
    {
        return _currentChart != null
            ? _currentChart.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position
            : 0.0;
    }

    private double GetMcLongitude()
    {
        return _currentChart != null ? _currentChart.Positions[ChartPoints.Mc].Ecliptical.MainPosSpeed.Position : 0.0;
    }


    private List<double> GetHouseLongitudesCurrentChart()
    {
        List<double> longitudes = new();
        _currentChart = _dataVaultCharts.GetCurrentChart();
        if (_currentChart == null) return longitudes;
        longitudes.AddRange(from cusp in _currentChart.Positions
            where cusp.Key.GetDetails().PointCat == PointCats.Cusp
            select cusp.Value.Ecliptical.MainPosSpeed.Position);
        return longitudes;
    }

    private Dictionary<ChartPoints, FullPointPos> GetCommonPointsCurrentChart()
    {
        _currentChart = _dataVaultCharts.GetCurrentChart();
        return _currentChart != null
            ? _currentChart.Positions.Where(item => item.Key.GetDetails().PointCat == PointCats.Common)
                .ToDictionary(item => item.Key, item => item.Value)
            : new Dictionary<ChartPoints, FullPointPos>();
    }

    public void Resize(double minSize)
    {
        _metrics.SetSizeFactor(minSize / 740.0);
        CanvasSize = _metrics.GridSize;
        _centerPoint = new Point(_metrics.GridSize / 2, _metrics.GridSize / 2);
        PrepareDraw();
    }

    public void PrepareDraw()
    {
        _currentChart = _dataVaultCharts.GetCurrentChart();
        HandleCircles();
        HandleSigns();
        HandleCusps();
        HandleCelPoints();
        HandleAspects();
    }

}