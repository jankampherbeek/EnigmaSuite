﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;

namespace Enigma.Test.Domain.Analysis;

[TestFixture]
public class TestOrbMethods
{

    [Test]
    public void TestRetrievingDetails()
    {
        OrbMethods method = OrbMethods.Weighted;
        OrbMethodDetails details = method.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.EqualTo("ref.enum.orbmethod.weighted"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (OrbMethods method in Enum.GetValues(typeof(OrbMethods)))
        {
            OrbMethodDetails details = method.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 1;
        OrbMethods method = OrbMethods.FixMajorMinor.OrbMethodForIndex(index);
        Assert.That(method, Is.EqualTo(OrbMethods.Weighted));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = 500;
        Assert.That(() => _ = OrbMethods.Weighted.OrbMethodForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllOrbMethodDetails()
    {
        List<OrbMethodDetails> allDetails = OrbMethods.Weighted.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].TextId, Is.EqualTo("ref.enum.orbmethod.fixmajorminor"));
            Assert.That(allDetails[1].OrbMethod, Is.EqualTo(OrbMethods.Weighted));
        });
    }

}
