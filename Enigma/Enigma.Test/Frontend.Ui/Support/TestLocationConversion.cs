// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Frontend.Ui;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Test.Frontend.Ui.Support;

[TestFixture]
public sealed class TestLocationConversion
{
    private readonly ILocationConversion _locationConversion =App.ServiceProvider.GetRequiredService<ILocationConversion>();


    [Test]
    public void TestNorthEast()
    {
        const string locationName = "Enschede - Netherlands";
        const double latitude = 52.21666666667;
        const double longitude = 6.9;
        const string expectedValue = "Enschede - Netherlands 52\u00b013′00″ N / 6\u00b054′00″ E";
        string retrievedValue = _locationConversion.CreateLocationDescription(locationName, latitude, longitude);
        Assert.That(retrievedValue.Replace(',', '.'), Is.EqualTo(expectedValue.Replace(',', '.')));
    }

    [Test]
    public void TestSouthWest()
    {
        const string locationName = "Rio de Janeiro Brazil";
        const double latitude = -22.9;
        const double longitude = -43.23333333333;
        const string expectedValue = "Rio de Janeiro Brazil 22\u00b054′00″ S / 43\u00b014′00″ W";
        string retrievedValue = _locationConversion.CreateLocationDescription(locationName, latitude, longitude);
        Assert.That(retrievedValue.Replace(',', '.'), Is.EqualTo(expectedValue.Replace(',', '.')));
    }

    [Test]
    public void TestNoName()
    {
        const string locationName = "";
        const double latitude = 52.21666666667;
        const double longitude = 6.9;
        const string expectedValue = "No name for location 52°13′00″ N / 6°54′00″ E";
        string retrievedValue = _locationConversion.CreateLocationDescription(locationName, latitude, longitude);
        Assert.That(retrievedValue.Replace(',', '.'), Is.EqualTo(expectedValue.Replace(',', '.')));
    }

}