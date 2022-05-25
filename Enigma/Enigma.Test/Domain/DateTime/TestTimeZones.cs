// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.DateTime;

namespace Enigma.Test.Domain.DateTime;

[TestFixture]
public class TestTimeZones
{
    [Test]
    public void TestRetrievingDetails()
    {
        double delta = 0.00000001;
        TimeZones timeZone = TimeZones.AZOT;
        ITimeZoneSpecifications specifications = new TimeZoneSpecifications();
        TimeZoneDetails details = specifications.DetailsForTimeZone(timeZone);
        Assert.IsNotNull(details);
        Assert.That(details.TimeZone, Is.EqualTo(timeZone));
        Assert.That(details.OffsetFromUt, Is.EqualTo(-1.0).Within(delta));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.timezone.azot"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        ITimeZoneSpecifications specifications = new TimeZoneSpecifications();

        foreach (TimeZones timeZone in Enum.GetValues(typeof(TimeZones)))
        {
            TimeZoneDetails details = specifications.DetailsForTimeZone(timeZone);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        ITimeZoneSpecifications specifications = new TimeZoneSpecifications();
        int timeZoneIndex = 29;
        TimeZones timeZone = specifications.TimeZoneForIndex(timeZoneIndex);
        Assert.That(timeZone, Is.EqualTo(TimeZones.BRT));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        ITimeZoneSpecifications specifications = new TimeZoneSpecifications();
        int timeZoneIndex = -100;
        Assert.That(() => _ = specifications.TimeZoneForIndex(timeZoneIndex), Throws.TypeOf<ArgumentException>());
    }

}