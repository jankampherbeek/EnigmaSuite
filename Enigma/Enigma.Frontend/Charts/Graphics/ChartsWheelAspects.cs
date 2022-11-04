// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Domain.RequestResponse;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Enigma.Frontend.Ui.Charts.Graphics;




public class ChartsWheelAspects : IChartsWheelAspects
{
    private readonly IAspectsApi _aspectsApi;
    private readonly IAspectForWheelFactory _aspectForWheelFactory;
    private readonly IRangeCheck _rangeCheck;

    public ChartsWheelAspects(IAspectsApi aspectsApi, IAspectForWheelFactory aspectForWheelFactory, IRangeCheck rangeCheck)
    {
        _aspectsApi = aspectsApi;
        _aspectForWheelFactory = aspectForWheelFactory;
        _rangeCheck = rangeCheck;
    }

    public List<Line> CreateAspectLines(CalculatedChart currentChart, ChartsWheelMetrics metrics, Point centerPoint)
    {
        List<Line> aspectLines = new();
        List<DrawableAspectCoordinatesSs> ssCoordinates = CreateSsCoordinates(currentChart, metrics, centerPoint);
        AspectRequest request = new(currentChart);
        List<EffectiveAspect> effSsAspects = _aspectsApi.AspectsForSolSysPoints(request);
        List<DrawableSolSysPointAspect> drawSsAspects = _aspectForWheelFactory.CreateSolSysAspectForWheel(effSsAspects);
        List<EffectiveAspect> effMuAspects = _aspectsApi.AspectsForMundanePoints(request);
        DimLine dimLine = new();
        foreach (var drawSsAspect in drawSsAspects)
        {
            Color aspectColor = metrics.MinorAspectsColor;
            if (drawSsAspect.AspectType == AspectTypes.Square || drawSsAspect.AspectType == AspectTypes.Opposition) aspectColor = metrics.HardAspectsColor;
            if (drawSsAspect.AspectType == AspectTypes.Triangle || drawSsAspect.AspectType == AspectTypes.Sextile) aspectColor = metrics.SoftAspectsColor;
            double lineWidth = metrics.AspectLineSize * drawSsAspect.Exactness / 100.0;
            SolarSystemPoints point1 = drawSsAspect.Point1;
            SolarSystemPoints point2 = drawSsAspect.Point2;
            DrawableAspectCoordinatesSs? drawCoordSs1 = null;
            DrawableAspectCoordinatesSs? drawCoordSs2 = null;
            foreach (var coord in ssCoordinates)
            {
                if (coord.SolSysPoint == point1)
                {
                    drawCoordSs1 = new(point1, coord.XCoordinate, coord.YCoordinate);
                }
                if (coord.SolSysPoint == point2)
                {
                    drawCoordSs2 = new(point2, coord.XCoordinate, coord.YCoordinate);
                }
            }
            if (drawCoordSs1 != null && drawCoordSs2 != null)
            {
                Point firstPoint = new(drawCoordSs1.XCoordinate, drawCoordSs1.YCoordinate);
                Point secondPoint = new(drawCoordSs2.XCoordinate, drawCoordSs2.YCoordinate);
                Line connectionLine = dimLine.CreateLine(firstPoint, secondPoint, lineWidth, aspectColor, metrics.SolSysPointConnectLineOpacity);
                aspectLines.Add(connectionLine);

            }

        }
        return aspectLines;
    }


    private List<DrawableAspectCoordinatesSs> CreateSsCoordinates(CalculatedChart currentChart, ChartsWheelMetrics metrics, Point centerPoint)
    {
        List<DrawableAspectCoordinatesSs> drawableAspectCoordinatesSs = new();
        double longAsc = currentChart.FullHousePositions.Ascendant.Longitude;
        DimPoint dimPoint = new(centerPoint);
        foreach (var ssPointPos in currentChart.SolSysPointPositions)
        {
            double longitude = ssPointPos.Longitude.Position;
            SolarSystemPoints ssPos = ssPointPos.SolarSystemPoint;
            double posOnCircle = _rangeCheck.InRange360(longitude - longAsc + 90.0);
            Point newPoint = dimPoint.CreatePoint(posOnCircle, metrics.OuterAspectRadius);
            drawableAspectCoordinatesSs.Add(new DrawableAspectCoordinatesSs(ssPos, newPoint.X, newPoint.Y));
        }
        return drawableAspectCoordinatesSs;
    }



    private List<DrawableAspectCoordinatesMu> CreateMuCoordinates(CalculatedChart currentChart, ChartsWheelMetrics metrics, Point centerPoint)
    {
        List<DrawableAspectCoordinatesMu> drawableAspectCoordinatesMu = new();
        double longAsc = currentChart.FullHousePositions.Ascendant.Longitude;
        DimPoint dimPoint = new(centerPoint);
        // Asc
        double posOnCircle = 90.0;
        Point ascPoint = dimPoint.CreatePoint(posOnCircle, metrics.OuterAspectRadius);
        drawableAspectCoordinatesMu.Add(new DrawableAspectCoordinatesMu("Asc", ascPoint.X, ascPoint.Y));
        // MC
        double longitudeMc = currentChart.FullHousePositions.Mc.Longitude;
        posOnCircle = _rangeCheck.InRange360(longitudeMc - longAsc + 90.0);
        Point mcPoint = dimPoint.CreatePoint(posOnCircle, metrics.OuterAspectRadius);
        drawableAspectCoordinatesMu.Add(new DrawableAspectCoordinatesMu("MC", mcPoint.X, mcPoint.Y));
        return drawableAspectCoordinatesMu;
    }

}
