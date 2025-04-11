// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.LocationAndTimeZones;
using Enigma.Core.LocationAndTimeZones;
using NUnit.Framework.Constraints;

namespace Enigma.Test.Integration;

[TestFixture]
public class IntegrationTestTimeZoneApi: IntegrationTestBase
{
    private readonly ITimeZoneApi _tzApi;

    public IntegrationTestTimeZoneApi()
    {
        _tzApi = GetService<ITimeZoneApi>();
    }

    [Test]
    [TestCase("Europe/Amsterdam", 2025, 3, 1, 12, 0, 0, 1.0, false, "CET", false, false)]
    [TestCase("Europe/Amsterdam", 2025, 4, 6, 21, 20, 0, 2.0, true, "CEST", false, false)]
    [TestCase("Europe/Amsterdam", 1928, 5, 1, 12, 0, 0, 0.32555555555556, false, "Zone 0.326", false, false)]
    [TestCase("Europe/London", 2001,12,28, 12, 16, 0, 0.0, false, "GMT/BST", false, false)]
    [TestCase("Europe/Brussels", 1864, 6, 12, 7, 0, 0, 0.29166666666666, false, "LMT", false, false)]
    [TestCase("Europe/Paris", 1973,4,19,8,30,0, 1.0, false,"CET", false, false)]
    [TestCase("Europe/Paris", 1730, 8, 30, 9,7,0, 0.15583333333333332, false, "LMT", false, false)]
    [TestCase("Europe/Paris",1953,8,2,14,0,0,1.0, false, "CET", false, false)]
    [TestCase("Europe/Rome", 1931,3,1,6,0,0,1.0, false,"CET", false, false)]
    [TestCase("Europe/Dublin", 1955,5,23,19,0,0, 1.0, true, "GMT/IST", false, false)]
    [TestCase("Europe/Oslo", 1947,5,15,23,55,0,1.0, false, "CET", false, false)]
    [TestCase("America/Argentina/Buenos_Aires", 1895,7,4,17,28,0, -4.28, false, "CMT", false, false)]  
    [TestCase("America/New_York", 1993,10,13,12,50,0, -4.0, true, "EDT", false, false)]
    [TestCase("America/New_York", 1887,9,13,2,15,0, -5.0, false, "ET", false, false)]
    [TestCase("Europe/Amsterdam", 1953,1,29,8,37,30,1.0, false, "CET", false, false)]
    [TestCase("Europe/Paris", 1809,9,28,17,0,0, 0.15583333333333332, false, "LMT", false, false)]
    [TestCase("Europe/Rome", -99,6, 29, 0,0,0, 0.8322222222, false, "LMT", false, false)]
    [TestCase("America/New_York", 1997,9,12,9,0,0,-4.0, true,"EDT", false, false)]
    [TestCase("Europe/Rome",1968,8,22,17,10,0,2.0, true, "CEST", false, false)]
    [TestCase("America/New_York", 1949,5,15,23,39,0,-4.0, true, "EDT", false, false)]
    // Invalid DST
    [TestCase("Europe/Amsterdam", 2025, 3, 30, 2, 15, 0, 2.0, true, "CEST", true, false)]
    // Ambiguous DST
    [TestCase("Europe/Amsterdam", 2025, 10, 26, 2, 15, 0, 1.0, false, "CET", false, true)]
    // Just before begin of DST
    [TestCase("Europe/Amsterdam", 2025, 3, 30, 1, 55, 0, 1.0, false, "CET", false, false)]
    // Just after end of of DST
    [TestCase("Europe/Amsterdam", 2025, 10, 26, 3, 05, 0, 1.0, false, "CET", false, false)]
    // Just after begin of DST    
    [TestCase("Europe/Amsterdam", 2025, 3, 30, 3, 05, 0, 2.0, true, "CEST", false, false)]
    // Just before end of DST
    [TestCase("Europe/Amsterdam", 2025, 10, 26, 1, 55, 0, 2.0, true, "CEST", false, false)]
    
    public void TestGetTimeZoneDst(string tzIndicator, int y, int mo, int d, int h, int mi, int s, 
        double expectedOffset, bool expectedDst, string expectedTzName, bool isInvalid, bool isAmbiguous)
    {
        var dt = new DateTimeHms(y, mo, d, h, mi, s);
        var result = _tzApi.GetTimeZoneDst(dt, tzIndicator);
        Assert.Multiple(() =>
        {
            Assert.That(result.Dst, Is.EqualTo(expectedDst));
            Assert.That(result.TzName, Is.EqualTo(expectedTzName));
            Assert.That(result.Offset, Is.EqualTo(expectedOffset).Within(1E-8));
            Assert.That(result.Invalid, Is.EqualTo(isInvalid));
            Assert.That(result.Ambiguous, Is.EqualTo(isAmbiguous));
        });
    }
}