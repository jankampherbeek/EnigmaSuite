// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Test.Frontend.Ui.Support;

[TestFixture]
public sealed class TestLocationConversion
{
    private readonly ILocationConversion _locationConversion = new LocationConversion();


    [Test]
    public void TestNorthEast()
    {
        string locationName = "Enschede - Netherlands";
        double latitude = 52.21666666667;
        double longitude = 6.9;
        string expectedValue = "Enschede - Netherlands 52.21666666667 N / 6.9 E";
        string retrievedValue = _locationConversion.CreateLocationDescription(locationName, latitude, longitude);
        Assert.That(retrievedValue.Replace(',', '.'), Is.EqualTo(expectedValue.Replace(',', '.')));
    }

    [Test]
    public void TestSouthWest()
    {
        string locationName = "Rio de Janeiro Brazil";
        double latitude = -22.9;
        double longitude = -43.23333333333;
        string expectedValue = "Rio de Janeiro Brazil 22.9 S / 43.23333333333 W";
        string retrievedValue = _locationConversion.CreateLocationDescription(locationName, latitude, longitude);
        Assert.That(retrievedValue.Replace(',', '.'), Is.EqualTo(expectedValue.Replace(',', '.')));
    }

    [Test]
    public void TestNoName()
    {
        string locationName = "";
        double latitude = 52.21666666667;
        double longitude = 6.9;
        string expectedValue = "No name for location 52.21666666667 N / 6.9 E";
        string retrievedValue = _locationConversion.CreateLocationDescription(locationName, latitude, longitude);
        Assert.That(retrievedValue.Replace(',', '.'), Is.EqualTo(expectedValue.Replace(',', '.')));
    }

}