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

public class TestTimeInputParser
{
    private ITimeInputParser _parser;
    private readonly char _separator = ':';


    [Test]
    public void HappyFlow()
    {
        string timeInput = "10:12:00";
        int[] timeValues = new int[] { 10, 12, 0 };
        double lmtOffset = 0.0;
        TimeZones timeZone = TimeZones.UT;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (timeValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(timeInput, _separator)).Returns(rangeResult);
        var _mockTimeValidator = new Mock<ITimeValidator>();
        FullTime? fullTime;
        _mockTimeValidator.Setup(x => x.CreateCheckedTime(timeValues, timeZone, lmtOffset, out fullTime)).Returns(true);
        _parser = new TimeInputParser(_mockValueRangeConverter.Object, _mockTimeValidator.Object);

        Assert.That(_parser.HandleTime(timeInput, timeZone, lmtOffset, out fullTime), Is.True);
    }

    [Test]
    public void NoSeconds()
    {
        string timeInput = "10:12";
        int[] timeValues = new int[] { 10, 12 };
        double lmtOffset = 0.0;
        TimeZones timeZone = TimeZones.UT;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (timeValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(timeInput, _separator)).Returns(rangeResult);
        var _mockTimeValidator = new Mock<ITimeValidator>();
        FullTime? fullTime;
        _mockTimeValidator.Setup(x => x.CreateCheckedTime(timeValues, timeZone, lmtOffset, out fullTime)).Returns(true);
        _parser = new TimeInputParser(_mockValueRangeConverter.Object, _mockTimeValidator.Object);

        Assert.That(_parser.HandleTime(timeInput, timeZone, lmtOffset, out fullTime), Is.True);
    }


    [Test]
    public void SyntaxError()
    {
        string timeInput = "10:xy:00";
        int[] timeValues = Array.Empty<int>();
        double lmtOffset = 0.0;
        TimeZones timeZone = TimeZones.UT;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (timeValues, false);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(timeInput, _separator)).Returns(rangeResult);
        var _mockTimeValidator = new Mock<ITimeValidator>();
        _parser = new TimeInputParser(_mockValueRangeConverter.Object, _mockTimeValidator.Object);

        Assert.That(_parser.HandleTime(timeInput, timeZone, lmtOffset, out FullTime? fullTime), Is.False);
    }

    [Test]
    public void TimeError()
    {
        string timeInput = "10:72:00";
        int[] timeValues = new int[] { 10, 72, 0 };
        double lmtOffset = 0.0;
        TimeZones timeZone = TimeZones.UT;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (timeValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(timeInput, _separator)).Returns(rangeResult);
        var _mockTimeValidator = new Mock<ITimeValidator>();
        FullTime? fullTime;
        _mockTimeValidator.Setup(x => x.CreateCheckedTime(timeValues, timeZone, lmtOffset, out fullTime)).Returns(false);
        _parser = new TimeInputParser(_mockValueRangeConverter.Object, _mockTimeValidator.Object);

        Assert.That(_parser.HandleTime(timeInput, timeZone, lmtOffset, out fullTime), Is.False);
    }

}
