// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.DateTime;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.RequestResponse;
using Moq;

namespace Enigma.Test.Core.Handlers.Calc.DateTime;

[TestFixture]
public class TestDateTimeHandler
{
    private readonly Calendars _calendar = Calendars.Gregorian;
    private readonly double _jdUt = 123456.789;
    private readonly bool _useJdForUt = true;
    private SimpleDateTime _dateTime;


    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, _calendar);
    }


    [Test]
    public void TestHappyFlow()
    {
        Mock<IDateTimeCalc> calcMock = CreateCalcMock();
        Mock<IDateTimeValidator> validatorMock = CreateValidatorMock();
        IDateTimeHandler handler = new DateTimeHandler(calcMock.Object, validatorMock.Object);
        DateTimeResponse response = handler.CalcDateTime(new DateTimeRequest(_jdUt, _useJdForUt, _calendar));
        Assert.That(response.DateTime, Is.EqualTo(_dateTime));
    }


    private Mock<IDateTimeCalc> CreateCalcMock()
    {
        var mock = new Mock<IDateTimeCalc>();
        mock.Setup(p => p.CalcDateTime(_jdUt, _calendar)).Returns(_dateTime);
        return mock;
    }

    private Mock<IDateTimeValidator> CreateValidatorMock()
    {
        var mock = new Mock<IDateTimeValidator>();

        mock.Setup(p => p.ValidateDateTime(_dateTime)).Returns(true);
        return mock;
    }


}