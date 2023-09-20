// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support.Validations;

namespace Enigma.Test.Frontend.Helpers.Validations;

[TestFixture]
public class TestTimeValidator
{

    [Test]
    public void TestHappyFlow()
    {
        const bool dst = false;
        const int hour = 22;
        const int minute = 7;
        const int second = 30;
        int[] timeInput = { hour, minute, second };
        const double offsetLmt = 0.0;
        var timeValidator = new TimeValidator();
        bool result = timeValidator.CreateCheckedTime(timeInput, TimeZones.Ut, offsetLmt, dst, out FullTime? fullTime);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(fullTime.HourMinuteSecond[0], Is.EqualTo(hour));
            Assert.That(fullTime.HourMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullTime.HourMinuteSecond[2], Is.EqualTo(second));
        });
    }

    [Test]
    public void TestWrongValueForHour()
    {
        const bool dst = false;
        const int hour = 24;
        const int minute = 7;
        const int second = 30;
        int[] timeInput = { hour, minute, second };
        const double offsetLmt = 0.0;
        var timeValidator = new TimeValidator();
        bool result = timeValidator.CreateCheckedTime(timeInput, TimeZones.Ut, offsetLmt, dst, out FullTime? _);
        Assert.That(result, Is.False);
    }


    [Test]
    public void TestWrongValueForMinute()
    {
        const bool dst = false;
        const int hour = 22;
        const int minute = 99;
        const int second = 30;
        int[] timeInput = { hour, minute, second };
        const double offsetLmt = 0.0;
        var timeValidator = new TimeValidator();
        bool result = timeValidator.CreateCheckedTime(timeInput, TimeZones.Ut, offsetLmt, dst, out FullTime? _);
        Assert.That(result, Is.False);
    }

    [Test]
    public void TestWrongValueForSecond()
    {
        const bool dst = false;
        const int hour = 22;
        const int minute = 7;
        const int second = 60;
        int[] timeInput = { hour, minute, second };
        const double offsetLmt = 0.0;
        var timeValidator = new TimeValidator();
        bool result = timeValidator.CreateCheckedTime(timeInput, TimeZones.Ut, offsetLmt, dst, out FullTime? _);
        Assert.That(result, Is.False);
    }

    [Test]
    public void TestNegativeValueForSecond()
    {
        const bool dst = false;
        const int hour = 22;
        const int minute = 7;
        const int second = -5;
        int[] timeInput = { hour, minute, second };
        const double offsetLmt = 0.0;
        var timeValidator = new TimeValidator();
        bool result = timeValidator.CreateCheckedTime(timeInput, TimeZones.Ut, offsetLmt, dst, out FullTime? _);
        Assert.That(result, Is.False);
    }

}