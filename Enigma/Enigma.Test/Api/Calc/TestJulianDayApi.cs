// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Calc;
using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Responses;
using Moq;

namespace Enigma.Test.Api.Calc;

[TestFixture]
public class TestJulianDayApi
{
    private readonly SimpleDateTime _dateTime = new(2022, 4, 20, 19.25, Calendars.Gregorian);
    private readonly JulianDayResponse _jdResponse = new(123456.789, 123456.790, 0.000345);


    [Test]
    public void TestHappyFlow()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerMock());
        JulianDayResponse actualResponse = api.GetJulianDay(_dateTime);
        Assert.That(_jdResponse, Is.EqualTo(actualResponse));
    }

    [Test]
    public void TestRequestDateTimeNull()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerMock());
        SimpleDateTime? errorDateTime = null;
        Assert.That(() => api.GetJulianDay(errorDateTime!), Throws.TypeOf<ArgumentNullException>());
    }


    private IJulDayHandler CreateHandlerMock()
    {
        var handlerMock = new Mock<IJulDayHandler>();
        handlerMock.Setup(p => p.CalcJulDay(_dateTime)).Returns(_jdResponse);
        return handlerMock.Object;
    }

}

