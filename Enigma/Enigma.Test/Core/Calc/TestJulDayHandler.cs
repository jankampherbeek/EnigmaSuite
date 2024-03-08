// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Responses;
using FakeItEasy;

namespace Enigma.Test.Core.Calc;

[TestFixture]
public class TestJulDayHandler
{
    private const double EXPECTED_JD = 123456.789;
    private SimpleDateTime? _dateTime;
    private const double DELTA = 0.00000001;


    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, Calendars.Gregorian);
    }


    [Test]
    public void TestHappyFlow()
    {
        IJulDayCalc calcFake = CreateCalcFake();
        IJulDayHandler handler = new JulDayHandler(calcFake);
        JulianDayResponse response = handler.CalcJulDay(_dateTime!);
        Assert.That(response.JulDayUt, Is.EqualTo(EXPECTED_JD).Within(DELTA));
    }

    private IJulDayCalc CreateCalcFake()
    {
        var fake = A.Fake<IJulDayCalc>();
        A.CallTo(() => fake.CalcJulDayUt(_dateTime!)).Returns(EXPECTED_JD);
        return fake;
    }

}