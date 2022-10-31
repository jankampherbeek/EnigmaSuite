// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.RequestResponse;
using Enigma.InputSupport.Validations;
using Moq;

namespace Enigma.Test.InputSupport.Validations;

[TestFixture]
public class TestDateValidator
{

    [Test]
    public void TestHappyFlow()
    {
        int year = 2022;
        int month = 5;
        int day = 23;
        int[] DateInput = new int[] { year, month, day };
        CheckDateTimeResponse response = new(true, true, "");
        var mockCheckDateTimeHandler = new Mock<ICheckDateTimeHandler>();
        mockCheckDateTimeHandler.Setup(x => x.CheckDateTime(It.IsAny<CheckDateTimeRequest>())).Returns(response);
        var dateValidator = new DateValidator(mockCheckDateTimeHandler.Object);
        bool result = dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out FullDate? fullDate);
        Assert.That(result, Is.True);
        Assert.NotNull(fullDate);
        Assert.That(year, Is.EqualTo(fullDate.YearMonthDay[0]));
        Assert.That(month, Is.EqualTo(fullDate.YearMonthDay[1]));
        Assert.That(day, Is.EqualTo(fullDate.YearMonthDay[2]));
    }


    [Test]
    public void TestMonthTooLarge()
    {
        int year = 2022;
        int month = 15;
        int day = 23;
        int[] DateInput = new int[] { year, month, day };
        CheckDateTimeResponse response = new(false, true, "");
        var mockCheckDateTimeHandler = new Mock<ICheckDateTimeHandler>();
        mockCheckDateTimeHandler.Setup(x => x.CheckDateTime(It.IsAny<CheckDateTimeRequest>())).Returns(response);
        var dateValidator = new DateValidator(mockCheckDateTimeHandler.Object);
        bool result = dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out FullDate? fullDate);
        Assert.That(result, Is.False);
    }

    [Test]
    public void TestDayTooSmall()
    {
        int year = 2022;
        int month = 15;
        int day = -1;
        int[] DateInput = new int[] { year, month, day };
        CheckDateTimeResponse response = new(false, true, "");
        var mockCheckDateTimeHandler = new Mock<ICheckDateTimeHandler>();
        mockCheckDateTimeHandler.Setup(x => x.CheckDateTime(It.IsAny<CheckDateTimeRequest>())).Returns(response);
        var dateValidator = new DateValidator(mockCheckDateTimeHandler.Object);
        bool result = dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out FullDate? fullDate);
        Assert.That(result, Is.False);
    }


    [Test]
    public void TestLeapYearTrue()
    {
        int year = 2024;
        int month = 2;
        int day = 29;
        int[] DateInput = new int[] { year, month, day };
        CheckDateTimeResponse response = new(true, true, "");
        var mockCheckDateTimeHandler = new Mock<ICheckDateTimeHandler>();
        mockCheckDateTimeHandler.Setup(x => x.CheckDateTime(It.IsAny<CheckDateTimeRequest>())).Returns(response);
        var dateValidator = new DateValidator(mockCheckDateTimeHandler.Object);
        bool result = dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out FullDate? fullDate);
        Assert.That(result, Is.True);
        Assert.NotNull(fullDate);
        Assert.That(year, Is.EqualTo(fullDate.YearMonthDay[0]));
        Assert.That(month, Is.EqualTo(fullDate.YearMonthDay[1]));
        Assert.That(day, Is.EqualTo(fullDate.YearMonthDay[2]));
    }


    [Test]
    public void TestLeapYearFalse()
    {
        int year = 2023;
        int month = 2;
        int day = 29;
        int[] DateInput = new int[] { year, month, day };
        CheckDateTimeResponse response = new(false, true, "");
        var mockCheckDateTimeHandler = new Mock<ICheckDateTimeHandler>();
        mockCheckDateTimeHandler.Setup(x => x.CheckDateTime(It.IsAny<CheckDateTimeRequest>())).Returns(response);
        var dateValidator = new DateValidator(mockCheckDateTimeHandler.Object);
        bool result = dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out FullDate? fullDate);
        Assert.That(result, Is.False);
    }


}