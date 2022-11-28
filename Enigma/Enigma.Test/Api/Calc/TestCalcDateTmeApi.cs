// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Api.Calc;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.RequestResponse;
using Moq;
using Enigma.Core.Handlers.Interfaces;

namespace Enigma.Test.Api.Astron;

[TestFixture]
public class TestCalcDateTimeApi
{
    private readonly DateTimeRequest _dateTimeRequest = new(123456.789, true, Calendars.Gregorian);
    private readonly DateTimeResponse _dateTimeResponse = new(new SimpleDateTime(2022, 4, 20, 19.6, Calendars.Gregorian), true, "");

    [Test]
    public void TestHappyFlow()
    {
        ICalcDateTimeApi api = new CalcDateTimeApi(CreateHandlerMock());
        DateTimeResponse actualResponse = api.GetDateTime(_dateTimeRequest);
        Assert.That(_dateTimeResponse, Is.EqualTo(actualResponse));
    }

    [Test]
    public void TestRequestNull()
    {
        ICalcDateTimeApi api = new CalcDateTimeApi(CreateHandlerMock());
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.That(() => api.GetDateTime(null), Throws.TypeOf<ArgumentNullException>());
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }


    private IDateTimeHandler CreateHandlerMock()
    {
        var handlerMock = new Mock<IDateTimeHandler>();
        handlerMock.Setup(p => p.CalcDateTime(_dateTimeRequest)).Returns(_dateTimeResponse);
        return handlerMock.Object;
    }

}