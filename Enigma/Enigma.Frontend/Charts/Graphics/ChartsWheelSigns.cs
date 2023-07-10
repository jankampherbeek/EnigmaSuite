// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Enigma.Frontend.Ui.Charts.Graphics;


public sealed class ChartsWheelSigns : IChartsWheelSigns
{


    public List<Line> CreateSignSeparators(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant)
    {
        List<Line> allSeparators = new();
        double offsetAsc = 30.0 - longAscendant % 30.0;
        DimPoint dimPoint = new(centerPoint);
        double hypothenusa1 = metrics.OuterHouseRadius;
        double hypothenusa2 = metrics.OuterSignRadius;
        for (int i = 0; i < 12; i++)
        {
            double angle = i * 30 + offsetAsc;
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;

            angle += 90.0;
            Point point1 = dimPoint.CreatePoint(angle, hypothenusa1);
            Point point2 = dimPoint.CreatePoint(angle, hypothenusa2);
            allSeparators.Add(DimLine.CreateLine(point1, point2, metrics.StrokeSize, Colors.MediumBlue, 1.0));
        }
        return allSeparators;
    }

    public List<TextBlock> CreateSignGlyphs(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant)
    {
        double offsetAsc = 30.0 - (longAscendant % 30.0);
        double hypothenusa = metrics.SignGlyphRadius;
        double fontSize = metrics.SignGlyphSize;
        string[] glyphs = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };
        int indexFirstGlyph = (int)((longAscendant / 30.0) + 1);
        int glyphIndex = indexFirstGlyph;
        DimPoint dimPoint = new(centerPoint);
        DimTextBlock dimTextBlock = new(metrics.GlyphsFontFamily, fontSize, 1.0, Colors.MediumBlue);
        List<TextBlock> glyphList = new();
        for (int i = 0; i < 12; i++)
        {
            if (glyphIndex > 11) glyphIndex = 0;
            double angle = i * 30 + offsetAsc + 90.0 + 15.0;
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;
            Point point1 = dimPoint.CreatePoint(angle, hypothenusa);
            glyphList.Add(dimTextBlock.CreateTextBlock(glyphs[glyphIndex], point1.X - fontSize / 3, point1.Y - fontSize / 1.8));
            glyphIndex++;
        }
        return glyphList;
    }
}
