// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestZodiacTypeSpecifications
{
 

    [Test]
    public void TestRetrievingDetails()
    {
        const ZodiacTypes zodiacType = ZodiacTypes.Sidereal;
        ZodiacTypeDetails details = zodiacType.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Type, Is.EqualTo(zodiacType));
            Assert.That(details.ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_SIDEREAL));
            Assert.That(details.Text, Is.EqualTo("Sidereal"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ZodiacTypes zodiacType in Enum.GetValues(typeof(ZodiacTypes)))
        {
            ZodiacTypeDetails details = zodiacType.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 1;
        ZodiacTypes system = ZodiacTypeExtensions.ZodiacTypeForIndex(index);
        Assert.That(system, Is.EqualTo(ZodiacTypes.Tropical));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 500;
        Assert.That(() => _ = ZodiacTypeExtensions.ZodiacTypeForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllZodiacTypeDetails()
    {
        List<ZodiacTypeDetails> allDetails = ZodiacTypeExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].Text, Is.EqualTo("Sidereal"));
            Assert.That(allDetails[1].Type, Is.EqualTo(ZodiacTypes.Tropical));
            Assert.That(allDetails[0].ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_SIDEREAL));
        });
    }

}