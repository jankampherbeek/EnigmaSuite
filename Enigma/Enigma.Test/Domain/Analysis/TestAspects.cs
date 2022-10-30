// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Analysis;

[TestFixture]
public class TestAspectSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        AspectTypes aspect = AspectTypes.BiQuintile;
        IAspectSpecifications specifications = new AspectSpecifications();

        AspectDetails details = specifications.DetailsForAspect(aspect);
        Assert.That(details, Is.Not.Null);
        Assert.That(details.Aspect, Is.EqualTo(AspectTypes.BiQuintile));
        Assert.That(details.Angle, Is.EqualTo(144.0));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IAspectSpecifications specifications = new AspectSpecifications();
        foreach (AspectTypes system in Enum.GetValues(typeof(AspectTypes)))
        {
            AspectDetails details = specifications.DetailsForAspect(system);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }
}