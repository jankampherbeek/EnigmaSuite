// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
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

public class TestGeoLatInputParser
{
    private const char SEPARATOR = ':';


    [Test]
    public void HappyFlow()
    {
        const string geoLatInput = "52:13:00";
        int[] geoLatValues = { 52, 13, 0 };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, true);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLatInput, SEPARATOR)).Returns(rangeResult);
        var mockGeoLatValidator = new Mock<IGeoLatValidator>();
        FullGeoLatitude? fullGeoLatitude;
        mockGeoLatValidator.Setup(x => x.CreateCheckedLatitude(geoLatValues, direction, out fullGeoLatitude)).Returns(true);
        IGeoLatInputParser parser = new GeoLatInputParser(mockValueRangeConverter.Object, mockGeoLatValidator.Object);

        Assert.That(parser.HandleGeoLat(geoLatInput, direction, out fullGeoLatitude), Is.True);
    }

    [Test]
    public void NoSeconds()
    {
        const string geoLatInput = "52:13";
        int[] geoLatValues = { 52, 13 };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, true);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLatInput, SEPARATOR)).Returns(rangeResult);
        var mockGeoLatValidator = new Mock<IGeoLatValidator>();
        FullGeoLatitude? fullGeoLatitude;
        mockGeoLatValidator.Setup(x => x.CreateCheckedLatitude(geoLatValues, direction, out fullGeoLatitude)).Returns(true);
        IGeoLatInputParser parser = new GeoLatInputParser(mockValueRangeConverter.Object, mockGeoLatValidator.Object);

        Assert.That(parser.HandleGeoLat(geoLatInput, direction, out fullGeoLatitude), Is.True);
    }


    [Test]
    public void SyntaxError()
    {
        const string geoLatInput = "5q:13:00";
        int[] geoLatValues = Array.Empty<int>();
        const Directions4GeoLat direction = Directions4GeoLat.North;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, false);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLatInput, SEPARATOR)).Returns(rangeResult);
        var mockGeoLatValidator = new Mock<IGeoLatValidator>();
        IGeoLatInputParser parser = new GeoLatInputParser(mockValueRangeConverter.Object, mockGeoLatValidator.Object);

        Assert.That(parser.HandleGeoLat(geoLatInput, direction, out FullGeoLatitude? _), Is.False);
    }

    [Test]
    public void LongitudeError()
    {
        const string geoLatInput = "90:00:00";
        int[] geoLatValues = { 90, 0, 0 };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        var mockValueRangeConverter = new Mock<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, true);
        mockValueRangeConverter.Setup(x => x.ConvertStringRangeToIntRange(geoLatInput, SEPARATOR)).Returns(rangeResult);
        var mockGeoLatValidator = new Mock<IGeoLatValidator>();
        FullGeoLatitude? fullGeoLatitude;
        mockGeoLatValidator.Setup(x => x.CreateCheckedLatitude(geoLatValues, direction, out fullGeoLatitude)).Returns(false);
        IGeoLatInputParser parser = new GeoLatInputParser(mockValueRangeConverter.Object, mockGeoLatValidator.Object);

        Assert.That(parser.HandleGeoLat(geoLatInput, direction, out fullGeoLatitude), Is.False);
    }

}
