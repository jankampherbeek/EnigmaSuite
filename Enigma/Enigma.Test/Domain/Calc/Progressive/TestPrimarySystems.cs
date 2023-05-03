// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.Progressive;

namespace Enigma.Test.Domain.Calc.Progressive;


[TestFixture]
public class TestPrimarySystemSpecifications
{

    [Test]
    public void TestRetrievingDetails()
    {
        PrimarySystems prSys = PrimarySystems.PlacidusUnderPoleMun;
        PrimarySystemDetails details = prSys.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.PrimarySystem, Is.EqualTo(PrimarySystems.PlacidusUnderPoleMun));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.primarysystem.placidusunderpolemun"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (PrimarySystems prSys in Enum.GetValues(typeof(PrimarySystems)))
        {
            if (prSys != PrimarySystems.None)
            {
                PrimarySystemDetails details = prSys.GetDetails();
                Assert.That(details, Is.Not.Null);
                Assert.That(details.TextId, Is.Not.Empty);
            }
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 3;
        PrimarySystems prSys = PrimarySystems.None.PrimarySystemsForIndex(index);
        Assert.That(prSys, Is.EqualTo(PrimarySystems.RegiomontanusMun));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = 500;
        Assert.That(() => _ = PrimarySystems.None.PrimarySystemsForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllPrimarySystemDetails()
    {
        List<PrimarySystemDetails> allDetails = PrimarySystems.None.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(8));
            Assert.That(allDetails[7].TextId, Is.EqualTo("ref.enum.primarysystem.campanuszod"));
            Assert.That(allDetails[5].PrimarySystem, Is.EqualTo(PrimarySystems.PlacidusUnderPoleZod));
        });
    }


}

