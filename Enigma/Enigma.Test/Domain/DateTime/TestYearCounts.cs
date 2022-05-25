// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.DateTime;

namespace Enigma.Test.Domain.DateTime;


[TestFixture]
public class TestYearCounts
{
    [Test]
    public void TestRetrievingDetails()
    {
        YearCounts yearCount = YearCounts.BCE;
        IYearCountSpecifications specifications = new YearCountSpecifications();
        YearCountDetails details = specifications.DetailsForYearCount(yearCount);
        Assert.IsNotNull(details);
        Assert.That(details.YearCount, Is.EqualTo(yearCount));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.yearcount.bce"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IYearCountSpecifications specifications = new YearCountSpecifications();
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
        IYearCountSpecifications specifications = new YearCountSpecifications();
        int yearCountIndex = 2;
        YearCounts yearCount = specifications.YearCountForIndex(yearCountIndex);
        Assert.That(yearCount, Is.EqualTo(YearCounts.Astronomical));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        IYearCountSpecifications specifications = new YearCountSpecifications();
        int yearCountIndex = 44;
        Assert.That(() => _ = specifications.YearCountForIndex(yearCountIndex), Throws.TypeOf<ArgumentException>());
    }
}