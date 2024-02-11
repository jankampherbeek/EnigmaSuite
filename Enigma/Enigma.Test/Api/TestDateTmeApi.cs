// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Moq;

namespace Enigma.Test.Api;

[TestFixture]
public class TestDateTimeApi
{
    private readonly DateTimeRequest _dateTimeRequest = new(123456.789, true, Calendars.Gregorian);
    private readonly DateTimeResponse _dateTimeResponse = new(new SimpleDateTime(2022, 4, 20, 19.6, Calendars.Gregorian), true, "");

    [Test]
    public void TestHappyFlow()
    {
        IDateTimeApi api = new DateTimeApi(CreateHandlerMock());
        DateTimeResponse actualResponse = api.GetDateTime(_dateTimeRequest);
        Assert.That(_dateTimeResponse, Is.EqualTo(actualResponse));
    }

    [Test]
    public void TestRequestNull()
    {
        DateTimeRequest? request = null;
        IDateTimeApi api = new DateTimeApi(CreateHandlerMock());
        Assert.That(() => api.GetDateTime(request!), Throws.TypeOf<ArgumentNullException>());
    }


    private IDateTimeHandler CreateHandlerMock()
    {
        var handlerMock = new Mock<IDateTimeHandler>();
        handlerMock.Setup(p => p.CalcDateTime(_dateTimeRequest)).Returns(_dateTimeResponse);
        return handlerMock.Object;
    }

}