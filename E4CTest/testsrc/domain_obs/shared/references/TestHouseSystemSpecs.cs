// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Shared.References;
using NUnit.Framework;
using System;

namespace E4CTest.domain.shared.references;

[TestFixture]
public class TestHouseSystemSpecs
{
    [Test]
    public void TestRetrievingDetails()
    {
        HouseSystems houseSystem = HouseSystems.Regiomontanus;
        IHouseSystemSpecs specifications = new HouseSystemSpecs();
        HouseSystemDetails details = specifications.DetailsForHouseSystem(houseSystem);
        Assert.IsNotNull(details);
        Assert.AreEqual(houseSystem, details.HouseSystem);
        Assert.AreEqual('R', details.SeId);
        Assert.AreEqual(12, details.NrOfCusps);
        Assert.IsTrue(details.CounterClockWise);
        Assert.IsTrue(details.QuadrantSystem);
        Assert.AreEqual("houseSystemRegiomontanus", details.TextId);
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IHouseSystemSpecs specifications = new HouseSystemSpecs();
        foreach (HouseSystems system in Enum.GetValues(typeof(HouseSystems)))
        {
            HouseSystemDetails details = specifications.DetailsForHouseSystem(system);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }
}