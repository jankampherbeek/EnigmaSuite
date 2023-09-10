// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestChartCategories
{

    [Test]
    public void TestRetrievingDetails()
    {
        const ChartCategories chartCategory = ChartCategories.Election;
        ChartCategoryDetails details = chartCategory.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Category, Is.EqualTo(chartCategory));
            Assert.That(details.Text, Is.EqualTo("Election"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ChartCategories chartCategory in Enum.GetValues(typeof(ChartCategories)))
        {
            ChartCategoryDetails details = chartCategory.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int chartCategoryIndex = 3;
        ChartCategories chartCategory = ChartCategoriesExtensions.ChartCategoryForIndex(chartCategoryIndex);
        Assert.That(chartCategory, Is.EqualTo(ChartCategories.Event));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int chartCategoryIndex = 500;
        Assert.That(() => _ = ChartCategoriesExtensions.ChartCategoryForIndex(chartCategoryIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllChartCatDetails()
    {
        List<ChartCategoryDetails> allDetails = ChartCategoriesExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(6));
            Assert.That(allDetails[0].Category, Is.EqualTo(ChartCategories.Unknown));
            Assert.That(allDetails[3].Category, Is.EqualTo(ChartCategories.Event));
            Assert.That(allDetails[4].Text, Is.EqualTo("Horary"));
        });
    }
}