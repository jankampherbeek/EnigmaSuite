// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.LocationAndTimezones;
using Enigma.Domain.LocationsZones;

namespace Enigma.Test.IntegrationTests;

[TestFixture]
public class TestIntTimezoneApi
{
    [Test]
    public void TestTimezoneApi()
    {
        var _api = new TimezoneApi();
        var dt = new DateTimeHms(2025, 4, 1, 22, 18, 0);
        const string tzIndicator = "Europe/Amsterdam";
        var zoneInfo = _api.ActualTimezone(dt, tzIndicator);
        Assert.Multiple(() =>
        {
            Assert.That(zoneInfo.Offset, Is.EqualTo(2.0).Within(1E-8));
            Assert.That(zoneInfo.Dst, Is.EqualTo(true));
            Assert.That(zoneInfo.TzName, Is.EqualTo("CEST"));
        });
    }
}