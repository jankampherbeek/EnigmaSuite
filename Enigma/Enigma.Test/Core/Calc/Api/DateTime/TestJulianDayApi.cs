// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.DateTime.JulDay;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.DateTime;
using Moq;

namespace Enigma.Test.Core.Calc.Api.DateTime;

[TestFixture]
public class TestJulianDayApi
{
    private readonly JulianDayRequest _jdRequest = new(new SimpleDateTime(2022, 4, 20, 19.25, Calendars.Gregorian));
    private readonly JulianDayResponse _jdResponse = new(123456.789, 123456.790, 0.000345, true, "");


    [Test]
    public void TestHappyFlow()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerMock());
        JulianDayResponse actualResponse = api.getJulianDay(_jdRequest);
        Assert.That(_jdResponse, Is.EqualTo(actualResponse));
    }

    public void TestRequestNull()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerMock());
        JulianDayRequest? errorRequest = null;
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.That(() => api.getJulianDay(errorRequest), Throws.TypeOf<ArgumentNullException>());
#pragma warning restore CS8604 // Possible null reference argument.
    }

    public void TestRequestDateTimeNull()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerMock());
        JulianDayRequest errorRequest = new(null);
        Assert.That(() => api.getJulianDay(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }


    private IJulDayHandler CreateHandlerMock()
    {
        var handlerMock = new Mock<IJulDayHandler>();
        handlerMock.Setup(p => p.CalcJulDay(_jdRequest)).Returns(_jdResponse);
        return handlerMock.Object;
    }

}

