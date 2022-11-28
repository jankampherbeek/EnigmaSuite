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

public class TestGeoLatInputParser
{
    private readonly char _separator = ':';



    [Test]
    public void HappyFlow()
    {
        string geoLatInput = "52:13:00";
        int[] geoLatValues = new int[] { 52, 13, 0 };
        Directions4GeoLat direction = Directions4GeoLat.North;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLatInput, _separator)).Returns(rangeResult);
        var _mockGeoLatValidator = new Mock<IGeoLatValidator>();
        FullGeoLatitude? fullGeoLatitude;
        _mockGeoLatValidator.Setup(x => x.CreateCheckedLatitude(geoLatValues, direction, out fullGeoLatitude)).Returns(true);
        IGeoLatInputParser _parser = new GeoLatInputParser(_mockValueRangeConverter.Object, _mockGeoLatValidator.Object);

        Assert.That(_parser.HandleGeoLat(geoLatInput, direction, out fullGeoLatitude), Is.True);
    }

    [Test]
    public void NoSeconds()
    {
        string geoLatInput = "52:13";
        int[] geoLatValues = new int[] { 52, 13 };
        Directions4GeoLat direction = Directions4GeoLat.North;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLatInput, _separator)).Returns(rangeResult);
        var _mockGeoLatValidator = new Mock<IGeoLatValidator>();
        FullGeoLatitude? fullGeoLatitude;
        _mockGeoLatValidator.Setup(x => x.CreateCheckedLatitude(geoLatValues, direction, out fullGeoLatitude)).Returns(true);
        IGeoLatInputParser _parser = new GeoLatInputParser(_mockValueRangeConverter.Object, _mockGeoLatValidator.Object);

        Assert.That(_parser.HandleGeoLat(geoLatInput, direction, out fullGeoLatitude), Is.True);
    }


    [Test]
    public void SyntaxError()
    {
        string geoLatInput = "5q:13:00";
        int[] geoLatValues = Array.Empty<int>();
        Directions4GeoLat direction = Directions4GeoLat.North;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, false);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLatInput, _separator)).Returns(rangeResult);
        var _mockGeoLatValidator = new Mock<IGeoLatValidator>();
        IGeoLatInputParser _parser = new GeoLatInputParser(_mockValueRangeConverter.Object, _mockGeoLatValidator.Object);

        Assert.That(_parser.HandleGeoLat(geoLatInput, direction, out FullGeoLatitude? fullGeoLatitude), Is.False);
    }

    [Test]
    public void LongitudeError()
    {
        string geoLatInput = "90:00:00";
        int[] geoLatValues = new int[] { 90, 0, 0 };
        Directions4GeoLat direction = Directions4GeoLat.North;
        var _mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, true);
        _mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLatInput, _separator)).Returns(rangeResult);
        var _mockGeoLatValidator = new Mock<IGeoLatValidator>();
        FullGeoLatitude? fullGeoLatitude;
        _mockGeoLatValidator.Setup(x => x.CreateCheckedLatitude(geoLatValues, direction, out fullGeoLatitude)).Returns(false);
        IGeoLatInputParser _parser = new GeoLatInputParser(_mockValueRangeConverter.Object, _mockGeoLatValidator.Object);

        Assert.That(_parser.HandleGeoLat(geoLatInput, direction, out fullGeoLatitude), Is.False);
    }

}
