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

public class TestGeoLongInputParser
{
    private readonly char _separator = ':';



    [Test]
    public void HappyFlow()
    {
        string geoLongInput = "123:45:00";
        int[] geoLongValues = new int[] { 123, 45, 0 };
        Directions4GeoLong direction = Directions4GeoLong.East;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLongInput, _separator)).Returns(rangeResult);
        var _mockGeoLongValidator = new Mock<IGeoLongValidator>();
        FullGeoLongitude? fullGeoLongitude;
        _mockGeoLongValidator.Setup(x => x.CreateCheckedLongitude(geoLongValues, direction, out fullGeoLongitude)).Returns(true);
        IGeoLongInputParser _parser = new GeoLongInputParser(_mockValueRangeConverter.Object, _mockGeoLongValidator.Object);

        Assert.That(_parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude), Is.True);
    }

    [Test]
    public void NoSeconds()
    {
        string geoLongInput = "123:45";
        int[] geoLongValues = new int[] { 123, 45 };
        Directions4GeoLong direction = Directions4GeoLong.East;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLongInput, _separator)).Returns(rangeResult);
        var _mockGeoLongValidator = new Mock<IGeoLongValidator>();
        FullGeoLongitude? fullGeoLongitude;
        _mockGeoLongValidator.Setup(x => x.CreateCheckedLongitude(geoLongValues, direction, out fullGeoLongitude)).Returns(true);
        IGeoLongInputParser _parser = new GeoLongInputParser(_mockValueRangeConverter.Object, _mockGeoLongValidator.Object);

        Assert.That(_parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude), Is.True);
    }


    [Test]
    public void SyntaxError()
    {
        string geoLongInput = "123:xw:00";
        int[] geoLongValues = Array.Empty<int>();
        Directions4GeoLong direction = Directions4GeoLong.East;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, false);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLongInput, _separator)).Returns(rangeResult);
        var _mockGeoLongValidator = new Mock<IGeoLongValidator>();
        IGeoLongInputParser _parser = new GeoLongInputParser(_mockValueRangeConverter.Object, _mockGeoLongValidator.Object);

        Assert.That(_parser.HandleGeoLong(geoLongInput, direction, out FullGeoLongitude? fullGeoLongitude), Is.False);
    }

    [Test]
    public void LongitudeError()
    {
        string geoLongInput = "123:75:00";
        int[] geoLongValues = new int[] { 123, 75, 0 };
        Directions4GeoLong direction = Directions4GeoLong.East;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLongInput, _separator)).Returns(rangeResult);
        var _mockGeoLongValidator = new Mock<IGeoLongValidator>();
        FullGeoLongitude? fullGeoLongitude;
        _mockGeoLongValidator.Setup(x => x.CreateCheckedLongitude(geoLongValues, direction, out fullGeoLongitude)).Returns(false);
        IGeoLongInputParser _parser = new GeoLongInputParser(_mockValueRangeConverter.Object, _mockGeoLongValidator.Object);

        Assert.That(_parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude), Is.False);
    }

}
