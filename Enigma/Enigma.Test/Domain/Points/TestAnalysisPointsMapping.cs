// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;

namespace Enigma.Test.Domain.Points;

[TestFixture]
public class TestCelPointToAnalysisPointMap
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestMapToAnalysisPointLongitude()
    {
        double longPos = 12.34;
        var celPoint = CelPoints.Sun;
        var pointGroup = PointGroups.CelPoints;
        var coordSystem = CoordinateSystems.Ecliptical;
        bool mainCoord = true;
        ICelPointToAnalysisPointMap pointMap = new CelPointToAnalysisPointMap();

        PosSpeed emptyPS = new(0.0, 0.0);
        PosSpeed longPS = new(longPos, 0.0);
        HorizontalCoordinates horcoord = new(0.0, 0.0);
        FullCelPointPos fullCelPointPos = CreateFullCelPointPos(celPoint, longPS, emptyPS, emptyPS, emptyPS, emptyPS, horcoord);
        AnalysisPoint resultingPoint = pointMap.MapToAnalysisPoint(fullCelPointPos, pointGroup, coordSystem, mainCoord);
        Assert.Multiple(() =>
        {
            Assert.That(resultingPoint.PointGroup, Is.EqualTo(PointGroups.CelPoints));
            Assert.That(resultingPoint.Position, Is.EqualTo(longPos).Within(_delta));
            Assert.That(resultingPoint.ItemId, Is.EqualTo((int)celPoint));
        });
    }

    [Test]
    public void TestMapToAnalysisPointDeclination()
    {
        double declPos = -8.88;
        var celPoint = CelPoints.Mars;
        var pointGroup = PointGroups.CelPoints;
        var coordSystem = CoordinateSystems.Equatorial;
        bool mainCoord = false;
        ICelPointToAnalysisPointMap pointMap = new CelPointToAnalysisPointMap();

        PosSpeed emptyPS = new(0.0, 0.0);
        PosSpeed declPS = new(declPos, 0.0);
        HorizontalCoordinates horcoord = new(0.0, 0.0);
        FullCelPointPos fullCelPointPos = CreateFullCelPointPos(celPoint, emptyPS, emptyPS, emptyPS, declPS, emptyPS, horcoord);
        AnalysisPoint resultingPoint = pointMap.MapToAnalysisPoint(fullCelPointPos, pointGroup, coordSystem, mainCoord);
        Assert.Multiple(() =>
        {
            Assert.That(resultingPoint.PointGroup, Is.EqualTo(PointGroups.CelPoints));
            Assert.That(resultingPoint.Position, Is.EqualTo(declPos).Within(_delta));
            Assert.That(resultingPoint.ItemId, Is.EqualTo((int)celPoint));
        });
    }

    private static FullCelPointPos CreateFullCelPointPos(CelPoints ssPoint, PosSpeed longPS, PosSpeed latPS, PosSpeed raPS, PosSpeed declPS, PosSpeed distPS, HorizontalCoordinates horCoord)
    {
        FullPointPos pointPos = new(longPS, latPS, raPS, declPS, horCoord);
        return new(ssPoint, distPS, pointPos);
    }

}


[TestFixture]
public class TestMundanePointToAnalysisPointMap
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestMapToAnalysisPointLongitude()
    {
        double longPos = 118.987;
        var pointGroup = PointGroups.MundanePoints;
        var coordSystem = CoordinateSystems.Ecliptical;
        var mPoint = MundanePoints.Ascendant;
        bool mainCoord = true;
        EquatorialCoordinates eqCoord = new(0.0, 0.0);
        HorizontalCoordinates horCoord = new(0.0, 0.0);
        CuspFullPos cuspPos = new("Cusp 5", longPos, eqCoord, horCoord);

        IMundanePointToAnalysisPointMap pointMap = new MundanePointToAnalysisPointMap();
        AnalysisPoint resultingPoint = pointMap.MapToAnalysisPoint(mPoint, cuspPos, pointGroup, coordSystem, mainCoord);
        Assert.Multiple(() =>
        {
            Assert.That(resultingPoint.PointGroup, Is.EqualTo(PointGroups.MundanePoints));
            Assert.That(resultingPoint.Position, Is.EqualTo(longPos).Within(_delta));
            Assert.That(resultingPoint.ItemId, Is.EqualTo((int)MundanePoints.Ascendant));
        });
    }
}