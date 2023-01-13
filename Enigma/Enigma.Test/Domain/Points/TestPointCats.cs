// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Points;

namespace Enigma.Test.Domain.Points;

[TestFixture]
public class TestPointCats
{

    [Test]
    public void TestRetrievingDetails()
    {
        PointCats pointCat = PointCats.Modern;
        PointCatDetails details = pointCat.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Category, Is.EqualTo(pointCat));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.pointcats.modern"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (PointCats category in Enum.GetValues(typeof(PointCats)))
        {
            if (category != PointCats.None)
            {
                PointCatDetails details = category.GetDetails();
                Assert.That(details, Is.Not.Null);
                Assert.That(details.TextId, Is.Not.Empty);
            }
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 4;
        PointCats cat = PointCats.None.PointCatForIndex(index);
        Assert.That(cat, Is.EqualTo(PointCats.Hypothetical));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = 55000;
        Assert.That(() => _ = PointCats.None.PointCatForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllPointCatDetails()
    {
        List<PointCatDetails> allDetails = PointCats.None.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(9));

            Assert.That(allDetails[0].TextId, Is.EqualTo("ref.enum.pointcats.classic"));
            Assert.That(allDetails[1].Category, Is.EqualTo(PointCats.Modern));
        });
    }

}

