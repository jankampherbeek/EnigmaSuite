// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Enums;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestObserverPositionSpecifications
{

    [Test]
    public void TestRetrievingDetails()
    {
        ObserverPositions position = ObserverPositions.TopoCentric;
        ObserverPositionDetails details = position.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Position, Is.EqualTo(position));
            Assert.That(details.ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_TOPOCTR));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.observerposition.topocentric"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ObserverPositions position in Enum.GetValues(typeof(ObserverPositions)))
        {
            ObserverPositionDetails details = position.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 1;
        ObserverPositions system = ObserverPositions.HelioCentric.ObserverPositionForIndex(index);
        Assert.That(system, Is.EqualTo(ObserverPositions.GeoCentric));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = 500;
        Assert.That(() => _ = ObserverPositions.HelioCentric.ObserverPositionForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllObserverPositionDetails()
    {
        List<ObserverPositionDetails> allDetails = ObserverPositions.HelioCentric.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(3));
            Assert.That(allDetails[0].TextId, Is.EqualTo("ref.enum.observerposition.heliocentric"));
            Assert.That(allDetails[1].Position, Is.EqualTo(ObserverPositions.GeoCentric));
            Assert.That(allDetails[2].ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_TOPOCTR));
        });
    }

}