// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestCoordinateSystemSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        CoordinateSystems system = CoordinateSystems.Equatorial;
        ICoordinateSystemSpecifications specifications = new CoordinateSystemSpecifications();
        CoordinateSystemDetails details = specifications.DetailsForCoordinateSystem(system);
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.CoordinateSystem, Is.EqualTo(system));
            Assert.That(details.ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_EQUATORIAL));
            Assert.That(details.TextId, Is.EqualTo("coordinateSysEquatorial"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        ICoordinateSystemSpecifications specifications = new CoordinateSystemSpecifications();
        foreach (CoordinateSystems system in Enum.GetValues(typeof(CoordinateSystems)))
        {
            CoordinateSystemDetails details = specifications.DetailsForCoordinateSystem(system);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }
}