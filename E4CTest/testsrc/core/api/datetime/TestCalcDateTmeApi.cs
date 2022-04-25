// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Api.Datetime;
using E4C.Core.CalendarAndClock.DateTime;
using E4C.Core.Shared.Domain;
using E4C.Shared.References;
using E4C.Shared.ReqResp;
using Moq;
using NUnit.Framework;
using System;

namespace E4CTest.core.api.datetime;

[TestFixture]
public class TestCalcDateTimeApi
{
    private readonly DateTimeRequest _dateTimeRequest = new DateTimeRequest(123456.789, true, Calendars.Gregorian);
    private readonly DateTimeResponse _dateTimeResponse = new DateTimeResponse(new SimpleDateTime(2022, 4, 20, 19.6, Calendars.Gregorian), true, "");

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
        Assert.That(() => api.getDateTime(null), Throws.TypeOf<ArgumentNullException>());
    }


    private IDateTimeHandler CreateHandlerMock()
    {
        var handlerMock = new Mock<IDateTimeHandler>();
        handlerMock.Setup(p => p.CalcDateTime(_dateTimeRequest)).Returns(_dateTimeResponse);
        return handlerMock.Object;
    }

}