// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.DateTime.DateTimeFromJd;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.DateTime;
using Moq;

namespace Enigma.Test.Core.App.Api.DateTime;

[TestFixture]
public class TestCalcDateTimeApi
{
    private readonly DateTimeRequest _dateTimeRequest = new(123456.789, true, Calendars.Gregorian);
    private readonly DateTimeResponse _dateTimeResponse = new(new SimpleDateTime(2022, 4, 20, 19.6, Calendars.Gregorian), true, "");

    [Test]
    public void TestHappyFlow()
    {
        ICalcDateTimeApi api = new CalcDateTimeApi(CreateHandlerMock());
        DateTimeResponse actualResponse = api.getDateTime(_dateTimeRequest);
        Assert.That(_dateTimeResponse, Is.EqualTo(actualResponse));
    }

    [Test]
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