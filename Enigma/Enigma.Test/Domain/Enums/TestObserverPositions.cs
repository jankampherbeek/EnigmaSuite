// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestObserverPositionSpecifications
{
    [Test]
    public void TestRetrievingDetails_Obsolete()
    {
        ObserverPositions position = ObserverPositions.TopoCentric;
        IObserverPositionSpecifications specifications = new ObserverPositionSpecifications();
        ObserverPositionDetails details = specifications.DetailsForObserverPosition(position);
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.ObserverPosition, Is.EqualTo(position));
            Assert.That(details.ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_TOPOCTR));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.observerposition.topocentric"));
        });
    }

    [Test]
    public void TestRetrievingDetails()
    {
        ObserverPositions position = ObserverPositions.TopoCentric;
        ObserverPositionDetails details = position.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.ObserverPosition, Is.EqualTo(position));
            Assert.That(details.ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_TOPOCTR));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.observerposition.topocentric"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IObserverPositionSpecifications specifications = new ObserverPositionSpecifications();
        foreach (ObserverPositions position in Enum.GetValues(typeof(ObserverPositions)))
        {
            ObserverPositionDetails details = specifications.DetailsForObserverPosition(position);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }
}