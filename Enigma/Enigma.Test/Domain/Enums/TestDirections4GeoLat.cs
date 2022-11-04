// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;


[TestFixture]
public class TestTestDirections4GeoLat
{
    private IDirections4GeoLatSpecifications specifications;

    [SetUp]
    public void SetUp()
    {
        specifications = new Directions4GeoLatSpecifications();
    }


    [Test]
    public void TestRetrievingDetails()
    {
        Directions4GeoLat direction = Directions4GeoLat.North;
        Directions4GeoLatDetails details = specifications.DetailsForDirection(direction);
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Direction, Is.EqualTo(direction));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.direction4geolat.north"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (Directions4GeoLat direction in Enum.GetValues(typeof(Directions4GeoLat)))
        {
            Directions4GeoLatDetails details = specifications.DetailsForDirection(direction);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int directionIndex = -1;
        Directions4GeoLat direction = specifications.DirectionForIndex(directionIndex);
        Assert.That(direction, Is.EqualTo(Directions4GeoLat.South));
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
        List<Directions4GeoLatDetails> allDetails = specifications.AllDirectionDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].Direction, Is.EqualTo(Directions4GeoLat.North));
            Assert.That(allDetails[1].TextId, Is.EqualTo("ref.enum.direction4geolat.south"));
        });
    }
}