// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace E4CTest.core.calendarandclock.julday;

using E4C.Core.CalendarAndClock.JulDay;
using E4C.Core.Shared.Domain;
using E4C.Exceptions;
using E4C.Shared.References;
using E4C.Shared.ReqResp;
using Moq;
using NUnit.Framework;

[TestFixture]
public class TestJulDayHandler
{
    private readonly double _expectedJd = 123456.789;
    private SimpleDateTime _dateTime;
    private readonly double _delta = 0.00000001;
    private readonly string _errorText = "Description of problem.";
    private readonly bool _useJdForUt = true;

    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, Calendars.Gregorian);
    }


    [Test]
    public void TestHappyFlow()
    {
        Mock<IJulDayCalc> calcMock = CreateCalcMock(); 
        IJulDayHandler handler = new JulDayHandler(calcMock.Object);
        JulianDayRequest request = new JulianDayRequest(_dateTime, _useJdForUt);
        JulianDayResponse response = handler.CalcJulDay(request);
        Assert.AreEqual(_expectedJd, response.JulDay, _delta);
        Assert.IsTrue(response.Success);
        Assert.AreEqual("", response.ErrorText);
    }

    [Test]
    public void TextSeException()
    {
        Mock<IJulDayCalc> calcExceptionMock = CreateCalcMockThrowingException();
        IJulDayHandler handler = new JulDayHandler(calcExceptionMock.Object);
        JulianDayRequest request = new JulianDayRequest(_dateTime, _useJdForUt);
        JulianDayResponse response = handler.CalcJulDay(request);
        Assert.IsFalse(response.Success);
        Assert.AreEqual(_errorText, response.ErrorText);
    }


    private Mock<IJulDayCalc> CreateCalcMock()
    {
        var mock = new Mock<IJulDayCalc>();
        mock.Setup(p => p.CalcJulDay(_dateTime)).Returns(_expectedJd);
        return mock;
    }

    private Mock<IJulDayCalc> CreateCalcMockThrowingException()
    {
        var mock = new Mock<IJulDayCalc>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.CalcJulDay(_dateTime)).Throws(exception);
        return mock;
    }

}