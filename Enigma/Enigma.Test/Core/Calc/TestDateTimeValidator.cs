// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;
using FakeItEasy;

namespace Enigma.Test.Core.Calc;

[TestFixture]
public class TestDateTimeValidator
{

    [Test]
    public void TestValidDate()
    {
        const Calendars calendar = Calendars.Gregorian;
        SimpleDateTime dateTime = new(2000, 1, 1, 12.0, calendar);
        var facadeFake = A.Fake<IDateConversionFacade>();
        A.CallTo(() => facadeFake.DateTimeIsValid(dateTime)).Returns(true);
        IDateTimeValidator validator = new DateTimeValidator(facadeFake);
        Assert.That(validator.ValidateDateTime(dateTime), Is.True);
    }

    [Test]
    public void TestInValidDate()
    {
        const Calendars calendar = Calendars.Gregorian;
        SimpleDateTime dateTime = new(2000, 13, 1, 12.0, calendar);
        var facadeFake = A.Fake<IDateConversionFacade>();
        A.CallTo(() => facadeFake.DateTimeIsValid(dateTime)).Returns(false);
        IDateTimeValidator validator = new DateTimeValidator(facadeFake);
        Assert.That(validator.ValidateDateTime(dateTime), Is.False);
    }
    
}