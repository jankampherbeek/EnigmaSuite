// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Astron.SolSysPoints;
using E4C.Core.Facades;
using E4C.Core.Shared.Domain;
using E4C.Shared.Constants;
using E4C.Shared.Domain;
using E4C.Shared.References;
using Moq;
using NUnit.Framework;

namespace E4CTest.Core.Astron.SolSysPoints;

[TestFixture]
public class TestSolSysPointCalc
{
    private readonly double _julianDayUt = 2123456.5;
    private readonly double _longitude = 52.0;
    private readonly double _latitude = 3.0;
    private readonly double _distance = 2.0;
    private readonly double _longSpeed = 0.7;
    private readonly double _latSpeed = -0.1;
    private readonly double _distSpeed = 0.02;
    private readonly double _delta = 0.00000001;
    int _flagsEcliptical = 0;

    [Test]
    public void TestCalculateSolSysPointLongitude()
    {
        PosSpeed[] _result = CalculatePosSpeedForSolSysPoint();
        Assert.AreEqual(_longitude, _result[0].Position, _delta);
        Assert.AreEqual(_longSpeed, _result[0].Speed, _delta);
    }

    [Test]
    public void TestCalculateSolSysPointLatitude()
    {
        PosSpeed[] _result = CalculatePosSpeedForSolSysPoint();
        Assert.AreEqual(_latitude, _result[1].Position, _delta);
        Assert.AreEqual(_latSpeed, _result[1].Speed, _delta);
    }

    [Test]
    public void TestCalculateSolSysPointDistance()
    {
        PosSpeed[] _result = CalculatePosSpeedForSolSysPoint();
        Assert.AreEqual(_distance, _result[2].Position, _delta);
        Assert.AreEqual(_distSpeed, _result[2].Speed, _delta);
    }

    private PosSpeed[] CalculatePosSpeedForSolSysPoint()
    {
        var _location = new Location("", 52.0, 6.0);
        var _mockSolSysPointSpecs = new Mock<ISolarSystemPointSpecifications>();
        _mockSolSysPointSpecs.Setup(p => p.DetailsForPoint(SolarSystemPoints.Mars)).
            Returns(new SolarSystemPointDetails(SolarSystemPoints.Mars, SolSysPointCats.Classic, CalculationTypes.SE, Constants.SE_MARS, true, true, "solSysPointMars"));
        var _mockCalcUtFacade = new Mock<ICalcUtFacade>();
        _mockCalcUtFacade.Setup(p => p.PosCelPointFromSe(_julianDayUt, Constants.SE_MARS, _flagsEcliptical)).Returns(new double[] {_longitude, _latitude, _distance, _longSpeed, _latSpeed, _distSpeed});
        ISolSysPointSECalc _calc = new SolSysPointSECalc(_mockCalcUtFacade.Object, _mockSolSysPointSpecs.Object);
        return _calc.CalculateSolSysPoint(SolarSystemPoints.Mars, _julianDayUt, _location, _flagsEcliptical);
    }

}