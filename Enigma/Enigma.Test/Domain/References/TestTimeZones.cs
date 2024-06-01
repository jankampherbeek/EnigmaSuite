// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestTimeZones
{
    private const double DELTA = 0.00000001;

    [Test]
    public void TestRetrievingDetails()
    {
        const TimeZones timeZone = TimeZones.Azot;
        TimeZoneDetails details = timeZone.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TimeZone, Is.EqualTo(timeZone));
            Assert.That(details.OffsetFromUt, Is.EqualTo(-1.0).Within(DELTA));
            Assert.That(details.RbKey, Is.EqualTo("ref.timezone.azot"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (TimeZones timeZone in Enum.GetValues(typeof(TimeZones)))
        {
            TimeZoneDetails details = timeZone.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKey, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int timeZoneIndex = 29;
        TimeZones timeZone = TimeZonesExtensions.TimeZoneForIndex(timeZoneIndex);
        Assert.That(timeZone, Is.EqualTo(TimeZones.Brt));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int timeZoneIndex = -100;
        Assert.That(() => _ = TimeZonesExtensions.TimeZoneForIndex(timeZoneIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllTimeZoneDetails()
    {
        List<TimeZoneDetails> allDetails = TimeZonesExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(33));
            Assert.That(allDetails[0].TimeZone, Is.EqualTo(TimeZones.Ut));
            Assert.That(allDetails[8].TimeZone, Is.EqualTo(TimeZones.Ist));
            Assert.That(allDetails[10].RbKey, Is.EqualTo("ref.timezone.mmt"));
            Assert.That(allDetails[20].OffsetFromUt, Is.EqualTo(-10.0).Within(DELTA));
        });
    }
}