// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.Support;
using Moq;

namespace Enigma.Test.Frontend.Ui.PresentationFactories;

[TestFixture]
public class TestProgPositionsForPresentationFactory
{

    [Test]
    public void TestHappyFlow()
    {
        var dmsConversionsMock = new Mock<IDoubleToDmsConversions>();
        string longTxt = "10" + EnigmaConstants.DEGREE_SIGN + "00" + EnigmaConstants.MINUTE_SIGN + "00" +
                        EnigmaConstants.SECOND_SIGN;
        char glyph = '4';     // glyph for Cancer
        dmsConversionsMock.Setup(p => p.ConvertDoubleToDmsWithGlyph(100.0)).Returns((longTxt, glyph));
        glyph = '7';        // glyph for Libra
        longTxt = "10" + EnigmaConstants.DEGREE_SIGN + "00" + EnigmaConstants.MINUTE_SIGN + "30" +
                         EnigmaConstants.SECOND_SIGN;
        dmsConversionsMock.Setup(p => p.ConvertDoubleToDmsWithGlyph(190.5)).Returns((longTxt, glyph));
        glyph = '=';        // glyph for Pisces
        longTxt = "20" + EnigmaConstants.DEGREE_SIGN + "12" + EnigmaConstants.MINUTE_SIGN + "48" +
                  EnigmaConstants.SECOND_SIGN;
        dmsConversionsMock.Setup(p => p.ConvertDoubleToDmsWithGlyph(350.2133333333333333333)).Returns((longTxt, glyph));
        Dictionary<ChartPoints, ProgPositions> progPos = CreateProgPositions();
        IProgPositionsForPresentationFactory factory =
            new ProgPositionsForPresentationFactory(dmsConversionsMock.Object, new GlyphsForChartPoints());

        List<PresentableProgPosition> presProgPos = factory.CreatePresProgPos(progPos);
        Assert.That(presProgPos, Has.Count.EqualTo(3));
        Assert.That(presProgPos[0].SignGlyph, Is.EqualTo('4'));      // glyph for Cancer
        Assert.That(presProgPos[0].PointGlyph, Is.EqualTo('a'));     // glyph for Sun
        Assert.That(presProgPos[2].Longitude.Equals("20" + EnigmaConstants.DEGREE_SIGN + "12" 
                                                    + EnigmaConstants.MINUTE_SIGN + "48" + EnigmaConstants.SECOND_SIGN));
    }


    private Dictionary<ChartPoints, ProgPositions> CreateProgPositions()
    {
        return new Dictionary<ChartPoints, ProgPositions >()
        {
            { ChartPoints.Sun, new ProgPositions(100.0, 0.0, 0.0, 0.0) },
            { ChartPoints.Moon, new ProgPositions(190.5, 0.0, 0.0, 0.0) },
            { ChartPoints.Saturn, new ProgPositions(350.2133333333333333333, 0.0, 0.0, 0.0) }
        };
    }
    
}