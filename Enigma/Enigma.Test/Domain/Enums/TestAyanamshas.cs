// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestAyanamshaSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        Ayanamshas ayanamsha = Ayanamshas.Huber;
        IAyanamshaSpecifications specifications = new AyanamshaSpecifications();
        AyanamshaDetails details = specifications.DetailsForAyanamsha(ayanamsha);
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
        IAyanamshaSpecifications specifications = new AyanamshaSpecifications();
        foreach (Ayanamshas system in Enum.GetValues(typeof(Ayanamshas)))
        {
            AyanamshaDetails details = specifications.DetailsForAyanamsha(system);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }
}