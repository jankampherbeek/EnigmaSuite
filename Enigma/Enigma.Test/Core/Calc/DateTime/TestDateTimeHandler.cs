// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.DateTime;
using Enigma.Core.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.References;
using Enigma.Domain.RequestResponse;
using Moq;

namespace Enigma.Test.Core.Calc.DateTime;

[TestFixture]
public class TestDateTimeHandler
{
    private const Calendars CALENDAR = Calendars.Gregorian;
    private const double JD_UT = 123456.789;
    private const bool USE_JD_FOR_UT = true;
    private SimpleDateTime? _dateTime;


    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, CALENDAR);
    }


    [Test]
    public void TestHappyFlow()
    {
        Mock<IDateTimeCalc> calcMock = CreateCalcMock();
        Mock<IDateTimeValidator> validatorMock = CreateValidatorMock();
        IDateTimeHandler handler = new DateTimeHandler(calcMock.Object, validatorMock.Object);
        DateTimeResponse response = handler.CalcDateTime(new DateTimeRequest(JD_UT, USE_JD_FOR_UT, CALENDAR));
        Assert.That(response.DateTime, Is.EqualTo(_dateTime));
    }


    private Mock<IDateTimeCalc> CreateCalcMock()
    {
        var mock = new Mock<IDateTimeCalc>();
        mock.Setup(p => p.CalcDateTime(JD_UT, CALENDAR)).Returns(_dateTime!);
        return mock;
    }

    private Mock<IDateTimeValidator> CreateValidatorMock()
    {
        var mock = new Mock<IDateTimeValidator>();

        mock.Setup(p => p.ValidateDateTime(_dateTime!)).Returns(true);
        return mock;
    }


}