// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api;
using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Moq;

namespace Enigma.Test.Api;

[TestFixture]
public class TestCheckDateTimeApi
{
    private SimpleDateTime? _simpleDateTime;
    private SimpleDateTime? _simpleDateTimeError;

    [SetUp]
    public void SetUp()
    {
        _simpleDateTime = new SimpleDateTime(2022, 4, 20, 19.6, Calendars.Gregorian);
        _simpleDateTimeError = new SimpleDateTime(2022, 44, 20, 19.6, Calendars.Gregorian);
    }


    [Test]
    public void TestHappyFlow()
    {
        IDateTimeApi api = new DateTimeApi(CreateHandlerMock());
        Assert.That(api.CheckDateTime(_simpleDateTime!), Is.True);
    }

    [Test]
    public void TestInvalidDate()
    {
        IDateTimeApi api = new DateTimeApi(CreateHandlerMockError());
        Assert.That(api.CheckDateTime(_simpleDateTimeError!), Is.False);
    }

    [Test]
    public void TestRequestNull()
    {
        IDateTimeApi api = new DateTimeApi(CreateHandlerMock());
        SimpleDateTime? sdt = null;
        Assert.That(() => api.CheckDateTime(sdt!), Throws.TypeOf<ArgumentNullException>());
    }

    private IDateTimeHandler CreateHandlerMock()
    {
        var handlerMock = new Mock<IDateTimeHandler>();
        handlerMock.Setup(p => p.CheckDateTime(_simpleDateTime!)).Returns(true);
        return handlerMock.Object;
    }

    private IDateTimeHandler CreateHandlerMockError()
    {
        var handlerMock = new Mock<IDateTimeHandler>();
        handlerMock.Setup(p => p.CheckDateTime(_simpleDateTimeError!)).Returns(false);
        return handlerMock.Object;
    }

}