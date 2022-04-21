// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.api.datetime;
using E4C.core.calendarandclock.datetime;
using E4C.core.calendarandclock.julday;
using E4C.core.shared.domain;
using E4C.shared.references;
using E4C.shared.reqresp;
using Moq;
using NUnit.Framework;
using System;

namespace E4CTest.core.api.datetime;

[TestFixture]
public class TestCalcDateTimeApi
{
    private double _julDay = 123456.789;
    private DateTimeRequest _dateTimeRequest;
    private DateTimeResponse _dateTimeResponse;
    private SimpleDateTime _simpleDateTime;


    [SetUp]
    public void SetUp()
    {
        _simpleDateTime = new SimpleDateTime(2022, 4, 20, 19.6, Calendars.Gregorian);
        _dateTimeRequest = new DateTimeRequest(_julDay, true, Calendars.Gregorian);
        _dateTimeResponse = new DateTimeResponse(_simpleDateTime, true, "");
    }

    [Test]
    public void TestHappyFlow()
    {
        ICalcDateTimeApi api = new CalcDateTimeApi(CreateHandlerMock());
        DateTimeResponse actualResponse = api.getDateTime(_dateTimeRequest);
        Assert.AreEqual(actualResponse, _dateTimeResponse);
    }

    public void TestRequestNull()
    {
        ICalcDateTimeApi api = new CalcDateTimeApi(CreateHandlerMock());
        DateTimeRequest errorRequest = null;
        Assert.That(() => api.getDateTime(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }


    private IDateTimeHandler CreateHandlerMock()
    {
        var handlerMock = new Mock<IDateTimeHandler>();
        handlerMock.Setup(p => p.CalcDateTime(_dateTimeRequest)).Returns(_dateTimeResponse);
        return handlerMock.Object;
    }

}