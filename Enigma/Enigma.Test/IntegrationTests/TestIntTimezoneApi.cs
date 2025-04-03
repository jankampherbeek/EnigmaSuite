// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.LocationAndTimezones;
using Enigma.Core.Calc;
using Enigma.Core.LocationAndTimeZones;
using Enigma.Domain.LocationsZones;
using Enigma.Facades.Se;
using Enigma.Frontend.Ui;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Test.IntegrationTests;

[TestFixture]
public class TestIntTimezoneApi
{
    private ITimezoneApi _api;
    private ITzHandler _tzHandler;
    private IJulDayCalc _jdCalc;
    private IDstHandler _dstHandler;
    private IJulDayFacade _jdFacade;
    private IDstParser _dstParser;
    
    [Test]
    public void TestTimezoneapi()
    {
        init();
        var dt = new DateTimeHms(2025, 4, 1, 22, 18, 0);
        var tzIndicator = "Europe/Amsterdam";
        var (zoneInfo, apiResult) = _api.ActualTimezone(dt, tzIndicator);
        Assert.That(zoneInfo.Offset, Is.EqualTo(2.0).Within(1E-8));
        Assert.That(zoneInfo.Dst, Is.EqualTo(true));
        Assert.That(zoneInfo.TzName, Is.EqualTo("CEST"));
    }

    private void init()
    {
        _jdFacade = new JulDayFacade();
        _jdCalc = new JulDayCalc(_jdFacade);
        _dstHandler = new DstHandler(_jdCalc, _dstParser);
        _tzHandler = new TzHandler(_jdCalc, _dstHandler);
        _api = new TimezoneApi(_tzHandler);
    }
    
}