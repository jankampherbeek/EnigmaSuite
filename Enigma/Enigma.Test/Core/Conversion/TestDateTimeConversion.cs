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
    [TestCase ("2025","2","19","ab","33","12", "Invalid hour format: ab")]
    [TestCase ("2025","2","19","14","ab","12", "Invalid minute format: ab")]
    [TestCase ("2025","2","19","14","33","ab", "Invalid second format: ab")]
    public void TestParseDateTimeFromTextInvalidHour(string y, string mo, string d, string h, string mi, string s, string expectedMsg)
    {
        string[] items = [y, mo, d, h, mi, s];
        var ex = Assert.Throws<FormatException>(() => DateTimeConversion.ParseDateTimeFromText(items));
        Assert.That(ex.Message, Does.Contain(expectedMsg));
    }
 
    
    [Test]
    [TestCase ("2025", "2", "19", "25", "33", "12", "Hour (25) must be between -23 and 23")]
    [TestCase ("2025", "2", "19", "10", "90", "12", "Minute (90) must be between 0 and 59")]
    [TestCase ("2025", "2", "19", "10", "9", "82", "Second (82) must be between 0 and 59")]
    public void TestParseDateTimeFromTextHourOutOfRange(string y, string mo, string d, string h, string mi, string s, string expectedMsg)
    {
        string[] items = [y, mo, d, h, mi, s];
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => DateTimeConversion.ParseDateTimeFromText(items));
        Assert.That(ex.Message, Does.Contain(expectedMsg));
    }

    [Test]
    [TestCase ("14", "50", "15", 14.0 + 50.0 / 60.0 + 15.0 / 3600.0)]
    [TestCase("-2", "20", "10", -2.0 - 20.0 / 60.0 - 10.0 / 3600.0)]   // negative value
    [TestCase ("", "", "", 0.0)]
    public void TestParseHmsFromTextHappyFlow(string hTxt, string mTxt, string sTxt, double expected)
    {
        var result = DateTimeConversion.ParseDHmsToDoubleFromText(hTxt, mTxt, sTxt);
        Assert.That(result, Is.EqualTo(expected).Within(1e-8));
    }
    
    [Test]
    [TestCase ("14", "100", "15", $"Minute (100) must be between 0 and 59")]
    [TestCase ("14", "-10", "15", $"Minute (-10) must be between 0 and 59")]
    [TestCase ("14", "10", "150", $"Second (150) must be between 0 and 59")]
    [TestCase ("14", "10", "-15", $"Second (-15) must be between 0 and 59")]
    public void TestParseHmsFromTextValueOutOfRange(string hTxt, string mTxt, string sTxt, string expectedMsg)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => DateTimeConversion.ParseDHmsToDoubleFromText(hTxt, mTxt, sTxt));
        Assert.That(ex.Message, Does.Contain(expectedMsg));
    }
    
    
    [Test]
    [TestCase (1.5, "1:30:00")]
    [TestCase (-10.5, "-10:30:00")]
    [TestCase (240.75, "240:45:00")]
    [TestCase (-100.75, "-100:45:00")]
    public void TestParseSexTextFromFloat(double value, string expected)
    {
        var result = DateTimeConversion.ParseSexTextFromFloat(value);
        Assert.That(result, Is.EqualTo(expected));
    }
}
