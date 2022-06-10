// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.DateTime;

namespace Enigma.Test.Domain.DateTime;


[TestFixture]
public class TestYearCounts
{
    private IYearCountSpecifications specifications;

    [SetUp]
    public void SetUp()
    {
        specifications = new YearCountSpecifications();
    }

    [Test]
    public void TestRetrievingDetails()
    {
        YearCounts yearCount = YearCounts.BCE;
        YearCountDetails details = specifications.DetailsForYearCount(yearCount);
        Assert.IsNotNull(details);
        Assert.That(details.YearCount, Is.EqualTo(yearCount));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.yearcount.bce"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
        {
            YearCountDetails details = specifications.DetailsForYearCount(yearCount);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int yearCountIndex = 2;
        YearCounts yearCount = specifications.YearCountForIndex(yearCountIndex);
        Assert.That(yearCount, Is.EqualTo(YearCounts.Astronomical));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int yearCountIndex = 44;
        Assert.That(() => _ = specifications.YearCountForIndex(yearCountIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDetailsForYearCounts()
    {
        List<YearCountDetails> allDetails = specifications.AllDetailsForYearCounts();
        Assert.That(allDetails.Count == 3);
        Assert.That(allDetails[0].YearCount, Is.EqualTo(YearCounts.CE));
        Assert.That(allDetails[2].TextId, Is.EqualTo("ref.enum.yearcount.astronomical"));
    }

}