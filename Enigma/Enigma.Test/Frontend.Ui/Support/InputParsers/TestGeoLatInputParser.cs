// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Frontend.Ui.Support.Parsers;
using Enigma.Frontend.Ui.Support.Validations;
using FakeItEasy;

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
        var valueRangeConverterFake = A.Fake<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, true);
        A.CallTo(() => valueRangeConverterFake.ConvertStringRangeToIntRange(geoLatInput, SEPARATOR)).Returns(rangeResult);
        var geoLatValidatorFake = A.Fake<IGeoLatValidator>();
        FullGeoLatitude? fullGeoLatitude;
        A.CallTo(() => geoLatValidatorFake.CreateCheckedLatitude(geoLatValues, direction, out fullGeoLatitude)).Returns(true);
        IGeoLatInputParser parser = new GeoLatInputParser(valueRangeConverterFake, geoLatValidatorFake);

        Assert.That(parser.HandleGeoLat(geoLatInput, direction, out fullGeoLatitude), Is.True);
    }

    [Test]
    public void NoSeconds()
    {
        const string geoLatInput = "52:13";
        int[] geoLatValues = { 52, 13 };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        var valueRangeConverterFake = A.Fake<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, true);
        A.CallTo(() => valueRangeConverterFake.ConvertStringRangeToIntRange(geoLatInput, SEPARATOR)).Returns(rangeResult);
        var geoLatValidatorFake = A.Fake<IGeoLatValidator>();
        FullGeoLatitude? fullGeoLatitude;
        A.CallTo(() => geoLatValidatorFake.CreateCheckedLatitude(geoLatValues, direction, out fullGeoLatitude)).Returns(true);
        IGeoLatInputParser parser = new GeoLatInputParser(valueRangeConverterFake, geoLatValidatorFake);
        
        Assert.That(parser.HandleGeoLat(geoLatInput, direction, out fullGeoLatitude), Is.True);
    }


    [Test]
    public void SyntaxError()
    {
        const string geoLatInput = "5q:13:00";
        int[] geoLatValues = Array.Empty<int>();
        const Directions4GeoLat direction = Directions4GeoLat.North;
        var valueRangeConverterFake = A.Fake<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, false);
        A.CallTo(() => valueRangeConverterFake.ConvertStringRangeToIntRange(geoLatInput, SEPARATOR)).Returns(rangeResult);
        var geoLatValidatorFake = A.Fake<IGeoLatValidator>();
        IGeoLatInputParser parser = new GeoLatInputParser(valueRangeConverterFake, geoLatValidatorFake);

        Assert.That(parser.HandleGeoLat(geoLatInput, direction, out FullGeoLatitude? _), Is.False);
    }

    [Test]
    public void LongitudeError()
    {
        const string geoLatInput = "90:00:00";
        int[] geoLatValues = { 90, 0, 0 };
        const Directions4GeoLat direction = Directions4GeoLat.North;
        var valueRangeConverterFake = A.Fake<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLatValues, true);
        A.CallTo(() => valueRangeConverterFake.ConvertStringRangeToIntRange(geoLatInput, SEPARATOR)).Returns(rangeResult);
        var geoLatValidatorFake = A.Fake<IGeoLatValidator>();
        FullGeoLatitude? fullGeoLatitude;
        A.CallTo(() => geoLatValidatorFake.CreateCheckedLatitude(geoLatValues, direction, out fullGeoLatitude)).Returns(false);
        IGeoLatInputParser parser = new GeoLatInputParser(valueRangeConverterFake, geoLatValidatorFake);

        Assert.That(parser.HandleGeoLat(geoLatInput, direction, out fullGeoLatitude), Is.False);
    }

}
