// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;
using Enigma.Domain.Constants;

namespace Enigma.Test.Domain.CalcVars;

[TestFixture]
public class TestCoordinateSystemSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        CoordinateSystems system = CoordinateSystems.Equatorial;
        ICoordinateSystemSpecifications specifications = new CoordinateSystemSpecifications();
        CoordinateSystemDetails details = specifications.DetailsForCoordinateSystem(system);
        Assert.IsNotNull(details);
        Assert.That(details.CoordinateSystem, Is.EqualTo(system));
        Assert.AreEqual(EnigmaConstants.SEFLG_EQUATORIAL, details.ValueForFlag);
        Assert.That(details.TextId, Is.EqualTo("coordinateSysEquatorial"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        ICoordinateSystemSpecifications specifications = new CoordinateSystemSpecifications();
        foreach (CoordinateSystems system in Enum.GetValues(typeof(CoordinateSystems)))
        {
            CoordinateSystemDetails details = specifications.DetailsForCoordinateSystem(system);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }
}