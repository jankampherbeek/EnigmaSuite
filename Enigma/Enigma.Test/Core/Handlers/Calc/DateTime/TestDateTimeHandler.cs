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
    private const Calendars Calendar = Calendars.Gregorian;
    private const double JdUt = 123456.789;
    private const bool UseJdForUt = true;
    private SimpleDateTime? _dateTime;


    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, Calendar);
    }


    [Test]
    public void TestHappyFlow()
    {
        Mock<IDateTimeCalc> calcMock = CreateCalcMock();
        Mock<IDateTimeValidator> validatorMock = CreateValidatorMock();
        IDateTimeHandler handler = new DateTimeHandler(calcMock.Object, validatorMock.Object);
        DateTimeResponse response = handler.CalcDateTime(new DateTimeRequest(JdUt, UseJdForUt, Calendar));
        Assert.That(response.DateTime, Is.EqualTo(_dateTime));
    }


    private Mock<IDateTimeCalc> CreateCalcMock()
    {
        var mock = new Mock<IDateTimeCalc>();
        mock.Setup(p => p.CalcDateTime(JdUt, Calendar)).Returns(_dateTime!);
        return mock;
    }

    private Mock<IDateTimeValidator> CreateValidatorMock()
    {
        var mock = new Mock<IDateTimeValidator>();

        mock.Setup(p => p.ValidateDateTime(_dateTime!)).Returns(true);
        return mock;
    }


}