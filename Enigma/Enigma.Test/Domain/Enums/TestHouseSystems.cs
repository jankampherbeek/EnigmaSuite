﻿// Jan Kampherbeek, (c) 2022.
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
        IHouseSystemSpecifications specifications = new HouseSystemSpecifications();
        HouseSystemDetails details = specifications.DetailsForHouseSystem(houseSystem);
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
        IHouseSystemSpecifications specifications = new HouseSystemSpecifications();
        foreach (HouseSystems system in Enum.GetValues(typeof(HouseSystems)))
        {
            HouseSystemDetails details = specifications.DetailsForHouseSystem(system);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }
}