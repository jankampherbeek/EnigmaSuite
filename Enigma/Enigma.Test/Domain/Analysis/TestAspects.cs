// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;

namespace Enigma.Test.Domain.Analysis;

[TestFixture]
public class TestAspectSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        Aspects aspect = Aspects.BiQuintile;
        IAspectSpecifications specifications = new AspectSpecifications();

        AspectDetails details = specifications.DetailsForAspect(aspect);
        Assert.That(details, Is.Not.Null);
        Assert.That(details.Aspect, Is.EqualTo(Aspects.BiQuintile));
        Assert.That(details.Angle, Is.EqualTo(144.0));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IAspectSpecifications specifications = new AspectSpecifications();
        foreach (Aspects system in Enum.GetValues(typeof(Aspects)))
        {
            AspectDetails details = specifications.DetailsForAspect(system);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }
}