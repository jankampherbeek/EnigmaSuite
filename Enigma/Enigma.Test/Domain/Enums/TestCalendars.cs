// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;


[TestFixture]
public class TestCalendars
{


    [Test]
    public void TestRetrievingDetails()
    {
        Calendars calendar = Calendars.Julian;
        CalendarDetails details = calendar.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Calendar, Is.EqualTo(calendar));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.calendar.julian"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
        {
            CalendarDetails details = calendar.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }


    [Test]
    public void TestAllCalendarDetails()
    {
        List<CalendarDetails> allDetails = Calendars.Gregorian.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].Calendar, Is.EqualTo(Calendars.Gregorian));
            Assert.That(allDetails[1].TextId, Is.EqualTo("ref.enum.calendar.julian"));
        });
    }

}