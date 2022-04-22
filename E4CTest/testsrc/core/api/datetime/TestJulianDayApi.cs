// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.api.datetime;
using E4C.Core.CalendarAndClock.JulDay;
using E4C.Core.Shared.Domain;
using E4C.Shared.References;
using E4C.Shared.ReqResp;
using Moq;
using NUnit.Framework;
using System;

namespace E4CTest.core.api.datetime;

[TestFixture]
public class TestJulianDayApi
{
    private double _julDay = 123456.789;
    private JulianDayRequest _jdRequest;
    private JulianDayResponse _jdResponse;


    [SetUp]
    public void SetUp()
    {
        _jdRequest = new JulianDayRequest(new SimpleDateTime(2022, 4, 20, 19.25, Calendars.Gregorian), true);
        _jdResponse = new JulianDayResponse(_julDay, true, "");
    }

    [Test]
    public void TestHappyFlow()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerMock());
        JulianDayResponse actualResponse = api.getJulianDay(_jdRequest);
        Assert.AreEqual(actualResponse, _jdResponse);
    }

    public void TestRequestNull()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerMock());
        JulianDayRequest errorRequest = null;
        Assert.That(() => api.getJulianDay(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }

    public void TestRequestDateTimeNull()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerMock());
        JulianDayRequest errorRequest = new JulianDayRequest(null, true);
        Assert.That(() => api.getJulianDay(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }


    private IJulDayHandler CreateHandlerMock()
    {
        var handlerMock= new Mock<IJulDayHandler>();
        handlerMock.Setup(p => p.CalcJulDay(_jdRequest)).Returns(_jdResponse);
        return handlerMock.Object;
    }

}

