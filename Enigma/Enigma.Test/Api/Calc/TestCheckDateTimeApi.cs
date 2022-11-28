// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Api.Calc;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.RequestResponse;
using Moq;

namespace Enigma.Test.Api.Astron;

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


    // TODO Urgent. Fix tests for DAteTimeApi (change into tewsts for handler?).
    /*
    [Test]
    public void TestHappyFlow()
    {
        IDateTimeApi api = new DateTimeApi(CreateApiMock());
        CheckDateTimeResponse actualResponse = api.CheckDateTime(_checkDateTimeRequest);
        Assert.That(_checkDateTimeResponse, Is.EqualTo(actualResponse));
    }

    [Test]
    public void TestInvalidDate()
    {
        IDateTimeApi api = new DateTimeApi(CreateHandlerMockError());
        CheckDateTimeResponse actualResponse = api.CheckDateTime(_checkDateTimeRequestError);
        Assert.That(_checkDateTimeResponseError, Is.EqualTo(actualResponse));
    }

    [Test]
    public void TestRequestNull()
    {
        IDateTimeApi api = new DateTimeApi(CreateApiMock());
        CheckDateTimeRequest? errorRequest = null;
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.That(() => api.CheckDateTime(errorRequest), Throws.TypeOf<ArgumentNullException>());
#pragma warning restore CS8604 // Possible null reference argument.
    }

    */
    private IDateTimeApi CreateApiMock()
    {
        var apiMock = new Mock<IDateTimeApi>();
        apiMock.Setup(p => p.CheckDateTime(_checkDateTimeRequest)).Returns(_checkDateTimeResponse);
        return apiMock.Object;
    }

    private IDateTimeApi CreateApiMockError()
    {
        var apiMock = new Mock<IDateTimeApi>();
        apiMock.Setup(p => p.CheckDateTime(_checkDateTimeRequestError)).Returns(_checkDateTimeResponseError);
        return apiMock.Object;
    }

}