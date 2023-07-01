// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.Progressive;

namespace Enigma.Test.Domain.Calc.Progressive;

[ TestFixture]
public class TestPrimaryKeySpecifications
{

    [Test]
    public void TestRetrievingDetails()
    {
        PrimaryKeys key = PrimaryKeys.NaibodRa;
        PrimaryKeyDetails details = key.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.PrimaryKeys, Is.EqualTo(PrimaryKeys.NaibodRa));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.primarykey.naibodra"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (PrimaryKeys keys in Enum.GetValues(typeof(PrimaryKeys)))
        {
            if (keys != PrimaryKeys.None)
            {
                PrimaryKeyDetails details = keys.GetDetails();
                Assert.That(details, Is.Not.Null);
                Assert.That(details.TextId, Is.Not.Empty);
            }
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 2;
        PrimaryKeys key = PrimaryKeys.None.PrimaryKeysForIndex(index);
        Assert.That(key, Is.EqualTo(PrimaryKeys.NaibodRa));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = 500;
        Assert.That(() => _ = PrimaryKeys.None.PrimaryKeysForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllPrimaryKeyDetails()
    {
        List<PrimaryKeyDetails> allDetails = PrimaryKeys.None.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(8));
            Assert.That(allDetails[2].TextId, Is.EqualTo("ref.enum.primarykey.brahera"));
            Assert.That(allDetails[5].PrimaryKeys, Is.EqualTo(PrimaryKeys.NaibodLongitude));
        });
    }


}
