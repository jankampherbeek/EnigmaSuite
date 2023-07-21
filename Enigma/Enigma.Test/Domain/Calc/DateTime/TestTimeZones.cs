// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;

namespace Enigma.Test.Domain.Calc.DateTime;

[TestFixture]
public class TestTimeZones
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestRetrievingDetails()
    {
        double delta = 0.00000001;
        TimeZones timeZone = TimeZones.Azot;
        TimeZoneDetails details = timeZone.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TimeZone, Is.EqualTo(timeZone));
            Assert.That(details.OffsetFromUt, Is.EqualTo(-1.0).Within(delta));
            Assert.That(details.Text, Is.EqualTo("-01:00: AZOT/Azores Standard Time"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (TimeZones timeZone in Enum.GetValues(typeof(TimeZones)))
        {
            TimeZoneDetails details = timeZone.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int timeZoneIndex = 29;
        TimeZones timeZone = TimeZones.Cet.TimeZoneForIndex(timeZoneIndex);
        Assert.That(timeZone, Is.EqualTo(TimeZones.Brt));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int timeZoneIndex = -100;
        Assert.That(() => _ = TimeZones.Mst.TimeZoneForIndex(timeZoneIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllTimeZoneDetails()
    {
        List<TimeZoneDetails> allDetails = TimeZones.Ut.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(33));
            Assert.That(allDetails[0].TimeZone, Is.EqualTo(TimeZones.Ut));
            Assert.That(allDetails[8].TimeZone, Is.EqualTo(TimeZones.Ist));
            Assert.That(allDetails[10].Text, Is.EqualTo("+06:30: MMT/Myanmar Standard Time"));
            Assert.That(allDetails[20].OffsetFromUt, Is.EqualTo(-10.0).Within(_delta));
        });
    }
}