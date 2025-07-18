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
public class ChartsWheelCanvasController(
    ChartsWheelMetrics metrics,
    IGraphicCelPoints graphicCelPoints,
    IChartsWheelSigns chartsWheelSigns,
    IChartsWheelCusps chartsWheelCusps,
    IChartsWheelCircles chartsWheelCircles,
    IChartsWheelAspects chartsWheelAspects)
{

    public bool NoTime { get; set; } = false;
    public bool NoAspects { get; set; } = false;
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
    public List<Ellipse> WheelCircles { get; private set; } = new();
    public List<Line> DegreeLines { get; private set; } = new();
    public List<Line> AspectLines { get; private set; } = new();

    public double CanvasSize { get; private set; }
    private Point _centerPoint;

    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;


    private CalculatedChart? _currentChart;

    private void HandleCircles()
    {
        WheelCircles = chartsWheelCircles.CreateCircles(metrics);
        DegreeLines = chartsWheelCircles.CreateDegreeLines(metrics, _centerPoint, GetAscendantLongitude());
    }

    private void HandleSigns()
    {
        SignSeparators = chartsWheelSigns.CreateSignSeparators(metrics, _centerPoint, GetAscendantLongitude());
        SignGlyphs = chartsWheelSigns.CreateSignGlyphs(metrics, _centerPoint, GetAscendantLongitude());
        if (ShowSignBackgroundColors)
        {
            SignBackgroundSectors = chartsWheelSigns.CreateSignBackgroundSectors(metrics, _centerPoint, GetAscendantLongitude());
        }
        else
        {
            SignBackgroundSectors.Clear();
        }
    }

    private void HandleCusps()
    {
        if (NoTime)
        {
            CuspLines.Clear();
            CuspCardinalLines.Clear();
            CuspCardinalIndicators.Clear();
            CuspTexts.Clear();
            return;
        }
        CuspLines = chartsWheelCusps.CreateCuspLines(metrics, _centerPoint, GetHouseLongitudesCurrentChart(),
            GetAscendantLongitude());
        CuspCardinalLines =
            chartsWheelCusps.CreateCardinalLines(metrics, _centerPoint, GetAscendantLongitude(), GetMcLongitude());
        CuspCardinalIndicators =
            chartsWheelCusps.CreateCardinalIndicators(metrics, _centerPoint, GetAscendantLongitude(),
                GetMcLongitude());
        CuspTexts = chartsWheelCusps.CreateCuspTexts(metrics, _centerPoint, GetHouseLongitudesCurrentChart(),
            GetAscendantLongitude());

    }

    private void HandleCelPoints()
    {
        var points = GetCommonPointsCurrentChart();
        var al = GetAscendantLongitude();
        CelPointGlyphs = graphicCelPoints.CreateCelPointGlyphsForWheel(metrics, points, _centerPoint, al);
        CelPointConnectLines = graphicCelPoints.CreateCelPointConnectLines(metrics, points, _centerPoint, al);
        CelPointTexts = graphicCelPoints.CreateCelPointTextsForWheel(metrics, points, _centerPoint, al);
    }

    private void HandleAspects()
    {
        AspectLines = chartsWheelAspects.CreateAspectLines(_dataVaultCharts.GetCurrentChart()!, metrics, _centerPoint, NoTime);
    }

    private double GetAscendantLongitude()
    {
        if (NoTime) return 0.0;
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
            ? _currentChart.Positions.Where(item => item.Key.GetDetails().PointCat == PointCats.Common 
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
        if (_currentChart == null)
        {
            _currentChart = _dataVaultCharts.GetCurrentChart();
        }
        HandleCircles();
        HandleSigns();
        HandleCusps();
        HandleCelPoints();
        HandleAspects();
    }

    /// <summary>
    /// Set a custom chart to display instead of the current chart
    /// </summary>
    /// <param name="chart">The chart to display</param>
    public void SetCustomChart(CalculatedChart chart)
    {
        _currentChart = chart;
        PrepareDraw();
    }

    /// <summary>
    /// Reset to use the current chart from DataVault
    /// </summary>
    public void ResetToCurrentChart()
    {
        _currentChart = null;
        PrepareDraw();
    }

}