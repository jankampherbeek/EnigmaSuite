// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Persistency.Converters;
using Enigma.Core.Calc.ReqResp;
using Moq;
using Enigma.Domain.DateTime;
using Enigma.Persistency.Domain;
using Enigma.Core.Calc.Interfaces;
using Enigma.Persistency.Interfaces;

namespace Enigma.Test.Persistency.Converters;

[TestFixture]
public class TestDateCheckedConversion
{
    private IDateCheckedConversion _dateCheckedConversion;


    [Test]
    public void TestCsvToDateHappyFlow()
    {
        string csvDateText = "2022/9/26";
        string csvCalText = "G";
        int year = 2022;
        int month = 9;
        int day = 26;
        double ut = 0.0;
        Calendars cal = Calendars.Gregorian;
        SimpleDateTime simpleDateTime = new(year, month, day, ut, cal);
        CheckDateTimeRequest request = new(simpleDateTime);
        bool validated = true;
        bool success = true;
        string errorText = "";
        var _mockCheckDateTimeApi = new Mock<ICheckDateTimeApi>();
        _mockCheckDateTimeApi.Setup(p => p.CheckDateTime(request)).Returns(new CheckDateTimeResponse(validated, success, errorText));

        _dateCheckedConversion = new DateCheckedConversion(_mockCheckDateTimeApi.Object);
        Tuple<PersistableDate, bool> result = _dateCheckedConversion.StandardCsvToDate(csvDateText, csvCalText);
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
        string csvDateText = "2022/9/31";
        string csvCalText = "G";
        int year = 2022;
        int month = 9;
        int day = 31;
        double ut = 0.0;
        Calendars cal = Calendars.Gregorian;
        SimpleDateTime simpleDateTime = new(year, month, day, ut, cal);
        CheckDateTimeRequest request = new(simpleDateTime);
        bool validated = false;
        bool success = true;
        string errorText = "";
        var _mockCheckDateTimeApi = new Mock<ICheckDateTimeApi>();
        _mockCheckDateTimeApi.Setup(p => p.CheckDateTime(request)).Returns(new CheckDateTimeResponse(validated, success, errorText));

        _dateCheckedConversion = new DateCheckedConversion(_mockCheckDateTimeApi.Object);
        Tuple<PersistableDate, bool> result = _dateCheckedConversion.StandardCsvToDate(csvDateText, csvCalText);
        Assert.That(!result.Item2);
    }

    [Test]
    public void TestCsvToDateNotNumeric()
    {
        string csvDateText = "2022/9w/31";
        string csvCalText = "G";
        int year = 2022;
        int month = 9;
        int day = 26;
        double ut = 0.0;
        Calendars cal = Calendars.Gregorian;
        SimpleDateTime simpleDateTime = new(year, month, day, ut, cal);
        CheckDateTimeRequest request = new(simpleDateTime);
        bool validated = true;                       // error should be handled before CheckDateTimeApi is called.
        bool success = true;
        string errorText = "";
        var _mockCheckDateTimeApi = new Mock<ICheckDateTimeApi>();
        _mockCheckDateTimeApi.Setup(p => p.CheckDateTime(request)).Returns(new CheckDateTimeResponse(validated, success, errorText));

        _dateCheckedConversion = new DateCheckedConversion(_mockCheckDateTimeApi.Object);
        Tuple<PersistableDate, bool> result = _dateCheckedConversion.StandardCsvToDate(csvDateText, csvCalText);
        Assert.That(!result.Item2);
    }

}

[TestFixture]
public class TestTimeCheckedConversion
{
    private double _delta = 0.00000001;
    private ITimeCheckedConversion _timeCheckedConversion;

    [SetUp]
    public void SetUp()
    {
        _timeCheckedConversion = new TimeCheckedConversion();
    }

    [Test]
    public void TestCsvToTimeHappyFlow()
    {
        string csvTime = "14:6:30";
        string csvDst = "0";
        string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(true));
            Assert.That(result.Item1, Is.Not.EqualTo(null));
            Assert.That(result.Item1.Hour, Is.EqualTo(14));
            Assert.That(result.Item1.Minute, Is.EqualTo(6));
            Assert.That(result.Item1.Second, Is.EqualTo(30));
            Assert.That(result.Item1.ZoneOffset, Is.EqualTo(1.0).Within(_delta));
            Assert.That(result.Item1.Dst, Is.EqualTo(0.0).Within(_delta));
        });
    }

    [Test]
    public void TestCsvToTimeHourTooLarge()
    {
        string csvTime = "24:6:30";
        string csvDst = "0";
        string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeHourTooSmall()
    {
        string csvTime = "-1:6:30";
        string csvDst = "0";
        string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeMinuteTooLarge()
    {
        string csvTime = "14:60:30";
        string csvDst = "0";
        string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeMinuteTooSmall()
    {
        string csvTime = "14:-2:30";
        string csvDst = "0";
        string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeSecondTooLarge()
    {
        string csvTime = "14:16:60";
        string csvDst = "0";
        string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeSecondTooSmall()
    {
        string csvTime = "14:16:-6";
        string csvDst = "0";
        string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }

    [Test]
    public void TestCsvToTimeNotNumeric()
    {
        string csvTime = "14:r6:30";
        string csvDst = "0";
        string csvZoneOffset = "1";
        Tuple<PersistableTime, bool> result = _timeCheckedConversion.StandardCsvToTime(csvTime, csvZoneOffset, csvDst);
        Assert.That(result.Item2, Is.EqualTo(false));
    }
}