﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;


[TestFixture]
public class TestResearchMethods
{

    [Test]
    public void TestRetrievingDetails()
    {
        const ResearchMethods method = ResearchMethods.CountAspects;
        ResearchMethodDetails details = method.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.ResearchMethod, Is.EqualTo(ResearchMethods.CountAspects));
            Assert.That(details.Text, Is.EqualTo("Count aspects"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ResearchMethods method in Enum.GetValues(typeof(ResearchMethods)))
        {
            ResearchMethodDetails details = method.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 4;
        ResearchMethods method = ResearchMethodsExtensions.ResearchMethodForIndex(index);
        Assert.That(method, Is.EqualTo(ResearchMethods.CountOccupiedMidpoints));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 5000;
        Assert.That(() => _ = ResearchMethodsExtensions.ResearchMethodForIndex(index), Throws.TypeOf<ArgumentException>());
    }


    [Test]
    public void TestAllResearchMethodDetails()
    {
        List<ResearchMethodDetails> allDetails = ResearchMethodsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(9));
            Assert.That(allDetails[1].Text, Is.EqualTo("Count positions in houses"));
            Assert.That(allDetails[2].MinNumberOfPoints, Is.EqualTo(2));
            Assert.That(allDetails[3].ResearchMethod, Is.EqualTo(ResearchMethods.CountUnaspected));
        });
    }






}
