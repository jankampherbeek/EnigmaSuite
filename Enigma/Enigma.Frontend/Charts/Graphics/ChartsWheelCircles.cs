// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Enigma.Frontend.Charts.Graphics;

public interface IChartsWheelCircles
{
    public List<Ellipse> CreateCircles(ChartsWheelMetrics metrics);
    public List<Line> CreateDegreeLines(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant);
}

public class ChartsWheelCircles: IChartsWheelCircles
{

    public List<Ellipse> CreateCircles(ChartsWheelMetrics metrics)
    {
        DimCircle dimCircle = new(metrics.BaseSize, metrics.SizeFactor);
        List<Ellipse> allCircles = new()
        {  // todo move definitions for colors to metrics
            dimCircle.CreateCircle(metrics.OuterRadius, 0.0, Colors.AliceBlue, Colors.White),
            dimCircle.CreateCircle(metrics.OuterSignRadius, metrics.StrokeSize, Colors.PaleTurquoise, Colors.CornflowerBlue),
            dimCircle.CreateCircle(metrics.OuterHouseRadius, metrics.StrokeSize, Colors.AntiqueWhite, Colors.CornflowerBlue),
            dimCircle.CreateCircle(metrics.OuterAspectRadius, metrics.StrokeSize, Colors.AliceBlue, Colors.CornflowerBlue)
        };
        return allCircles;
    }

    public List<Line> CreateDegreeLines(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant)
    {
        double offsetAsc = 30.0 - (longAscendant % 30.0);
        DimDegreeIndications dimDegreeIndications = new(centerPoint, offsetAsc, metrics.OuterHouseRadius, metrics.DegreesRadius, metrics.Degrees5Radius);
        return dimDegreeIndications.CreateDegreeIndications();
    }
}