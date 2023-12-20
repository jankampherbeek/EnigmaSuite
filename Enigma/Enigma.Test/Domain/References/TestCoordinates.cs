// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestCoordinates
{
    [Test]
    public void TestRetrievingDetails()
    {
        const Coordinates coordinate = Coordinates.RightAscension;
        CoordinateDetails details = coordinate.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Coordinate, Is.EqualTo(coordinate));
            Assert.That(details.CoordinateSystem, Is.EqualTo(CoordinateSystems.Equatorial));
            Assert.That(details.Text, Is.EqualTo("Right Ascension"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (Coordinates coordinate in Enum.GetValues(typeof(Coordinates)))
        {
            CoordinateDetails details = coordinate.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 1;
        Coordinates coordinate = CoordinatesExtensions.CoordinateForIndex(index);
        Assert.That(coordinate, Is.EqualTo(Coordinates.Latitude));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 500;
        Assert.That(() => _ = CoordinatesExtensions.CoordinateForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllCoordinateDetails()
    {
        List<CoordinateDetails> allDetails = CoordinatesExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(6));
            Assert.That(allDetails[0].Text, Is.EqualTo("Longitude"));
            Assert.That(allDetails[3].CoordinateSystem, Is.EqualTo(CoordinateSystems.Equatorial));
        });
    }

}