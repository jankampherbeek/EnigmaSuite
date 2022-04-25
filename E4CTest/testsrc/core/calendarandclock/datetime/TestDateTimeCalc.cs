// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.CalendarAndClock.DateTime;
using E4C.Core.Facades;
using E4C.Core.Shared.Domain;
using E4C.Shared.References;
using Moq;
using NUnit.Framework;


namespace E4CTest.core.calendarandclock.datetime;

[TestFixture]
public class TestDateTimeCalc
{

    [Test]
    public void TestCalcDateTime()
    {
        double jd = 12345.6789;
        Calendars calendar = Calendars.Gregorian;
        SimpleDateTime dateTime = new SimpleDateTime(2000, 1, 1, 12.0, calendar);
        var mock = new Mock<IRevJulFacade>();
        mock.Setup(p => p.DateTimeFromJd(jd, calendar)).Returns(dateTime);
        IDateTimeCalc dateTimeCalc = new DateTimeCalc(mock.Object);
        SimpleDateTime result = dateTimeCalc.CalcDateTime(jd, calendar);
        Assert.AreEqual(dateTime, result);
    }


}

