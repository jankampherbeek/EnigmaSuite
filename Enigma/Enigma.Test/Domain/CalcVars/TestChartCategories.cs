// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;

namespace Enigma.Test.Domain.CalcVars;

[TestFixture]
public class TestChartCategories
{
    [Test]
    public void TestRetrievingDetails()
    {
        ChartCategories chartCategory = ChartCategories.Election;
        IChartCategorySpecifications specifications = new ChartCategorySpecifications();
        ChartCategoryDetails details = specifications.DetailsForCategory(chartCategory);
        Assert.IsNotNull(details);
        Assert.AreEqual(chartCategory, details.Category);
        Assert.AreEqual("ref.enum.chartcategories.election", details.TextId);
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IChartCategorySpecifications specifications = new ChartCategorySpecifications();

        foreach (ChartCategories chartCategory in Enum.GetValues(typeof(ChartCategories)))
        {
            ChartCategoryDetails details = specifications.DetailsForCategory(chartCategory);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        IChartCategorySpecifications specifications = new ChartCategorySpecifications();
        int chartCategoryIndex = 3;
        ChartCategories chartCategory = specifications.ChartCategoryForIndex(chartCategoryIndex);
        Assert.AreEqual(ChartCategories.Event, chartCategory);
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        IChartCategorySpecifications specifications = new ChartCategorySpecifications();
        int chartCategoryIndex = 500;
        Assert.That(() => _ = specifications.ChartCategoryForIndex(chartCategoryIndex), Throws.TypeOf<ArgumentException>());
    }

}