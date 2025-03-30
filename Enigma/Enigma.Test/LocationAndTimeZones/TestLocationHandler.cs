// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.LocationAndTimeZones;

namespace Enigma.Test.LocationAndTimeZones;

[TestFixture]
public class TestLocationHandler
{
    [Test]
    public void TestGetAllCountries()
    {
        ILocationHandler handler = new LocationHandler();
        var allCountries = handler.AllCountries();
        Assert.That(allCountries, Has.Count.EqualTo(252));
    }

    [Test]
    public void TestGetCityForCountry()
    {
        ILocationHandler handler = new LocationHandler();
        var allCities = handler.CitiesForCountry("NL");
        Assert.That(allCities, Has.Count.EqualTo(1872));
    }

    [Test]
    public void TestCityContent()
    {
        ILocationHandler handler = new LocationHandler();
        var allCities = handler.CitiesForCountry("NL");
        foreach (var city in allCities)
        {
            if (!city.Name.Equals("Enschede")) continue;
            Assert.Multiple(() =>
            {
                Assert.That(city.Region, Is.EqualTo("Overijssel"));
                Assert.That(city.IndicationTz, Is.EqualTo("Europe/Amsterdam"));
            });
        }
    }
    
}