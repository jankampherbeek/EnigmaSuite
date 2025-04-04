﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;


[TestFixture]
public class TestYearCounts
{

    [Test]
    public void TestRetrievingDetails()
    {
        const YearCounts yearCount = YearCounts.BCE;
        YearCountDetails details = yearCount.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.YearCount, Is.EqualTo(yearCount));
            Assert.That(details.Text, Is.EqualTo("BCE"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
        {
            YearCountDetails details = yearCount.GetDetails();
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int yearCountIndex = 2;
        YearCounts yearCount = YearCountsExtensions.YearCountForIndex(yearCountIndex);
        Assert.That(yearCount, Is.EqualTo(YearCounts.Astronomical));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int yearCountIndex = 44;
        Assert.That(() => _ = YearCountsExtensions.YearCountForIndex(yearCountIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDetailsForYearCounts()
    {
        List<YearCountDetails> allDetails = YearCountsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(3));
            Assert.That(allDetails[0].YearCount, Is.EqualTo(YearCounts.CE));
            Assert.That(allDetails[2].Text, Is.EqualTo("Astronomical"));
        });
    }
}