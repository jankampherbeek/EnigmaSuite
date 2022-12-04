// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestAyanamshaSpecifications
{


    [Test]
    public void TestRetrievingDetails()
    {
        Ayanamshas ayanamsha = Ayanamshas.Huber;
        AyanamshaDetails details = ayanamsha.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.Ayanamsha, Is.EqualTo(Ayanamshas.Huber));
            Assert.That(details.SeId, Is.EqualTo(12));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.ayanamsha.huber"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (Ayanamshas ayanamsha in Enum.GetValues(typeof(Ayanamshas)))
        {
            AyanamshaDetails details = ayanamsha.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 1;
        Ayanamshas ayanamsha = Ayanamshas.None.AyanamshaForIndex(index);
        Assert.That(ayanamsha, Is.EqualTo(Ayanamshas.Fagan));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = 500;
        Assert.That(() => _ = Ayanamshas.None.AyanamshaForIndex(index), Throws.TypeOf<ArgumentException>());
    }


    [Test]
    public void TestAllAyanamshaDetails()
    {
        List<AyanamshaDetails> allDetails = Ayanamshas.None.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(41));
            Assert.That(allDetails[0].TextId, Is.EqualTo("ref.enum.ayanamsha.none"));
            Assert.That(allDetails[10].SeId, Is.EqualTo(9));
            Assert.That(allDetails[20].Ayanamsha, Is.EqualTo(Ayanamshas.J1900));
        });
    }
}