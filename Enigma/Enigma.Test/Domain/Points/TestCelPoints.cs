// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Constants;
using Enigma.Domain.Enums;
using Enigma.Domain.Points;

namespace Enigma.Test.Domain.Points;

[TestFixture]
public class TestCelPoints
{
    [Test]
    public void TestRetrievingDetails()
    {
        CelPoints point = CelPoints.Neptune;
        CelPointDetails details = point.GetDetails();
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
        foreach (CelPoints point in Enum.GetValues(typeof(CelPoints)))
        {
            CelPointDetails details = point.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 9;
        CelPoints celPoint = CelPoints.Moon.CelestialPointForIndex(index);
        Assert.That(celPoint, Is.EqualTo(CelPoints.Neptune));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = -100;
        Assert.That(() => _ = CelPoints.Ceres.CelestialPointForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllCelPointDetails()
    {
        List<CelPointDetails> allDetails = CelPoints.Vesta.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(49));

            Assert.That(allDetails[0].CelPointCat, Is.EqualTo(CelPointCats.Classic));
            Assert.That(allDetails[3].TextId, Is.EqualTo("venus"));
            Assert.That(allDetails[46].CalculationType, Is.EqualTo(CalculationTypes.Numeric));
            Assert.That(allDetails[27].SeId, Is.EqualTo(EnigmaConstants.SE_CERES));
        });
    }
}
