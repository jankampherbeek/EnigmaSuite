// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestPrimDirTimeKeys
{
    [Test]
    public void TestRetrievingDetails()
    {
        const PrimDirTimeKeys timeKey = PrimDirTimeKeys.Brahe;
        PrimDirTimeKeysDetails details = timeKey.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.EqualTo("ref.primdirtimekey.brahe"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (PrimDirTimeKeys timeKey in Enum.GetValues(typeof(PrimDirTimeKeys)))
        {
            PrimDirTimeKeysDetails details = timeKey.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 4;
        PrimDirTimeKeys timeKey = PrimDirTimeKeysExtensions.PrimDirTimeKeyForIndex(index);
        Assert.That(timeKey, Is.EqualTo(PrimDirTimeKeys.VanDam));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 333;
        Assert.That(() => _ = PrimDirTimeKeysExtensions.PrimDirTimeKeyForIndex(index), Throws.TypeOf<ArgumentException>());
    }


    [Test]
    public void TestAllPrimDirTimeKeysDetails()
    {
        List<PrimDirTimeKeysDetails> allDetails = PrimDirTimeKeysExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(5));
            Assert.That(allDetails[1].RbKey, Is.EqualTo("ref.primdirtimekey.ptolemy"));
            Assert.That(allDetails[3].RbKey, Is.EqualTo("ref.primdirtimekey.placidus"));
        });
    }
    
}