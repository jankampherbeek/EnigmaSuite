// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Locational;
using Enigma.InputSupport.Conversions;
using Enigma.InputSupport.InputParsers;
using Enigma.InputSupport.Validations;
using Moq;

namespace Enigma.Test.InputSupport.InputParsers;

[TestFixture]

public class TestGeoLongInputParser
{
    private IGeoLongInputParser _parser;
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
        _parser = new GeoLongInputParser(_mockValueRangeConverter.Object, _mockGeoLongValidator.Object);

        Assert.IsTrue(_parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude));
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
        _parser = new GeoLongInputParser(_mockValueRangeConverter.Object, _mockGeoLongValidator.Object);

        Assert.IsTrue(_parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude));
    }


    [Test]
    public void SyntaxError()
    {
        string geoLongInput = "123:xw:00";
        int[] geoLongValues = new int[] { };
        Directions4GeoLong direction = Directions4GeoLong.East;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, false);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLongInput, _separator)).Returns(rangeResult);
        var _mockGeoLongValidator = new Mock<IGeoLongValidator>();
        FullGeoLongitude? fullGeoLongitude;
        _parser = new GeoLongInputParser(_mockValueRangeConverter.Object, _mockGeoLongValidator.Object);

        Assert.IsFalse(_parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude));
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
        _parser = new GeoLongInputParser(_mockValueRangeConverter.Object, _mockGeoLongValidator.Object);

        Assert.IsFalse(_parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude));
    }

}
