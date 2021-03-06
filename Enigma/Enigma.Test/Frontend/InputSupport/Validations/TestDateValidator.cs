// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.DateTime;
using Enigma.Frontend.InputSupport.Validations;
using Moq;

namespace Enigma.Test.Frontend.InputSupport.Validations;

[TestFixture]
public class TestDateValidator
{
    private IDateValidator _dateValidator;


    [Test]
    public void TestHappyFlow()
    {
        int year = 2022;
        int month = 5;
        int day = 23;
        int[] DateInput = new int[] { year, month, day };
        CheckDateTimeResponse response = new(true, true, "");
        var mockCheckDateTimeApi = new Mock<ICheckDateTimeApi>();
        mockCheckDateTimeApi.Setup(x => x.CheckDateTime(It.IsAny<CheckDateTimeRequest>())).Returns(response);
        _dateValidator = new DateValidator(mockCheckDateTimeApi.Object);
        FullDate? fullDate = null;
        bool result = _dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out fullDate);
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
        var mockCheckDateTimeApi = new Mock<ICheckDateTimeApi>();
        mockCheckDateTimeApi.Setup(x => x.CheckDateTime(It.IsAny<CheckDateTimeRequest>())).Returns(response);
        _dateValidator = new DateValidator(mockCheckDateTimeApi.Object);
        FullDate? fullDate = null;
        bool result = _dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out fullDate);
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
        var mockCheckDateTimeApi = new Mock<ICheckDateTimeApi>();
        mockCheckDateTimeApi.Setup(x => x.CheckDateTime(It.IsAny<CheckDateTimeRequest>())).Returns(response);
        _dateValidator = new DateValidator(mockCheckDateTimeApi.Object);
        FullDate? fullDate = null;
        bool result = _dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out fullDate);
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
        var mockCheckDateTimeApi = new Mock<ICheckDateTimeApi>();
        mockCheckDateTimeApi.Setup(x => x.CheckDateTime(It.IsAny<CheckDateTimeRequest>())).Returns(response);
        _dateValidator = new DateValidator(mockCheckDateTimeApi.Object);
        FullDate? fullDate = null;
        bool result = _dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out fullDate);
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
            var mockCheckDateTimeApi = new Mock<ICheckDateTimeApi>();
            mockCheckDateTimeApi.Setup(x => x.CheckDateTime(It.IsAny<CheckDateTimeRequest>())).Returns(response);
            _dateValidator = new DateValidator(mockCheckDateTimeApi.Object);
            FullDate? fullDate = null;
            bool result = _dateValidator.CreateCheckedDate(DateInput, Calendars.Gregorian, YearCounts.Astronomical, out fullDate);
            Assert.That(result, Is.False);
        }


}