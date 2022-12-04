// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;

namespace Enigma.Test.Domain.Enums;


[TestFixture]
public class TestYearCounts
{

    [Test]
    public void TestRetrievingDetails()
    {
        YearCounts yearCount = YearCounts.BCE;
        YearCountDetails details = yearCount.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.YearCount, Is.EqualTo(yearCount));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.yearcount.bce"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
        {
            YearCountDetails details = yearCount.GetDetails();
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int yearCountIndex = 2;
        YearCounts yearCount = YearCounts.CE.YearCountForIndex(yearCountIndex);
        Assert.That(yearCount, Is.EqualTo(YearCounts.Astronomical));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int yearCountIndex = 44;
        Assert.That(() => _ = YearCounts.CE.YearCountForIndex(yearCountIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDetailsForYearCounts()
    {
        List<YearCountDetails> allDetails = YearCounts.CE.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(3));
            Assert.That(allDetails[0].YearCount, Is.EqualTo(YearCounts.CE));
            Assert.That(allDetails[2].TextId, Is.EqualTo("ref.enum.yearcount.astronomical"));
        });
    }
}