// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Calc.DateTime;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Exceptions;
using Enigma.Domain.RequestResponse;
using Moq;


namespace Enigma.Test.Core.Calc.DateTime.CheckDateTime;

[TestFixture]
public class TestDateTimeHandler
{
    private readonly Calendars _calendar = Calendars.Gregorian;
    private SimpleDateTime _dateTime;


    private readonly string _errorText = "Description of problem.";


    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, _calendar);
    }

    // TODO Urgent  Fix tests for DateTimeHandler.


    /*

    [Test]
    public void TestHappyFlow()
    {
        Mock<ICheckDateTimeValidator> validatorMock = CreateValidatorMock();
        IDateTimeHandler handler = new DateTimeHandler(validatorMock.Object);
        CheckDateTimeRequest request = new(_dateTime);
        CheckDateTimeResponse response = handler.CheckDateTime(request);
        Assert.Multiple(() =>
        {
            Assert.That(response.Validated, Is.True);
            Assert.That(response.Success, Is.True);
            Assert.That(response.ErrorText, Is.EqualTo(""));
        });
    }

    [Test]
    public void TextSeException()
    {
        Mock<ICheckDateTimeValidator> validatorExceptionMock = CreateCalcMockThrowingException();
        IDateTimeHandler handler = new DateTimeHandler(validatorExceptionMock.Object);
        CheckDateTimeRequest request = new(_dateTime);
        CheckDateTimeResponse response = handler.CheckDateTime(request);
        Assert.Multiple(() =>
        {
            Assert.That(response.Success, Is.False);
            Assert.That(response.ErrorText, Is.EqualTo(_errorText));
        });
    }
    */

    private Mock<ICheckDateTimeValidator> CreateValidatorMock()
    {
        var mock = new Mock<ICheckDateTimeValidator>();
        mock.Setup(p => p.ValidateDateTime(_dateTime)).Returns(true);
        return mock;
    }

    private Mock<ICheckDateTimeValidator> CreateCalcMockThrowingException()
    {
        var mock = new Mock<ICheckDateTimeValidator>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.ValidateDateTime(_dateTime)).Throws(exception);
        return mock;
    }

}