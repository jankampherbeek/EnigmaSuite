// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Locational;
using Enigma.InputSupport.Validations;


namespace Enigma.Test.InputSupport.Validations;

[TestFixture]
public class TestGeoLongValidator
{
    private readonly GeoLongValidator _geoLongValidator = new();
    private readonly double delta = 0.00000001;

    [Test]
    public void TestHappyFlow()
    {
        int degree = 6;
        int minute = 54;
        int second = 0;
        int[] longInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLong.East;
        bool Result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out FullGeoLongitude? fullGeoLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(Result, Is.True);
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLongitude.Longitude, Is.EqualTo(6.9).Within(delta));
            Assert.That(fullGeoLongitude.Direction, Is.EqualTo(Directions4GeoLong.East));
            Assert.That(fullGeoLongitude.GeoLongFullText, Is.EqualTo("+6:54:00"));
        });
    }

    [Test]
    public void TestNoSeconds()
    {
        int degree = 6;
        int minute = 54;
        int second = 0;
        int[] longInput = new int[] { degree, minute };
        var direction = Directions4GeoLong.East;
        bool Result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out FullGeoLongitude? fullGeoLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(Result, Is.True);
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLongitude.Longitude, Is.EqualTo(6.9).Within(delta));
            Assert.That(fullGeoLongitude.Direction, Is.EqualTo(Directions4GeoLong.East));
            Assert.That(fullGeoLongitude.GeoLongFullText, Is.EqualTo("+6:54:00"));
        });
    }

    [Test]
    public void TestDirectionWest()
    {
        int degree = 6;
        int minute = 54;
        int second = 0;
        int[] longInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLong.West;
        bool Result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out FullGeoLongitude? fullGeoLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(Result, Is.True);
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[0], Is.EqualTo(degree));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[1], Is.EqualTo(minute));
            Assert.That(fullGeoLongitude.DegreeMinuteSecond[2], Is.EqualTo(second));
            Assert.That(fullGeoLongitude.Longitude, Is.EqualTo(-6.9).Within(delta));
            Assert.That(fullGeoLongitude.Direction, Is.EqualTo(Directions4GeoLong.West));
            Assert.That(fullGeoLongitude.GeoLongFullText, Is.EqualTo("-6:54:00"));
        });
    }

    [Test]
    public void TestDegreeTooLarge()
    {
        int degree = 181;
        int minute = 54;
        int second = 0;
        int[] longInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLong.East;
        bool Result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out _);
        Assert.That(Result, Is.False);
    }


    [Test]
    public void TestMinuteNegative()
    {
        int degree = 52;
        int minute = -13;
        int second = 0;
        int[] longInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLong.East;
        bool Result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out _);
        Assert.That(Result, Is.False);
    }


    [Test]
    public void TestSecondTooLarge()
    {
        int degree = 6;
        int minute = 54;
        int second = 60;
        int[] longInput = new int[] { degree, minute, second };
        var direction = Directions4GeoLong.East;
        bool Result = _geoLongValidator.CreateCheckedLongitude(longInput, direction, out _);
        Assert.That(Result, Is.False);
    }
}