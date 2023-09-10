// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.References;
using Enigma.Frontend.Helpers.InputParsers;
using Enigma.Frontend.Helpers.Interfaces;
using Moq;

namespace Enigma.Test.Frontend.Helpers.InputParsers;

[TestFixture]

public class TestDateInputParser
{
    private const char SEPARATOR = '/';
    private const Calendars CAL = Calendars.Gregorian;
    private const YearCounts YEAR_COUNT = YearCounts.CE;


    [Test]
    public void HappyFlow()
    {
        const string dateInput = "2022/6/8";
        int[] dateValues = { 2022, 6, 8 };
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (dateValues, true);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(dateInput, SEPARATOR)).Returns(rangeResult);
        var mockDateValidator = new Mock<IDateValidator>();
        FullDate? fullDate;
        mockDateValidator.Setup(x => x.CreateCheckedDate(dateValues, CAL, YEAR_COUNT, out fullDate)).Returns(true);
        var parser = new DateInputParser(mockValueRangeConverter.Object, mockDateValidator.Object);

        Assert.That(parser.HandleDate(dateInput, CAL, YEAR_COUNT, out fullDate), Is.True);
    }

    [Test]
    public void SyntaxError()
    {
        const string dateInput = "2022/a/8";
        int[] dateValues = Array.Empty<int>();
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (dateValues, false);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(dateInput, SEPARATOR)).Returns(rangeResult);
        var mockDateValidator = new Mock<IDateValidator>();
        var parser = new DateInputParser(mockValueRangeConverter.Object, mockDateValidator.Object);

        Assert.That(parser.HandleDate(dateInput, CAL, YEAR_COUNT, out FullDate? _), Is.False);
    }

    [Test]
    public void DateError()
    {
        const string dateInput = "2022/13/8";
        int[] dateValues = { 2022, 13, 8 };
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (dateValues, true);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(dateInput, SEPARATOR)).Returns(rangeResult);
        var mockDateValidator = new Mock<IDateValidator>();
        FullDate? fullDate;
        mockDateValidator.Setup(x => x.CreateCheckedDate(dateValues, CAL, YEAR_COUNT, out fullDate)).Returns(false);
        var parser = new DateInputParser(mockValueRangeConverter.Object, mockDateValidator.Object);

        Assert.That(parser.HandleDate(dateInput, CAL, YEAR_COUNT, out fullDate), Is.False);
    }

}
