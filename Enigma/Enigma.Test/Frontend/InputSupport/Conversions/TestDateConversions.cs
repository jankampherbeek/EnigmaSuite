// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.DateTime;
using Enigma.Frontend.InputSupport.Conversions;
using Moq;

namespace Enigma.Test.Frontend.InputSupport.Conversions;


[TestFixture]
public class TestDateConversions
{

    readonly private double delta = 0.00000001;

    [Test]
    public void TestInputDateToJdNrHappyFlow()
    {
        string[] inputDate = new string[] { "1953", "1", "29" };
        var calendar = Calendars.Gregorian;
        var yearCount = YearCounts.CE;
        double jdnrUt = 2434406.5;
        double jdnrEt = 2434406.5000001;
        double deltaT = 0.000234;
        var mockJulianDayApi = new Mock<IJulianDayApi>();
        var mockCalcDateTimeApi = new Mock<ICalcDateTimeApi>();
        var dateTime = new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian);
        var jdRequest = new JulianDayRequest(dateTime);
        var jdResponse = new JulianDayResponse(jdnrUt, jdnrEt, deltaT, true, "");
        mockJulianDayApi.Setup(p => p.getJulianDay(jdRequest)).Returns(jdResponse);
        IDateConversions conversions = new DateConversions(mockCalcDateTimeApi.Object, mockJulianDayApi.Object);
        Assert.That(conversions.InputDateToJdNr(inputDate, calendar, yearCount), Is.EqualTo(jdnrUt).Within(delta));
    }


    [Test]
    public void TestInputDateToJdNrError()
    {
        string[] inputDate = new string[] { "1953", "Jan", "29" };
        var calendar = Calendars.Gregorian;
        var yearCount = YearCounts.CE;
        double jdnrUt = 2434406.5;
        double jdnrEt = 2434406.5000001;
        double deltaT = 0.000234;
        var mockJulianDayApi = new Mock<IJulianDayApi>();
        var mockCalcDateTimeApi = new Mock<ICalcDateTimeApi>();
        var jdResponse = new JulianDayResponse(jdnrUt, jdnrEt, deltaT, true, "");
        mockJulianDayApi.Setup(p => p.getJulianDay(new JulianDayRequest(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian)))).Returns(jdResponse);

        IDateConversions conversions = new DateConversions(mockCalcDateTimeApi.Object, mockJulianDayApi.Object);
        Assert.Throws<ArgumentException>(() => conversions.InputDateToJdNr(inputDate, calendar, yearCount));
    }

    [Test]
    public void TestInputDateToDecimalsHappyFlow()
    {
        string[] inputDate = new string[] { "1953", "1", "29" };
        var mockJulianDayApi = new Mock<IJulianDayApi>();
        var mockCalcDateTimeApi = new Mock<ICalcDateTimeApi>();
        IDateConversions conversions = new DateConversions(mockCalcDateTimeApi.Object, mockJulianDayApi.Object);
        int[] dateResult = conversions.InputDateToDecimals(inputDate);
        Assert.That(dateResult[0], Is.EqualTo(1953));
        Assert.That(dateResult[1], Is.EqualTo(1));
        Assert.That(dateResult[2], Is.EqualTo(29));
    }

    [Test]
    public void TestInputDateToDecimalsError()
    {
        string[] inputDate = new string[] { "xx", "1", "29" };
        var mockJulianDayApi = new Mock<IJulianDayApi>();
        var mockCalcDateTimeApi = new Mock<ICalcDateTimeApi>();
        IDateConversions conversions = new DateConversions(mockCalcDateTimeApi.Object, mockJulianDayApi.Object);
        Assert.Throws<ArgumentException>(() => conversions.InputDateToDecimals(inputDate));
    }

}

