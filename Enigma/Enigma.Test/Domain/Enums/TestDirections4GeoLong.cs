// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;


[TestFixture]
public class TestTestDirections4GeoLong
{
    private IDirections4GeoLongSpecifications specifications;

    [SetUp]
    public void SetUp()
    {
        specifications = new Directions4GeoLongSpecifications();
    }


    [Test]
    public void TestRetrievingDetails()
    {
        Directions4GeoLong direction = Directions4GeoLong.East;
        Directions4GeoLongDetails details = specifications.DetailsForDirection(direction);
        Assert.IsNotNull(details);
        Assert.That(details.Direction, Is.EqualTo(direction));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.direction4geolong.east"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (Directions4GeoLong direction in Enum.GetValues(typeof(Directions4GeoLong)))
        {
            Directions4GeoLongDetails details = specifications.DetailsForDirection(direction);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int directionIndex = -1;
        Directions4GeoLong direction = specifications.DirectionForIndex(directionIndex);
        Assert.That(direction, Is.EqualTo(Directions4GeoLong.West));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int directionIndex = 500;
        Assert.That(() => _ = specifications.DirectionForIndex(directionIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDirectionDetails()
    {
        List<Directions4GeoLongDetails> allDetails = specifications.AllDirectionDetails();
        Assert.That(allDetails.Count, Is.EqualTo(2));
        Assert.That(allDetails[0].Direction, Is.EqualTo(Directions4GeoLong.East));
        Assert.That(allDetails[1].TextId, Is.EqualTo("ref.enum.direction4geolong.west"));
    }

}