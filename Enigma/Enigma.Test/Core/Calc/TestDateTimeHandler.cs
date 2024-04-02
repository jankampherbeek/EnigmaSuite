// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using FakeItEasy;

namespace Enigma.Test.Core.Calc;

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
        IDateTimeCalc calcFake = CreateCalcFake();
        IDateTimeValidator validatorFake = CreateValidatorFake();
        IDateTimeHandler handler = new DateTimeHandler(calcFake, validatorFake);
        DateTimeResponse response = handler.CalcDateTime(new DateTimeRequest(JD_UT, USE_JD_FOR_UT, CALENDAR));
        Assert.That(response.DateTime, Is.EqualTo(_dateTime));
    }


    private IDateTimeCalc CreateCalcFake()
    {
        var fake = A.Fake<IDateTimeCalc>();
        A.CallTo(() => fake.CalcDateTime(JD_UT, CALENDAR)).Returns(_dateTime!);
        return fake;
    }

    private IDateTimeValidator CreateValidatorFake()
    {
        var fake = A.Fake<IDateTimeValidator>();
        A.CallTo(() => fake.ValidateDateTime(_dateTime!)).Returns(true);
        return fake;
    }

}