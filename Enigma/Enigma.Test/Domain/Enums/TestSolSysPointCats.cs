// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestCelPointCatSpecifications
{

    [Test]
    public void TestRetrievingDetails()
    {
        CelPointCats celPointCat = CelPointCats.Modern;
        ICelPointCatSpecifications specifications = new CelPointCatSpecifications();
        CelPointCatDetails details = specifications.DetailsForCategory(celPointCat);
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Category, Is.EqualTo(celPointCat));
            Assert.That(details.TextId, Is.EqualTo("enumCelPointCatModern"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        ICelPointCatSpecifications specifications = new CelPointCatSpecifications();
        foreach (CelPointCats category in Enum.GetValues(typeof(CelPointCats)))
        {
            CelPointCatDetails details = specifications.DetailsForCategory(category);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }
}

[TestFixture]
public class TestCelPointSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        CelPoints point = CelPoints.Neptune;
        ICelPointSpecifications specifications = new CelPointSpecifications();
        CelPointDetails details = specifications.DetailsForPoint(point);
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.CelPoint, Is.EqualTo(point));
            Assert.That(details.CelPointCat, Is.EqualTo(CelPointCats.Modern));
            Assert.That(details.SeId, Is.EqualTo(EnigmaConstants.SE_NEPTUNE));
            Assert.That(details.UseForHeliocentric, Is.True);
            Assert.That(details.UseForGeocentric, Is.True);
            Assert.That(details.TextId, Is.EqualTo("neptune"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        ICelPointSpecifications specifications = new CelPointSpecifications();
        foreach (CelPoints point in Enum.GetValues(typeof(CelPoints)))
        {
            CelPointDetails details = specifications.DetailsForPoint(point);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }
}
