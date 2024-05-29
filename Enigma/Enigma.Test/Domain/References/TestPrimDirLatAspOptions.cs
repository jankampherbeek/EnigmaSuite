// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestPrimDirLatAspOptions
{
    [Test]
    public void TestRetrievingDetails()
    {
        const PrimDirLatAspOptions option = PrimDirLatAspOptions.Promissor;
        PrimDirLatAspOptionDetails details = option.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.EqualTo("ref.primdirlataspoption.promissor"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (PrimDirLatAspOptions option in Enum.GetValues(typeof(PrimDirLatAspOptions)))
        {
            PrimDirLatAspOptionDetails details = option.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 2;
        PrimDirLatAspOptions option = PrimDirLatAspOptionsExtensions.PrimDirLatAspOptionForIndex(index);
        Assert.That(option, Is.EqualTo(PrimDirLatAspOptions.Promissor));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 999;
        Assert.That(() => _ = PrimDirLatAspOptionsExtensions.PrimDirLatAspOptionForIndex(index), Throws.TypeOf<ArgumentException>());
    }


    [Test]
    public void TestAllPrimDirLatAspOptionDetails()
    {
        List<PrimDirLatAspOptionDetails> allDetails = PrimDirLatAspOptionsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(4));
            Assert.That(allDetails[0].RbKey, Is.EqualTo("ref.primdirlataspoption.none"));
            Assert.That(allDetails[3].RbKey, Is.EqualTo("ref.primdirlataspoption.bianchini"));
        });
    }
    
}