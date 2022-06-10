// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.DateTime;

namespace Enigma.Test.Domain.DateTime;

[TestFixture]
public class TestTimeZones
{
    private ITimeZoneSpecifications specifications;
    private readonly double _delta = 0.00000001;

    [SetUp]
    public void SetUp()
    {
        specifications = new TimeZoneSpecifications();
    }

    [Test]
    public void TestRetrievingDetails()
    {
        double delta = 0.00000001;
        TimeZones timeZone = TimeZones.AZOT;
        TimeZoneDetails details = specifications.DetailsForTimeZone(timeZone);
        Assert.IsNotNull(details);
        Assert.That(details.TimeZone, Is.EqualTo(timeZone));
        Assert.That(details.OffsetFromUt, Is.EqualTo(-1.0).Within(delta));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.timezone.azot"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
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
        int timeZoneIndex = 29;
        TimeZones timeZone = specifications.TimeZoneForIndex(timeZoneIndex);
        Assert.That(timeZone, Is.EqualTo(TimeZones.BRT));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int timeZoneIndex = -100;
        Assert.That(() => _ = specifications.TimeZoneForIndex(timeZoneIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllTimeZoneDetails()
    {
        List<TimeZoneDetails> allDetails = specifications.AllTimeZoneDetails();
        Assert.That(allDetails.Count == 33);
        Assert.That(allDetails[0].TimeZone, Is.EqualTo(TimeZones.UT));
        Assert.That(allDetails[8].TimeZone, Is.EqualTo(TimeZones.IST));
        Assert.That(allDetails[10].TextId, Is.EqualTo("ref.enum.timezone.mmt"));
        Assert.That(allDetails[20].OffsetFromUt, Is.EqualTo(-10.0).Within(_delta));
    }

}