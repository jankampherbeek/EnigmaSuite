// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Enigma.Frontend.Charts.Graphics;

/// <summary>
/// Dimensioned circle, with all metrics and colors defined, that can be shown in a graphic wheel.
/// </summary>
internal class DimCircle
{
    private readonly double _defaultCanvasSize;
    private readonly double _sizeFactor;

    /// <summary>
    /// Construct a dimensioned circle class.
    /// </summary>
    /// <param name="defaultCanvasSize">The initial size of the canvas, without any change in sizes.</param>
    /// <param name="sizeFactor">The current size as related to the defaultCanvasSize, expressed as a factor 0 .. 1.</param>
    public DimCircle(double defaultCanvasSize, double sizeFactor)
    {
        _defaultCanvasSize = defaultCanvasSize;
        _sizeFactor = sizeFactor;
    }

    /// <summary>
    /// Create a circle (specific form of an ellipse) that can be shown as graphic on a canvas. Sizes are in relation to the default canvas size.
    /// </summary>
    /// <param name="circleRadius">Radius of the circle to create.</param>
    /// <param name="strokeThickness">Width of the line of the circle.</param>
    /// <param name="fillColor">Color within the circle.</param>
    /// <param name="strokeColor">Color of the line of the circle.</param>
    /// <returns>Constructed circle.</returns>
    public Ellipse CreateCircle(double circleRadius, double strokeThickness, Color fillColor, Color strokeColor)
    {
        Ellipse circle = new()
        {
            Margin = new Thickness(((_defaultCanvasSize / 2) * _sizeFactor) - circleRadius),
            Width = circleRadius * 2,
            Height = circleRadius * 2,
            StrokeThickness = strokeThickness,
            Fill = new SolidColorBrush(fillColor),
            Stroke = new SolidColorBrush(strokeColor)
        };
        return circle;
    }
}

