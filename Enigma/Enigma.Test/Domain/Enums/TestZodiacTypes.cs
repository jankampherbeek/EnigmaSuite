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
    public void TestRetrievingDetails()
    {
        ZodiacTypes zodiacType = ZodiacTypes.Sidereal;
        IZodiacTypeSpecifications specifications = new ZodiacTypeSpecifications();
        ZodiacTypeDetails details = specifications.DetailsForZodiacType(zodiacType);
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.ZodiacType, Is.EqualTo(zodiacType));
            Assert.That(details.ValueForFlag, Is.EqualTo(EnigmaConstants.SEFLG_SIDEREAL));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.zodiactype.sidereal"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IZodiacTypeSpecifications specifications = new ZodiacTypeSpecifications();
        foreach (ZodiacTypes zodiacType in Enum.GetValues(typeof(ZodiacTypes)))
        {
            ZodiacTypeDetails details = specifications.DetailsForZodiacType(zodiacType);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }
}