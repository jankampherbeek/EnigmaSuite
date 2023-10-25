// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestPrimaryKeySpecifications
{

    [Test]
    public void TestRetrievingDetails()
    {
        const PrimaryKeys key = PrimaryKeys.Naibod;
        PrimaryKeyDetails details = key.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.PrimaryKey, Is.EqualTo(PrimaryKeys.Naibod));
            Assert.That(details.Text, Is.EqualTo("Naibod"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (PrimaryKeys keys in Enum.GetValues(typeof(PrimaryKeys)))
        {
            PrimaryKeyDetails details = keys.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 2;
        PrimaryKeys key = PrimaryKeyExtensions.PrimaryKeysForIndex(index);
        Assert.That(key, Is.EqualTo(PrimaryKeys.Naibod));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 500;
        Assert.That(() => _ = PrimaryKeyExtensions.PrimaryKeysForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllPrimaryKeyDetails()
    {
        List<PrimaryKeyDetails> allDetails = PrimaryKeyExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(5));
            Assert.That(allDetails[2].Text, Is.EqualTo("Brahe"));
            Assert.That(allDetails[4].PrimaryKey, Is.EqualTo(PrimaryKeys.VanDam));
        });
    }


}
