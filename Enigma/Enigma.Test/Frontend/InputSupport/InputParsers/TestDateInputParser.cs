// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.DateTime;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.InputSupport.InputParsers;
using Enigma.Frontend.InputSupport.Validations;
using Moq;

namespace Enigma.Test.Frontend.InputSupport.InputParsers;

[TestFixture]

public class TestDateInputParser
{
    private IDateInputParser _parser;
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
        _parser = new DateInputParser(_mockValueRangeConverter.Object, _mockDateValidator.Object);
        
        Assert.IsTrue(_parser.HandleGeoLong(dateInput, cal, yearCount, out fullDate));
    }

    [Test]
    public void SyntaxError()
    {
        string dateInput = "2022/a/8";
        int[] dateValues = new int[] { };
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (dateValues, false);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(dateInput, _separator)).Returns(rangeResult);
        var _mockDateValidator = new Mock<IDateValidator>();
        FullDate? fullDate;
        _parser = new DateInputParser(_mockValueRangeConverter.Object, _mockDateValidator.Object);

        Assert.IsFalse(_parser.HandleGeoLong(dateInput, cal, yearCount, out fullDate));
    }

    [Test]
    public void DateError()
    {
        string dateInput = "2022/13/8";
        int[] dateValues = new int[] {2022, 13, 8 };
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (dateValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(dateInput, _separator)).Returns(rangeResult);
        var _mockDateValidator = new Mock<IDateValidator>();
        FullDate? fullDate;
        _mockDateValidator.Setup(x => x.CreateCheckedDate(dateValues, cal, yearCount, out fullDate)).Returns(false);
        _parser = new DateInputParser(_mockValueRangeConverter.Object, _mockDateValidator.Object);

        Assert.IsFalse(_parser.HandleGeoLong(dateInput, cal, yearCount, out fullDate));
    }

}
