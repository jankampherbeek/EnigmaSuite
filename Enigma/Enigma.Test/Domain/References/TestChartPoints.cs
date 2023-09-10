// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestChartPoints
{
    [Test]
    public void TestRetrievingDetails()
    {
        const ChartPoints point = ChartPoints.Neptune;
        PointDetails details = point.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Point, Is.EqualTo(point));
            Assert.That(details.PointCat, Is.EqualTo(PointCats.Common));
            Assert.That(details.Text, Is.EqualTo("Neptune"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ChartPoints point in Enum.GetValues(typeof(ChartPoints)))
        {
            if (point == ChartPoints.Sun) continue;
            PointDetails details = point.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 9;
        ChartPoints chartPoint = PointsExtensions.PointForIndex(index);
        Assert.That(chartPoint, Is.EqualTo(ChartPoints.Neptune));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = -100;
        Assert.That(() => _ = PointsExtensions.PointForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllChartPointDetails()
    {
        List<PointDetails> allDetails = PointsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(92));

            Assert.That(allDetails[0].PointCat, Is.EqualTo(PointCats.Common));
            Assert.That(allDetails[3].Text, Is.EqualTo("Venus"));
        });
    }
}
