// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts.Prog.PrimDir;

namespace Enigma.Test.Domain.Charts.Prog.PrimDir;

[TestFixture]
public class TestPrimDirMethods
{
    [Test]
    public void TestRetrievingDetails()
    {
        const PrimDirMethods method = PrimDirMethods.Topocentric;
        PrimDirMethodDetails details = method.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.EqualTo("ref.primdirmethod.topocentric"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (PrimDirMethods method in Enum.GetValues(typeof(PrimDirMethods)))
        {
            PrimDirMethodDetails details = method.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 1;
        PrimDirMethods method = PrimDirMethodsExtensions.PrimDirMethodForIndex(index);
        Assert.That(method, Is.EqualTo(PrimDirMethods.Regiomontanus));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 500;
        Assert.That(() => _ = PrimDirMethodsExtensions.PrimDirMethodForIndex(index), Throws.TypeOf<ArgumentException>());
    }


    [Test]
    public void TestAllPrimDirMethodDetails()
    {
        List<PrimDirMethodDetails> allDetails = PrimDirMethodsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(3));
            Assert.That(allDetails[0].RbKey, Is.EqualTo("ref.primdirmethod.placidus"));
            Assert.That(allDetails[1].RbKey, Is.EqualTo("ref.primdirmethod.regiomontanus"));
        });
    }
    
}