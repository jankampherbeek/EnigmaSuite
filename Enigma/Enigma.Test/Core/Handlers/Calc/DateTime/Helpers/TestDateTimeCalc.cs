﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Calc.DateTime.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Facades.Interfaces;
using Moq;


namespace Enigma.Test.Core.Handlers.Calc.DateTime.Helpers;

[TestFixture]
public class TestDateTimeCalc
{

    [Test]
    public void TestCalcDateTime()
    {
        double jd = 12345.6789;
        Calendars calendar = Calendars.Gregorian;
        SimpleDateTime dateTime = new(2000, 1, 1, 12.0, calendar);
        var mock = new Mock<IRevJulFacade>();
        mock.Setup(p => p.DateTimeFromJd(jd, calendar)).Returns(dateTime);
        IDateTimeCalc dateTimeCalc = new DateTimeCalc(mock.Object);
        SimpleDateTime result = dateTimeCalc.CalcDateTime(jd, calendar);
        Assert.That(result, Is.EqualTo(dateTime));
    }


}
