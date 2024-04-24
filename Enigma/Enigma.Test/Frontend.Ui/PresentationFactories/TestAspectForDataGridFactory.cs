// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.Support.Conversions;
using FakeItEasy;

namespace Enigma.Test.Frontend.Ui.PresentationFactories;


[TestFixture]
public class TestAspectForDataGridFactory
{

    [Test]
    public void TestHappyFlow()
    {
        var dmsConversionsFake = A.Fake<IDoubleToDmsConversions>();
        A.CallTo(() => dmsConversionsFake.ConvertDoubleToPositionsDmsText(3.0))
            .Returns("3" + EnigmaConstants.DEGREE_SIGN + "00" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN);
        A.CallTo(() => dmsConversionsFake.ConvertDoubleToPositionsDmsText(3.0))
            .Returns("3" + EnigmaConstants.DEGREE_SIGN + "00" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN);
        A.CallTo(() => dmsConversionsFake.ConvertDoubleToPositionsDmsText(2.5))
            .Returns("2" + EnigmaConstants.DEGREE_SIGN + "30" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN);
        A.CallTo(() => dmsConversionsFake.ConvertDoubleToPositionsDmsText(10.0))
            .Returns("10" + EnigmaConstants.DEGREE_SIGN + "00" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN);
        IAspectForDataGridFactory aspectForDataGridFactory = new AspectForDataGridFactory(dmsConversionsFake);

        IEnumerable<DefinedAspect> definedAspects = CreateDefinedAspects();
        List<PresentableAspects> presAspects = aspectForDataGridFactory.CreateAspectForDataGrid(definedAspects);

        Assert.Multiple(() =>
        {
            Assert.That(presAspects, Has.Count.EqualTo(2));
            Assert.That(presAspects[0].AspectGlyph, Is.EqualTo('B'));
            Assert.That(presAspects[1].AspectGlyph, Is.EqualTo('E'));
            Assert.That(presAspects[0].Point2Glyph, Is.EqualTo('b'));
            Assert.That(presAspects[1].Point2Glyph, Is.EqualTo('g'));
            Assert.That(presAspects[0].OrbText, Is.EqualTo("3°00′00″"));
            Assert.That(presAspects[0].OrbExactness, Is.EqualTo(70.00));
        });
    }

    private static IEnumerable<DefinedAspect> CreateDefinedAspects()
    {
        List<DefinedAspect> definedAspects = new()
        {
            new DefinedAspect(ChartPoints.Sun, ChartPoints.Moon, new AspectDetails(AspectTypes.Conjunction, 0.0, "Conjunction", 'B', 1.0), 10.0, 3.0),
            new DefinedAspect(ChartPoints.Ascendant, ChartPoints.Jupiter, new AspectDetails(AspectTypes.Square, 90.0, "Square", 'E', 0.85), 10.0, 2.5)
        };
        return definedAspects;

    }
}