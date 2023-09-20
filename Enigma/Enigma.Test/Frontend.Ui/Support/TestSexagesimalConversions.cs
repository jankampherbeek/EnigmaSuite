// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.References;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Test.Frontend.Helpers;

[TestFixture]
public class TestSexagesimalConversions
{
    private readonly ISexagesimalConversions _conversions = new SexagesimalConversions();
    private const double DELTA = 0.00000001;

    [Test]
    public void TestInputGeoLatToDoubleHappyFlow()
    {
        string[] inputLat = { "52", "13", "0" };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        const double expected = 52.2166666667;
        Assert.That(_conversions.InputGeoLatToDouble(inputLat, direction), Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestInputGeoLatToDoubleWest()
    {
        string[] inputLat = { "52", "13", "0" };
        const Directions4GeoLat direction = Directions4GeoLat.South;
        const double expected = -52.2166666667;
        Assert.That(_conversions.InputGeoLatToDouble(inputLat, direction), Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestInputGeoLatToDoubleError()
    {
        string[] inputLat = { "xx", "13", "0" };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        Assert.Throws<ArgumentException>(() => _conversions.InputGeoLatToDouble(inputLat, direction));
    }

    [Test]
    public void TestInputGeoLongToDoubleHappyFlow()
    {
        string[] inputLong = { "12", "15", "20" };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        const double expected = 12.25555555556;
        Assert.That(_conversions.InputGeoLongToDouble(inputLong, direction), Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestInputGeoLongToDoubleWest()
    {
        string[] inputLong = { "12", "15", "20" };
        const Directions4GeoLong direction = Directions4GeoLong.West;
        const double expected = -12.25555555556;
        Assert.That(_conversions.InputGeoLongToDouble(inputLong, direction), Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestInputGeoLongToDoubleError()
    {
        string[] inputLong = { "12", "x", "20" };
        const Directions4GeoLong direction = Directions4GeoLong.West;
        Assert.Throws<ArgumentException>(() => _conversions.InputGeoLongToDouble(inputLong, direction));
    }

    [Test]
    public void TestInputTimeToDoubleHoursHappyFlow()
    {
        string[] inputTime = { "8", "37", "30" };
        const double expected = 8.625;
        Assert.That(_conversions.InputTimeToDoubleHours(inputTime), Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestInputTimeToDoubleHoursError()
    {
        string[] inputTime = { "8", "xx", "30" };
        Assert.Throws<ArgumentException>(() => _conversions.InputTimeToDoubleHours(inputTime));
    }
}