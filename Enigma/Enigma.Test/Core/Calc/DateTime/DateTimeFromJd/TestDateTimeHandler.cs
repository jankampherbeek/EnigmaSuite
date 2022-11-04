// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.DateTime.DateTimeFromJd;
using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Exceptions;
using Enigma.Domain.RequestResponse;
using Moq;

namespace Enigma.Test.Core.Calc.DateTime.DateTimeFromJd;

[TestFixture]
public class TestDateTimeHandler
{
    private readonly Calendars _calendar = Calendars.Gregorian;
    private readonly double _jdUt = 123456.789;
    private readonly bool _useJdForUt = true;
    private SimpleDateTime _dateTime;
    private readonly string _errorText = "Description of problem.";


    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, _calendar);
    }


    [Test]
    public void TestHappyFlow()
    {
        Mock<IDateTimeCalc> calcMock = CreateCalcMock();
        IDateTimeHandler handler = new DateTimeHandler(calcMock.Object);
        DateTimeResponse response = handler.CalcDateTime(new DateTimeRequest(_jdUt, _useJdForUt, _calendar));
        Assert.Multiple(() =>
        {
            Assert.That(response.DateTime, Is.EqualTo(_dateTime));
            Assert.That(response.Success, Is.True);
            Assert.That(response.ErrorText, Is.EqualTo(""));
        });
    }

    [Test]
    public void TextSeException()
    {
        Mock<IDateTimeCalc> calcExceptionMock = CreateCalcMockThrowingException();
        IDateTimeHandler handler = new DateTimeHandler(calcExceptionMock.Object);
        DateTimeResponse response = handler.CalcDateTime(new DateTimeRequest(_jdUt, _useJdForUt, _calendar));
        Assert.Multiple(() =>
        {
            Assert.That(response.Success, Is.False);
            Assert.That(response.ErrorText, Is.EqualTo(_errorText));
        });
    }

    private Mock<IDateTimeCalc> CreateCalcMock()
    {
        var mock = new Mock<IDateTimeCalc>();
        mock.Setup(p => p.CalcDateTime(_jdUt, _calendar)).Returns(_dateTime);
        return mock;
    }

    private Mock<IDateTimeCalc> CreateCalcMockThrowingException()
    {
        var mock = new Mock<IDateTimeCalc>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.CalcDateTime(_jdUt, _calendar)).Throws(exception);
        return mock;
    }

}