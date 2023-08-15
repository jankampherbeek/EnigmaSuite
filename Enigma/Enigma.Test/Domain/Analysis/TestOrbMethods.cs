// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;

namespace Enigma.Test.Domain.Analysis;

[TestFixture]
public class TestOrbMethods
{

    [Test]
    public void TestRetrievingDetails()
    {
        const OrbMethods method = OrbMethods.Weighted;
        OrbMethodDetails details = method.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.EqualTo("Weighted"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (OrbMethods method in Enum.GetValues(typeof(OrbMethods)))
        {
            OrbMethodDetails details = method.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 500;
        Assert.That(() => _ = OrbMethodsExtensions.OrbMethodForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllOrbMethodDetails()
    {
        List<OrbMethodDetails> allDetails = OrbMethodsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(1));
            Assert.That(allDetails[0].OrbMethod, Is.EqualTo(OrbMethods.Weighted));
        });
    }

}

