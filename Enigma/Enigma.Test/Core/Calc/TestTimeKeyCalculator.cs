// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using FakeItEasy;

namespace Enigma.Test.Core.Calc;




[TestFixture]
public class TestSolarArcCalculator
{
    private const double DELTA = 0.00000001;
    private const double JD_RADIX = 2000000;
    private const double JD_EVENT = 2000038;
    private const double TIMESPAN = 38.0;
    private readonly Location? _location = new("Anywhere", 0.0, 0.0);
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
        var calcFake = A.Fake<ICelPointSeCalc>();
        A.CallTo(() => calcFake.CalculateCelPoint((int)ChartPoints.Sun, JD_RADIX, _location, FLAGS)).Returns(_jdRadixEclSunPos);
        A.CallTo(() => calcFake.CalculateCelPoint((int)ChartPoints.Sun, JD_EVENT, _location, FLAGS)).Returns(_jdEventEclSunPos);
        return calcFake;
    }

}



