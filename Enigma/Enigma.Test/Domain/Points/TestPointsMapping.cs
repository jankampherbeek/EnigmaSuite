// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Test.Domain.Points;

[TestFixture]
public class TestPointsMapping
{
    private const double Delta = 0.00000001;
    private readonly IPointsMapping _mapping = new PointsMapping();
    private const double Longitude = 2.0;
    private const double Latitude = 3.0;
    private const double RightAscension = 4.0;
    private const double Declination = 5.0;
    private const double Azimuth = 6.0;
    private const double Altitude = 6.5;

    [Test]
    public void TestSinglePointLongitude()
    {
        KeyValuePair<ChartPoints, FullPointPos> fullCommonPos = new(ChartPoints.Jupiter, CreateFullPointPos());
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCommonPos, CoordinateSystems.Ecliptical, true);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(Longitude).Within(Delta));
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
            Assert.That(posPoint.Position, Is.EqualTo(Latitude).Within(Delta));
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
            Assert.That(posPoint.Position, Is.EqualTo(RightAscension).Within(Delta));
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
            Assert.That(posPoint.Position, Is.EqualTo(Declination).Within(Delta));
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
            Assert.That(posPoint.Position, Is.EqualTo(Azimuth).Within(Delta));
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
            Assert.That(posPoint.Position, Is.EqualTo(Altitude).Within(Delta));
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
            Assert.That(posPoints[0].Position, Is.EqualTo(RightAscension).Within(Delta));
        });
    }


    private static FullPointPos CreateFullPointPos()
    {
        PosSpeed psDistance = new(1.0, 1.5);
        PosSpeed psLongitude = new(Longitude, 2.5);
        PosSpeed psLatitude = new(Latitude, 3.5);
        PosSpeed psRightAscension = new(RightAscension, 4.5);
        PosSpeed psDeclination = new(Declination, 5.5);
        PosSpeed psAzimuth = new(Azimuth, 0.0);
        PosSpeed psAltitude = new(Altitude, 0.0);
        PointPosSpeeds ppsEcliptical = new(psLongitude, psLatitude, psDistance);
        PointPosSpeeds ppsEquatorial = new(psRightAscension, psDeclination, psDistance);
        PointPosSpeeds ppsHorizontal = new(psAzimuth, psAltitude, psDistance);
        return new FullPointPos(ppsEcliptical, ppsEquatorial, ppsHorizontal);
    }
}