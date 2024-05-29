// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestPrimDirConverseOptions
{
    [Test]
    public void TestRetrievingDetails()
    {
        const PrimDirConverseOptions option = PrimDirConverseOptions.ConverseOriginal;
        PrimDirConverseOptionDetails details = option.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.EqualTo("ref.primdirconverseoption.original"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (PrimDirConverseOptions option in Enum.GetValues(typeof(PrimDirConverseOptions)))
        {
            PrimDirConverseOptionDetails details = option.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 2;
        PrimDirConverseOptions option = PrimDirConverseOptionsExtensions.PrimDirConverseOptionForIndex(index);
        Assert.That(option, Is.EqualTo(PrimDirConverseOptions.ConverseModern));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 500;
        Assert.That(() => _ = PrimDirConverseOptionsExtensions.PrimDirConverseOptionForIndex(index), Throws.TypeOf<ArgumentException>());
    }


    [Test]
    public void TestAllPrimDirConverseOptionDetails()
    {
        List<PrimDirConverseOptionDetails> allDetails = PrimDirConverseOptionsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(3));
            Assert.That(allDetails[0].RbKey, Is.EqualTo("ref.primdirconverseoption.none"));
            Assert.That(allDetails[2].RbKey, Is.EqualTo("ref.primdirconverseoption.modern"));
        });
    }
    
}