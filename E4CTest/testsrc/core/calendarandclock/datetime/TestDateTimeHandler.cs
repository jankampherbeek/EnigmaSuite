// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4CTest.core.calendarandclock.datetime;

using E4C.core.calendarandclock.datetime;
using E4C.core.shared.domain;
using E4C.exceptions;
using E4C.shared.references;
using E4C.shared.reqresp;
using Moq;
using NUnit.Framework;

[TestFixture]
public class TestDateTimeHandler
{
    private readonly Calendars _calendar = Calendars.Gregorian;
    private readonly double _jdUt = 123456.789;
    private readonly bool _useJdForUt = true;
    private SimpleDateTime _dateTime;
    private readonly string _errorText = "Description of problem.";


    [SetUp]
    public void SetUp()
    {
        _dateTime = new SimpleDateTime(2000, 1, 1, 12.0, _calendar);
    }


    [Test]
    public void TestHappyFlow()
    {
        Mock<IDateTimeCalc> calcMock = CreateCalcMock();
        IDateTimeHandler handler = new DateTimeHandler(calcMock.Object);
        DateTimeResponse response = handler.CalcDateTime(new DateTimeRequest(_jdUt, _useJdForUt, _calendar));
        Assert.AreEqual(_dateTime, response.DateTime);
        Assert.IsTrue(response.Success);
        Assert.AreEqual("", response.ErrorText);
    }

    [Test]
    public void TextSeException()
    {
        Mock<IDateTimeCalc> calcExceptionMock = CreateCalcMockThrowingException();
        IDateTimeHandler handler = new DateTimeHandler(calcExceptionMock.Object);
        DateTimeResponse response = handler.CalcDateTime(new DateTimeRequest(_jdUt, _useJdForUt, _calendar));
        Assert.IsFalse(response.Success);
        Assert.AreEqual(_errorText, response.ErrorText);
    }


    private Mock<IDateTimeCalc> CreateCalcMock()
    {
        var mock = new Mock<IDateTimeCalc>();
        mock.Setup(p => p.CalcDateTime(_jdUt, _calendar)).Returns(_dateTime);
        return mock;
    }

    private Mock<IDateTimeCalc> CreateCalcMockThrowingException()
    {
        var mock = new Mock<IDateTimeCalc>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.CalcDateTime(_jdUt, _calendar)).Throws(exception);
        return mock;
    }

}