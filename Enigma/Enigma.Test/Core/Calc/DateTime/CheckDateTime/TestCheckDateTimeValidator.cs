// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Calc.DateTime;
using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Facades.Interfaces;
using Moq;


namespace Enigma.Test.Core.Calc.DateTime.CheckDateTime;

[TestFixture]
public class TestCheckDateTimeValidator
{

    [Test]
    public void TestValidDate()
    {
        Calendars calendar = Calendars.Gregorian;
        SimpleDateTime dateTime = new(2000, 1, 1, 12.0, calendar);
        var facadeMock = new Mock<IDateConversionFacade>();
        facadeMock.Setup(p => p.DateTimeIsValid(dateTime)).Returns(true);
        ICheckDateTimeValidator validator = new CheckDateTimeValidator(facadeMock.Object);
        Assert.That(validator.ValidateDateTime(dateTime), Is.True);
    }

    [Test]
    public void TestInValidDate()
    {
        Calendars calendar = Calendars.Gregorian;
        SimpleDateTime dateTime = new(2000, 13, 1, 12.0, calendar);
        var facadeMock = new Mock<IDateConversionFacade>();
        facadeMock.Setup(p => p.DateTimeIsValid(dateTime)).Returns(false);
        ICheckDateTimeValidator validator = new CheckDateTimeValidator(facadeMock.Object);
        Assert.That(validator.ValidateDateTime(dateTime), Is.False);
    }



}