// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestSigns
{

    [Test]
    public void TestRetrievingDetails()
    {
        SignDetails details = Signs.Cancer.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Sign, Is.EqualTo(Signs.Cancer));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.sign.cancer.text"));
            Assert.That(details.TextIdAbbreviated, Is.EqualTo("ref.enum.sign.cancer.abbr"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (Signs sign in Enum.GetValues(typeof(Signs)))
        {
            if (sign != Signs.None)
            {
                SignDetails details = sign.GetDetails();
                Assert.That(details, Is.Not.Null);
                Assert.That(details.TextId, Is.Not.Empty);
            }
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int signIndex = 2;
        Signs sign = Signs.None.SignForIndex(signIndex);
        Assert.That(sign, Is.EqualTo(Signs.Taurus));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int signIndex = 300;
        Assert.That(() => _ = Signs.None.SignForIndex(signIndex), Throws.TypeOf<ArgumentException>());
    }


}