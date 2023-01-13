// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Frontend.Helpers.Validations;
using Moq;

namespace Enigma.Test.Frontend.Helpers.Validations;

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
        var mockDateTimeApi = new Mock<IDateTimeApi>();
        mockDateTimeApi.Setup(x => x.CheckDateTime(It.IsAny<SimpleDateTime>())).Returns(true);
        var dateValidator = new DateValidator(mockDateTimeApi.Object);
        bool result = dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out FullDate? fullDate);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(fullDate, Is.Not.Null);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.That(year, Is.EqualTo(fullDate.YearMonthDay[0]));
            Assert.That(month, Is.EqualTo(fullDate.YearMonthDay[1]));
            Assert.That(day, Is.EqualTo(fullDate.YearMonthDay[2]));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        });
    }

    [Test]
    public void TestMonthTooLarge()
    {
        int year = 2022;
        int month = 15;
        int day = 23;
        int[] DateInput = new int[] { year, month, day };
        var mockDateTimeApi = new Mock<IDateTimeApi>();
        mockDateTimeApi.Setup(x => x.CheckDateTime(It.IsAny<SimpleDateTime>())).Returns(false);
        var dateValidator = new DateValidator(mockDateTimeApi.Object);
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
        var mockDateTimeApi = new Mock<IDateTimeApi>();
        mockDateTimeApi.Setup(x => x.CheckDateTime(It.IsAny<SimpleDateTime>())).Returns(false);
        var dateValidator = new DateValidator(mockDateTimeApi.Object);
        bool result = dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out FullDate? fullDate);
        Assert.That(result, Is.False);
    }




}