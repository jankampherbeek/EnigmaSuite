// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4CTest.core.calendarandclock.checkdatetime;

using E4C.core.calendarandclock.checkdatetime;
using E4C.core.calendarandclock.datetime;
using E4C.core.shared.domain;
using E4C.exceptions;
using E4C.shared.references;
using E4C.shared.reqresp;
using Moq;
using NUnit.Framework;

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
        Mock<ICheckDateTimeValidator> validatorMock = CreateValidatorMock();
        ICheckDateTimeHandler handler = new CheckDateTimeHandler(validatorMock.Object);
        CheckDateTimeRequest request = new CheckDateTimeRequest(_dateTime);
        CheckDateTimeResponse response = handler.CheckDateTime(request);
        Assert.IsTrue(response.Validated);
        Assert.IsTrue(response.Success);
        Assert.AreEqual("", response.ErrorText);
    }

    [Test]
    public void TextSeException()
    {
        Mock<ICheckDateTimeValidator> validatorExceptionMock = CreateCalcMockThrowingException();
        ICheckDateTimeHandler handler = new CheckDateTimeHandler(validatorExceptionMock.Object);
        CheckDateTimeRequest request = new CheckDateTimeRequest(_dateTime);
        CheckDateTimeResponse response = handler.CheckDateTime(request);
        Assert.IsFalse(response.Success);
        Assert.AreEqual(_errorText, response.ErrorText);
    }


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