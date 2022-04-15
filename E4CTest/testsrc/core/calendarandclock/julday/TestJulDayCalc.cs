// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.core.calendarandclock.julday;
using E4C.core.facades;
using E4C.core.shared.domain;
using E4C.shared.references;
using Moq;
using NUnit.Framework;


namespace E4CTest.core.calendarandclock.julday;

[TestFixture]
public class TestJulDayCalc
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestCalcJulDay()
    {
        double jd = 12345.6789;
        SimpleDateTime dateTime = new SimpleDateTime(2000, 1, 1, 12.0, Calendars.Gregorian); 
        var mock = new Mock<IJulDayFacade>();
        mock.Setup(p => p.JdFromSe(dateTime)).Returns(jd);
        IJulDayCalc jdCalc = new JulDayCalc(mock.Object);
        double jdResult = jdCalc.CalcJulDay(dateTime);
        Assert.AreEqual(jd, jdResult, _delta);
    }


}

