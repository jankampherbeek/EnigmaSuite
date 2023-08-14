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
        IEnumerable<DefinedAspect> defSsAspects = _aspectsApi.AspectsForCelPoints(request);
        List<DrawableCelPointAspect> drawSsAspects = _aspectForWheelFactory.CreateCelPointAspectForWheel(defSsAspects);
        foreach ((ChartPoints point1, ChartPoints point2, double exactness, AspectTypes aspectTypes) in drawSsAspects)
        {
            Color aspectColor;
            switch (aspectTypes)
            {
                case AspectTypes.Square:
                case AspectTypes.Opposition:
                    aspectColor = metrics.HardAspectsColor;
                    break;
                case AspectTypes.Triangle:
                case AspectTypes.Sextile:
                    aspectColor = metrics.SoftAspectsColor;
                    break;
                default:
                    aspectColor = metrics.MinorAspectsColor;
                    break;
            }

            double lineWidth = metrics.AspectLineSize * exactness / 100.0;
            if (lineWidth < 0.5) lineWidth = 0.5;
            DrawableAspectCoordinatesCp? drawCoordSs1 = null;
            DrawableAspectCoordinatesCp? drawCoordSs2 = null;
            foreach (var coord in ssCoordinates)
            {
                if (coord.CelPoint == point1)
                {
                    drawCoordSs1 = coord with { CelPoint = point1 };
                }
                if (coord.CelPoint == point2)
                {
                    drawCoordSs2 = coord with { CelPoint = point2 };
                }
            }

            if (drawCoordSs1 == null || drawCoordSs2 == null) continue;
            Point firstPoint = new(drawCoordSs1.XCoordinate, drawCoordSs1.YCoordinate);
            Point secondPoint = new(drawCoordSs2.XCoordinate, drawCoordSs2.YCoordinate);
            Line connectionLine = DimLine.CreateLine(firstPoint, secondPoint, lineWidth, aspectColor, ChartsWheelMetrics.AspectOpacity);
            aspectLines.Add(connectionLine);

        }
        return aspectLines;
    }


    private static List<DrawableAspectCoordinatesCp> CreateSsCoordinates(CalculatedChart currentChart, ChartsWheelMetrics metrics, Point centerPoint)
    {
        List<DrawableAspectCoordinatesCp> drawableAspectCoordinatesSs = new();
        double longAsc = currentChart.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position;
        DimPoint dimPoint = new(centerPoint);
        foreach (var ssPointPos in currentChart.Positions)
        {
            if (ssPointPos.Key.GetDetails().PointCat != PointCats.Common && ssPointPos.Key != ChartPoints.Mc &&
                ssPointPos.Key != ChartPoints.Ascendant) continue;
            double longitude = ssPointPos.Value.Ecliptical.MainPosSpeed.Position;
            ChartPoints ssPos = ssPointPos.Key;
            double posOnCircle = longitude - longAsc + 90.0;
            if (posOnCircle < 0.0) posOnCircle += 360.0;
            if (posOnCircle >= 360.0) posOnCircle -= 360.0;
            Point newPoint = dimPoint.CreatePoint(posOnCircle, metrics.OuterAspectRadius);
            drawableAspectCoordinatesSs.Add(new DrawableAspectCoordinatesCp(ssPos, newPoint.X, newPoint.Y));
        }
        return drawableAspectCoordinatesSs;
    }

}
