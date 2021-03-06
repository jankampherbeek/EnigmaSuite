// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;
using Enigma.Domain.Constants;

namespace Enigma.Test.Domain.CalcVars;

[TestFixture]
public class TestSolSysPointCatSpecifications
{

    [Test]
    public void TestRetrievingDetails()
    {
        SolSysPointCats solSysPointCat = SolSysPointCats.Modern;
        ISolSysPointCatSpecifications specifications = new SolSysPointCatSpecifications();
        SolSysPointCatDetails details = specifications.DetailsForCategory(solSysPointCat);
        Assert.That(details, Is.Not.Null);
        Assert.That(details.Category, Is.EqualTo(solSysPointCat));
        Assert.That(details.TextId, Is.EqualTo("enumSolSysPointCatModern"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        ISolSysPointCatSpecifications specifications = new SolSysPointCatSpecifications();
        foreach (SolSysPointCats category in Enum.GetValues(typeof(SolSysPointCats)))
        {
            SolSysPointCatDetails details = specifications.DetailsForCategory(category);
            Assert.That(details, Is.Not.Null);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }
}

[TestFixture]
public class TestSolarSystemPointSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        SolarSystemPoints point = SolarSystemPoints.Neptune;
        ISolarSystemPointSpecifications specifications = new SolarSystemPointSpecifications();
        SolarSystemPointDetails details = specifications.DetailsForPoint(point);
        Assert.IsNotNull(details);
        Assert.That(details.SolarSystemPoint, Is.EqualTo(point));
        Assert.That(details.SolSysPointCat, Is.EqualTo(SolSysPointCats.Modern));
        Assert.That(details.SeId, Is.EqualTo(EnigmaConstants.SE_NEPTUNE));
        Assert.IsTrue(details.UseForHeliocentric);
        Assert.IsTrue(details.UseForGeocentric);
        Assert.That(details.TextId, Is.EqualTo("neptune"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        ISolarSystemPointSpecifications specifications = new SolarSystemPointSpecifications();
        foreach (SolarSystemPoints point in Enum.GetValues(typeof(SolarSystemPoints)))
        {
            SolarSystemPointDetails details = specifications.DetailsForPoint(point);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }
}
