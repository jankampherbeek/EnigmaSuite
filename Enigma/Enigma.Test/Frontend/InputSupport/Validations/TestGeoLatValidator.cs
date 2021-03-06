// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Locational;
using Enigma.Frontend.InputSupport.Validations;

namespace Enigma.Test.Frontend.InputSupport.Validations;

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
        FullGeoLatitude? fullGeoLatitude = null;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out fullGeoLatitude);
        Assert.IsTrue(Result);
        Assert.That(fullGeoLatitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
        Assert.That(fullGeoLatitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
        Assert.That(fullGeoLatitude.DegreeMinuteSecond[2], Is.EqualTo(second));
        Assert.That(fullGeoLatitude.Latitude, Is.EqualTo(52.216666666667).Within(_delta));
        Assert.That(fullGeoLatitude.GeoLatFullText, Is.EqualTo("+52:13:00"));
    }

    [Test]
    public void TestNoSeconds()
    {
        int degree = 52;
        int minute = 13;
        int second = 0;
        int[] latInput = new int[] { degree, minute };
        var direction = Directions4GeoLat.North;
        FullGeoLatitude? fullGeoLatitude = null;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out fullGeoLatitude);
        Assert.IsTrue(Result);
        Assert.That(fullGeoLatitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
        Assert.That(fullGeoLatitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
        Assert.That(fullGeoLatitude.DegreeMinuteSecond[2], Is.EqualTo(second));
        Assert.That(fullGeoLatitude.Latitude, Is.EqualTo(52.216666666667).Within(_delta));
        Assert.That(fullGeoLatitude.GeoLatFullText, Is.EqualTo("+52:13:00"));
    }

    [Test]
    public void TestDirectionSouth()
    {
        int degree = 52;
        int minute = 13;
        int second = 0;
        int[] latInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLat.South;
        FullGeoLatitude? fullGeoLatitude = null;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out fullGeoLatitude);
        Assert.IsTrue(Result);
        Assert.That(fullGeoLatitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
        Assert.That(fullGeoLatitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
        Assert.That(fullGeoLatitude.DegreeMinuteSecond[2], Is.EqualTo(second));
        Assert.That(fullGeoLatitude.Latitude, Is.EqualTo(-52.216666666667).Within(_delta));
        Assert.That(fullGeoLatitude.GeoLatFullText, Is.EqualTo("-52:13:00"));
    }

    [Test]
    public void TestDegreeTooLarge()
    {
        int degree = 90;
        int minute = 0;
        int second = 0;
        int[] latInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLat.North;
        FullGeoLatitude? fullGeoLatitude = null;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out fullGeoLatitude);
        Assert.IsFalse(Result);
    }

    [Test]
    public void TestDegreeNegative()
    {
        int degree = -52;
        int minute = 13;
        int second = 0;
        int[] latInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLat.North;
        FullGeoLatitude? fullGeoLatitude = null;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out fullGeoLatitude);
        Assert.IsFalse(Result);
    }

    [Test]
    public void TestMinuteTooLarge()
    {
        int degree = 52;
        int minute = 113;
        int second = 0;
        int[] latInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLat.North;
        FullGeoLatitude? fullGeoLatitude = null;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out fullGeoLatitude);
        Assert.IsFalse(Result);
    }

    [Test]
    public void TestSecondNegative()
    {
        int degree = 52;
        int minute = 13;
        int second = -11;
        int[] latInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLat.North;
        FullGeoLatitude? fullGeoLatitude = null;
        bool Result = _geoLatValidator.CreateCheckedLatitude(latInput, direction, out fullGeoLatitude);
        Assert.IsFalse(Result);
    }


}