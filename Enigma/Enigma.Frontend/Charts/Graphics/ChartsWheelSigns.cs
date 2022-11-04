// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Enigma.Frontend.Ui.Charts.Graphics;


public class ChartsWheelSigns : IChartsWheelSigns
{
    private readonly IRangeCheck _rangeCheck;

    public ChartsWheelSigns(IRangeCheck rangeCheck)
    {
        _rangeCheck = rangeCheck;
    }


    public List<Line> CreateSignSeparators(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant)
    {
        List<Line> allSeparators = new();
        double offsetAsc = 30.0 - (longAscendant % 30.0);
        DimLine dimLine = new();
        DimPoint dimPoint = new(centerPoint);
        Point point1;
        Point point2;
        double angle;
        double hypothenusa1 = metrics.OuterHouseRadius;
        double hypothenusa2 = metrics.OuterSignRadius;
        for (int i = 0; i < 12; i++)
        {
            angle = _rangeCheck.InRange360((i * 30) + offsetAsc) + 90.0;
            point1 = dimPoint.CreatePoint(angle, hypothenusa1);
            point2 = dimPoint.CreatePoint(angle, hypothenusa2);
            allSeparators.Add(dimLine.CreateLine(point1, point2, metrics.StrokeSize, Colors.SlateBlue, 1.0));
        }
        return allSeparators;
    }

    public List<TextBlock> CreateSignGlyphs(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant)
    {
        Point point1;
        double offsetAsc = 30.0 - (longAscendant % 30.0);
        double hypothenusa = metrics.SignGlyphRadius;
        double fontSize = metrics.SignGlyphSize;
        string[] glyphs = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };
        int indexFirstGlyph = (int)((longAscendant / 30.0) + 1);
        int glyphIndex = indexFirstGlyph;
        DimPoint dimPoint = new(centerPoint);
        DimTextBlock dimTextBlock = new(metrics.GlyphsFontFamily, fontSize, 0.7, Colors.SlateBlue);
        List<TextBlock> glyphList = new();
        for (int i = 0; i < 12; i++)
        {
            if (glyphIndex > 11) glyphIndex = 0;
            double angle = _rangeCheck.InRange360((i * 30) + offsetAsc + 90.0 + 15.0);
            point1 = dimPoint.CreatePoint(angle, hypothenusa);
            glyphList.Add(dimTextBlock.CreateTextBlock(glyphs[glyphIndex], point1.X - (fontSize / 3), point1.Y - (fontSize / 1.8)));
            glyphIndex++;
        }
        return glyphList;
    }
}
