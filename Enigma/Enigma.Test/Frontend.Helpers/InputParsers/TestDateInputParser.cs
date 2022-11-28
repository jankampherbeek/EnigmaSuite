// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Frontend.Helpers.InputParsers;
using Enigma.Frontend.Helpers.Interfaces;
using Moq;

namespace Enigma.Test.Frontend.Helpers.InputParsers;

[TestFixture]

public class TestDateInputParser
{
    private readonly char _separator = '/';
    private readonly Calendars cal = Calendars.Gregorian;
    private readonly YearCounts yearCount = YearCounts.CE;


    [Test]
    public void HappyFlow()
    {
        string dateInput = "2022/6/8";
        int[] dateValues = new int[] { 2022, 6, 8 };
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (dateValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(dateInput, _separator)).Returns(rangeResult);
        var _mockDateValidator = new Mock<IDateValidator>();
        FullDate? fullDate;
        _mockDateValidator.Setup(x => x.CreateCheckedDate(dateValues, cal, yearCount, out fullDate)).Returns(true);
        var _parser = new DateInputParser(_mockValueRangeConverter.Object, _mockDateValidator.Object);

        Assert.That(_parser.HandleGeoLong(dateInput, cal, yearCount, out fullDate), Is.True);
    }

    [Test]
    public void SyntaxError()
    {
        string dateInput = "2022/a/8";
        int[] dateValues = Array.Empty<int>();
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (dateValues, false);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(dateInput, _separator)).Returns(rangeResult);
        var _mockDateValidator = new Mock<IDateValidator>();
        var _parser = new DateInputParser(_mockValueRangeConverter.Object, _mockDateValidator.Object);

        Assert.That(_parser.HandleGeoLong(dateInput, cal, yearCount, out FullDate? fullDate), Is.False);
    }

    [Test]
    public void DateError()
    {
        string dateInput = "2022/13/8";
        int[] dateValues = new int[] { 2022, 13, 8 };
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (dateValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(dateInput, _separator)).Returns(rangeResult);
        var _mockDateValidator = new Mock<IDateValidator>();
        FullDate? fullDate;
        _mockDateValidator.Setup(x => x.CreateCheckedDate(dateValues, cal, yearCount, out fullDate)).Returns(false);
        var _parser = new DateInputParser(_mockValueRangeConverter.Object, _mockDateValidator.Object);

        Assert.That(_parser.HandleGeoLong(dateInput, cal, yearCount, out fullDate), Is.False);
    }

}
