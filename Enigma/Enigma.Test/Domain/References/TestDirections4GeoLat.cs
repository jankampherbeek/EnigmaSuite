// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;


[TestFixture]
public class TestTestDirections4GeoLat
{

    [Test]
    public void TestRetrievingDetails()
    {
        const Directions4GeoLat direction = Directions4GeoLat.North;
        Directions4GeoLatDetails details = direction.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Direction, Is.EqualTo(direction));
            Assert.That(details.Text, Is.EqualTo("North"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (Directions4GeoLat direction in Enum.GetValues(typeof(Directions4GeoLat)))
        {
            Directions4GeoLatDetails details = direction.GetDetails();
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int directionIndex = -1;
        Directions4GeoLat direction = Directions4GeoLatExtensions.DirectionForIndex(directionIndex);
        Assert.That(direction, Is.EqualTo(Directions4GeoLat.South));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int directionIndex = 500;
        Assert.That(() => _ = Directions4GeoLatExtensions.DirectionForIndex(directionIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDirectionDetails()
    {
        List<Directions4GeoLatDetails> allDetails = Directions4GeoLatExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].Direction, Is.EqualTo(Directions4GeoLat.North));
            Assert.That(allDetails[1].Text, Is.EqualTo("South"));
        });
    }
}