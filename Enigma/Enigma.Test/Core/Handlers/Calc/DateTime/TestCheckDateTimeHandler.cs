﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Calc.DateTime;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Exceptions;
using Moq;


namespace Enigma.Test.Core.Handlers.Calc.DateTime;

[TestFixture]
public class TestCheckDateTimeHandler
{
    private readonly Calendars _calendar = Calendars.Gregorian;
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
        Mock<IDateTimeValidator> validatorMock = CreateValidatorMock();
        Mock<IDateTimeCalc> calcMock = CreateCalcMock();
        IDateTimeHandler handler = new DateTimeHandler(calcMock.Object, validatorMock.Object);
        Assert.That(handler.CheckDateTime(_dateTime), Is.True);
    }

    [Test]
    public void TextSeException()
    {
        Mock<IDateTimeCalc> calcMock = CreateCalcMock();
        Mock<IDateTimeValidator> validatorExceptionMock = CreateCalcMockThrowingException();
        IDateTimeHandler handler = new DateTimeHandler(calcMock.Object, validatorExceptionMock.Object);
        Assert.That(handler.CheckDateTime(_dateTime), Is.False);
    }


    private Mock<IDateTimeValidator> CreateValidatorMock()
    {
        var mock = new Mock<IDateTimeValidator>();
        mock.Setup(p => p.ValidateDateTime(_dateTime)).Returns(true);
        return mock;
    }

    private Mock<IDateTimeCalc> CreateCalcMock()
    {
        var mock = new Mock<IDateTimeCalc>();
        mock.Setup(p => p.CalcDateTime(It.IsAny<double>(), It.IsAny<Calendars>())).Returns(_dateTime);
        return mock;
    }


    private Mock<IDateTimeValidator> CreateCalcMockThrowingException()
    {
        var mock = new Mock<IDateTimeValidator>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.ValidateDateTime(_dateTime)).Throws(exception);
        return mock;
    }

}