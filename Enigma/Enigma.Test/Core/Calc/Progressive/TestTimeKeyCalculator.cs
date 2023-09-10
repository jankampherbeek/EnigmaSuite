// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Progressive;
using Enigma.Core.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Moq;

namespace Enigma.Test.Core.Calc.Progressive;


[TestFixture]
public class TestFixedTimeKey
{
    private readonly IFixedTimeKey _timeKey = new FixedTimeKey();
    private const double DAYS = 32.5;
    private const double KEY_LENGTH = 0.99;
    private const double ARC = 32.175;    // _days * _keyLength;
    private const double DELTA = 0.00000001;

    [Test]
    public void TestArcFromDays()
    {
        double actual = _timeKey.ArcFromDays(DAYS, KEY_LENGTH);
        Assert.That(actual, Is.EqualTo(ARC).Within(DELTA));
    }

    [Test]
    public void TestDaysFromArc()
    {
        double actual = _timeKey.DaysFromArc(ARC, KEY_LENGTH);
        Assert.That(actual, Is.EqualTo(DAYS).Within(DELTA));
    }

}


[TestFixture]
public class TestPlacidusTimeKey
{
    private const double ARC = 38.5;
    private const double DAYS = 38.0;
    private const double JD_RADIX = 2200000;
    private const int FLAGS = 258;
    private readonly Location _location = new("", 0.0, 0.0);
    private PointPosSpeeds? _jdRadixEclSunPos;
    private PointPosSpeeds? _jdRadixEquSunPos;
    private PointPosSpeeds? _jdRadixHorSunPos;
    private FullPointPos? _progPosSun;
    private const double DELTA = 0.00000001;
    private const double ZERO = 0.0;
    private IPlacidusTimeKey? _placidusTimeKey;

    [SetUp]
    public void SetUp()
    {
        _jdRadixEclSunPos = new PointPosSpeeds(new[] { 222.0, 1.01, ZERO, ZERO, ZERO, ZERO });
        _jdRadixEquSunPos = new PointPosSpeeds(new[] { 221.0, 1.02, ZERO, ZERO, ZERO, ZERO });
        _jdRadixHorSunPos = new PointPosSpeeds(new[] { ZERO, ZERO, ZERO, ZERO, ZERO, ZERO });
        _progPosSun = new FullPointPos(_jdRadixEclSunPos, _jdRadixEquSunPos, _jdRadixHorSunPos);
        _placidusTimeKey = new PlacidusTimeKey(CreateSolarArcCalculatorMock(), CreateSeFlagsMock(), CreatePositionFinderMock());
    }

    [Test]
    public void TestArcFromDays()
    {
        double actual = _placidusTimeKey!.ArcFromDays(DAYS, JD_RADIX, CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, _location);
        Assert.That(actual, Is.EqualTo(ARC).Within(DELTA));
    }

    [Test]
    public void TestDaysFromArc()
    {
        // TODO fix TestDaysFromArc
     //   double actual = _placidusTimeKey!.DaysFromArc(JdRadix, _progPosSun!, CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, _location);
     //   Assert.That(actual, Is.EqualTo(Days).Within(Delta));
    }

    private static IPositionFinder CreatePositionFinderMock()
    {
        var mockPosFinder = new Mock<IPositionFinder>();
        return mockPosFinder.Object;
    }

    private ISolarArcCalculator CreateSolarArcCalculatorMock()
    {
        var mockCalc = new Mock<ISolarArcCalculator>();
        mockCalc.Setup(p => p.CalcSolarArcForTimespan(It.IsAny<double>(), It.IsAny<double>(), _location, It.IsAny<int>())).Returns(ARC);
        return mockCalc.Object;
    }

    private static ISeFlags CreateSeFlagsMock()
    {
        var mockFlags = new Mock<ISeFlags>();
        mockFlags.Setup(p => p.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical)).Returns(FLAGS);
        return mockFlags.Object;
    }

}


[TestFixture]
public class TestSolarArcCalculator
{
    private const double DELTA = 0.00000001;
    private const double JD_RADIX = 2000000;
    private const double JD_EVENT = 2000038;
    private const double TIMESPAN = 38.0;
    private readonly Location _location = new("Anywhere", 0.0, 0.0);
    private const int FLAGS = 258;
    private readonly PosSpeed[] _jdRadixEclSunPos = new PosSpeed[3];
    private readonly PosSpeed[] _jdEventEclSunPos = new PosSpeed[3];
    private ISolarArcCalculator? _calculator;


    [SetUp]
    public void SetUp()
    {
        PosSpeed emptyPosSpeed = new(0.0, 0.0);
        _jdRadixEclSunPos[0] = new PosSpeed(222.0, 1.01);
        _jdRadixEclSunPos[1] = emptyPosSpeed;
        _jdRadixEclSunPos[2] = emptyPosSpeed;
        _jdEventEclSunPos[0] = new PosSpeed(260.1, 0.58);
        _jdEventEclSunPos[1] = emptyPosSpeed;
        _jdEventEclSunPos[2] = emptyPosSpeed;
        _calculator = new SolarArcCalculator(CreateCelPointSeCalcMock());

    }


    [Test]
    public void TestCalcSolarArcForTimespan()
    {
        const double expected = 38.1;
        double actual = _calculator!.CalcSolarArcForTimespan(JD_RADIX, TIMESPAN, _location, FLAGS);
        Assert.That(actual, Is.EqualTo(expected).Within(DELTA));

    }

    private ICelPointSeCalc CreateCelPointSeCalcMock()
    {
        var mockCalc = new Mock<ICelPointSeCalc>();
        mockCalc.Setup(p => p.CalculateCelPoint(ChartPoints.Sun, JD_RADIX, _location, FLAGS)).Returns(_jdRadixEclSunPos);
        mockCalc.Setup(p => p.CalculateCelPoint(ChartPoints.Sun, JD_EVENT, _location, FLAGS)).Returns(_jdEventEclSunPos);
        return mockCalc.Object;
    }

}



