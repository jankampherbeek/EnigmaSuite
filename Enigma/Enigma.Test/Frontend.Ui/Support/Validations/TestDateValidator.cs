// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support.Validations;
using Moq;

namespace Enigma.Test.Frontend.Helpers.Validations;

[TestFixture]
public class TestDateValidator
{

    [Test]
    public void TestHappyFlow()
    {
        const int year = 2022;
        const int month = 5;
        const int day = 23;
        int[] dateInput = { year, month, day };
        var mockDateTimeApi = new Mock<IDateTimeApi>();
        mockDateTimeApi.Setup(x => x.CheckDateTime(It.IsAny<SimpleDateTime>())).Returns(true);
        var dateValidator = new DateValidator(mockDateTimeApi.Object);
        bool result = dateValidator.CreateCheckedDate(dateInput, Calendars.Gregorian, YearCounts.Astronomical, out FullDate? fullDate);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(fullDate, Is.Not.Null);
            Assert.That(fullDate!.YearMonthDay[0], Is.EqualTo(year));
            Assert.That(fullDate.YearMonthDay[1], Is.EqualTo(month));
            Assert.That(fullDate.YearMonthDay[2], Is.EqualTo(day));
        });
    }

    [Test]
    public void TestMonthTooLarge()
    {
        const int year = 2022;
        const int month = 15;
        const int day = 23;
        int[] dateInput = { year, month, day };
        var mockDateTimeApi = new Mock<IDateTimeApi>();
        mockDateTimeApi.Setup(x => x.CheckDateTime(It.IsAny<SimpleDateTime>())).Returns(false);
        var dateValidator = new DateValidator(mockDateTimeApi.Object);
        bool result = dateValidator.CreateCheckedDate(dateInput, Calendars.Gregorian, YearCounts.Astronomical, out FullDate? _);
        Assert.That(result, Is.False);
    }

    [Test]
    public void TestDayTooSmall()
    {
        const int year = 2022;
        const int month = 15;
        const int day = -1;
        int[] dateInput = { year, month, day };
        var mockDateTimeApi = new Mock<IDateTimeApi>();
        mockDateTimeApi.Setup(x => x.CheckDateTime(It.IsAny<SimpleDateTime>())).Returns(false);
        var dateValidator = new DateValidator(mockDateTimeApi.Object);
        bool result = dateValidator.CreateCheckedDate(dateInput, Calendars.Gregorian, YearCounts.Astronomical, out FullDate? _);
        Assert.That(result, Is.False);
    }




}