// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Frontend.InputSupport.Conversions;

namespace Enigma.Test.Frontend.InputSupport.Conversions;


[TestFixture]

public class TextDoubleToDmsConversions
{
    private IDoubleToDmsConversions _conversions = new DoubleToDmsConversions();


    [Test]
    public void TestHappyFlowDoubleToDms()
    {
        double inputValue = 6.5;
        string expectedResult = "06" + EnigmaConstants.DEGREE_SIGN + "30" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN;
        Assert.That(_conversions.ConvertDoubleToPositionsText(inputValue), Is.EqualTo(expectedResult));
    }

    [Test]
    public void TestNegativeValueDoubleToDms()
    {
        double inputValue = -6.5;
        string expectedResult = "-06" + EnigmaConstants.DEGREE_SIGN + "30" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN;
        Assert.That(_conversions.ConvertDoubleToPositionsText(inputValue), Is.EqualTo(expectedResult));
    }

    [Test]
    public void TestLargeValueDoubleToDms()
    {
        double inputValue = 342.5;
        string expectedResult = "342" + EnigmaConstants.DEGREE_SIGN + "30" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN;
        Assert.That(_conversions.ConvertDoubleToPositionsText(inputValue), Is.EqualTo(expectedResult));
    }

    [Test]
    public void TestBorderValueDoubleToDms()
    {
        double inputValue = 42.999999;
        string expectedResult = "42" + EnigmaConstants.DEGREE_SIGN + "59" + EnigmaConstants.MINUTE_SIGN + "59" + EnigmaConstants.SECOND_SIGN;
        Assert.That(_conversions.ConvertDoubleToPositionsText(inputValue), Is.EqualTo(expectedResult));
    }

    [Test]
    public void TestHappyFlowDoubleToLongWithGlyph()
    {
        double inputValue = 36.5;
        string expectedText = "06" + EnigmaConstants.DEGREE_SIGN + "30" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN;
        string expectedGlyph = "2";
        Assert.That(_conversions.ConvertDoubleToLongWithGlyph(inputValue).longTxt, Is.EqualTo(expectedText));
        Assert.That(_conversions.ConvertDoubleToLongWithGlyph(inputValue).glyph, Is.EqualTo(expectedGlyph));
    }

    [Test]
    public void TestZereDegreesDoubleToLongWithGlyph()
    {
        double inputValue = 0.0;
        string expectedText = "00" + EnigmaConstants.DEGREE_SIGN + "00" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN;
        string expectedGlyph = "1";
        Assert.That(_conversions.ConvertDoubleToLongWithGlyph(inputValue).longTxt, Is.EqualTo(expectedText));
        Assert.That(_conversions.ConvertDoubleToLongWithGlyph(inputValue).glyph, Is.EqualTo(expectedGlyph));
    }
}
