// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Frontend.Helpers.Validations;


namespace Enigma.Test.Frontend.Helpers.Validations;

[TestFixture]
public class TestGeoLongValidator
{
    private readonly GeoLongValidator _geoLongValidator = new();
    private const double DELTA = 0.00000001;

    [Test]
    public void TestHappyFlow()
    {
        const int degree = 6;
        const int minute = 54;
        const int second = 0;
        int[] longInput = { degree, minute, second };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        bool result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out FullGeoLongitude? fullGeoLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLongitude.Longitude, Is.EqualTo(6.9).Within(DELTA));
            Assert.That(fullGeoLongitude.Direction, Is.EqualTo(Directions4GeoLong.East));
            Assert.That(fullGeoLongitude.GeoLongFullText, Is.EqualTo("+6:54:00"));
        });
    }

    [Test]
    public void TestNoSeconds()
    {
        const int degree = 6;
        const int minute = 54;
        const int second = 0;
        int[] longInput = { degree, minute };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        bool result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out FullGeoLongitude? fullGeoLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLongitude.Longitude, Is.EqualTo(6.9).Within(DELTA));
            Assert.That(fullGeoLongitude.Direction, Is.EqualTo(Directions4GeoLong.East));
            Assert.That(fullGeoLongitude.GeoLongFullText, Is.EqualTo("+6:54:00"));
        });
    }

    [Test]
    public void TestDirectionWest()
    {
        const int degree = 6;
        const int minute = 54;
        const int second = 0;
        int[] longInput = { degree, minute, second };
        const Directions4GeoLong direction = Directions4GeoLong.West;
        bool result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out FullGeoLongitude? fullGeoLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLongitude.Longitude, Is.EqualTo(-6.9).Within(DELTA));
            Assert.That(fullGeoLongitude.Direction, Is.EqualTo(Directions4GeoLong.West));
            Assert.That(fullGeoLongitude.GeoLongFullText, Is.EqualTo("-6:54:00"));
        });
    }

    [Test]
    public void TestDegreeTooLarge()
    {
        const int degree = 181;
        const int minute = 54;
        const int second = 0;
        int[] longInput = { degree, minute, second };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        bool result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out _);
        Assert.That(result, Is.False);
    }


    [Test]
    public void TestMinuteNegative()
    {
        const int degree = 52;
        const int minute = -13;
        const int second = 0;
        int[] longInput = { degree, minute, second };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        bool result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out _);
        Assert.That(result, Is.False);
    }


    [Test]
    public void TestSecondTooLarge()
    {
        const int degree = 6;
        const int minute = 54;
        const int second = 60;
        int[] longInput = { degree, minute, second };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        bool result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out _);
        Assert.That(result, Is.False);
    }
}