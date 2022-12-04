// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestChartCategories
{ 

    [Test]
    public void TestRetrievingDetails()
    {
        ChartCategories chartCategory = ChartCategories.Election;
        ChartCategoryDetails details = chartCategory.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Category, Is.EqualTo(chartCategory));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.chartcategories.election"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ChartCategories chartCategory in Enum.GetValues(typeof(ChartCategories)))
        {
            ChartCategoryDetails details = chartCategory.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int chartCategoryIndex = 3;
        ChartCategories chartCategory =  ChartCategories.Election.ChartCategoryForIndex(chartCategoryIndex);
        Assert.That(chartCategory, Is.EqualTo(ChartCategories.Event));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int chartCategoryIndex = 500;
        Assert.That(() => _ = ChartCategories.Election.ChartCategoryForIndex(chartCategoryIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllChartCatDetails()
    {
        List<ChartCategoryDetails> allDetails = ChartCategories.Election.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(6));
            Assert.That(allDetails[0].Category, Is.EqualTo(ChartCategories.Unknown));
            Assert.That(allDetails[3].Category, Is.EqualTo(ChartCategories.Event));
            Assert.That(allDetails[4].TextId, Is.EqualTo("ref.enum.chartcategories.horary"));
        });
    }
}