// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Locational;
using Enigma.Frontend.InputSupport.Conversions;

namespace Enigma.Test.Frontend.InputSupport.Conversions;

[TestFixture]
public class TestSexagesimalConversions
{
    readonly private ISexagesimalConversions conversions = new SexagesimalConversions();
    readonly private double delta = 0.00000001;

    [Test]
    public void TestInputGeoLatToDoubleHappyFlow()
    {
        string[] inputLat = new string[] { "52", "13", "0" };
        Directions4GeoLat direction = Directions4GeoLat.North;
        double expected = 52.2166666667;
        Assert.AreEqual(expected, conversions.InputGeoLatToDouble(inputLat, direction), delta);
    }

    [Test]
    public void TestInputGeoLatToDoubleWest()
    {
        string[] inputLat = new string[] { "52", "13", "0" };
        Directions4GeoLat direction = Directions4GeoLat.South;
        double expected = -52.2166666667;
        Assert.AreEqual(expected, conversions.InputGeoLatToDouble(inputLat, direction), delta);
    }

    [Test]
    public void TestInputGeoLatToDoubleError()
    {
        string[] inputLat = new string[] { "xx", "13", "0" };
        Directions4GeoLat direction = Directions4GeoLat.North;
        Assert.Throws<ArgumentException>(() => conversions.InputGeoLatToDouble(inputLat, direction));
    }

    [Test]
    public void TestInputGeoLongToDoubleHappyFlow()
    {
        string[] inputLong = new string[] { "12", "15", "20" };
        Directions4GeoLong direction = Directions4GeoLong.East;
        double expected = 12.25555555556;
        Assert.AreEqual(expected, conversions.InputGeoLongToDouble(inputLong, direction), delta);
    }

    [Test]
    public void TestInputGeoLongToDoubleWest()
    {
        string[] inputLong = new string[] { "12", "15", "20" };
        Directions4GeoLong direction = Directions4GeoLong.West;
        double expected = -12.25555555556;
        Assert.AreEqual(expected, conversions.InputGeoLongToDouble(inputLong, direction), delta);
    }

    [Test]
    public void TestInputGeoLongToDoubleError()
    {
        string[] inputLong = new string[] { "12", "x", "20" };
        Directions4GeoLong direction = Directions4GeoLong.West;
        Assert.Throws<ArgumentException>(() => conversions.InputGeoLongToDouble(inputLong, direction));
    }

    [Test]
    public void TestInputTimeToDoubleHoursHappyFlow()
    {
        string[] inputTime = new string[] { "8", "37", "30" };
        double expected = 8.625;
        Assert.AreEqual(expected, conversions.InputTimeToDoubleHours(inputTime), delta);
    }

    [Test]
    public void TestInputTimeToDoubleHoursError()
    {
        string[] inputTime = new string[] { "8", "xx", "30" };
        Assert.Throws<ArgumentException>(() => conversions.InputTimeToDoubleHours(inputTime));
    }
}