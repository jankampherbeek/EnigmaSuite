// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Locational;
using Enigma.InputSupport.Validations;

namespace Enigma.Test.InputSupport.Validations;

[TestFixture]
public class TestGeoLatValidator
{
    private readonly double _delta = 0.00000001;
    private GeoLatValidator _geoLatValidator = new();

    [Test]
    public void TestHappyFlow()
    {
        int degree = 52;
        int minute = 13;
        int second = 0;
        int[] latInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLat.North;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out FullGeoLatitude? fullGeoLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(Result, Is.True);
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLatitude.Latitude, Is.EqualTo(52.216666666667).Within(_delta));
            Assert.That(fullGeoLatitude.GeoLatFullText, Is.EqualTo("+52:13:00"));
        });
    }

    [Test]
    public void TestNoSeconds()
    {
        int degree = 52;
        int minute = 13;
        int second = 0;
        int[] latInput = new int[] { degree, minute };
        var direction = Directions4GeoLat.North;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out FullGeoLatitude? fullGeoLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(Result, Is.True);
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLatitude.Latitude, Is.EqualTo(52.216666666667).Within(_delta));
            Assert.That(fullGeoLatitude.GeoLatFullText, Is.EqualTo("+52:13:00"));
        });
    }

    [Test]
    public void TestDirectionSouth()
    {
        int degree = 52;
        int minute = 13;
        int second = 0;
        int[] latInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLat.South;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out FullGeoLatitude? fullGeoLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(Result, Is.True);
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLatitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLatitude.Latitude, Is.EqualTo(-52.216666666667).Within(_delta));
            Assert.That(fullGeoLatitude.GeoLatFullText, Is.EqualTo("-52:13:00"));
        });
    }

    [Test]
    public void TestDegreeTooLarge()
    {
        int degree = 90;
        int minute = 0;
        int second = 0;
        int[] latInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLat.North;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out FullGeoLatitude? fullGeoLatitude);
        Assert.That(Result, Is.False);
    }

    [Test]
    public void TestDegreeNegative()
    {
        int degree = -52;
        int minute = 13;
        int second = 0;
        int[] latInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLat.North;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out FullGeoLatitude? fullGeoLatitude);
        Assert.That(Result, Is.False);
    }

    [Test]
    public void TestMinuteTooLarge()
    {
        int degree = 52;
        int minute = 113;
        int second = 0;
        int[] latInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLat.North;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out FullGeoLatitude? fullGeoLatitude);
        Assert.That(Result, Is.False);
    }

    [Test]
    public void TestSecondNegative()
    {
        int degree = 52;
        int minute = 13;
        int second = -11;
        int[] latInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLat.North;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out FullGeoLatitude? fullGeoLatitude);
        Assert.That(Result, Is.False);
    }


}