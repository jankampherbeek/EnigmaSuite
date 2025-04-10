// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Conversion;

using NUnit.Framework;
using System;
using Enigma.Core.Conversion;

[TestFixture]
public class TestDateTimeConversion
{
    
    [Test]
    public void TestParseDateTimeFromTextHappyFlow()
    {
        string[] items = ["1953", "1", "29", "8", "37", "30"];
        const double ut = 8 + 37.0 / 60.0 + 30.0 / 3600.0;
        var expected = new SimpleDateTime(1953, 1, 29, ut, Calendars.Gregorian);
        var result = DateTimeConversion.ParseDateTimeFromText(items);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestParseDateTimeFromTextNoTime()
    {
        string[] items = ["2025", "2", "19"];
        const double ut = 0.0;
        var expected = new SimpleDateTime(2025, 2, 19, ut, Calendars.Gregorian);
        var result = DateTimeConversion.ParseDateTimeFromText(items);
        Assert.That(result,  Is.EqualTo(expected));
    }

    [Test]
    public void TestParseDateTimeFromTextNoMinutesSeconds()
    {
        string[] items = ["2025", "2", "19", "14"];
        const double ut = 14.0;
        var expected = new SimpleDateTime(2025, 2, 19, ut, Calendars.Gregorian);
        var result = DateTimeConversion.ParseDateTimeFromText(items);
        Assert.That(result,  Is.EqualTo(expected));
    }

    [Test]
    public void TestParseDateTimeFromTextNoSeconds()
    {
        string[] items = ["2025", "2", "19", "14", "33"];
        const double ut = 14.0 + 33.0 / 60.0;
        var expected = new SimpleDateTime(2025, 2, 19, ut, Calendars.Gregorian);
        var result = DateTimeConversion.ParseDateTimeFromText(items);
        Assert.That(result,  Is.EqualTo(expected));
    }

    [Test]
    public void TestParseDateTimeFromTextIncomplete()
    {
        string[] items = ["2025", "2"];
        var ex = Assert.Throws<ArgumentException>(() => DateTimeConversion.ParseDateTimeFromText(items));
        Assert.That(ex.Message, Does.Contain("Not enough items to define a date"));
    }

    [Test]
    public void TestParseDateTimeFromTextInvalidHour()
    {
        string[] items = ["2025", "2", "19", "ab", "33", "12"];
        var ex = Assert.Throws<FormatException>(() => DateTimeConversion.ParseDateTimeFromText(items));
        Assert.That(ex.Message, Does.Contain("Invalid hour format: ab"));
    }

    [Test]
    public void TestParseDateTimeFromTextInvalidMinute()
    {
        string[] items = ["2025", "2", "19", "14", "ab", "12"];
        var ex = Assert.Throws<FormatException>(() => DateTimeConversion.ParseDateTimeFromText(items));
        Assert.That(ex.Message, Does.Contain("Invalid minute format: ab"));
    }

    
    [Test]
    public void TestParseDateTimeFromTextInvalidSecond()
    {
        string[] items = ["2025", "2", "19", "14", "33", "ab"];
        var ex = Assert.Throws<FormatException>(() => DateTimeConversion.ParseDateTimeFromText(items));
        Assert.That(ex.Message, Does.Contain("Invalid second format: ab"));
    }

    [Test]
    [TestCase ("14", "50", "15", 14.0 + 50.0 / 60.0 + 15.0 / 3600.0)]
    [TestCase("-2", "20", "10", -2.0 - 20.0 / 60.0 - 10.0 / 3600.0)]   // negative value
    [TestCase ("", "", "", 0.0)]
    public void TestParseHmsFromTextHappyFlow(string hTxt, string mTxt, string sTxt, double expected)
    {
        var result = DateTimeConversion.ParseHmsFromText(hTxt, mTxt, sTxt);
        Assert.That(result, Is.EqualTo(expected).Within(1e-8));
    }
    
    [Test]
    public void TestParseSexTextFromFloat()
    {
        const double value = 1.5;
        const string expected = "1:30:00";

        var result = DateTimeConversion.ParseSexTextFromFloat(value);
        Assert.That(result, Is.EqualTo(expected));
    }
}
