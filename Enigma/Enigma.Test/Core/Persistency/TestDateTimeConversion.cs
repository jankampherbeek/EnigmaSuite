// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Core.Persistency.Helpers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;
using Moq;

namespace Enigma.Test.Core.Persistency;

[TestFixture]
public class TestDateCheckedConversion
{
    [Test]
    public void TestCsvToDateHappyFlow()
    {
        const string csvDateText = "2022/9/26";
        const string csvCalText = "G";
        const int year = 2022;
        const int month = 9;
        const int day = 26;
        const double ut = 0.0;
        const Calendars cal = Calendars.Gregorian;
        SimpleDateTime simpleDateTime = new(year, month, day, ut, cal);
        var mockDateTimeValidator = new Mock<IDateTimeValidator>();
        mockDateTimeValidator.Setup(p => p.ValidateDateTime(simpleDateTime)).Returns(true);
        IDateCheckedConversion dateCheckedConversion = new DateCheckedConversion(mockDateTimeValidator.Object);
        Tuple<PersistableDate, bool> result = dateCheckedConversion.StandardCsvToDate(csvDateText, csvCalText);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2);
            Assert.That(result.Item1.Year, Is.EqualTo(year));
            Assert.That(result.Item1.Month, Is.EqualTo(month));
            Assert.That(result.Item1.Day, Is.EqualTo(day));
        });
    }

    [Test]
    public void TestCsvToDateInvalidDate()
    {
        const string csvDateText = "2022/9/31";
        const string csvCalText = "G";
        const int year = 2022;
        const int month = 9;
        const int day = 31;
        const double ut = 0.0;
        const Calendars cal = Calendars.Gregorian;
        SimpleDateTime simpleDateTime = new(year, month, day, ut, cal);
        var mockDateTimeValidator = new Mock<IDateTimeValidator>();
        mockDateTimeValidator.Setup(p => p.ValidateDateTime(simpleDateTime)).Returns(false);
        IDateCheckedConversion dateCheckedConversion = new DateCheckedConversion(mockDateTimeValidator.Object);
        Tuple<PersistableDate, bool> result = dateCheckedConversion.StandardCsvToDate(csvDateText, csvCalText);
        Assert.That(!result.Item2);
    }

    [Test]
    public void TestCsvToDateNotNumeric()
    {
        const string csvDateText = "2022/9w/31";
        const string csvCalText = "G";
        const int year = 2022;
        const int month = 9;
        const int day = 26;
        const double ut = 0.0;
        const Calendars cal = Calendars.Gregorian;
        SimpleDateTime simpleDateTime = new(year, month, day, ut, cal);
        var mockDateTimeValidator = new Mock<IDateTimeValidator>();
        mockDateTimeValidator.Setup(p => p.ValidateDateTime(simpleDateTime)).Returns(false);
        IDateCheckedConversion dateCheckedConversion = new DateCheckedConversion(mockDateTimeValidator.Object);
        Tuple<PersistableDate, bool> result = dateCheckedConversion.StandardCsvToDate(csvDateText, csvCalText);
        Assert.That(!result.Item2);
    }

}

[TestFixture]
public class TestTimeCheckedConversion
{
    private const double DELTA = 0.00000001;
    private ITimeCheckedConversion? _timeCheckedConversion;

    [SetUp]
    public void SetUp()
    {
        _timeCheckedConversion = new TimeCheckedConversion();
    }

    [Test]
    public void TestCsvToTimeHappyFlow()
    {
        const string csvTime = "14:6:30";
        const string csvDst = "0";
        const string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion!.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(true));
            Assert.That(result.Item1, Is.Not.EqualTo(null));
            Assert.That(result.Item1.Hour, Is.EqualTo(14));
            Assert.That(result.Item1.Minute, Is.EqualTo(6));
            Assert.That(result.Item1.Second, Is.EqualTo(30));
            Assert.That(result.Item1.ZoneOffset, Is.EqualTo(1.0).Within(DELTA));
            Assert.That(result.Item1.Dst, Is.EqualTo(0.0).Within(DELTA));
        });
    }

    [Test]
    public void TestCsvToTimeHourTooLarge()
    {
        const string csvTime = "24:6:30";
        const string csvDst = "0";
        const string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion!.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeHourTooSmall()
    {
        const string csvTime = "-1:6:30";
        const string csvDst = "0";
        const string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion!.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeMinuteTooLarge()
    {
        const string csvTime = "14:60:30";
        const string csvDst = "0";
        const string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion!.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeMinuteTooSmall()
    {
        const string csvTime = "14:-2:30";
        const string csvDst = "0";
        const string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion!.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeSecondTooLarge()
    {
        const string csvTime = "14:16:60";
        const string csvDst = "0";
        const string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion!.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeSecondTooSmall()
    {
        const string csvTime = "14:16:-6";
        const string csvDst = "0";
        const string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion!.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeNotNumeric()
    {
        const string csvTime = "14:r6:30";
        const string csvDst = "0";
        const string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion!.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

}