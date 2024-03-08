// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;
using FakeItEasy;

namespace Enigma.Test.Core.Calc;

[TestFixture]
public class TestDateTimeCalc
{

    [Test]
    public void TestCalcDateTime()
    {
        const double jd = 12345.6789;
        const Calendars calendar = Calendars.Gregorian;
        SimpleDateTime dateTime = new(2000, 1, 1, 12.0, calendar);
        var facadeFake = A.Fake<IRevJulFacade>();
        A.CallTo(() => facadeFake.DateTimeFromJd(jd, calendar)).Returns(dateTime);
        IDateTimeCalc dateTimeCalc = new DateTimeCalc(facadeFake);
        SimpleDateTime result = dateTimeCalc.CalcDateTime(jd, calendar);
        Assert.That(result, Is.EqualTo(dateTime));
    }
}

