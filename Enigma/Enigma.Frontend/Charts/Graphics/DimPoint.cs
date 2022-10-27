// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Util;
using System;
using System.Windows;


namespace Enigma.Frontend.Charts.Graphics;

/// <summary>
/// Point with the correct X-Y-coördinates. Is used by shapes to define their positions on the canvas.
/// </summary>
internal class DimPoint
{
    private readonly Point _centerPoint;

    /// <summary>
    /// Constructs a DimPoint class.
    /// </summary>
    /// <param name="centerPoint">The central referencepoint that is exactly in the middle of the graphic.</param>
    public DimPoint(Point centerPoint)
    {
        _centerPoint = centerPoint;
    }

    /// <summary>
    /// Create a point with then correct coördinates.
    /// </summary>
    /// <param name="angle">Angle in degrees, starting from zero at the top and running counter-clockwise.</param>
    /// <param name="hypothenusa">The radius of the (fictitious) circle that defines the distance of the point from the center point.</param>
    /// <returns>The constructed point.</returns>
    public Point CreatePoint(double angle, double hypothenusa)
    {
        double x = _centerPoint.X - (Math.Sin(MathExtra.DegToRad(angle)) * hypothenusa);
        double y = _centerPoint.Y - (Math.Cos(MathExtra.DegToRad(angle)) * hypothenusa);
        return new Point(x, y);
    }


}






