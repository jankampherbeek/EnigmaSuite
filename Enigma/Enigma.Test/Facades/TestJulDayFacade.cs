// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Test.Facades;

[TestFixture]
public class TestJulDayFacade
{
    private const double DELTA = 0.00000001;
    
    [Test]
    public void TestJdFromSe()
    {
        const string path = "se";
        SeInitializer.SetEphePath(path);
        IJulDayFacade facade = new JulDayFacade();
        SimpleDateTime dateTime = new(2024, 5, 6, 20.5,Calendars.Gregorian);
        const double expectedJd = 2460437.3541666665; 
        double resultJd = facade.JdFromSe(dateTime);
        Assert.That(resultJd, Is.EqualTo(expectedJd).Within(DELTA));
    }

    [Test]
    public void TestDateTimeFromJd()
    {
        const string path = "se";
        SeInitializer.SetEphePath(path);
        IJulDayFacade facade = new JulDayFacade();
        const double jd = 2460437.3541666665; 
        SimpleDateTime expectedDateTime = new(2024, 5, 6, 20.5,Calendars.Gregorian);
        SimpleDateTime resultDatetime = facade.DateTimeFromJd(jd, Calendars.Gregorian);
        Assert.Multiple(() =>
        {
            Assert.That(resultDatetime.Year, Is.EqualTo(expectedDateTime.Year));
            Assert.That(resultDatetime.Month, Is.EqualTo(expectedDateTime.Month));
            Assert.That(resultDatetime.Day, Is.EqualTo(expectedDateTime.Day));
            Assert.That(resultDatetime.Calendar, Is.EqualTo(expectedDateTime.Calendar));
            Assert.That(resultDatetime.Ut, Is.EqualTo(expectedDateTime.Ut).Within(DELTA));
        });
    }
}