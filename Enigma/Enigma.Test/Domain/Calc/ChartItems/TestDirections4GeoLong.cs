﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Test.Domain.Calc.ChartItems;


[TestFixture]
public class TestTestDirections4GeoLong
{

    [Test]
    public void TestRetrievingDetails()
    {
        Directions4GeoLong direction = Directions4GeoLong.East;
        Directions4GeoLongDetails details = direction.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Direction, Is.EqualTo(direction));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.direction4geolong.east"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (Directions4GeoLong direction in Enum.GetValues(typeof(Directions4GeoLong)))
        {
            Directions4GeoLongDetails details = direction.GetDetails();
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int directionIndex = -1;
        Directions4GeoLong direction = Directions4GeoLong.West.DirectionForIndex(directionIndex);
        Assert.That(direction, Is.EqualTo(Directions4GeoLong.West));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int directionIndex = 500;
        Assert.That(() => _ = Directions4GeoLong.East.DirectionForIndex(directionIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDirectionDetails()
    {
        List<Directions4GeoLongDetails> allDetails = Directions4GeoLong.East.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].Direction, Is.EqualTo(Directions4GeoLong.East));
            Assert.That(allDetails[1].TextId, Is.EqualTo("ref.enum.direction4geolong.west"));
        });
    }
}