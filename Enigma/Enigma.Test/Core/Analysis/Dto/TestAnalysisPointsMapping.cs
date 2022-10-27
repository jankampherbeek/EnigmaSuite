// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis.Dto;
using Enigma.Domain.CalcVars;
using Enigma.Domain.Positional;

namespace Enigma.Test.Core.Analysis.Dto;

[TestFixture]
public class TestSolSysPointToAnalysisPointMap
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestMapToAnalysisPointLongitude(){
        double longPos = 12.34;
        var solSysPoint = SolarSystemPoints.Sun;
        var pointGroup = PointGroups.SolarSystemPoints;
        var coordSystem = CoordinateSystems.Ecliptical;
        bool mainCoord = true;
        ISolSysPointToAnalysisPointMap pointMap = new SolSysPointToAnalysisPointMap();

        PosSpeed emptyPS = new(0.0, 0.0);
        PosSpeed longPS = new(longPos, 0.0);
        HorizontalCoordinates horcoord = new(0.0, 0.0);
        FullSolSysPointPos fullSolSysPointPos = CreateFullSolSysPointPos(solSysPoint, longPS, emptyPS, emptyPS, emptyPS, emptyPS, horcoord);
        AnalysisPoint resultingPoint = pointMap.MapToAnalysisPoint(fullSolSysPointPos, pointGroup, coordSystem, mainCoord);

        Assert.That(resultingPoint.PointGroup, Is.EqualTo(PointGroups.SolarSystemPoints));
        Assert.That(resultingPoint.Position, Is.EqualTo(longPos).Within(_delta));
        Assert.That(resultingPoint.ItemId, Is.EqualTo((int)solSysPoint));
    }

    [Test]
    public void TestMapToAnalysisPointDeclination()
    {
        double declPos = -8.88;
        var solSysPoint = SolarSystemPoints.Mars;
        var pointGroup = PointGroups.SolarSystemPoints;
        var coordSystem = CoordinateSystems.Equatorial;
        bool mainCoord = false;
        ISolSysPointToAnalysisPointMap pointMap = new SolSysPointToAnalysisPointMap();

        PosSpeed emptyPS = new(0.0, 0.0);
        PosSpeed declPS = new(declPos, 0.0);
        HorizontalCoordinates horcoord = new (0.0, 0.0);
        FullSolSysPointPos fullSolSysPointPos = CreateFullSolSysPointPos(solSysPoint, emptyPS, emptyPS, emptyPS, declPS, emptyPS, horcoord);
        AnalysisPoint resultingPoint = pointMap.MapToAnalysisPoint(fullSolSysPointPos, pointGroup, coordSystem, mainCoord);

        Assert.That(resultingPoint.PointGroup, Is.EqualTo(PointGroups.SolarSystemPoints));
        Assert.That(resultingPoint.Position, Is.EqualTo(declPos).Within(_delta));
        Assert.That(resultingPoint.ItemId, Is.EqualTo((int)solSysPoint));
    }

    private FullSolSysPointPos CreateFullSolSysPointPos(SolarSystemPoints ssPoint, PosSpeed longPS, PosSpeed latPS, PosSpeed raPS, PosSpeed declPS, PosSpeed distPS, HorizontalCoordinates horCoord)
    {
        return new(ssPoint, longPS, latPS, raPS, declPS, distPS, horCoord);
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
        CuspFullPos cuspPos = new(longPos, eqCoord, horCoord);

        IMundanePointToAnalysisPointMap pointMap = new MundanePointToAnalysisPointMap();
        AnalysisPoint resultingPoint = pointMap.MapToAnalysisPoint(mPoint, cuspPos, pointGroup, coordSystem, mainCoord);
        Assert.That(resultingPoint.PointGroup, Is.EqualTo(PointGroups.MundanePoints));
        Assert.That(resultingPoint.Position, Is.EqualTo(longPos).Within(_delta));
        Assert.That(resultingPoint.ItemId, Is.EqualTo((int)MundanePoints.Ascendant));
    }
}