﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Frontend.Helpers.Validations;

namespace Enigma.Test.Frontend.Helpers.Validations;

[TestFixture]
public class TestTimeValidator
{

    [Test]
    public void TestHappyFlow()
    {
        int hour = 22;
        int minute = 7;
        int second = 30;
        int[] timeInput = new int[] { hour, minute, second };
        double offsetLmt = 0.0;
        var timeValidator = new TimeValidator();
        bool Result = timeValidator.CreateCheckedTime(timeInput, TimeZones.UT, offsetLmt, out FullTime? fullTime);
        Assert.Multiple(() =>
        {
            Assert.That(Result, Is.True);
            Assert.That(fullTime.HourMinuteSecond[0], Is.EqualTo(hour));
            Assert.That(fullTime.HourMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullTime.HourMinuteSecond[2], Is.EqualTo(second));
        });
    }

    [Test]
    public void TestWrongValueForHour()
    {
        int hour = 24;
        int minute = 7;
        int second = 30;
        int[] timeInput = new int[] { hour, minute, second };
        double offsetLmt = 0.0;
        var timeValidator = new TimeValidator();
        bool Result = timeValidator.CreateCheckedTime(timeInput, TimeZones.UT, offsetLmt, out FullTime? _);
        Assert.That(Result, Is.False);
    }


    [Test]
    public void TestWrongValueForMinute()
    {
        int hour = 22;
        int minute = 99;
        int second = 30;
        int[] timeInput = new int[] { hour, minute, second };
        double offsetLmt = 0.0;
        var timeValidator = new TimeValidator();
        bool Result = timeValidator.CreateCheckedTime(timeInput, TimeZones.UT, offsetLmt, out FullTime? _);
        Assert.That(Result, Is.False);
    }

    [Test]
    public void TestWrongValueForSecond()
    {
        int hour = 22;
        int minute = 7;
        int second = 60;
        int[] timeInput = new int[] { hour, minute, second };
        double offsetLmt = 0.0;
        var timeValidator = new TimeValidator();
        bool Result = timeValidator.CreateCheckedTime(timeInput, TimeZones.UT, offsetLmt, out FullTime? _);
        Assert.That(Result, Is.False);
    }

    [Test]
    public void TestNegativeValueForSecond()
    {
        int hour = 22;
        int minute = 7;
        int second = -5;
        int[] timeInput = new int[] { hour, minute, second };
        double offsetLmt = 0.0;
        var timeValidator = new TimeValidator();
        bool Result = timeValidator.CreateCheckedTime(timeInput, TimeZones.UT, offsetLmt, out FullTime? _);
        Assert.That(Result, Is.False);
    }

}