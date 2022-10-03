// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace E4CTest.core.calendarandclock.julday;

using Enigma.Core.Calc.DateTime.JulDay;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.DateTime;
using Enigma.Domain.Exceptions;
using Moq;


[TestFixture]
public class TestJulDayHandler
{
    private readonly double _expectedJd = 123456.789;
    private SimpleDateTime _dateTime;
    private readonly double _delta = 0.00000001;
    private readonly string _errorText = "Description of problem.";

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
        JulianDayRequest request = new JulianDayRequest(_dateTime);
        JulianDayResponse response = handler.CalcJulDay(request);
        Assert.That(response.JulDayUt, Is.EqualTo(_expectedJd).Within(_delta));
        Assert.That(response.Success);
        Assert.That(response.ErrorText, Is.EqualTo(""));
    }

    [Test]
    public void TextSeException()
    {
        Mock<IJulDayCalc> calcExceptionMock = CreateCalcMockThrowingException();
        IJulDayHandler handler = new JulDayHandler(calcExceptionMock.Object);
        JulianDayRequest request = new JulianDayRequest(_dateTime);
        JulianDayResponse response = handler.CalcJulDay(request);
        Assert.That(!response.Success);
        Assert.That(response.ErrorText, Is.EqualTo(_errorText));
    }


    private Mock<IJulDayCalc> CreateCalcMock()
    {
        var mock = new Mock<IJulDayCalc>();
        mock.Setup(p => p.CalcJulDayUt(_dateTime)).Returns(_expectedJd);
        return mock;
    }

    private Mock<IJulDayCalc> CreateCalcMockThrowingException()
    {
        var mock = new Mock<IJulDayCalc>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.CalcJulDayUt(_dateTime)).Throws(exception);
        return mock;
    }

}