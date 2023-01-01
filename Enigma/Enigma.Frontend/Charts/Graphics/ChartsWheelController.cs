// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Enigma.Frontend.Ui.Charts.Graphics;

public class ChartsWheelController
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
    private readonly DataVault _dataVault;
    private readonly IChartsWheelCelPoints _chartsWheelCelPoints;
    private readonly IChartsWheelSigns _chartsWheelSigns;
    private readonly IChartsWheelCusps _chartsWheelCusps;
    private readonly IChartsWheelCircles _chartsWheelCircles;
    private readonly IChartsWheelAspects _chartsWheelAspects;

    private CalculatedChart? _currentChart;

    public ChartsWheelController(ChartsWheelMetrics metrics,
        IChartsWheelCelPoints chartsWheelCelPoints,
        IChartsWheelSigns chartsWheelSigns,
        IChartsWheelCusps chartsWheelCusps,
        IChartsWheelCircles chartsWheelCircles,
        IChartsWheelAspects chartsWheelAspects)
    {
        _dataVault = DataVault.Instance;
        _chartsWheelCelPoints = chartsWheelCelPoints;
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
        CuspLines = _chartsWheelCusps.CreateCuspLines(_metrics, _centerPoint, GetHouseLongitudesCurrentChart(), GetAscendantLongitude());
        CuspCardinalLines = _chartsWheelCusps.CreateCardinalLines(_metrics, _centerPoint, GetAscendantLongitude(), GetMcLongitude());
        CuspCardinalIndicators = _chartsWheelCusps.CreateCardinalIndicators(_metrics, _centerPoint, GetAscendantLongitude(), GetMcLongitude());
        CuspTexts = _chartsWheelCusps.CreateCuspTexts(_metrics, _centerPoint, GetHouseLongitudesCurrentChart(), GetAscendantLongitude());

    }

    private void HandleCelPoints()
    {
        CelPointGlyphs = _chartsWheelCelPoints.CreateCelPointGlyphs(_metrics, GetCelPointsCurrentChart(), _centerPoint, GetAscendantLongitude());
        CelPointConnectLines = _chartsWheelCelPoints.CreateCelPointConnectLines(_metrics, GetCelPointsCurrentChart(), _centerPoint, GetAscendantLongitude());
        CelPointTexts = _chartsWheelCelPoints.CreateCelPointTexts(_metrics, GetCelPointsCurrentChart(), _centerPoint, GetAscendantLongitude());
    }

    private void HandleAspects()
    {
        AspectLines = _chartsWheelAspects.CreateAspectLines(_dataVault.GetLastChart(), _metrics, _centerPoint);
    }

    public double GetAscendantLongitude()
    {
        return _currentChart != null ? _currentChart.FullHousePositions.Ascendant.Longitude : 0.0;
    }

    public double GetMcLongitude()
    {
        return _currentChart != null ? _currentChart.FullHousePositions.Mc.Longitude : 0.0;
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

    public List<FullCelPointPos> GetCelPointsCurrentChart()
    {
        _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null)
        {
            return _currentChart.CelPointPositions;
        }
        else
        {
            return new List<FullCelPointPos>();
        }
    }

    public void Resize(double minSize)
    {
        _metrics.SetSizeFactor(minSize / 740.0);
        CanvasSize = _metrics.GridSize;
        _centerPoint = new(_metrics.GridSize / 2, _metrics.GridSize / 2);
        PrepareDraw();
    }

    public void PrepareDraw()
    {
        _currentChart = _dataVault.GetLastChart();
        HandleCircles();
        HandleSigns();
        HandleCusps();
        HandleCelPoints();
        HandleAspects();
    }

}