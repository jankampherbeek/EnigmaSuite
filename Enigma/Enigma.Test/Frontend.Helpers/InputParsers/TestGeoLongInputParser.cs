// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.References;
using Enigma.Frontend.Helpers.InputParsers;
using Enigma.Frontend.Helpers.Interfaces;
using Moq;

namespace Enigma.Test.Frontend.Helpers.InputParsers;

[TestFixture]

public class TestGeoLongInputParser
{
    private const char SEPARATOR = ':';



    [Test]
    public void HappyFlow()
    {
        const string geoLongInput = "123:45:00";
        int[] geoLongValues = { 123, 45, 0 };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, true);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLongInput, SEPARATOR)).Returns(rangeResult);
        var mockGeoLongValidator = new Mock<IGeoLongValidator>();
        FullGeoLongitude? fullGeoLongitude;
        mockGeoLongValidator.Setup(x => x.CreateCheckedLongitude(geoLongValues, direction, out fullGeoLongitude)).Returns(true);
        IGeoLongInputParser parser = new GeoLongInputParser(mockValueRangeConverter.Object, mockGeoLongValidator.Object);

        Assert.That(parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude), Is.True);
    }

    [Test]
    public void NoSeconds()
    {
        const string geoLongInput = "123:45";
        int[] geoLongValues = { 123, 45 };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, true);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLongInput, SEPARATOR)).Returns(rangeResult);
        var mockGeoLongValidator = new Mock<IGeoLongValidator>();
        FullGeoLongitude? fullGeoLongitude;
        mockGeoLongValidator.Setup(x => x.CreateCheckedLongitude(geoLongValues, direction, out fullGeoLongitude)).Returns(true);
        IGeoLongInputParser parser = new GeoLongInputParser(mockValueRangeConverter.Object, mockGeoLongValidator.Object);

        Assert.That(parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude), Is.True);
    }


    [Test]
    public void SyntaxError()
    {
        const string geoLongInput = "123:xw:00";
        int[] geoLongValues = Array.Empty<int>();
        const Directions4GeoLong direction = Directions4GeoLong.East;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, false);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLongInput, SEPARATOR)).Returns(rangeResult);
        var mockGeoLongValidator = new Mock<IGeoLongValidator>();
        IGeoLongInputParser parser = new GeoLongInputParser(mockValueRangeConverter.Object, mockGeoLongValidator.Object);

        Assert.That(parser.HandleGeoLong(geoLongInput, direction, out FullGeoLongitude? _), Is.False);
    }

    [Test]
    public void LongitudeError()
    {
        const string geoLongInput = "123:75:00";
        int[] geoLongValues = { 123, 75, 0 };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, true);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLongInput, SEPARATOR)).Returns(rangeResult);
        var mockGeoLongValidator = new Mock<IGeoLongValidator>();
        FullGeoLongitude? fullGeoLongitude;
        mockGeoLongValidator.Setup(x => x.CreateCheckedLongitude(geoLongValues, direction, out fullGeoLongitude)).Returns(false);
        IGeoLongInputParser parser = new GeoLongInputParser(mockValueRangeConverter.Object, mockGeoLongValidator.Object);

        Assert.That(parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude), Is.False);
    }

}
