// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;
using Enigma.Domain.Points;

namespace Enigma.Test.Domain.Points;

[TestFixture]
public class TestZodiacPoints
{


    [Test]
    public void TestRetrievingDetails()
    {
        ZodiacPointDetails details = ZodiacPoints.ZeroCancer.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.ZodiacPoint, Is.EqualTo(ZodiacPoints.ZeroCancer));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.zodiacpoints.id.zerocn"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ZodiacPoints point in Enum.GetValues(typeof(ZodiacPoints)))
        {
            if (point != ZodiacPoints.None)
            {
                ZodiacPointDetails details = point.GetDetails();
                Assert.That(details, Is.Not.Null);
                Assert.That(details.TextId, Is.Not.Empty);
            }
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int zodPointIndex = 0;
        ZodiacPoints zodPoint = ZodiacPoints.None.ZodiacPointForIndex(zodPointIndex);
        Assert.That(zodPoint, Is.EqualTo(ZodiacPoints.ZeroAries));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int zodPointIndex = 300;
        Assert.That(() => _ = ZodiacPoints.None.ZodiacPointForIndex(zodPointIndex), Throws.TypeOf<ArgumentException>());
    }


}