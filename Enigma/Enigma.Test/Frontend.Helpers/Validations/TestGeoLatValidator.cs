// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Frontend.Helpers.Validations;

namespace Enigma.Test.Frontend.Helpers.Validations;

[TestFixture]
public class TestGeoLatValidator
{
    private const double DELTA = 0.00000001;
    private readonly GeoLatValidator _geoLatValidator = new();

    [Test]
    public void TestHappyFlow()
    {
        const int degree = 52;
        const int minute = 13;
        const int second = 0;
        int[] latInput = { degree, minute, second };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        bool result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out FullGeoLatitude? fullGeoLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLatitude.Latitude, Is.EqualTo(52.216666666667).Within(DELTA));
            Assert.That(fullGeoLatitude.GeoLatFullText, Is.EqualTo("+52:13:00"));
        });
    }

    [Test]
    public void TestNoSeconds()
    {
        const int degree = 52;
        const int minute = 13;
        const int second = 0;
        int[] latInput = { degree, minute };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        bool result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out FullGeoLatitude? fullGeoLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLatitude.Latitude, Is.EqualTo(52.216666666667).Within(DELTA));
            Assert.That(fullGeoLatitude.GeoLatFullText, Is.EqualTo("+52:13:00"));
        });
    }

    [Test]
    public void TestDirectionSouth()
    {
        const int degree = 52;
        const int minute = 13;
        const int second = 0;
        int[] latInput = { degree, minute, second };
        const Directions4GeoLat direction = Directions4GeoLat.South;
        bool result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out FullGeoLatitude? fullGeoLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLatitude.Latitude, Is.EqualTo(-52.216666666667).Within(DELTA));
            Assert.That(fullGeoLatitude.GeoLatFullText, Is.EqualTo("-52:13:00"));
        });
    }

    [Test]
    public void TestDegreeTooLarge()
    {
        const int degree = 90;
        const int minute = 0;
        const int second = 0;
        int[] latInput = { degree, minute, second };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        bool result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out FullGeoLatitude? _);
        Assert.That(result, Is.False);
    }

    [Test]
    public void TestDegreeNegative()
    {
        const int degree = -52;
        const int minute = 13;
        const int second = 0;
        int[] latInput = { degree, minute, second };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        bool result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out _);
        Assert.That(result, Is.False);
    }

    [Test]
    public void TestMinuteTooLarge()
    {
        const int degree = 52;
        const int minute = 113;
        const int second = 0;
        int[] latInput = { degree, minute, second };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        bool result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out _);
        Assert.That(result, Is.False);
    }

    [Test]
    public void TestSecondNegative()
    {
        const int degree = 52;
        const int minute = 13;
        const int second = -11;
        int[] latInput = { degree, minute, second };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        bool result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out _);
        Assert.That(result, Is.False);
    }


}