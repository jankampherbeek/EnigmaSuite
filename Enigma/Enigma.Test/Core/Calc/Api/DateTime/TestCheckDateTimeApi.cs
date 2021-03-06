// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.DateTime.CheckDateTime;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.DateTime;
using Moq;

namespace Enigma.Test.Core.App.Api.DateTime;

[TestFixture]
public class TestCheckDateTimeApi
{
    private CheckDateTimeRequest _checkDateTimeRequest;
    private CheckDateTimeRequest _checkDateTimeRequestError;
    private CheckDateTimeResponse _checkDateTimeResponse;
    private CheckDateTimeResponse _checkDateTimeResponseError;
    private SimpleDateTime _simpleDateTime;
    private SimpleDateTime _simpleDateTimeError;

    [SetUp]
    public void SetUp()
    {
        _simpleDateTime = new SimpleDateTime(2022, 4, 20, 19.6, Calendars.Gregorian);
        _simpleDateTimeError = new SimpleDateTime(2022, 44, 20, 19.6, Calendars.Gregorian);
        _checkDateTimeRequest = new CheckDateTimeRequest(_simpleDateTime);
        _checkDateTimeRequestError = new CheckDateTimeRequest(_simpleDateTimeError);
        _checkDateTimeResponse = new CheckDateTimeResponse(true, true, "");
        _checkDateTimeResponseError = new CheckDateTimeResponse(false, true, "");
    }

    [Test]
    public void TestHappyFlow()
    {
        ICheckDateTimeApi api = new CheckDateTimeApi(CreateHandlerMock());
        CheckDateTimeResponse actualResponse = api.CheckDateTime(_checkDateTimeRequest);
        Assert.That(_checkDateTimeResponse, Is.EqualTo(actualResponse));
    }

    public void TestInvalidDate()
    {
        ICheckDateTimeApi api = new CheckDateTimeApi(CreateHandlerMockError());
        CheckDateTimeResponse actualResponse = api.CheckDateTime(_checkDateTimeRequestError);
        Assert.That(_checkDateTimeResponseError, Is.EqualTo(actualResponse));
    }

    public void TestRequestNull()
    {
        ICheckDateTimeApi api = new CheckDateTimeApi(CreateHandlerMock());
        CheckDateTimeRequest errorRequest = null;
        Assert.That(() => api.CheckDateTime(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }


    private ICheckDateTimeHandler CreateHandlerMock()
    {
        var handlerMock = new Mock<ICheckDateTimeHandler>();
        handlerMock.Setup(p => p.CheckDateTime(_checkDateTimeRequest)).Returns(_checkDateTimeResponse);
        return handlerMock.Object;
    }

    private ICheckDateTimeHandler CreateHandlerMockError()
    {
        var handlerMock = new Mock<ICheckDateTimeHandler>();
        handlerMock.Setup(p => p.CheckDateTime(_checkDateTimeRequestError)).Returns(_checkDateTimeResponseError);
        return handlerMock.Object;
    }

}