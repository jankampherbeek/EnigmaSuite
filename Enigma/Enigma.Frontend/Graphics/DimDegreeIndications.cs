// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Enigma.Frontend.Ui.Graphics;

internal sealed class DimDegreeIndications
{
    private readonly Point _centerPoint;
    private readonly double _offsetAsc;
    private readonly double _hypoStart;
    private readonly double _hypoDegree;
    private readonly double _hypo5Degree;
    public DimDegreeIndications(Point centerPoint, double offsetAsc, double hypoStart, double hypoDegree, double hypo5Degree)  // todo 0.3 use metrics as a parameter
    {
        _centerPoint = centerPoint;
        _offsetAsc = offsetAsc;
        _hypoStart = hypoStart;
        _hypoDegree = hypoDegree;
        _hypo5Degree = hypo5Degree;
    }

    public List<Line> CreateDegreeIndications()
    {
        List<Line> degreeIndications = new();
        double angle = _offsetAsc;
        for (int i = 0; i < 360; i++)
        {
            double actualHypo = (i % 5 == 0) ? _hypo5Degree : _hypoDegree;
            Point point1 = new DimPoint(_centerPoint).CreatePoint(angle, actualHypo);
            Point point2 = new DimPoint(_centerPoint).CreatePoint(angle, _hypoStart);
            degreeIndications.Add(DimLine.CreateLine(point1, point2, 1, Colors.MediumBlue, 1.0));
            angle += 1.0;
            if (angle >= 360.0) angle -= 360.0;
        }
        return degreeIndications;
    }
}