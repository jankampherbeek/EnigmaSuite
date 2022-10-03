// Jan Kampherbeek, (c) 2022.
// Enigma Research is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;
using Enigma.Domain.Constants;

namespace Enigma.Test.Domain.CalcVars;

[TestFixture]
public class TestObserverPositionSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        ObserverPositions position = ObserverPositions.TopoCentric;
        IObserverPositionSpecifications specifications = new ObserverPositionSpecifications();
        ObserverPositionDetails details = specifications.DetailsForObserverPosition(position);
        Assert.IsNotNull(details);
        Assert.That(details.ObserverPosition, Is.EqualTo(position));
        Assert.That(details.ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_TOPOCTR));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.observerposition.topocentric"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IObserverPositionSpecifications specifications = new ObserverPositionSpecifications();
        foreach (ObserverPositions position in Enum.GetValues(typeof(ObserverPositions)))
        {
            ObserverPositionDetails details = specifications.DetailsForObserverPosition(position);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }
}