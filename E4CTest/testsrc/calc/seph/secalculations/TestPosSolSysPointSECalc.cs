// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using domain.shared;
using E4C.calc.seph.secalculations;
using E4C.calc.seph.sefacade;
using E4C.domain.shared.positions;
using E4C.domain.shared.references;
using E4C.domain.shared.specifications;
using E4C.Models.Domain;
using Moq;
using NUnit.Framework;

namespace E4CTest.calc.seph.secalculations;

[TestFixture]
public class TestPositionSolSysPointCalc
{

    [Test]
    public void TestCalculateSolSysPointEcliptical()
    {
        double _delta = 0.00000001;
        FullSolSysPointPos _result = CalculateFullSolSysPointPos();
        Assert.AreEqual(100.0, _result.Longitude.Position, _delta);
        Assert.AreEqual(-2.0, _result.Latitude.Position, _delta);
    }

    [Test]
    public void TestCalculateSolSysPointEquatorial()
    {
        double _delta = 0.00000001;
        FullSolSysPointPos _result = CalculateFullSolSysPointPos();
        Assert.AreEqual(99.0, _result.RightAscension.Position, _delta);
        Assert.AreEqual(-1.0, _result.Declination.Position, _delta);
    }

    [Test]
    public void TestCalculateSolSysPointDistance()
    {
        double _delta = 0.00000001;
        FullSolSysPointPos _result = CalculateFullSolSysPointPos();
        Assert.AreEqual(3.3, _result.Distance.Position, _delta);
        Assert.AreEqual(0.003, _result.Distance.Speed, _delta);
    }

    [Test]
    public void TestCalculateSolSysPointHorizontal()
    {
        double _delta = 0.00000001;
        FullSolSysPointPos _result = CalculateFullSolSysPointPos();
        Assert.AreEqual(66.6, _result.AzimuthAltitude.Azimuth, _delta);
        Assert.AreEqual(45.0, _result.AzimuthAltitude.Altitude, _delta);
    }

    private FullSolSysPointPos CalculateFullSolSysPointPos()
    {
        double _julianDayUt = 2123456.5;
        int _flagsEcliptical = 0;
        int _flagsEquatorial = 1;
        var _eclipticalPositions = new double[] { 100.0, -2.0, 3.3, 0.5, -0.1, 0.003 };
        var _equatorialPositions = new double[] { 99.0, -1.0, 3.3, 0.51, -0.09, 0.003 };
        var _location = new Location("", 52.0, 6.0);
        var _mockCelPointCalc = new Mock<ISePosCelPointFacade>();
        _mockCelPointCalc.Setup(p => p.PosCelPointFromSe(_julianDayUt, Constants.SE_MARS, _flagsEcliptical)).Returns(_eclipticalPositions);
        _mockCelPointCalc.Setup(p => p.PosCelPointFromSe(_julianDayUt, Constants.SE_MARS, _flagsEquatorial)).Returns(_equatorialPositions);
        var _mockHorCoordCalc = new Mock<IHorizontalCoordinatesFacade>();
        _mockHorCoordCalc.Setup(p => p.CalculateHorizontalCoordinates(_julianDayUt, new double[] { 52.0, 6.0, 0.0 }, new double[] { 100.0, -2.0, 3.3 }, _flagsEcliptical)).
            Returns(new HorizontalPos(66.6, 45.0));
        var _mockSolSysPointSpecs = new Mock<ISolarSystemPointSpecifications>();
        _mockSolSysPointSpecs.Setup(p => p.DetailsForPoint(SolarSystemPoints.Mars)).
            Returns(new SolarSystemPointDetails(SolarSystemPoints.Mars, SolSysPointCats.Classic, CalculationTypes.SE, Constants.SE_MARS, true, true, "solSysPointMars"));
        PositionSolSysPointSECalc _calc = new(_mockCelPointCalc.Object, _mockHorCoordCalc.Object, _mockSolSysPointSpecs.Object);
        return _calc.CalculateSolSysPoint(SolarSystemPoints.Mars, _julianDayUt, _location, _flagsEcliptical, _flagsEquatorial);
    }
}