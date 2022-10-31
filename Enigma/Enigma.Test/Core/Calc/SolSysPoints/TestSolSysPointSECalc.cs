// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.SolSysPoints;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Constants;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;
using Moq;

namespace EnigmaTest.Core.Calc.SolSysPoints;

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
    readonly int _flagsEcliptical = 0;

    [Test]
    public void TestCalculateSolSysPointLongitude()
    {
        PosSpeed[] _result = CalculatePosSpeedForSolSysPoint();
        Assert.Multiple(() =>
        {
            Assert.That(_result[0].Position, Is.EqualTo(_longitude).Within(_delta));
            Assert.That(_result[0].Speed, Is.EqualTo(_longSpeed).Within(_delta));
        });
    }

    [Test]
    public void TestCalculateSolSysPointLatitude()
    {
        PosSpeed[] _result = CalculatePosSpeedForSolSysPoint();
        Assert.Multiple(() =>
        {
            Assert.That(_result[1].Position, Is.EqualTo(_latitude).Within(_delta));
            Assert.That(_result[1].Speed, Is.EqualTo(_latSpeed).Within(_delta));
        });
    }

    [Test]
    public void TestCalculateSolSysPointDistance()
    {
        PosSpeed[] _result = CalculatePosSpeedForSolSysPoint();
        Assert.Multiple(() =>
        {
            Assert.That(_result[2].Position, Is.EqualTo(_distance).Within(_delta));
            Assert.That(_result[2].Speed, Is.EqualTo(_distSpeed).Within(_delta));
        });
    }

    private PosSpeed[] CalculatePosSpeedForSolSysPoint()
    {
        var _location = new Location("", 52.0, 6.0);
        var _mockSolSysPointSpecs = new Mock<ISolarSystemPointSpecifications>();
        _mockSolSysPointSpecs.Setup(p => p.DetailsForPoint(SolarSystemPoints.Mars)).
            Returns(new SolarSystemPointDetails(SolarSystemPoints.Mars, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_MARS, true, true, "solSysPointMars", "f"));
        var _mockCalcUtFacade = new Mock<ICalcUtFacade>();
        _mockCalcUtFacade.Setup(p => p.PosCelPointFromSe(_julianDayUt, EnigmaConstants.SE_MARS, _flagsEcliptical)).Returns(new double[] { _longitude, _latitude, _distance, _longSpeed, _latSpeed, _distSpeed });
        ISolSysPointSECalc _calc = new SolSysPointSECalc(_mockCalcUtFacade.Object, _mockSolSysPointSpecs.Object);
        return _calc.CalculateSolSysPoint(SolarSystemPoints.Mars, _julianDayUt, _location, _flagsEcliptical);
    }

}