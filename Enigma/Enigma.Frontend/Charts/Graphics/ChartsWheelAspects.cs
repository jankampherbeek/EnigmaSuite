// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Domain.RequestResponse;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Enigma.Frontend.Ui.Charts.Graphics;


// TODO 0.1 Analysis, check aspects in chart

public sealed class ChartsWheelAspects : IChartsWheelAspects
{
    private readonly IAspectsApi _aspectsApi;
    private readonly IAspectForWheelFactory _aspectForWheelFactory;

    public ChartsWheelAspects(IAspectsApi aspectsApi, IAspectForWheelFactory aspectForWheelFactory)
    {
        _aspectsApi = aspectsApi;
        _aspectForWheelFactory = aspectForWheelFactory;
    }

    public List<Line> CreateAspectLines(CalculatedChart currentChart, ChartsWheelMetrics metrics, Point centerPoint)
    {
        List<Line> aspectLines = new();
        List<DrawableAspectCoordinatesCp> ssCoordinates = CreateSsCoordinates(currentChart, metrics, centerPoint);
        AstroConfig config = CurrentConfig.Instance.GetConfig();
        AspectRequest request = new(currentChart, config);
        List<DefinedAspect> defSsAspects = _aspectsApi.AspectsForCelPoints(request);
        List<DrawableCelPointAspect> drawSsAspects = _aspectForWheelFactory.CreateCelPointAspectForWheel(defSsAspects);
  //      List<DefinedAspect> defMuAspects = _aspectsApi.AspectsForMundanePoints(request);                                  // todo 0.1 add mundane aspects to chartwheel
        DimLine dimLine = new();
        foreach (var drawSsAspect in drawSsAspects)
        {
            Color aspectColor = metrics.MinorAspectsColor;
            if (drawSsAspect.AspectType == AspectTypes.Square || drawSsAspect.AspectType == AspectTypes.Opposition) aspectColor = metrics.HardAspectsColor;
            if (drawSsAspect.AspectType == AspectTypes.Triangle || drawSsAspect.AspectType == AspectTypes.Sextile) aspectColor = metrics.SoftAspectsColor;
            double lineWidth = metrics.AspectLineSize * drawSsAspect.Exactness / 100.0;
            if (lineWidth < 0.5) lineWidth = 0.5;
            ChartPoints point1 = drawSsAspect.Point1;
            ChartPoints point2 = drawSsAspect.Point2;
            DrawableAspectCoordinatesCp? drawCoordSs1 = null;
            DrawableAspectCoordinatesCp? drawCoordSs2 = null;
            foreach (var coord in ssCoordinates)
            {
                if (coord.CelPoint == point1)
                {
                    drawCoordSs1 = new(point1, coord.XCoordinate, coord.YCoordinate);
                }
                if (coord.CelPoint == point2)
                {
                    drawCoordSs2 = new(point2, coord.XCoordinate, coord.YCoordinate);
                }
            }
            if (drawCoordSs1 != null && drawCoordSs2 != null)
            {
                Point firstPoint = new(drawCoordSs1.XCoordinate, drawCoordSs1.YCoordinate);
                Point secondPoint = new(drawCoordSs2.XCoordinate, drawCoordSs2.YCoordinate);
                Line connectionLine = dimLine.CreateLine(firstPoint, secondPoint, lineWidth, aspectColor, metrics.AspectOpacity);
                aspectLines.Add(connectionLine);

            }

        }
        return aspectLines;
    }


    private List<DrawableAspectCoordinatesCp> CreateSsCoordinates(CalculatedChart currentChart, ChartsWheelMetrics metrics, Point centerPoint)
    {
        List<DrawableAspectCoordinatesCp> drawableAspectCoordinatesSs = new();
        double longAsc = currentChart.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position;
        DimPoint dimPoint = new(centerPoint);
        foreach (var ssPointPos in currentChart.Positions)
        {
            if (ssPointPos.Key.GetDetails().PointCat == PointCats.Common || ssPointPos.Key == ChartPoints.Mc || ssPointPos.Key == ChartPoints.Ascendant)
            {
                double longitude = ssPointPos.Value.Ecliptical.MainPosSpeed.Position;
                ChartPoints ssPos = ssPointPos.Key;
                double posOnCircle = longitude - longAsc + 90.0;
                if (posOnCircle < 0.0) posOnCircle += 360.0;
                if (posOnCircle >= 360.0) posOnCircle -= 360.0;
                Point newPoint = dimPoint.CreatePoint(posOnCircle, metrics.OuterAspectRadius);
                drawableAspectCoordinatesSs.Add(new DrawableAspectCoordinatesCp(ssPos, newPoint.X, newPoint.Y));
            }
        }
        return drawableAspectCoordinatesSs;
    }


    /*
    private List<DrawableAspectCoordinatesMu> CreateMuCoordinates(CalculatedChart currentChart, ChartsWheelMetrics metrics, Point centerPoint)
    {
        List<DrawableAspectCoordinatesMu> drawableAspectCoordinatesMu = new();
        double longAsc = currentChart.Positions.Angles[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position;
        DimPoint dimPoint = new(centerPoint);
        // Asc
        double posOnCircle = 90.0;
        Point ascPoint = dimPoint.CreatePoint(posOnCircle, metrics.OuterAspectRadius);
        drawableAspectCoordinatesMu.Add(new DrawableAspectCoordinatesMu("Asc", ascPoint.X, ascPoint.Y));
        // MC
        double longitudeMc = currentChart.Positions.Angles[ChartPoints.Mc].Ecliptical.MainPosSpeed.Position;  
        posOnCircle = longitudeMc - longAsc + 90.0;
        if (posOnCircle < 0.0) posOnCircle += 360.0;
        if (posOnCircle >= 360.0) posOnCircle -= 360.0;
        Point mcPoint = dimPoint.CreatePoint(posOnCircle, metrics.OuterAspectRadius);
        drawableAspectCoordinatesMu.Add(new DrawableAspectCoordinatesMu("MC", mcPoint.X, mcPoint.Y));
        return drawableAspectCoordinatesMu;
    }
    */
}
