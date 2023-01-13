// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Domain.Interfaces;
using Enigma.Core.Domain.Points;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.ChartItems.Coordinates;
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
        FullChartPointPos fullCelPointPos = CreateFullCelPointPos();
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCelPointPos, CoordinateSystems.Ecliptical, true);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_longitude).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestSinglePointLatitude()
    {
        FullChartPointPos fullCelPointPos = CreateFullCelPointPos();
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCelPointPos, CoordinateSystems.Ecliptical, false);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_latitude).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestSinglePointRightAscension()
    {
        FullChartPointPos fullCelPointPos = CreateFullCelPointPos();
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCelPointPos, CoordinateSystems.Equatorial, true);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_rightAscension).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestSinglePointDeclination()
    {
        FullChartPointPos fullCelPointPos = CreateFullCelPointPos();
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCelPointPos, CoordinateSystems.Equatorial, false);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_declination).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestSinglePointAzimuth()
    {
        FullChartPointPos fullCelPointPos = CreateFullCelPointPos();
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCelPointPos, CoordinateSystems.Horizontal, true);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_azimuth).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestSinglePointAltitude()
    {
        FullChartPointPos fullCelPointPos = CreateFullCelPointPos();
        PositionedPoint posPoint = _mapping.MapFullPointPos2PositionedPoint(fullCelPointPos, CoordinateSystems.Horizontal, false);
        Assert.Multiple(() =>
        {
            Assert.That(posPoint.Position, Is.EqualTo(_altitude).Within(_delta));
            Assert.That(posPoint.Point, Is.EqualTo(ChartPoints.Jupiter));
        });
    }

    [Test]
    public void TestMultiplePoints()
    {
        List<FullChartPointPos> fcpPositions = new()
        {
            CreateFullCelPointPos(),
            CreateFullCelPointPos()
        };
        List<PositionedPoint> posPoints = _mapping.MapFullPointPos2PositionedPoint(fcpPositions, CoordinateSystems.Equatorial, true);
        Assert.Multiple(() =>
        {
            Assert.That(posPoints, Has.Count.EqualTo(2));
            Assert.That(posPoints[1].Point, Is.EqualTo(ChartPoints.Jupiter));
            Assert.That(posPoints[0].Position, Is.EqualTo(_rightAscension).Within(_delta));
        });
    }


    private FullChartPointPos CreateFullCelPointPos()
    {

        ChartPoints celPoint = ChartPoints.Jupiter;
        PosSpeed psDistance = new(1.0, 1.5);
        PosSpeed psLongitude = new (_longitude, 2.5);
        PosSpeed psLatitude = new (_latitude, 3.5);
        PosSpeed psRightAscension = new(_rightAscension, 4.5);
        PosSpeed psDeclination = new(_declination, 5.5);
        HorizontalCoordinates hcAzimuthAltitude = new(_azimuth, _altitude);
        FullPointPos fullPointPos = new(psLongitude, psLatitude, psRightAscension, psDeclination, hcAzimuthAltitude); 
        return new FullChartPointPos(celPoint, psDistance, fullPointPos);
    }
}