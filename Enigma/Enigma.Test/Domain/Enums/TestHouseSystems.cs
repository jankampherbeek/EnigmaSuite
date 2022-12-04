// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestHouseSystemSpecifications
{

    [Test]
    public void TestRetrievingDetails()
    {
        HouseSystems houseSystem = HouseSystems.Regiomontanus;
        HouseSystemDetails details = houseSystem.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.HouseSystem, Is.EqualTo(houseSystem));
            Assert.That(details.SeId, Is.EqualTo('R'));
            Assert.That(details.NrOfCusps, Is.EqualTo(12));
            Assert.That(details.CounterClockWise, Is.True);
            Assert.That(details.QuadrantSystem, Is.True);
            Assert.That(details.TextId, Is.EqualTo("ref.enum.housesystemregiomontanus"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (HouseSystems system in Enum.GetValues(typeof(HouseSystems)))
        {
            HouseSystemDetails details = system.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 5;
        HouseSystems system = HouseSystems.NoHouses.HouseSystemForIndex(index);
        Assert.That(system, Is.EqualTo(HouseSystems.Campanus));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = 1000;
        Assert.That(() => _ = HouseSystems.NoHouses.HouseSystemForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDetailsForHouseSystems()
    {
        List<HouseSystemDetails> allDetails = HouseSystems.NoHouses.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(22));
            Assert.That(allDetails[1].CounterClockWise, Is.EqualTo(true));
            Assert.That(allDetails[5].QuadrantSystem, Is.EqualTo(true));
            Assert.That(allDetails[8].SeSupported, Is.EqualTo(true));
            Assert.That(allDetails[8].NrOfCusps, Is.EqualTo(12));
            Assert.That(allDetails[19].NrOfCusps, Is.EqualTo(36));
            Assert.That(allDetails[12].SeId, Is.EqualTo('A'));
            Assert.That(allDetails[3].TextId, Is.EqualTo("ref.enum.housesystemporphyri"));
            Assert.That(allDetails[14].HouseSystem, Is.EqualTo(HouseSystems.EqualAries));
        });
    }
}
