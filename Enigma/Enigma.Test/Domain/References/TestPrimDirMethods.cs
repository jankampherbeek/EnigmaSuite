// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestPrimDirMethods
{
    [Test]
    public void TestRetrievingDetails()
    {
        const PrimDirMethods method = PrimDirMethods.Campanus;
        PrimDirMethodDetails details = method.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.EqualTo("ref.primdirmethod.campanus"));
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
        Assert.That(method, Is.EqualTo(PrimDirMethods.PlacidusPole));
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
            Assert.That(allDetails, Has.Count.EqualTo(5));
            Assert.That(allDetails[0].RbKey, Is.EqualTo("ref.primdirmethod.placidus"));
            Assert.That(allDetails[2].RbKey, Is.EqualTo("ref.primdirmethod.regiomontanus"));
        });
    }
    
}