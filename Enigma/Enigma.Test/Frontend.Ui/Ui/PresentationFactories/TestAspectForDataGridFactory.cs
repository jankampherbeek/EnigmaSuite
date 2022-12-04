// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Constants;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.PresentationFactories;
using Moq;

namespace Enigma.Test.Frontend.Ui.PresentationFactories;


[TestFixture]
public class TestAspectForDataGridFactory
{

    [Test]
    public void TestHappyFlow()
    {
        var dmsConversionsMock = new Mock<IDoubleToDmsConversions>();
        dmsConversionsMock.Setup(p => p.ConvertDoubleToPositionsDmsText(3.0)).Returns("3" + EnigmaConstants.DEGREE_SIGN + "00" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN);
        dmsConversionsMock.Setup(p => p.ConvertDoubleToPositionsDmsText(2.5)).Returns("2" + EnigmaConstants.DEGREE_SIGN + "30" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN);
        dmsConversionsMock.Setup(p => p.ConvertDoubleToPositionsDmsText(10.0)).Returns("10" + EnigmaConstants.DEGREE_SIGN + "00" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN);
        IAspectForDataGridFactory aspectForDataGridFactory = new AspectForDataGridFactory(dmsConversionsMock.Object);

        List<EffectiveAspect> effAspects = CreateEffAspects();
        List<PresentableAspects> presAspects = aspectForDataGridFactory.CreateAspectForDataGrid(effAspects);

        Assert.Multiple(() =>
        {
            Assert.That(presAspects, Has.Count.EqualTo(2));
            Assert.That(presAspects[0].AspectGlyph, Is.EqualTo("B"));
            Assert.That(presAspects[1].AspectGlyph, Is.EqualTo("E"));
            Assert.That(presAspects[0].Point1TextGlyph, Is.EqualTo("a"));
            Assert.That(presAspects[1].Point1TextGlyph, Is.EqualTo("Asc"));
            Assert.That(presAspects[0].Point2Glyph, Is.EqualTo("b"));
            Assert.That(presAspects[1].Point2Glyph, Is.EqualTo("g"));
            Assert.That(presAspects[0].OrbText, Is.EqualTo("10°00′00″"));
            Assert.That(presAspects[0].ExactnessText, Is.EqualTo("70.00"));
        });
    }

    private static List<EffectiveAspect> CreateEffAspects()
    {
        List<EffectiveAspect> effAspects = new()
        {
            new EffectiveAspect(CelPoints.Sun, CelPoints.Moon, new AspectDetails(AspectTypes.Conjunction, 0.0, "ref.enum.aspect.conjunction", "B", 1.0), 10.0, 3.0),
            new EffectiveAspect("Asc", CelPoints.Jupiter, new AspectDetails(AspectTypes.Square, 90.0, "ref.enum.aspect.square", "E", 0.85), 10.0, 2.5)
        };
        return effAspects;

    }
}