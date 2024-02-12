// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Frontend.Ui.Support.Parsers;
using Enigma.Frontend.Ui.Support.Validations;
using Moq;

namespace Enigma.Test.Frontend.Ui.Support.InputParsers;

[TestFixture]

public class TestTimeInputParser
{

    private const char SEPARATOR = ':';


    [Test]
    public void HappyFlow()
    {
        const bool dst = false;
        const string timeInput = "10:12:00";
        int[] timeValues = { 10, 12, 0 };
        const double lmtOffset = 0.0;
        const TimeZones timeZone = TimeZones.Ut;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (timeValues, true);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(timeInput, SEPARATOR)).Returns(rangeResult);
        var mockTimeValidator = new Mock<ITimeValidator>();
        FullTime? fullTime;
        mockTimeValidator.Setup(x => x.CreateCheckedTime(timeValues, timeZone, lmtOffset, dst, out fullTime)).Returns(true);
        ITimeInputParser parser = new TimeInputParser(mockValueRangeConverter.Object, mockTimeValidator.Object);

        Assert.That(parser.HandleTime(timeInput, timeZone, lmtOffset, dst, out _), Is.True);
    }

    [Test]
    public void NoSeconds()
    {
        const bool dst = false;
        const string timeInput = "10:12";
        int[] timeValues = { 10, 12 };
        const double lmtOffset = 0.0;
        const TimeZones timeZone = TimeZones.Ut;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (timeValues, true);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(timeInput, SEPARATOR)).Returns(rangeResult);
        var mockTimeValidator = new Mock<ITimeValidator>();
        FullTime? fullTime;
        mockTimeValidator.Setup(x => x.CreateCheckedTime(timeValues, timeZone, lmtOffset, dst, out fullTime)).Returns(true);
        ITimeInputParser parser = new TimeInputParser(mockValueRangeConverter.Object, mockTimeValidator.Object);

        Assert.That(parser.HandleTime(timeInput, timeZone, lmtOffset, dst, out fullTime), Is.True);
    }


    [Test]
    public void SyntaxError()
    {
        const bool dst = false;
        const string timeInput = "10:xy:00";
        int[] timeValues = Array.Empty<int>();
        const double lmtOffset = 0.0;
        const TimeZones timeZone = TimeZones.Ut;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (timeValues, false);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(timeInput, SEPARATOR)).Returns(rangeResult);
        var mockTimeValidator = new Mock<ITimeValidator>();
        ITimeInputParser parser = new TimeInputParser(mockValueRangeConverter.Object, mockTimeValidator.Object);

        Assert.That(parser.HandleTime(timeInput, timeZone, lmtOffset, dst, out FullTime? _), Is.False);
    }

    [Test]
    public void TimeError()
    {
        const bool dst = false;
        const string timeInput = "10:72:00";
        int[] timeValues = { 10, 72, 0 };
        const double lmtOffset = 0.0;
        const TimeZones timeZone = TimeZones.Ut;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (timeValues, true);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(timeInput, SEPARATOR)).Returns(rangeResult);
        var mockTimeValidator = new Mock<ITimeValidator>();
        FullTime? fullTime;
        mockTimeValidator.Setup(x => x.CreateCheckedTime(timeValues, timeZone, lmtOffset, dst, out fullTime)).Returns(false);
        ITimeInputParser parser = new TimeInputParser(mockValueRangeConverter.Object, mockTimeValidator.Object);

        Assert.That(parser.HandleTime(timeInput, timeZone, lmtOffset, dst, out fullTime), Is.False);
    }

}
