// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Api.Datetime;
using E4C.Core.Shared.Domain;
using E4C.Shared.References;
using E4C.Shared.ReqResp;
using E4C.Ui.Shared;
using Moq;
using NUnit.Framework;
using System;

namespace E4CTest.Ui.Shared;


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
        double jdnr = 2434406.5;
        var mockJulianDayApi = new Mock<IJulianDayApi>();
        var mockCalcDateTimeApi = new Mock<ICalcDateTimeApi>();
        var dateTime = new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian);
        var jdRequest = new JulianDayRequest(dateTime, true);
        var jdResponse = new JulianDayResponse(jdnr, true, "");
        mockJulianDayApi.Setup(p => p.getJulianDay(jdRequest)).Returns(jdResponse);
        DateConversions conversions = new(mockCalcDateTimeApi.Object,  mockJulianDayApi.Object);
        Assert.AreEqual(jdnr, conversions.InputDateToJdNr(inputDate, calendar, yearCount), delta);
    }

    [Test]
    public void TestInputDateToJdNrError()
    {
        string[] inputDate = new string[] { "1953", "Jan", "29" };
        var calendar = Calendars.Gregorian;
        var yearCount = YearCounts.CE;
        double jdnr = 2434406.5;
        var mockJulianDayApi = new Mock<IJulianDayApi>();
        var mockCalcDateTimeApi = new Mock<ICalcDateTimeApi>();
        var jdResponse = new JulianDayResponse(jdnr, true, "");
        mockJulianDayApi.Setup(p => p.getJulianDay(new JulianDayRequest(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian), true))).Returns(jdResponse);
        DateConversions conversions = new(mockCalcDateTimeApi.Object,  mockJulianDayApi.Object);
        Assert.Throws<ArgumentException>(() => conversions.InputDateToJdNr(inputDate, calendar, yearCount));
    }

    [Test]
    public void TestInputDateToDecimalsHappyFlow()
    {
        string[] inputDate = new string[] { "1953", "1", "29" };
        var mockJulianDayApi = new Mock<IJulianDayApi>();
        var mockCalcDateTimeApi = new Mock<ICalcDateTimeApi>();
        DateConversions conversions = new(mockCalcDateTimeApi.Object, mockJulianDayApi.Object);
        int[] dateResult = conversions.InputDateToDecimals(inputDate);
        Assert.AreEqual(1953, dateResult[0]);
        Assert.AreEqual(1, dateResult[1]);
        Assert.AreEqual(29, dateResult[2]);
    }

    [Test]
    public void TestInputDateToDecimalsError()
    {
        string[] inputDate = new string[] { "xx", "1", "29" };
        var mockJulianDayApi = new Mock<IJulianDayApi>();
        var mockCalcDateTimeApi = new Mock<ICalcDateTimeApi>();
        DateConversions conversions = new(mockCalcDateTimeApi.Object, mockJulianDayApi.Object);
        Assert.Throws<ArgumentException>(() => conversions.InputDateToDecimals(inputDate));
    }

}

