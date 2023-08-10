// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Test.Domain.Calc.ChartItems;

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
            Assert.That(details.Text, Is.EqualTo("Cancer"));
            Assert.That(details.TextAbbreviated, Is.EqualTo("CAN"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (Signs sign in Enum.GetValues(typeof(Signs)))
        {
            SignDetails details = sign.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int signIndex = 2;
        Signs sign = Signs.Aries.SignForIndex(signIndex);
        Assert.That(sign, Is.EqualTo(Signs.Taurus));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int signIndex = 300;
        Assert.That(() => _ = Signs.Aries.SignForIndex(signIndex), Throws.TypeOf<ArgumentException>());
    }


}