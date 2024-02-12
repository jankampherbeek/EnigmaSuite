// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Enigma.Frontend.Ui.Graphics;

public interface IChartsWheelCircles
{
    public List<Ellipse> CreateCircles(ChartsWheelMetrics metrics);
    public List<Line> CreateDegreeLines(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant);
}

public sealed class ChartsWheelCircles : IChartsWheelCircles
{

    public List<Ellipse> CreateCircles(ChartsWheelMetrics metrics)
    {
        DimCircle dimCircle = new(metrics.BaseSize, metrics.SizeFactor);
        List<Ellipse> allCircles = new()
        {  // todo 0.3 move definitions for colors to metrics
            dimCircle.CreateCircle(metrics.OuterRadius, 0.0, Colors.AliceBlue, Colors.White),
            dimCircle.CreateCircle(metrics.OuterSignRadius, metrics.StrokeSize, Colors.PaleTurquoise, Colors.MediumBlue),
            dimCircle.CreateCircle(metrics.OuterHouseRadius, metrics.StrokeSize, Colors.FloralWhite, Colors.MediumBlue),
            dimCircle.CreateCircle(metrics.OuterAspectRadius, metrics.StrokeSize, Colors.AliceBlue, Colors.MediumBlue)
        };
        return allCircles;
    }

    public List<Line> CreateDegreeLines(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant)
    {
        double offsetAsc = 30.0 - longAscendant % 30.0;
        DimDegreeIndications dimDegreeIndications = new(centerPoint, offsetAsc, metrics.OuterHouseRadius, metrics.DegreesRadius, metrics.Degrees5Radius);
        return dimDegreeIndications.CreateDegreeIndications();
    }
}