// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System;

namespace Enigma.Frontend.Ui.Graphics;

public interface IChartsWheelSigns
{
    public List<Line> CreateSignSeparators(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant);
    public List<TextBlock> CreateSignGlyphs(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant);
    public List<Polygon> CreateSignBackgroundSectors(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant);
}
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

    public List<Polygon> CreateSignBackgroundSectors(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant)
    {
        List<Polygon> signSectors = new();
        double offsetAsc = 30.0 - longAscendant % 30.0;
        DimPoint dimPoint = new(centerPoint);
        
        // Define colors for each element group with 40% opacity
        Color fireColor = Color.FromArgb(102, 255, 0, 0);      // Pure Red with 40% opacity
        Color earthColor = Color.FromArgb(102, 139, 69, 19);    // Brown with 40% opacity  
        Color airColor = Color.FromArgb(102, 0, 0, 255);        // Pure Blue with 40% opacity
        Color waterColor = Color.FromArgb(102, 0, 255, 0);      // Pure Green with 40% opacity
        
        // Define which signs belong to which elements
        // Fire signs: Aries, Leo, Sagittarius
        // Earth signs: Taurus, Virgo, Capricorn
        // Air signs: Gemini, Libra, Aquarius
        // Water signs: Cancer, Scorpio, Pisces
        
        for (int signIndex = 0; signIndex < 12; signIndex++)
        {
            // Use the original angle calculation that was working
            double startAngle = signIndex * 30.0 + offsetAsc;
            double endAngle = (signIndex + 1) * 30.0 + offsetAsc;
            
            // Determine color based on sign - account for ascendant position
            Color signColor;
            string signName;
            
            // Calculate which sign this sector represents based on the ascendant
            // The ascendant determines which sign is at the top (9 o'clock position)
            int ascendantSign = (int)(longAscendant / 30.0);
            int currentSign = (ascendantSign + signIndex - 1) % 12; // Subtract 1 to fix the offset
            
            // Map each sign to its correct color based on the current sign
            switch (currentSign)
            {
                case 0: // Aries
                    signColor = fireColor;
                    signName = "Aries";
                    break;
                case 1: // Taurus
                    signColor = earthColor;
                    signName = "Taurus";
                    break;
                case 2: // Gemini
                    signColor = airColor;
                    signName = "Gemini";
                    break;
                case 3: // Cancer
                    signColor = waterColor;
                    signName = "Cancer";
                    break;
                case 4: // Leo
                    signColor = fireColor;
                    signName = "Leo";
                    break;
                case 5: // Virgo
                    signColor = earthColor;
                    signName = "Virgo";
                    break;
                case 6: // Libra
                    signColor = airColor;
                    signName = "Libra";
                    break;
                case 7: // Scorpio
                    signColor = waterColor;
                    signName = "Scorpio";
                    break;
                case 8: // Sagittarius
                    signColor = fireColor;
                    signName = "Sagittarius";
                    break;
                case 9: // Capricorn
                    signColor = earthColor;
                    signName = "Capricorn";
                    break;
                case 10: // Aquarius
                    signColor = airColor;
                    signName = "Aquarius";
                    break;
                case 11: // Pisces
                    signColor = waterColor;
                    signName = "Pisces";
                    break;
                default:
                    signColor = fireColor;
                    signName = "Unknown";
                    break;
            }
            
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Sign {signIndex}: {signName} (ascendant sign: {ascendantSign}, current sign: {currentSign}) - Color: R={signColor.R}, G={signColor.G}, B={signColor.B}, A={signColor.A}");
            
            // Create annular sector between OuterHouseRadius and OuterSignRadius
            PointCollection sectorPoints = new();
            
            // Convert angles to radians
            double startRad = startAngle * Math.PI / 180.0;
            double endRad = endAngle * Math.PI / 180.0;
            
            // Create points for the annular sector
            // Inner arc (OuterHouseRadius)
            for (int i = 0; i <= 10; i++)
            {
                double angle = startRad + (endRad - startRad) * i / 10.0;
                double x = centerPoint.X + metrics.OuterHouseRadius * Math.Cos(angle);
                double y = centerPoint.Y - metrics.OuterHouseRadius * Math.Sin(angle);
                sectorPoints.Add(new Point(x, y));
            }
            
            // Outer arc (OuterSignRadius) in reverse order
            for (int i = 10; i >= 0; i--)
            {
                double angle = startRad + (endRad - startRad) * i / 10.0;
                double x = centerPoint.X + metrics.OuterSignRadius * Math.Cos(angle);
                double y = centerPoint.Y - metrics.OuterSignRadius * Math.Sin(angle);
                sectorPoints.Add(new Point(x, y));
            }
            
            Polygon sector = new()
            {
                Points = sectorPoints,
                Fill = new SolidColorBrush(signColor),
                Stroke = null,
                StrokeThickness = 0
            };
            
            signSectors.Add(sector);
        }
        
        return signSectors;
    }
}
