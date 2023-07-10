// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Domain.Points;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;

namespace Enigma.Test.Domain.Points;

[TestFixture]
public class TestPointsMapping
{
    private readonly double _delta = 0.00000001;
    private readonly IPointsMapping _mapping = new PointsMapping();
    private readonly double _longitude = 2.0;
    private readonly double _latitude = 3.0;
    private readonly double _rightAscension = 4.0;
    private readonly double _declination = 5.0;
    private readonly double _azimuth = 6.0;
    private readonly double _altitude = 6.5;

    [Test]
    public void TestSinglePointLongitude()
    {
        KeyValuePair<ChartPoints, FullPointPos> fullCommonPos = new(ChartPoints.Jupiter, CreateFullPointPos());
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCommonPos, CoordinateSystems.Ecliptical, true);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_longitude).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestSinglePointLatitude()
    {
        KeyValuePair<ChartPoints, FullPointPos> fullCommonPos = new(ChartPoints.Jupiter, CreateFullPointPos());
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCommonPos, CoordinateSystems.Ecliptical, false);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_latitude).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestSinglePointRightAscension()
    {
        KeyValuePair<ChartPoints, FullPointPos> fullCommonPos = new(ChartPoints.Jupiter, CreateFullPointPos());
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCommonPos, CoordinateSystems.Equatorial, true);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_rightAscension).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestSinglePointDeclination()
    {
        KeyValuePair<ChartPoints, FullPointPos> fullCommonPos = new(ChartPoints.Jupiter, CreateFullPointPos());
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCommonPos, CoordinateSystems.Equatorial, false);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_declination).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestSinglePointAzimuth()
    {
        KeyValuePair<ChartPoints, FullPointPos> fullCommonPos = new(ChartPoints.Jupiter, CreateFullPointPos());
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCommonPos, CoordinateSystems.Horizontal, true);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_azimuth).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestSinglePointAltitude()
    {
        KeyValuePair<ChartPoints, FullPointPos> fullCommonPos = new(ChartPoints.Jupiter, CreateFullPointPos());
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCommonPos, CoordinateSystems.Horizontal, false);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_altitude).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestMultiplePoints()
    {
        Dictionary<ChartPoints, FullPointPos> positions = new()
        {
            { ChartPoints.Mars, CreateFullPointPos() },
            { ChartPoints.Jupiter, CreateFullPointPos() }
        };

        List<PositionedPoint> posPoints = _mapping.MapFullPointPos2PositionedPoint(positions, CoordinateSystems.Equatorial, true);
        Assert.Multiple(() =>
        {
            Assert.That(posPoints, Has.Count.EqualTo(2));
            Assert.That(posPoints[1].Point, Is.EqualTo(ChartPoints.Jupiter));
            Assert.That(posPoints[0].Position, Is.EqualTo(_rightAscension).Within(_delta));
        });
    }


    private FullPointPos CreateFullPointPos()
    {
        PosSpeed psDistance = new(1.0, 1.5);
        PosSpeed psLongitude = new(_longitude, 2.5);
        PosSpeed psLatitude = new(_latitude, 3.5);
        PosSpeed psRightAscension = new(_rightAscension, 4.5);
        PosSpeed psDeclination = new(_declination, 5.5);
        PosSpeed psAzimuth = new(_azimuth, 0.0);
        PosSpeed psAltitude = new(_altitude, 0.0);
        PointPosSpeeds ppsEcliptical = new(psLongitude, psLatitude, psDistance);
        PointPosSpeeds ppsEquatorial = new(psRightAscension, psDeclination, psDistance);
        PointPosSpeeds ppsHorizontal = new(psAzimuth, psAltitude, psDistance);
        return new FullPointPos(ppsEcliptical, ppsEquatorial, ppsHorizontal);
    }
}