// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Enigma.Core.Handlers.Calc.Progressive;
using Moq;
using Enigma.Domain.Calc.Progressive;

namespace Enigma.Test.Core.Handlers.Calc.Progressive;


[TestFixture]
public class TestFixedTimeKey
{
    private readonly IFixedTimeKey _timeKey = new FixedTimeKey();
    private readonly double _days = 32.5;
    private readonly double _keyLength = 0.99;
    private readonly double _arc = 32.175;    // _days * _keyLength;
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestArcFromDays()
    {
        double actual = _timeKey.ArcFromDays(_days, _keyLength);
        Assert.That(actual, Is.EqualTo(_arc).Within(_delta));
    }

    [Test]
    public void TestDaysFromArc()
    {
        double actual = _timeKey.DaysFromArc(_arc, _keyLength);
        Assert.That(actual, Is.EqualTo(_days).Within(_delta));
    }

}


[TestFixture]
public class TestPlacidusTimeKey
{

    private readonly double _arc = 38.5;
    private readonly double _days = 38.0;
    private readonly double _jdRadix = 2200000;
    private readonly int _flags = 258;
    private readonly Location _location = new("", 0.0, 0.0);
    private PointPosSpeeds _jdRadixEclSunPos;
    private PointPosSpeeds _jdRadixEquSunPos;
    private PointPosSpeeds _jdRadixHorSunPos;
    private FullPointPos _progPosSun;
    private readonly double _delta = 0.00000001;
    private readonly double _zero = 0.0;
    private IPlacidusTimeKey _placidusTimeKey;

    [SetUp]
    public void SetUp()
    {
        _jdRadixEclSunPos = new PointPosSpeeds(new double[] { 222.0, 1.01, _zero, _zero, _zero, _zero });
        _jdRadixEquSunPos = new PointPosSpeeds(new double[] { 221.0, 1.02, _zero, _zero, _zero, _zero });
        _jdRadixHorSunPos = new PointPosSpeeds(new double[] { _zero, _zero, _zero, _zero, _zero, _zero });
        _progPosSun = new FullPointPos(_jdRadixEclSunPos, _jdRadixEquSunPos, _jdRadixHorSunPos);
        _placidusTimeKey = new PlacidusTimeKey(CreateSolarArcCalculatorMock(), CreateSeFlagsMock(), CreatePositionFinderMock());
    }

    [Test]
    public void TestArcFromDays()
    {
        double actual = _placidusTimeKey.ArcFromDays(_days, _jdRadix, CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, _location);
        Assert.That(actual, Is.EqualTo(_arc).Within(_delta));
    }

    public void TestDaysFromArc()
    {
        double actual = _placidusTimeKey.DaysFromArc(_jdRadix, _progPosSun, CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, _location);
        Assert.That(actual, Is.EqualTo(_days).Within(_delta));
    }

    private IPositionFinder CreatePositionFinderMock()
    {
        var mockPosFinder = new Mock<IPositionFinder>();
        return mockPosFinder.Object;
    }

    private ISolarArcCalculator CreateSolarArcCalculatorMock()
    {
        var mockCalc = new Mock<ISolarArcCalculator>();
        mockCalc.Setup(p => p.CalcSolarArcForTimespan(It.IsAny<double>(), It.IsAny<double>(), _location, It.IsAny<int>())).Returns(_arc);
        return mockCalc.Object;
    }

    private ISeFlags CreateSeFlagsMock()
    {
        var mockFlags = new Mock<ISeFlags>();
        mockFlags.Setup(p => p.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical)).Returns(_flags);
        return mockFlags.Object;
    }

}


[TestFixture]
public class TestSolarArcCalculator
{
    private readonly double _delta = 0.00000001;
    private readonly double _jdRadix = 2000000;
    private readonly double _jdEvent = 2000038;
    private readonly double _timespan = 38.0;
    private readonly Location _location = new("Anywhere", 0.0, 0.0);
    private readonly int _flags = 258;
    private readonly PosSpeed[] jdRadixEclSunPos = new PosSpeed[3];
    private readonly PosSpeed[] jdEventEclSunPos = new PosSpeed[3];
    private ISolarArcCalculator _calculator;


    [SetUp]
    public void SetUp()
    {
        PosSpeed emptyPosSpeed = new(0.0, 0.0);
        jdRadixEclSunPos[0] = new(222.0, 1.01);
        jdRadixEclSunPos[1] = emptyPosSpeed;
        jdRadixEclSunPos[2] = emptyPosSpeed;
        jdEventEclSunPos[0] = new(260.1, 0.58);
        jdEventEclSunPos[1] = emptyPosSpeed;
        jdEventEclSunPos[2] = emptyPosSpeed;
        _calculator = new SolarArcCalculator(CreateCelPointSECalcMock());

    }


    [Test]
    public void TestCalcSolarArcForTimespan()
    {
        double expected = 38.1;
        double actual = _calculator.CalcSolarArcForTimespan(_jdRadix, _timespan, _location, _flags);
        Assert.That(actual, Is.EqualTo(expected).Within(_delta));

    }

    private ICelPointSECalc CreateCelPointSECalcMock()
    {
        var mockCalc = new Mock<ICelPointSECalc>();
        mockCalc.Setup(p => p.CalculateCelPoint(ChartPoints.Sun, _jdRadix, _location, _flags)).Returns(jdRadixEclSunPos);
        mockCalc.Setup(p => p.CalculateCelPoint(ChartPoints.Sun, _jdEvent, _location, _flags)).Returns(jdEventEclSunPos);
        return mockCalc.Object;
    }


}



