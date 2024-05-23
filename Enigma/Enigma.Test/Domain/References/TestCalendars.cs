// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;


[TestFixture]
public class TestCalendars
{

    [Test]
    public void TestRetrievingDetails()
    {
        const Calendars calendar = Calendars.Julian;
        CalendarDetails details = calendar.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Calendar, Is.EqualTo(calendar));
            Assert.That(details.RbKeyShort, Is.EqualTo("J"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
        {
            CalendarDetails details = calendar.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.RbKeyShort, Is.Not.Empty);
        }
    }


    [Test]
    public void TestAllCalendarDetails()
    {
        List<CalendarDetails> allDetails = CalendarsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].Calendar, Is.EqualTo(Calendars.Gregorian));
            Assert.That(allDetails[1].RbKeyShort, Is.EqualTo("J"));
        });
    }

}