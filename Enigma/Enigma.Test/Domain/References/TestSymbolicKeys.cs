// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestSymbolicKeys
{
    
    [Test]
    public void TestRetrievingDetails()
    {
        const SymbolicKeys key = SymbolicKeys.OneDegree;
        SymbolicKeyDetails details = key.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.SymbolicKey, Is.EqualTo(SymbolicKeys.OneDegree));
            Assert.That(details.Text, Is.EqualTo("One degree"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (SymbolicKeys keys in Enum.GetValues(typeof(SymbolicKeys)))
        { 
            SymbolicKeyDetails details = keys.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 2;
        SymbolicKeys key = SymbolicKeyExtensions.SymbolicKeysForIndex(index);
        Assert.That(key, Is.EqualTo(SymbolicKeys.TrueSun));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 999;
        Assert.That(() => _ = SymbolicKeyExtensions.SymbolicKeysForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllSymbolicKeyDetails()
    {
        List<SymbolicKeyDetails> allDetails = SymbolicKeyExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(3));
            Assert.That(allDetails[1].Text, Is.EqualTo("True Sun"));
            Assert.That(allDetails[2].SymbolicKey, Is.EqualTo(SymbolicKeys.MeanSun));
        });
    }

    
    
}