// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.DateTime;
using Enigma.Frontend.InputSupport.Validations;
using Moq;

namespace Enigma.Test.Frontend.InputSupport.Validations;

[TestFixture]
public class TestTimeValidator
{
    private ITimeValidator _timeValidator;


    [Test]
    public void TestHappyFlow()
    {
        int hour = 22;
        int minute = 7;
        int second = 30;
        int[] timeInput = new int[] {hour, minute, second };
        double offsetLmt = 0.0;
        var mockTimeZonespecifications = new Mock<ITimeZoneSpecifications>();
        mockTimeZonespecifications.Setup(x => x.DetailsForTimeZone(TimeZones.UT)).Returns(new TimeZoneDetails(TimeZones.UT, 0.0, "ref.enum.timezone.ut"));
        _timeValidator = new TimeValidator(mockTimeZonespecifications.Object);
        FullTime? fullTime = null;
        bool Result = _timeValidator.CreateCheckedTime(timeInput, TimeZones.UT, offsetLmt, out fullTime);
        Assert.IsTrue(Result);
        Assert.That(fullTime.HourMinuteSecond[0], Is.EqualTo(hour));
        Assert.That(fullTime.HourMinuteSecond[1], Is.EqualTo(minute));
        Assert.That(fullTime.HourMinuteSecond[2], Is.EqualTo(second));
    }


    [Test]
    public void TestWrongValueForHour()
    {
        int hour = 24;
        int minute = 7;
        int second = 30;
        int[] timeInput = new int[] { hour, minute, second };
        double offsetLmt = 0.0;
        var mockTimeZonespecifications = new Mock<ITimeZoneSpecifications>();
        mockTimeZonespecifications.Setup(x => x.DetailsForTimeZone(TimeZones.UT)).Returns(new TimeZoneDetails(TimeZones.UT, 0.0, "ref.enum.timezone.ut"));
        _timeValidator = new TimeValidator(mockTimeZonespecifications.Object);
        FullTime? fullTime = null;
        bool Result = _timeValidator.CreateCheckedTime(timeInput, TimeZones.UT, offsetLmt, out fullTime);
        Assert.IsFalse(Result);
    }


    [Test]
    public void TestWrongValueForMinute()
    {
        int hour = 22;
        int minute = 99;
        int second = 30;
        int[] timeInput = new int[] { hour, minute, second };
        double offsetLmt = 0.0;
        var mockTimeZonespecifications = new Mock<ITimeZoneSpecifications>();
        mockTimeZonespecifications.Setup(x => x.DetailsForTimeZone(TimeZones.UT)).Returns(new TimeZoneDetails(TimeZones.UT, 0.0, "ref.enum.timezone.ut"));
        _timeValidator = new TimeValidator(mockTimeZonespecifications.Object);
        FullTime? fullTime = null;
        bool Result = _timeValidator.CreateCheckedTime(timeInput, TimeZones.UT, offsetLmt, out fullTime);
        Assert.IsFalse(Result);
    }

    [Test]
    public void TestWrongValueForSecond()
    {
        int hour = 22;
        int minute = 7;
        int second = 60;
        int[] timeInput = new int[] { hour, minute, second };
        double offsetLmt = 0.0;
        var mockTimeZonespecifications = new Mock<ITimeZoneSpecifications>();
        mockTimeZonespecifications.Setup(x => x.DetailsForTimeZone(TimeZones.UT)).Returns(new TimeZoneDetails(TimeZones.UT, 0.0, "ref.enum.timezone.ut"));
        _timeValidator = new TimeValidator(mockTimeZonespecifications.Object);
        FullTime? fullTime = null;
        bool Result = _timeValidator.CreateCheckedTime(timeInput, TimeZones.UT, offsetLmt, out fullTime);
        Assert.IsFalse(Result);
    }

    [Test]
    public void TestNegativeValueForSecond()
    {
        int hour = 22;
        int minute = 7;
        int second = -5;
        int[] timeInput = new int[] { hour, minute, second };
        double offsetLmt = 0.0;
        var mockTimeZonespecifications = new Mock<ITimeZoneSpecifications>();
        mockTimeZonespecifications.Setup(x => x.DetailsForTimeZone(TimeZones.UT)).Returns(new TimeZoneDetails(TimeZones.UT, 0.0, "ref.enum.timezone.ut"));
        _timeValidator = new TimeValidator(mockTimeZonespecifications.Object);
        FullTime? fullTime = null;
        bool Result = _timeValidator.CreateCheckedTime(timeInput, TimeZones.UT, offsetLmt, out fullTime);
        Assert.IsFalse(Result);
    }

}