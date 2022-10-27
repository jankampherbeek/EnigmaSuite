// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;

namespace Enigma.Test.Domain.CalcVars;

[TestFixture]
public class TestAyanamshaSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        Ayanamshas ayanamsha = Ayanamshas.Huber;
        IAyanamshaSpecifications specifications = new AyanamshaSpecifications();
        AyanamshaDetails details = specifications.DetailsForAyanamsha(ayanamsha);
        Assert.IsNotNull(details);
        Assert.That(details.Ayanamsha, Is.EqualTo(Ayanamshas.Huber));
        Assert.That(details.SeId, Is.EqualTo(12));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.ayanamsha.huber"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IAyanamshaSpecifications specifications = new AyanamshaSpecifications();
        foreach (Ayanamshas system in Enum.GetValues(typeof(Ayanamshas)))
        {
            AyanamshaDetails details = specifications.DetailsForAyanamsha(system);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }
}