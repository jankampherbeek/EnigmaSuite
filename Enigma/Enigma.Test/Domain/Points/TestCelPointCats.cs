// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Points;

namespace Enigma.Test.Domain.Points;

[TestFixture]
public class TestCelPointCats
{

    [Test]
    public void TestRetrievingDetails()
    {
        CelPointCats celPointCat = CelPointCats.Modern;
        CelPointCatDetails details = celPointCat.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Category, Is.EqualTo(celPointCat));
            Assert.That(details.TextId, Is.EqualTo("enumCelPointCatModern"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (CelPointCats category in Enum.GetValues(typeof(CelPointCats)))
        {
            CelPointCatDetails details = category.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 4;
        CelPointCats cat = CelPointCats.MathPoint.CelPointCatForIndex(index);
        Assert.That(cat, Is.EqualTo(CelPointCats.Hypothetical));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = 500;
        Assert.That(() => _ = CelPointCats.MathPoint.CelPointCatForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllCelPointCatDetails()
    {
        List<CelPointCatDetails> allDetails = CelPointCats.Classic.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(5));

            Assert.That(allDetails[0].TextId, Is.EqualTo("enumCelPointCatClassic"));
            Assert.That(allDetails[1].Category, Is.EqualTo(CelPointCats.Modern));
        });
    }

}

