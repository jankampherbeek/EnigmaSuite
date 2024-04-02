// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
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

public class TestGeoLongInputParser
{
    private const char SEPARATOR = ':';



    [Test]
    public void HappyFlow()
    {
        const string geoLongInput = "123:45:00";
        int[] geoLongValues = { 123, 45, 0 };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        var valueRangeConverterFake = A.Fake<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, true);
        A.CallTo(() => valueRangeConverterFake.ConvertStringRangeToIntRange(geoLongInput, SEPARATOR)).Returns(rangeResult);
        var geoLongValidatorFake = A.Fake<IGeoLongValidator>();
        FullGeoLongitude? fullGeoLongitude;
        A.CallTo(() =>  geoLongValidatorFake.CreateCheckedLongitude(geoLongValues, direction, out fullGeoLongitude)).Returns(true);
        IGeoLongInputParser parser = new GeoLongInputParser(valueRangeConverterFake, geoLongValidatorFake);

        Assert.That(parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude), Is.True);
    }

    [Test]
    public void NoSeconds()
    {
        const string geoLongInput = "123:45";
        int[] geoLongValues = { 123, 45 };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        var valueRangeConverterFake = A.Fake<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, true);
        A.CallTo(() => valueRangeConverterFake.ConvertStringRangeToIntRange(geoLongInput, SEPARATOR)).Returns(rangeResult);
        var geoLongValidatorFake = A.Fake<IGeoLongValidator>();
        FullGeoLongitude? fullGeoLongitude;
        A.CallTo(() => geoLongValidatorFake.CreateCheckedLongitude(geoLongValues, direction, out fullGeoLongitude)).Returns(true);
        IGeoLongInputParser parser = new GeoLongInputParser(valueRangeConverterFake, geoLongValidatorFake);

        Assert.That(parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude), Is.True);
    }


    [Test]
    public void SyntaxError()
    {
        const string geoLongInput = "123:xw:00";
        int[] geoLongValues = Array.Empty<int>();
        const Directions4GeoLong direction = Directions4GeoLong.East;
        var valueRangeConverterFake = A.Fake<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, false);
        A.CallTo(() => valueRangeConverterFake.ConvertStringRangeToIntRange(geoLongInput, SEPARATOR)).Returns(rangeResult);
        var geoLongValidatorFake = A.Fake<IGeoLongValidator>();
        IGeoLongInputParser parser = new GeoLongInputParser(valueRangeConverterFake, geoLongValidatorFake);

        Assert.That(parser.HandleGeoLong(geoLongInput, direction, out FullGeoLongitude? _), Is.False);
    }

    [Test]
    public void LongitudeError()
    {
        const string geoLongInput = "123:75:00";
        int[] geoLongValues = { 123, 75, 0 };
        const Directions4GeoLong direction = Directions4GeoLong.East;
        var valueRangeConverterFake = A.Fake<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (geoLongValues, true);
        A.CallTo(() => valueRangeConverterFake.ConvertStringRangeToIntRange(geoLongInput, SEPARATOR)).Returns(rangeResult);
        var geoLongValidatorFake = A.Fake<IGeoLongValidator>();
        FullGeoLongitude? fullGeoLongitude;
        A.CallTo(() => geoLongValidatorFake.CreateCheckedLongitude(geoLongValues, direction, out fullGeoLongitude)).Returns(false);
        IGeoLongInputParser parser = new GeoLongInputParser(valueRangeConverterFake, geoLongValidatorFake);

        Assert.That(parser.HandleGeoLong(geoLongInput, direction, out fullGeoLongitude), Is.False);
    }

}
