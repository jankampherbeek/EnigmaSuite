// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Persistency.Converters;
using Enigma.Persistency.Interfaces;

namespace Enigma.Text.Persistency.Converters;

[TestFixture]
public class TestLocationConversion
{
    private double _delta = 0.00000001;
    private ILocationCheckedConversion _locationConversion;

    [SetUp]
    public void SetUp()
    {
        _locationConversion = new LocationCheckedConversion();
    }


    [Test]
    public void TestHappyFlowLongitude()
    {
        string csvLongitude = "6:52:31:E";
        double expectedLongitude = 6.87527777778;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLongitude(csvLongitude);
        Assert.That(result.Item2, Is.EqualTo(true));
        Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(_delta));
    }

    [Test]
    public void TestHappyFlowLatitude()
    {
        string csvLatitude = "52:12:37:N";
        double expectedLongitude = 52.21027777778;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLatitude(csvLatitude);
        Assert.That(result.Item2, Is.EqualTo(true));
        Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(_delta));
    }

    [Test]
    public void TestSpacesInInput()
    {
        string csvLongitude = " 6:52:31:E  ";
        double expectedLongitude = 6.87527777778;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLongitude(csvLongitude);
        Assert.That(result.Item2, Is.EqualTo(true));
        Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(_delta));
    }

    [Test]
    public void TestDegreesTooLargeLongitude()
    {
        string csvLongitude = "186:52:31:E";
        double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLongitude(csvLongitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(_delta));
    }

    [Test]
    public void TestMinutesTooLargeLongitude()
    {
        string csvLongitude = "6:62:31:E";
        double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLongitude(csvLongitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(_delta));
    }

    [Test]
    public void TestSecondsTooLargeLongitude()
    {
        string csvLongitude = "6:52:61:E";
        double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLongitude(csvLongitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(_delta));
    }


    [Test]
    public void TestDegreesTooSmallLongitude()
    {
        string csvLongitude = "-6:52:31:E";
        double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLongitude(csvLongitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(_delta));
    }

    [Test]
    public void TestMinutesTooSmallLongitude()
    {
        string csvLongitude = "6:-52:31:E";
        double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLongitude(csvLongitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(_delta));
    }

    [Test]
    public void TestSecondsTooSmallLongitude()
    {
        string csvLongitude = "6:52:-31:E";
        double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLongitude(csvLongitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(_delta));
    }

    [Test]
    public void TestWrongDirectionLongitude()
    {
        string csvLongitude = "6:52:31:N";
        double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLongitude(csvLongitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(_delta));
    }

    [Test]
    public void TestNotNumericLongitude()
    {
        string csvLongitude = "6:aa:31:E";
        double expectedLongitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLongitude(csvLongitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLongitude).Within(_delta));
    }

    [Test]
    public void TestDegreesTooLargeLatitude()
    {
        string csvLatitude = "96:12:37:N";
        double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLatitude(csvLatitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(_delta));
    }

    [Test]
    public void TestMinutesTooLargeLatitude()
    {
        string csvLatitude = "6:62:37:N";
        double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLatitude(csvLatitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(_delta));
    }

    [Test]
    public void TestSecondsTooLargeLatitude()
    {
        string csvLatitude = "6:12:67:N";
        double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLatitude(csvLatitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(_delta));
    }

    [Test]
    public void TestDegreesTooSmallLatitude()
    {
        string csvLatitude = "-52:12:37:N";
        double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLatitude(csvLatitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(_delta));
    }

    [Test]
    public void TestMinutesTooSmallLatitude()
    {
        string csvLatitude = "52:-12:37:N";
        double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLatitude(csvLatitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(_delta));
    }

    [Test]
    public void TestSecondsTooSmallLatitude()
    {
        string csvLatitude = "52:12:-37:N";
        double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLatitude(csvLatitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(_delta));
    }

    [Test]
    public void TestWrongDirectionLatitude()
    {
        string csvLatitude = "52:12:37:E";
        double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLatitude(csvLatitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(_delta));
    }

    [Test]
    public void TestNotNumericLatitude()
    {
        string csvLatitude = "52:12:k7:E";
        double expectedLatitude = 0.0;
        Tuple<double, bool> result = _locationConversion.StandardCsvToLatitude(csvLatitude);
        Assert.That(result.Item2, Is.EqualTo(false));
        Assert.That(result.Item1, Is.EqualTo(expectedLatitude).Within(_delta));
    }

}