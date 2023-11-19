// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestObserverPositionSpecifications
{

    [Test]
    public void TestRetrievingDetails()
    {
        const ObserverPositions position = ObserverPositions.TopoCentric;
        ObserverPositionDetails details = position.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Position, Is.EqualTo(position));
            Assert.That(details.ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_TOPOCTR));
            Assert.That(details.Text, Is.EqualTo("Topocentric (with parallax)"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ObserverPositions position in Enum.GetValues(typeof(ObserverPositions)))
        {
             ObserverPositionDetails details = position.GetDetails();
             Assert.That(details, Is.Not.Null);
             Assert.That(details.Text, Is.Not.Empty);
        }
    }

    
    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 0;
        ObserverPositions system = ObserverPositionsExtensions.ObserverPositionForIndex(index);
        Assert.That(system, Is.EqualTo(ObserverPositions.GeoCentric));
    }
    
    
    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 500;
        Assert.That(() => _ = ObserverPositionsExtensions.ObserverPositionForIndex(index), 
            Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllObserverPositionDetails()
    {
        IEnumerable<ObserverPositionDetails> allDetails = ObserverPositionsExtensions.AllDetails();
        List<ObserverPositionDetails> detailList = new List<ObserverPositionDetails>(allDetails);
        Assert.Multiple(() =>
        {
            Assert.That(detailList, Has.Count.EqualTo(3));
            Assert.That(detailList[2].Text, Is.EqualTo("Heliocentric"));
            Assert.That(detailList[0].Position, Is.EqualTo(ObserverPositions.GeoCentric));
            Assert.That(detailList[1].ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_TOPOCTR));
        });
    }

}