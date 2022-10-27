// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;

namespace Enigma.Test.Domain.CalcVars;

[TestFixture]
public class TestZodiacalPoints
{
    private IZodiacalPointSpecifications specifications;

    [SetUp]
    public void SetUp()
    {
        specifications = new ZodiacalPointSpecifications();
    }


    [Test]
    public void TestRetrievingDetails()
    {
        ZodiacalPointDetails details = specifications.DetailsForPoint(ZodiacalPoints.ZeroCancer);
        Assert.That(details, Is.Not.Null);
        Assert.That(details.ZodiacalPoint, Is.EqualTo(ZodiacalPoints.ZeroCancer));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.zodiacalpoints.id.zerocn"));
        Assert.That(details.TextIdAbbreviated, Is.EqualTo("ref.enum.zodiacalpoints.idabbr.zerocn"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ZodiacalPoints point in Enum.GetValues(typeof(ZodiacalPoints)))
        {
            ZodiacalPointDetails details = specifications.DetailsForPoint(point);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId.Length, Is.GreaterThan(0));
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int zodPointIndex = 0;
        ZodiacalPointDetails zodPointDetails = specifications.DetailsForPoint(zodPointIndex);
        Assert.That(zodPointDetails, Is.Not.Null);
        Assert.That(zodPointDetails.ZodiacalPoint, Is.EqualTo(ZodiacalPoints.ZeroAries));

    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int zodPointIndex = 300;
        Assert.That(() => _ = specifications.DetailsForPoint(zodPointIndex), Throws.TypeOf<ArgumentException>());
    }


}