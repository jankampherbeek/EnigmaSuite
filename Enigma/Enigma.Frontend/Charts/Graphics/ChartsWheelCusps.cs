// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.InputSupport.Conversions;
using Enigma.Frontend.Support;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Enigma.Frontend.Interfaces;
using Enigma.InputSupport.Interfaces;

namespace Enigma.Frontend.Charts.Graphics;

public class ChartsWheelCusps: IChartsWheelCusps
{
    private readonly IRangeCheck _rangeCheck;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;

    public ChartsWheelCusps(IRangeCheck rangeCheck, IDoubleToDmsConversions doubleToDmsConversions)
    {
        _rangeCheck = rangeCheck;
        _doubleToDmsConversions = doubleToDmsConversions;
    }

    public List<Line> CreateCuspLines(ChartsWheelMetrics metrics, Point centerPoint, List<double> housePositions, double longAscendant)
    {
        List<Line> cuspLines = new();
        for (int i = 0; i < housePositions.Count; i++)
        {
            double angle = _rangeCheck.InRange360(housePositions[i] - longAscendant + 90.0);
            double width = ((i % 3) == 0) ? metrics.StrokeSizeDouble : metrics.StrokeSize;
            cuspLines.Add(CreateSingleCuspLine(metrics, centerPoint, angle, metrics.OuterAspectRadius, metrics.OuterHouseRadius, width));
        }
        return cuspLines;
    }

    public List<Line> CreateCardinalLines(ChartsWheelMetrics metrics,Point centerPoint, double longAscendant, double longMc)
    {
        List<Line> cardinalLines = new();
        double angle = 90.0;
        double hypothenusa1 = metrics.OuterSignRadius;
        double hypothenusa2 = metrics.OuterRadius;
        cardinalLines.Add(CreateSingleCuspLine(metrics, centerPoint,angle, hypothenusa1, hypothenusa2, metrics.StrokeSizeDouble));
        angle = _rangeCheck.InRange360(angle + 180.0);
        cardinalLines.Add(CreateSingleCuspLine(metrics, centerPoint, angle, hypothenusa1, hypothenusa2, metrics.StrokeSizeDouble));
        angle = _rangeCheck.InRange360(longMc - longAscendant + 90.0);
        cardinalLines.Add(CreateSingleCuspLine(metrics, centerPoint, angle, hypothenusa1, hypothenusa2, metrics.StrokeSizeDouble));
        angle = _rangeCheck.InRange360(angle + 180.0);
        cardinalLines.Add(CreateSingleCuspLine(metrics, centerPoint, angle, hypothenusa1, hypothenusa2, metrics.StrokeSizeDouble));
        return cardinalLines;
    }

    private Line CreateSingleCuspLine(ChartsWheelMetrics metrics, Point centerPoint, double angle, double hypothenusa1, double hypothenusa2, double strokeSize)
    {
        DimPoint dimPoint = new(centerPoint);
        DimLine dimLine = new();
        Point point1 = dimPoint.CreatePoint(angle, hypothenusa1);
        Point point2 = dimPoint.CreatePoint(angle, hypothenusa2);
        return dimLine.CreateLine(point1, point2, strokeSize, metrics.CuspLineColor, metrics.CuspLineOpacity);
    }

    public List<TextBlock> CreateCardinalIndicators(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant, double longMc)
    {
        DimPoint dimPoint = new(centerPoint);
        DimTextBlock cuspsDimTextBlock = new(metrics.PositionTextsFontFamily, metrics.CardinalFontSize, metrics.CuspTextOpacity, metrics.CuspTextColor);
        List<TextBlock> cardinalIndicators = new();
        double xOffset = metrics.CardinalFontSize / 3;
        double yOffset = metrics.CardinalFontSize / 1.8;
       
        // Asc
        double angle = 90.0;
        string text = "A";
        Point posPoint = dimPoint.CreatePoint(angle, metrics.CardinalIndicatorRadius);
        cardinalIndicators.Add(cuspsDimTextBlock.CreateTextBlock(text, posPoint.X - xOffset, posPoint.Y - yOffset));
        // Desc
        angle = _rangeCheck.InRange360(angle + 180.0);
        text = "D";
        posPoint = dimPoint.CreatePoint(angle, metrics.CardinalIndicatorRadius);
        cardinalIndicators.Add(cuspsDimTextBlock.CreateTextBlock(text, posPoint.X - xOffset, posPoint.Y - yOffset));
        // MC
        angle = _rangeCheck.InRange360(longMc - longAscendant + 90.0);
        text = "M";
        posPoint = dimPoint.CreatePoint(angle, metrics.CardinalIndicatorRadius);
        cardinalIndicators.Add(cuspsDimTextBlock.CreateTextBlock(text, posPoint.X - xOffset, posPoint.Y - yOffset));
        // IC
        angle = _rangeCheck.InRange360(angle + 180.0);
        text = "I";
        posPoint = dimPoint.CreatePoint(angle, metrics.CardinalIndicatorRadius);
        cardinalIndicators.Add(cuspsDimTextBlock.CreateTextBlock(text, posPoint.X - xOffset, posPoint.Y - yOffset));
        return cardinalIndicators;
    }




    public List<TextBlock> CreateCuspTexts(ChartsWheelMetrics metrics, Point centerPoint, List<double> housePositions, double longAscendant)
    {
        List<TextBlock> cuspTexts = new();
        DimPoint dimPoint = new(centerPoint);
        DimTextBlock cuspsDimTextBlock = new(metrics.PositionTextsFontFamily, metrics.PositionTextSize, metrics.CuspTextOpacity, metrics.CuspTextColor);
        for (int i = 0; i < housePositions.Count; i++)
        {
            double angle = _rangeCheck.InRange360(housePositions[i] - longAscendant + 90.0);
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
            Point point1 = dimPoint.CreatePoint(angle + textOffsetDegrees, metrics.CuspTextRadius + yOffset);
            swapAngle = 90.0 - rotateAngle;
            rotateTransform.Angle = _rangeCheck.InRange360(180.0 + swapAngle);
            string text = _doubleToDmsConversions.ConvertDoubleToDmInSignNoGlyph(housePositions[i]);
            TextBlock posText = cuspsDimTextBlock.CreateTextBlock(text, point1.X, point1.Y, rotateTransform);
            cuspTexts.Add(posText);
        }
        return cuspTexts;
    }
}
