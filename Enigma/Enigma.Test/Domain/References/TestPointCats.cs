// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestPointCats
{

    [Test]
    public void TestRetrievingDetails()
    {
        const PointCats pointCat = PointCats.Common;
        PointCatDetails details = pointCat.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Category, Is.EqualTo(pointCat));
            Assert.That(details.RbKey, Is.EqualTo("ref.pointcat.common"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (PointCats category in Enum.GetValues(typeof(PointCats)))
        {
            PointCatDetails details = category.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 4;
        PointCats cat = PointCatsExtensions.PointCatForIndex(index);
        Assert.That(cat, Is.EqualTo(PointCats.Lots));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 55000;
        Assert.That(() => _ = PointCatsExtensions.PointCatForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllPointCatDetails()
    {
        List<PointCatDetails> allDetails = PointCatsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(6));
            Assert.That(allDetails[0].RbKey, Is.EqualTo("ref.pointcat.common"));
            Assert.That(allDetails[1].Category, Is.EqualTo(PointCats.Angle));
        });
    }

}

