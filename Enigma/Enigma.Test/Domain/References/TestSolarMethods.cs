// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestSolarMethods
{
    
    [Test]
    public void TestRetrievingDetails()
    {
        const SolarMethods method = SolarMethods.SiderealParallax;
        SolarMethodDetails details = method.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.Method, Is.EqualTo(SolarMethods.SiderealParallax));
            Assert.That(details.MethodName, Is.EqualTo("Sidereal return with parallax"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (SolarMethods method in Enum.GetValues(typeof(SolarMethods)))
        {
            SolarMethodDetails details = method.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.MethodName, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 1;
        SolarMethods method = SolarMethodsExtensions.MethodForIndex(index);
        Assert.That(method, Is.EqualTo(SolarMethods.TropicalParallax));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 333;
        Assert.That(() => _ = SolarMethodsExtensions.MethodForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllSolarMethodDetails()
    {
        List<SolarMethodDetails> allDetails = SolarMethodsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(4));
            Assert.That(allDetails[3].MethodName, Is.EqualTo("Sidereal return, no parallax"));
            Assert.That(allDetails[2].Method, Is.EqualTo(SolarMethods.SiderealParallax));
        });
    }
    
}