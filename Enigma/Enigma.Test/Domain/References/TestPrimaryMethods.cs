// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;


[TestFixture]
public class TestPrimarySystemSpecifications
{

    [Test]
    public void TestRetrievingDetails()
    {
        const PrimaryDirMethods prMethod = PrimaryDirMethods.PlacidusEcliptical;
        PrimaryDirMethodDetails details = prMethod.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.DirMethod, Is.EqualTo(PrimaryDirMethods.PlacidusEcliptical));
            Assert.That(details.MethodName, Is.EqualTo("Placidus - ecliptical"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (PrimaryDirMethods prMethod in Enum.GetValues(typeof(PrimaryDirMethods)))
        {
            PrimaryDirMethodDetails details = prMethod.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.MethodName, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 1;
        PrimaryDirMethods prDirMethod = PrimaryDirMethodsExtensions.MethodForIndex(index);
        Assert.That(prDirMethod, Is.EqualTo(PrimaryDirMethods.PlacidusEcliptical));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 500;
        Assert.That(() => _ = PrimaryDirMethodsExtensions.MethodForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllPrimarySystemDetails()
    {
        List<PrimaryDirMethodDetails> allDetails = PrimaryDirMethodsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(4));
            Assert.That(allDetails[1].MethodName, Is.EqualTo("Placidus - ecliptical"));
            Assert.That(allDetails[2].DirMethod, Is.EqualTo(PrimaryDirMethods.RegiomontanusMundane));
        });
    }

}

