// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Calc;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.RequestResponse;
using Moq;

namespace Enigma.Test.Api.Astron;

[TestFixture]
public class TestJulianDayApi
{
    private readonly JulianDayRequest _jdRequest = new(new SimpleDateTime(2022, 4, 20, 19.25, Calendars.Gregorian));
    private readonly JulianDayResponse _jdResponse = new(123456.789, 123456.790, 0.000345, true, "");


    [Test]
    public void TestHappyFlow()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerMock());
        JulianDayResponse actualResponse = api.GetJulianDay(_jdRequest);
        Assert.That(_jdResponse, Is.EqualTo(actualResponse));
    }

    [Test]
    public void TestRequestDateTimeNull()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerMock());
        JulianDayRequest? errorRequest = null;
        Assert.That(() => api.GetJulianDay(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }


    private IJulDayHandler CreateHandlerMock()
    {
        var handlerMock = new Mock<IJulDayHandler>();
        handlerMock.Setup(p => p.CalcJulDay(_jdRequest)).Returns(_jdResponse);
        return handlerMock.Object;
    }

}

