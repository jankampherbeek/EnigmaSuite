// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.DateTime;

namespace Enigma.Test.Domain.DateTime;


[TestFixture]
public class TestCalendarSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        Calendars calendar = Calendars.Julian;
        ICalendarSpecifications specifications = new CalendarSpecifications();
        CalendarDetails details = specifications.DetailsForCalendar(calendar);
        Assert.IsNotNull(details);
        Assert.That(details.Calendar, Is.EqualTo(calendar));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.calendar.julian"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        ICalendarSpecifications specifications = new CalendarSpecifications();
        foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
        {
            CalendarDetails details = specifications.DetailsForCalendar(calendar);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        ICalendarSpecifications specifications = new CalendarSpecifications();
        int calendarIndex = 1;
        Calendars calendar = specifications.CalendarForIndex(calendarIndex);
        Assert.That(calendar, Is.EqualTo(Calendars.Julian));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        ICalendarSpecifications specifications = new CalendarSpecifications();
        int calendarIndex = 333;
        Assert.That(() => _ = specifications.CalendarForIndex(calendarIndex), Throws.TypeOf<ArgumentException>());

    }

}