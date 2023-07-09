// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Charts;
using Enigma.Domain.Constants;
using Enigma.Domain.Points;
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
        dmsConversionsMock.Setup(p => p.ConvertDoubleToPositionsDmsText(3.0)).Returns("3" + EnigmaConstants.DegreeSign + "00" + EnigmaConstants.MinuteSign + "00" + EnigmaConstants.SecondSign);
        dmsConversionsMock.Setup(p => p.ConvertDoubleToPositionsDmsText(2.5)).Returns("2" + EnigmaConstants.DegreeSign + "30" + EnigmaConstants.MinuteSign + "00" + EnigmaConstants.SecondSign);
        dmsConversionsMock.Setup(p => p.ConvertDoubleToPositionsDmsText(10.0)).Returns("10" + EnigmaConstants.DegreeSign + "00" + EnigmaConstants.MinuteSign + "00" + EnigmaConstants.SecondSign);
        IAspectForDataGridFactory aspectForDataGridFactory = new AspectForDataGridFactory(dmsConversionsMock.Object);

        List<DefinedAspect> definedAspects = CreateDefinedAspects();
        List<PresentableAspects> presAspects = aspectForDataGridFactory.CreateAspectForDataGrid(definedAspects);

        Assert.Multiple(() =>
        {
            Assert.That(presAspects, Has.Count.EqualTo(2));
            Assert.That(presAspects[0].AspectGlyph, Is.EqualTo('B'));
            Assert.That(presAspects[1].AspectGlyph, Is.EqualTo('E'));
            //         Assert.That(presAspects[0].Point1Text, Is.EqualTo('a'));  // TODO 0.1 fix test
            Assert.That(presAspects[0].Point2Glyph, Is.EqualTo('b'));
            Assert.That(presAspects[1].Point2Glyph, Is.EqualTo('g'));
            Assert.That(presAspects[0].OrbText, Is.EqualTo("3°00′00″"));
            Assert.That(presAspects[0].ExactnessText, Is.EqualTo("70.00"));
        });
    }

    private static List<DefinedAspect> CreateDefinedAspects()
    {
        List<DefinedAspect> definedAspects = new()
        {
            new DefinedAspect(ChartPoints.Sun, ChartPoints.Moon, new AspectDetails(AspectTypes.Conjunction, 0.0, "ref.enum.aspect.conjunction", 'B', 1.0), 10.0, 3.0),
            new DefinedAspect(ChartPoints.Ascendant, ChartPoints.Jupiter, new AspectDetails(AspectTypes.Square, 90.0, "ref.enum.aspect.square", 'E', 0.85), 10.0, 2.5)
        };
        return definedAspects;

    }
}