// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Test.Domain.Calc.ChartItems;


[TestFixture]
public class TestTestDirections4GeoLat
{

    [Test]
    public void TestRetrievingDetails()
    {
        Directions4GeoLat direction = Directions4GeoLat.North;
        Directions4GeoLatDetails details = direction.GetDetails();
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
            Directions4GeoLatDetails details = direction.GetDetails();
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int directionIndex = -1;
        Directions4GeoLat direction = Directions4GeoLat.North.DirectionForIndex(directionIndex);
        Assert.That(direction, Is.EqualTo(Directions4GeoLat.South));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int directionIndex = 500;
        Assert.That(() => _ = Directions4GeoLat.North.DirectionForIndex(directionIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDirectionDetails()
    {
        List<Directions4GeoLatDetails> allDetails = Directions4GeoLat.South.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].Direction, Is.EqualTo(Directions4GeoLat.North));
            Assert.That(allDetails[1].TextId, Is.EqualTo("ref.enum.direction4geolat.south"));
        });
    }
}