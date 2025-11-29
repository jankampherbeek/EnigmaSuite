// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.AltZodiacStart;

public static class AltZodiacStartOrchestrator
{

    public static CalculatedChart ChangeStartingPoint(CalculatedChart radix, ChartPoints point)
    {
        var offset = DefineOffset(radix, point);
        var altChart = CreateAltChart(radix, offset);
        return altChart;
    }

    private static double DefineOffset(CalculatedChart radix, ChartPoints point)
    {
        var offset = 0.0;
        foreach (var radixPoint in radix.Positions)
        {
            if (radixPoint.Key == point)
            {
                offset = radixPoint.Value.Ecliptical.MainPosSpeed.Position;
            }     
        }
        return offset;
    }

    private static CalculatedChart CreateAltChart(CalculatedChart radix, double offset)
    {
        Dictionary <ChartPoints, FullPointPos> altPositions = new();  
        var obliquity = radix.Obliquity;
        var inputtedChartData = radix.InputtedChartData;
        foreach (var radixPoint in radix.Positions)
        {
            var equatorial = radixPoint.Value.Equatorial;
            var horizontal = radixPoint.Value.Horizontal;
            var deviation = radixPoint.Value.Ecliptical.DeviationPosSpeed;
            var distance = radixPoint.Value.Ecliptical.DistancePosSpeed;
            var speed = radixPoint.Value.Ecliptical.MainPosSpeed.Speed;
            var altLongitude = radixPoint.Value.Ecliptical.MainPosSpeed.Position + offset;
            if (altLongitude > 360.0) altLongitude -= 360.0;
            var main = new PosSpeed(altLongitude, speed);
            PointPosSpeeds newEcliptical = new(main, deviation, distance);
            FullPointPos newPos = new(newEcliptical, equatorial, horizontal);
            altPositions.Add(radixPoint.Key, newPos);
        }
        return new CalculatedChart(altPositions, inputtedChartData, obliquity);
    }

}