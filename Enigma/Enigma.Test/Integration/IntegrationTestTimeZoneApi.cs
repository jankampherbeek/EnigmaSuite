// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.LocationAndTimeZones;
using Enigma.Core.LocationAndTimeZones;

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
    [TestCase("Europe/Amsterdam", 2025, 3, 1, 12, 0, 0, 1.0, false, "CET")]
    [TestCase("Europe/Amsterdam", 2025, 4, 6, 21, 20, 0, 2.0, true, "CEST")]
    [TestCase("Europe/Amsterdam", 1928, 5, 1, 12, 0, 0, 0.32555555555556, false, "Zone 0.326")]
    [TestCase("Europe/London", 2001,12,28, 12, 16, 0, 0.0, false, "GMT/BST")]
    [TestCase("Europe/Brussels", 1864, 6, 12, 7, 0, 0, 0.29166666666666, false, "LMT")]
    [TestCase("Europe/Paris", 1973,4,19,8,30,0, 1.0, false,"CET")]
    [TestCase("Europe/Paris", 1730, 8, 30, 9,7,0, 0.15583333333333332, false, "LMT")]
    [TestCase("Europe/Paris",1953,8,2,14,0,0,1.0, false, "CET")]
    [TestCase("Europe/Rome", 1931,3,1,6,0,0,1.0, false,"CET")]
    [TestCase("Europe/Dublin", 1955,5,23,19,0,0, 1.0, true, "GMT/IST")]
    
    public void TestGetTimeZoneDst(string tzIndicator, int y, int mo, int d, int h, int mi, int s, 
        double expectedOffset, bool expectedDst, string expectedTzName)
    {
        var dt = new DateTimeHms(y, mo, d, h, mi, s);
        var result = _tzApi.GetTimeZoneDst(dt, tzIndicator);
        Assert.Multiple(() =>
        {
            Assert.That(result.Dst, Is.EqualTo(expectedDst));
            Assert.That(result.TzName, Is.EqualTo(expectedTzName));
            Assert.That(result.Offset, Is.EqualTo(expectedOffset).Within(1E-8));
        });
    }
}