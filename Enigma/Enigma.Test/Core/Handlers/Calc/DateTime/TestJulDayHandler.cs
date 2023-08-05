// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Test.Core.Handlers.Calc.DateTime;

using Enigma.Core.Handlers.Calc.DateTime;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.RequestResponse;
using Moq;


[TestFixture]
public class TestJulDayHandler
{
    private const double EXPECTED_JD = 123456.789;
    private SimpleDateTime? _dateTime;
    private const double DELTA = 0.00000001;


    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, Calendars.Gregorian);
    }


    [Test]
    public void TestHappyFlow()
    {
        Mock<IJulDayCalc> calcMock = CreateCalcMock();
        IJulDayHandler handler = new JulDayHandler(calcMock.Object);
        JulianDayResponse response = handler.CalcJulDay(_dateTime!);
        Assert.That(response.JulDayUt, Is.EqualTo(EXPECTED_JD).Within(DELTA));
    }

    private Mock<IJulDayCalc> CreateCalcMock()
    {
        var mock = new Mock<IJulDayCalc>();
        mock.Setup(p => p.CalcJulDayUt(_dateTime!)).Returns(EXPECTED_JD);
        return mock;
    }

}