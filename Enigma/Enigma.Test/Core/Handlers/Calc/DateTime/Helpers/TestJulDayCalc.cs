// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Calc.DateTime.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Facades.Interfaces;
using Moq;

namespace Enigma.Test.Core.Handlers.Calc.DateTime.Helpers;

[TestFixture]
public class TestJulDayCalc
{
    private readonly double _delta = 0.00000001;
    private readonly double _jd = 12345.6789;
    private readonly double _deltaT = 0.000123;
    private SimpleDateTime _dateTime;
    private IJulDayCalc _jdCalc;

    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, Calendars.Gregorian);
        var mock = new Mock<IJulDayFacade>();
        mock.Setup(p => p.JdFromSe(_dateTime)).Returns(_jd);
        mock.Setup(p => p.DeltaTFromSe(_jd)).Returns(_deltaT);
        _jdCalc = new JulDayCalc(mock.Object);
    }

    [Test]
    public void TestCalcJulDay()
    {
        double jdResult = _jdCalc.CalcJulDayUt(_dateTime);
        Assert.That(jdResult, Is.EqualTo(_jd).Within(_delta));
    }

    [Test]
    public void TestCalcDeltaT()
    {
        double deltaTResult = _jdCalc.CalcDeltaT(_jd);
        Assert.That(deltaTResult, Is.EqualTo(_deltaT).Within(_delta));
    }

}

