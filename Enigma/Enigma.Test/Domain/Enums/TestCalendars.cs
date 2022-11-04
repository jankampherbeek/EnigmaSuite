// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;


[TestFixture]
public class TestCalendarSpecifications
{
    private ICalendarSpecifications specifications;

    [SetUp]
    public void SetUp()
    {
        specifications = new CalendarSpecifications();
    }

    [Test]
    public void TestRetrievingDetails()
    {
        Calendars calendar = Calendars.Julian;
        CalendarDetails details = specifications.DetailsForCalendar(calendar);
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
            CalendarDetails details = specifications.DetailsForCalendar(calendar);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int calendarIndex = 1;
        Calendars calendar = specifications.CalendarForIndex(calendarIndex);
        Assert.That(calendar, Is.EqualTo(Calendars.Julian));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int calendarIndex = 333;
        Assert.That(() => _ = specifications.CalendarForIndex(calendarIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllCalendarDetails()
    {
        List<CalendarDetails> allDetails = specifications.AllCalendarDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].Calendar, Is.EqualTo(Calendars.Gregorian));
            Assert.That(allDetails[1].TextId, Is.EqualTo("ref.enum.calendar.julian"));
        });
    }
}