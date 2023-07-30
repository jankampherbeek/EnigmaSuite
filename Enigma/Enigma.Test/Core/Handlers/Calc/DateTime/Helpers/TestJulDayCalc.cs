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
    private const double Delta = 0.00000001;
    private const double Jd = 12345.6789;
    private const double DeltaT = 0.000123;
    private SimpleDateTime? _dateTime;
    private IJulDayCalc? _jdCalc;

    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, Calendars.Gregorian);
        var mock = new Mock<IJulDayFacade>();
        mock.Setup(p => p.JdFromSe(_dateTime)).Returns(Jd);
        mock.Setup(p => p.DeltaTFromSe(Jd)).Returns(DeltaT);
        _jdCalc = new JulDayCalc(mock.Object);
    }

    [Test]
    public void TestCalcJulDay()
    {
        double jdResult = _jdCalc!.CalcJulDayUt(_dateTime!);
        Assert.That(jdResult, Is.EqualTo(Jd).Within(Delta));
    }

    [Test]
    public void TestCalcDeltaT()
    {
        double deltaTResult = _jdCalc!.CalcDeltaT(Jd);
        Assert.That(deltaTResult, Is.EqualTo(DeltaT).Within(Delta));
    }

}

