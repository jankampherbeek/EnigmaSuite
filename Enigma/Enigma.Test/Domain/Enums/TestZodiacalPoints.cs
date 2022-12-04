// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestZodiacalPoints
{

 
    [Test]
    public void TestRetrievingDetails()
    {
        ZodiacalPointDetails details = ZodiacalPoints.ZeroCancer.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.ZodiacalPoint, Is.EqualTo(ZodiacalPoints.ZeroCancer));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.zodiacalpoints.id.zerocn"));
            Assert.That(details.TextIdAbbreviated, Is.EqualTo("ref.enum.zodiacalpoints.idabbr.zerocn"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ZodiacalPoints point in Enum.GetValues(typeof(ZodiacalPoints)))
        {
            ZodiacalPointDetails details = point.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int zodPointIndex = 0;
        ZodiacalPoints zodPoint = ZodiacalPoints.ZeroAries.ZodiacalPointForIndex(zodPointIndex);
        Assert.That(zodPoint, Is.EqualTo(ZodiacalPoints.ZeroAries));

    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int zodPointIndex = 300;
        Assert.That(() => _ = ZodiacalPoints.ZeroCancer.ZodiacalPointForIndex(zodPointIndex), Throws.TypeOf<ArgumentException>());
    }


}