// Jan Kampherbeek, (c) 2022.
// Enigma Research is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;

namespace Enigma.Test.Domain.CalcVars;

[TestFixture]
public class TestHouseSystemSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        HouseSystems houseSystem = HouseSystems.Regiomontanus;
        IHouseSystemSpecs specifications = new HouseSystemSpecs();
        HouseSystemDetails details = specifications.DetailsForHouseSystem(houseSystem);
        Assert.IsNotNull(details);
        Assert.That(details.HouseSystem, Is.EqualTo(houseSystem));
        Assert.That(details.SeId, Is.EqualTo('R'));
        Assert.That(details.NrOfCusps, Is.EqualTo(12));
        Assert.That(details.CounterClockWise, Is.True);
        Assert.That(details.QuadrantSystem, Is.True);
        Assert.That(details.TextId, Is.EqualTo("ref.enum.housesystemregiomontanus"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IHouseSystemSpecs specifications = new HouseSystemSpecs();
        foreach (HouseSystems system in Enum.GetValues(typeof(HouseSystems)))
        {
            HouseSystemDetails details = specifications.DetailsForHouseSystem(system);
            Assert.IsNotNull(details);
            Assert.That(details.TextId.Length > 0, Is.True);
        }
    }
}
