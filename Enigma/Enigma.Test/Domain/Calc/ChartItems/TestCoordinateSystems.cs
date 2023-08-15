// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Constants;

namespace Enigma.Test.Domain.Calc.ChartItems;

[TestFixture]
public class TestCoordinateSystems
{
    [Test]
    public void TestRetrievingDetails()
    {
        const CoordinateSystems system = CoordinateSystems.Equatorial;
        CoordinateSystemDetails details = system.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.CoordSystem, Is.EqualTo(system));
            Assert.That(details.ValueForFlag, Is.EqualTo(EnigmaConstants.SeflgEquatorial));
            Assert.That(details.TextId, Is.EqualTo("coordinateSysEquatorial"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (CoordinateSystems system in Enum.GetValues(typeof(CoordinateSystems)))
        {
            CoordinateSystemDetails details = system.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 1;
        CoordinateSystems system = CoordinateSystemsExtensions.CoordinateSystemForIndex(index);
        Assert.That(system, Is.EqualTo(CoordinateSystems.Equatorial));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 500;
        Assert.That(() => _ = CoordinateSystemsExtensions.CoordinateSystemForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllCoordinateSystemDetails()
    {
        List<CoordinateSystemDetails> allDetails = CoordinateSystemsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(3));
            Assert.That(allDetails[0].TextId, Is.EqualTo("coordinateSysEcliptic"));
            Assert.That(allDetails[1].CoordSystem, Is.EqualTo(CoordinateSystems.Equatorial));
            Assert.That(allDetails[2].ValueForFlag, Is.EqualTo(0));
        });
    }

}