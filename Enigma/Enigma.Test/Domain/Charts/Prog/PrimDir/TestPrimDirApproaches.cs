// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts.Prog.PrimDir;

namespace Enigma.Test.Domain.Charts.Prog.PrimDir;

[TestFixture]
public class TestPrimDirApproaches
{
    [Test]
    public void TestRetrievingDetails()
    {
        const PrimDirApproaches option = PrimDirApproaches.Mundane;
        PrimDirApproachesDetails details = option.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.EqualTo("ref.primdirapproach.inmundo"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (PrimDirApproaches option in Enum.GetValues(typeof(PrimDirApproaches)))
        {
            PrimDirApproachesDetails details = option.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 1;
        PrimDirApproaches option = PrimDirApproachesExtensions.PrimDirApproachForIndex(index);
        Assert.That(option, Is.EqualTo(PrimDirApproaches.Zodiacal));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 333;
        Assert.That(() => _ = PrimDirApproachesExtensions.PrimDirApproachForIndex(index), Throws.TypeOf<ArgumentException>());
    }


    [Test]
    public void TestAllPrimDirApproachesDetails()
    {
        List<PrimDirApproachesDetails> allDetails = PrimDirApproachesExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].RbKey, Is.EqualTo("ref.primdirapproach.inmundo"));
            Assert.That(allDetails[1].RbKey, Is.EqualTo("ref.primdirapproach.inzodiaco"));
        });
    }
    
}