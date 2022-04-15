// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.core.calendarandclock.datetime;
using E4C.core.calendarandclock.julday;
using E4C.core.facades;
using E4C.core.shared.domain;
using E4C.shared.references;
using Moq;
using NUnit.Framework;


namespace E4CTest.core.calendarandclock.datetime;

[TestFixture]
public class TestDateTimeCalc
{
    private readonly double _delta = 0.00000001;

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

