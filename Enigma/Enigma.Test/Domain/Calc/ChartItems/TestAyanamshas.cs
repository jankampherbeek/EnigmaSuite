// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Test.Domain.Calc.ChartItems;

[TestFixture]
public class TestAyanamshaSpecifications
{


    [Test]
    public void TestRetrievingDetails()
    {
        const Ayanamshas ayanamsha = Ayanamshas.Huber;
        AyanamshaDetails details = ayanamsha.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.Ayanamsha, Is.EqualTo(Ayanamshas.Huber));
            Assert.That(details.SeId, Is.EqualTo(12));
            Assert.That(details.Text, Is.EqualTo("Huber"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (Ayanamshas ayanamsha in Enum.GetValues(typeof(Ayanamshas)))
        {
            AyanamshaDetails details = ayanamsha.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 1;
        Ayanamshas ayanamsha = AyanamshaExtensions.AyanamshaForIndex(index);
        Assert.That(ayanamsha, Is.EqualTo(Ayanamshas.Fagan));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 500;
        Assert.That(() => _ = AyanamshaExtensions.AyanamshaForIndex(index), Throws.TypeOf<ArgumentException>());
    }


    [Test]
    public void TestAllAyanamshaDetails()
    {
        List<AyanamshaDetails> allDetails = AyanamshaExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(41));
            Assert.That(allDetails[0].Text, Is.EqualTo("None (Tropical zodiac)"));
            Assert.That(allDetails[10].SeId, Is.EqualTo(9));
            Assert.That(allDetails[20].Ayanamsha, Is.EqualTo(Ayanamshas.J1900));
        });
    }
}