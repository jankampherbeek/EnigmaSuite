// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Conversions;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Test.Frontend.Ui.Conversions;


[TestFixture]

public class TestDoubleToDmsConversions
{
    private readonly IDoubleToDmsConversions _conversions = new DoubleToDmsConversions();


    [Test]
    public void TestHappyFlowDoubleToDms()
    {
        const double inputValue = 6.5;
        string expectedResult = "6" + EnigmaConstants.DegreeSign + "30" + EnigmaConstants.MinuteSign + "00" + EnigmaConstants.SecondSign;
        Assert.That(_conversions.ConvertDoubleToPositionsDmsText(inputValue), Is.EqualTo(expectedResult));
    }

    [Test]
    public void TestNegativeValueDoubleToDms()
    {
        const double inputValue = -6.5;
        string expectedResult = "-6" + EnigmaConstants.DegreeSign + "30" + EnigmaConstants.MinuteSign + "00" + EnigmaConstants.SecondSign;
        Assert.That(_conversions.ConvertDoubleToPositionsDmsText(inputValue), Is.EqualTo(expectedResult));
    }

    [Test]
    public void TestSmallNegativeValueDoubleToDms()
    {
        const double inputValue = -0.03;
        string expectedResult = "-0" + EnigmaConstants.DegreeSign + "01" + EnigmaConstants.MinuteSign + "48" + EnigmaConstants.SecondSign;
        Assert.That(_conversions.ConvertDoubleToPositionsDmsText(inputValue), Is.EqualTo(expectedResult));
    }


    [Test]
    public void TestLargeValueDoubleToDms()
    {
        const double inputValue = 342.5;
        string expectedResult = "342" + EnigmaConstants.DegreeSign + "30" + EnigmaConstants.MinuteSign + "00" + EnigmaConstants.SecondSign;
        Assert.That(_conversions.ConvertDoubleToPositionsDmsText(inputValue), Is.EqualTo(expectedResult));
    }

    [Test]
    public void TestBorderValueDoubleToDms()
    {
        const double inputValue = 42.999999;
        string expectedResult = "42" + EnigmaConstants.DegreeSign + "59" + EnigmaConstants.MinuteSign + "59" + EnigmaConstants.SecondSign;
        Assert.That(_conversions.ConvertDoubleToPositionsDmsText(inputValue), Is.EqualTo(expectedResult));
    }

    [Test]
    public void TestHappyFlowDoubleToLongWithGlyph()
    {
        const double inputValue = 36.5;
        string expectedText = "6" + EnigmaConstants.DegreeSign + "30" + EnigmaConstants.MinuteSign + "00" + EnigmaConstants.SecondSign;
        const char expectedGlyph = '2';
        Assert.Multiple(() =>
        {
            Assert.That(_conversions.ConvertDoubleToDmsWithGlyph(inputValue).longTxt, Is.EqualTo(expectedText));
            Assert.That(_conversions.ConvertDoubleToDmsWithGlyph(inputValue).glyph, Is.EqualTo(expectedGlyph));
        });
    }

    [Test]
    public void TestZeroDegreesDoubleToLongWithGlyph()
    {
        const double inputValue = 0.0;
        string expectedText = "0" + EnigmaConstants.DegreeSign + "00" + EnigmaConstants.MinuteSign + "00" + EnigmaConstants.SecondSign;
        const char expectedGlyph = '1';
        Assert.Multiple(() =>
        {
            Assert.That(_conversions.ConvertDoubleToDmsWithGlyph(inputValue).longTxt, Is.EqualTo(expectedText));
            Assert.That(_conversions.ConvertDoubleToDmsWithGlyph(inputValue).glyph, Is.EqualTo(expectedGlyph));
        });
    }

    [Test]
    public void TestHappyFlowDoubleToLongInSignNoGlyph()
    {
        const double inputValue = 136.5;
        string expectedText = "16" + EnigmaConstants.DegreeSign + "30" + EnigmaConstants.MinuteSign + "00" + EnigmaConstants.SecondSign;
        Assert.That(_conversions.ConvertDoubleToDmsWithGlyph(inputValue).longTxt, Is.EqualTo(expectedText));
    }


}
