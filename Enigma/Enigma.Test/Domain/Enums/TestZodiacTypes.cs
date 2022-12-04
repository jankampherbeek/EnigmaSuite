// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestZodiacTypeSpecifications
{
    [Test]
    public void TestRetrievingDetails_Obsolete()
    {
        ZodiacTypes zodiacType = ZodiacTypes.Sidereal;
        ZodiacTypeDetails details = zodiacType.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Type, Is.EqualTo(zodiacType));
            Assert.That(details.ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_SIDEREAL));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.zodiactype.sidereal"));
        });
    }

    [Test]
    public void TestRetrievingDetails()
    {
        ZodiacTypes zodiacType = ZodiacTypes.Sidereal;
        ZodiacTypeDetails details = zodiacType.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Type, Is.EqualTo(zodiacType));
            Assert.That(details.ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_SIDEREAL));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.zodiactype.sidereal"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ZodiacTypes zodiacType in Enum.GetValues(typeof(ZodiacTypes)))
        {
            ZodiacTypeDetails details = zodiacType.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 1;
        ZodiacTypes system = ZodiacTypes.Sidereal.ZodiacTypeForIndex(index);
        Assert.That(system, Is.EqualTo(ZodiacTypes.Tropical));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = 500;
        Assert.That(() => _ = ZodiacTypes.Tropical.ZodiacTypeForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllZodiacTypeDetails()
    {
        List<ZodiacTypeDetails> allDetails = ZodiacTypes.Tropical.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].TextId, Is.EqualTo("ref.enum.zodiactype.sidereal"));
            Assert.That(allDetails[1].Type, Is.EqualTo(ZodiacTypes.Tropical));
            Assert.That(allDetails[0].ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_SIDEREAL));
        });
    }

}