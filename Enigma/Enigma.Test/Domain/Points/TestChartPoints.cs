﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Points;

namespace Enigma.Test.Domain.Points;

[TestFixture]
public class TestChartPoints
{
    [Test]
    public void TestRetrievingDetails()
    {
        ChartPoints point = ChartPoints.Neptune;
        PointDetails details = point.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Point, Is.EqualTo(point));
            Assert.That(details.PointCat, Is.EqualTo(PointCats.Modern));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.celpoint.neptune"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ChartPoints point in Enum.GetValues(typeof(ChartPoints)))
        {
            if (point != ChartPoints.None)
            {
                PointDetails details = point.GetDetails();
                Assert.That(details, Is.Not.Null);
                Assert.That(details.TextId, Is.Not.Empty);
            }
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 9;
        ChartPoints chartPoint = ChartPoints.Moon.PointForIndex(index);
        Assert.That(chartPoint, Is.EqualTo(ChartPoints.Neptune));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = -100;
        Assert.That(() => _ = ChartPoints.None.PointForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllChartPointDetails()
    {
        List<PointDetails> allDetails = ChartPoints.None.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(93));

            Assert.That(allDetails[0].PointCat, Is.EqualTo(PointCats.Classic));
            Assert.That(allDetails[3].TextId, Is.EqualTo("ref.enum.celpoint.venus"));
        });
    }
}