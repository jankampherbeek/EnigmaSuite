// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestHouseSystemSpecifications
{

    [Test]
    public void TestRetrievingDetails()
    {
        const HouseSystems houseSystem = HouseSystems.Regiomontanus;
        HouseSystemDetails details = houseSystem.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.HouseSystem, Is.EqualTo(houseSystem));
            Assert.That(details.SeId, Is.EqualTo('R'));
            Assert.That(details.NrOfCusps, Is.EqualTo(12));
            Assert.That(details.CounterClockWise, Is.True);
            Assert.That(details.QuadrantSystem, Is.True);
            Assert.That(details.Text, Is.EqualTo("Regiomontanus"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (HouseSystems system in Enum.GetValues(typeof(HouseSystems)))
        {
            HouseSystemDetails details = system.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 5;
        HouseSystems system = HouseSystemsExtensions.HouseSystemForIndex(index);
        Assert.That(system, Is.EqualTo(HouseSystems.Campanus));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 1000;
        Assert.That(() => _ = HouseSystemsExtensions.HouseSystemForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDetailsForHouseSystems()
    {
        List<HouseSystemDetails> allDetails = HouseSystemsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(25));
            Assert.That(allDetails[1].CounterClockWise, Is.EqualTo(true));
            Assert.That(allDetails[5].QuadrantSystem, Is.EqualTo(true));
            Assert.That(allDetails[8].SeSupported, Is.EqualTo(true));
            Assert.That(allDetails[8].NrOfCusps, Is.EqualTo(12));
            Assert.That(allDetails[19].NrOfCusps, Is.EqualTo(36));
            Assert.That(allDetails[12].SeId, Is.EqualTo('A'));
            Assert.That(allDetails[3].Text, Is.EqualTo("Porphyri"));
            Assert.That(allDetails[14].HouseSystem, Is.EqualTo(HouseSystems.EqualAries));
        });
    }
}
