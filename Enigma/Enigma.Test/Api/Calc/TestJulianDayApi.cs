// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Calc;
using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Responses;
using Enigma.Facades.Se;
using FakeItEasy;

namespace Enigma.Test.Api.Calc;

[TestFixture]
public class TestJulianDayApi
{
    private readonly SimpleDateTime _dateTime = new(2022, 4, 20, 19.25, Calendars.Gregorian);
    private readonly JulianDayResponse _jdResponse = new(123456.789, 123456.790, 0.000345);


    [Test]
    public void TestHappyFlow()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerFake(), CreateRevJulFacadeFake());
        var actualResponse = api.GetJulianDay(_dateTime);
        Assert.That(_jdResponse, Is.EqualTo(actualResponse));
    }

    [Test]
    public void TestRequestDateTimeNull()
    {
        IJulianDayApi api = new JulianDayApi(CreateHandlerFake(), CreateRevJulFacadeFake());
        SimpleDateTime? errorDateTime = null;
        Assert.That(() => api.GetJulianDay(errorDateTime!), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestDateTimeFromJd()
    {
        var julDay = 123456.789;
        var calendar = Calendars.Gregorian;
        var expectedDateTime = new SimpleDateTime(2022, 4, 20, 19.25, Calendars.Gregorian);
        
        IJulianDayApi api = new JulianDayApi(CreateHandlerFake(), CreateRevJulFacadeFake());
        var actualDateTime = api.DateTimeFromJd(julDay, calendar);
        Assert.That(expectedDateTime, Is.EqualTo(actualDateTime));
    }


    private IJulDayHandler CreateHandlerFake()
    {
        var handlerFake = A.Fake<IJulDayHandler>();
        A.CallTo(() => handlerFake.CalcJulDay(_dateTime)).Returns(_jdResponse);
        return handlerFake;
    }

    private IRevJulFacade CreateRevJulFacadeFake()
    {
        var facadeFake = A.Fake<IRevJulFacade>();
        var expectedDateTime = new SimpleDateTime(2022, 4, 20, 19.25, Calendars.Gregorian);
        A.CallTo(() => facadeFake.DateTimeFromJd(A<double>._, A<Calendars>._)).Returns(expectedDateTime);
        return facadeFake;
    }

}

