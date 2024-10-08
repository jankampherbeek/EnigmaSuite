﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.Graphics;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Serilog;

namespace Enigma.Frontend.Ui.Graphics;

// Interfaces for frontend graphics.
public interface IChartsWheelAspects
{
    public List<Line> CreateAspectLines(CalculatedChart currentChart, ChartsWheelMetrics metrics, Point centerPoint, bool noTime);
}

public sealed class ChartsWheelAspects : IChartsWheelAspects
{
    private readonly IAspectsApi _aspectsApi;
    private readonly IAspectForWheelFactory _aspectForWheelFactory;

    public ChartsWheelAspects(IAspectsApi aspectsApi, IAspectForWheelFactory aspectForWheelFactory)
    {
        _aspectsApi = aspectsApi;
        _aspectForWheelFactory = aspectForWheelFactory;
    }

    public List<Line> CreateAspectLines(CalculatedChart currentChart, ChartsWheelMetrics metrics, Point centerPoint, bool noTime)
    {
        List<Line> aspectLines = new();
        List<DrawableAspectCoordinatesCp> ssCoordinates = CreateSsCoordinates(currentChart, metrics, centerPoint, noTime);
        Log.Information("ChartsWheelaspect.CreateAspectLines(): retrieving config from CurrentConfig");
        AstroConfig config = CurrentConfig.Instance.GetConfig();
        AspectRequest request = new(currentChart, config);
        IEnumerable<DefinedAspect> defSsAspects = _aspectsApi.AspectsForCelPoints(request);
        List<DrawableCelPointAspect> drawSsAspects = _aspectForWheelFactory.CreateCelPointAspectForWheel(defSsAspects);
        foreach ((ChartPoints point1, ChartPoints point2, double exactness, AspectTypes aspectTypes) in drawSsAspects)
        {


            if (config.AspectColors.TryGetValue(aspectTypes, out string value))
            {
                Color aspectColor = (Color)ColorConverter.ConvertFromString(value);
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
                Line connectionLine = DimLine.CreateLine(firstPoint, secondPoint, lineWidth, aspectColor,
                    ChartsWheelMetrics.AspectOpacity);
                aspectLines.Add(connectionLine);
            }

        }
        return aspectLines;
    }


    private static List<DrawableAspectCoordinatesCp> CreateSsCoordinates(CalculatedChart currentChart, 
        ChartsWheelMetrics metrics, Point centerPoint, bool noTime)
    {
        List<DrawableAspectCoordinatesCp> drawableAspectCoordinatesSs = new();
        double longAsc = currentChart.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position;
        DimPoint dimPoint = new(centerPoint);
        foreach (var ssPointPos in currentChart.Positions)
        {
            if (noTime && (ssPointPos.Key == ChartPoints.Ascendant || ssPointPos.Key == ChartPoints.Mc ||
                           ssPointPos.Key == ChartPoints.Vertex || ssPointPos.Key == ChartPoints.EastPoint ||
                           ssPointPos.Key == ChartPoints.FortunaSect || ssPointPos.Key == ChartPoints.FortunaNoSect))
            {
                continue;
            }
            if (ssPointPos.Key.GetDetails().PointCat != PointCats.Common && ssPointPos.Key != ChartPoints.Mc &&
                ssPointPos.Key != ChartPoints.Ascendant) continue;
            double longitude = ssPointPos.Value.Ecliptical.MainPosSpeed.Position;
            ChartPoints ssPos = ssPointPos.Key;
            double posOnCircle = longitude - longAsc + 90.0;
            if (posOnCircle < 0.0) posOnCircle += 360.0;
            if (posOnCircle >= 360.0) posOnCircle -= 360.0;
            if (noTime) posOnCircle = longitude + 90.0;
            Point newPoint = dimPoint.CreatePoint(posOnCircle, metrics.OuterAspectRadius);
            drawableAspectCoordinatesSs.Add(new DrawableAspectCoordinatesCp(ssPos, newPoint.X, newPoint.Y));
        }
        return drawableAspectCoordinatesSs;
    }

}
