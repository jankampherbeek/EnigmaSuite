// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Core.Persistency.Helpers;

namespace Enigma.Test.Core.Persistency;

[TestFixture]
public class TestLocationConversion
{
    private const double DELTA = 0.00000001;
    private ILocationCheckedConversion? _locationConversion;

    [SetUp]
    public void SetUp()
    {
        _locationConversion = new LocationCheckedConversion();
    }


    [Test]
    public void TestHappyFlowLongitude()
    {
        const string csvLongitude = "6:52:31:E";
        const double expectedLongitude = 6.87527777778;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLongitude(csvLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(true));
            Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestHappyFlowLatitude()
    {
        const string csvLatitude = "52:12:37:N";
        const double expectedLongitude = 52.21027777778;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLatitude(csvLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(true));
            Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestSpacesInInput()
    {
        const string csvLongitude = " 6:52:31:E  ";
        const double expectedLongitude = 6.87527777778;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLongitude(csvLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(true));
            Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestDegreesTooLargeLongitude()
    {
        const string csvLongitude = "186:52:31:E";
        const double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLongitude(csvLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestMinutesTooLargeLongitude()
    {
        const string csvLongitude = "6:62:31:E";
        const double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLongitude(csvLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestSecondsTooLargeLongitude()
    {
        const string csvLongitude = "6:52:61:E";
        const double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLongitude(csvLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestDegreesTooSmallLongitude()
    {
        const string csvLongitude = "-6:52:31:E";
        const double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLongitude(csvLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestMinutesTooSmallLongitude()
    {
        const string csvLongitude = "6:-52:31:E";
        const double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLongitude(csvLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestSecondsTooSmallLongitude()
    {
        const string csvLongitude = "6:52:-31:E";
        const double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLongitude(csvLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestWrongDirectionLongitude()
    {
        const string csvLongitude = "6:52:31:N";
        const double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLongitude(csvLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestNotNumericLongitude()
    {
        const string csvLongitude = "6:aa:31:E";
        const double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLongitude(csvLongitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestDegreesTooLargeLatitude()
    {
        const string csvLatitude = "96:12:37:N";
        const double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLatitude(csvLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(DELTA));
        });
    }

    [Test]
    public void TestMinutesTooLargeLatitude()
    {
        const string csvLatitude = "6:62:37:N";
        const double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLatitude(csvLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(DELTA));
        });
    }

    [Test]
    public void TestSecondsTooLargeLatitude()
    {
        const string csvLatitude = "6:12:67:N";
        const double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLatitude(csvLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(DELTA));
        });
    }

    [Test]
    public void TestDegreesTooSmallLatitude()
    {
        const string csvLatitude = "-52:12:37:N";
        const double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLatitude(csvLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(DELTA));
        });
    }

    [Test]
    public void TestMinutesTooSmallLatitude()
    {
        const string csvLatitude = "52:-12:37:N";
        const double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLatitude(csvLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(DELTA));
        });
    }

    [Test]
    public void TestSecondsTooSmallLatitude()
    {
        const string csvLatitude = "52:12:-37:N";
        const double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLatitude(csvLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(DELTA));
        });
    }

    [Test]
    public void TestWrongDirectionLatitude()
    {
        const string csvLatitude = "52:12:37:E";
        const double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLatitude(csvLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(DELTA));
        });
    }

    [Test]
    public void TestNotNumericLatitude()
    {
        const string csvLatitude = "52:12:k7:E";
        const double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion!.StandardCsvToLatitude(csvLatitude);
        Assert.Multiple(() =>
        {
            Assert.That(result.Item2, Is.EqualTo(false));
            Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(DELTA));
        });
    }
}