// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Enigma.Frontend.Ui.Charts.Graphics;

/// <summary>
/// A graphic line with the correct coordinates, color and linewidth.
/// </summary>
internal class DimLine
{

    /// <summary>
    /// Create a drawable line.
    /// </summary>
    /// <param name="point1">First position</param>
    /// <param name="point2">Second position</param>
    /// <param name="lineWidth">Width in logical pixels</param>
    /// <param name="lineColor">The color for the line</param>
    /// <param name="opacity">The opacity of the line in values from 0 .. 1</param>
    /// <returns></returns>
    public Line CreateLine(Point point1, Point point2, double lineWidth, Color lineColor, double opacity)
    {
        Line line = new()
        {
            X1 = point1.X,
            X2 = point2.X,
            Y1 = point1.Y,
            Y2 = point2.Y,
            Stroke = new SolidColorBrush(lineColor),
            StrokeThickness = lineWidth,
            Opacity = opacity
        };
        return line;
    }
}

