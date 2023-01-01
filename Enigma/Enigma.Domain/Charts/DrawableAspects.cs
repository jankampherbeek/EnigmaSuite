// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Points;

namespace Enigma.Domain.Charts;

/// <summary>Aspect, between two celestial points, to be shown in a chart wheel.</summary>
public record DrawableCelPointAspect
{
    public CelPoints Point1 { get; }
    public CelPoints Point2 { get; }
    public double Exactness { get; }
    public AspectTypes AspectType { get; }

    /// <summary>
    /// Constructor for a drawable aspect between 2 celestial points.
    /// </summary>
    /// <param name="point1">The first celestial point.</param>
    /// <param name="point2">The second celestial point.</param>
    /// <param name="exactness">The exactness (unused fraction of the max. orb) as a percentage.</param>
    /// <param name="aspectType">The aspect type (conjunction, opposition etc.).</param>
    public DrawableCelPointAspect(CelPoints point1, CelPoints point2, double exactness, AspectTypes aspectType)
    {
        Point1 = point1;
        Point2 = point2;
        Exactness = exactness;
        AspectType = aspectType;
    }
}


/// <summary>Aspect between a mundane point and a celestial point, that can be shown in a wheel.</summary>
public record DrawableMundaneAspect
{
    public string MundanePoint { get; }
    public CelPoints CelPoint { get; }
    public double Exactness { get; }
    public AspectTypes AspectType { get; }

    /// <summary>
    /// Constructor for a drawable aspect between a mundane point and a celestial point. 
    /// </summary>
    /// <param name="mundanePoint"></param>
    /// <param name="celPoint">The celestial point point.</param>
    /// <param name="exactness">The exactness (unused fraction of the max. orb) as a percentage.</param>
    /// <param name="aspectType">The aspect type (conjunction, opposition etc.).</param>
    public DrawableMundaneAspect(string mundanePoint, CelPoints celPoint, double exactness, AspectTypes aspectType)
    {
        MundanePoint = mundanePoint;
        CelPoint = celPoint;
        Exactness = exactness;
        AspectType = aspectType;
    }
}


/// <summary>X-Y-coordinates for the start, or end, of a drawable aspect-line.</summary>
/// <remarks>The coordinates are for a celestial point.</remarks>
public record DrawableAspectCoordinatesSs
{
    public CelPoints CelPoint { get; }
    public double XCoordinate { get; }
    public double YCoordinate { get; }

    /// <summary>
    /// Constructor for the coordinates of one side of an aspect line.
    /// </summary>
    /// <param name="celPoint">The celestial point at one side of the aspect line.</param>
    /// <param name="xCoordinate">Value for X.</param>
    /// <param name="yCoordinate">Value for Y.</param>
    public DrawableAspectCoordinatesSs(CelPoints celPoint, double xCoordinate, double yCoordinate)
    {
        CelPoint = celPoint;
        XCoordinate = xCoordinate;
        YCoordinate = yCoordinate;
    }
}


/// <summary>X-Y-coordinates for the start, or end, of a drawable aspect-line.</summary>
/// <remarks>The coordinates are for a mundane point.</remarks>
public record DrawableAspectCoordinatesMu
{
    public string MundanePoint { get; }
    public double XCoordinate { get; }
    public double YCoordinate { get; }

    /// <summary>
    /// Constructor for the coordinates of one side of an aspect line.
    /// </summary>
    /// <param name="mundanePoint">The mundane point at one side of the aspect line.</param>
    /// <param name="xCoordinate">Value for X.</param>
    /// <param name="yCoordinate">Value for Y.</param>
    public DrawableAspectCoordinatesMu(string mundanePoint, double xCoordinate, double yCoordinate)
    {
        MundanePoint = mundanePoint;
        XCoordinate = xCoordinate;
        YCoordinate = yCoordinate;
    }
}




