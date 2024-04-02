// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using FakeItEasy;

namespace Enigma.Test.Core.Calc;

[TestFixture]
public class TestCheckDateTimeHandler
{
    private const Calendars CALENDAR = Calendars.Gregorian;
    private SimpleDateTime? _dateTime;


    private const string ERROR_TEXT = "Description of problem.";


    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, CALENDAR);
    }

    [Test]
    public void TestHappyFlow()
    {
        var validatorFake = CreateValidatorFake();
        var calcFake = CreateCalcFake();
        IDateTimeHandler handler = new DateTimeHandler(calcFake, validatorFake);
        Assert.That(handler.CheckDateTime(_dateTime!), Is.True);
    }

    [Test]
    public void TextSeException()
    {
        var calcFake = CreateCalcFake();
        var validatorExceptionFake = CreateValidatorFakeThrowingException();
        IDateTimeHandler handler = new DateTimeHandler(calcFake, validatorExceptionFake);
        Assert.That(handler.CheckDateTime(_dateTime!), Is.False);
    }


    private IDateTimeValidator CreateValidatorFake()
    {
        var validatorFake = A.Fake<IDateTimeValidator>();
        A.CallTo(() => validatorFake.ValidateDateTime(_dateTime)).Returns(true);
        return validatorFake;
    }

    private IDateTimeCalc CreateCalcFake()
    {
        var calcFake = A.Fake<IDateTimeCalc>();
        A.CallTo(() => calcFake.CalcDateTime(A<double>._, A<Calendars>._)).Returns(_dateTime!);
        return calcFake;
    }


    private IDateTimeValidator CreateValidatorFakeThrowingException()
    {
        var validatorFake = A.Fake<IDateTimeValidator>();
        var exception = new SwissEphException(ERROR_TEXT);
        A.CallTo(() => validatorFake.ValidateDateTime(_dateTime)).Throws(exception);
        return validatorFake;
    }

}