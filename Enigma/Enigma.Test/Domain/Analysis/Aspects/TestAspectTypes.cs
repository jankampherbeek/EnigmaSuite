// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis.Aspects;

namespace Enigma.Test.Domain.Analysis.Aspects;

[TestFixture]
public class TestAspectTypes
{
    private const double Delta = 0.00000001;

    [Test]
    public void TestRetrievingDetails()
    {
        const AspectTypes aspect = AspectTypes.BiQuintile;
        AspectDetails details = aspect.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Aspect, Is.EqualTo(AspectTypes.BiQuintile));
            Assert.That(details.Angle, Is.EqualTo(144.0).Within(Delta));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (AspectTypes aspectType in Enum.GetValues(typeof(AspectTypes)))
        {
            AspectDetails details = aspectType.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 1;
        AspectTypes aspectType = AspectTypes.Square.AspectTypeForIndex(index);
        Assert.That(aspectType, Is.EqualTo(AspectTypes.Opposition));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 300;
        Assert.That(() => _ = AspectTypes.Centile.AspectTypeForIndex(index), Throws.TypeOf<ArgumentException>());
    }
}