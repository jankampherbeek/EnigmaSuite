// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;
using Enigma.Domain.Constants;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.PresentationFactories;
using Enigma.Frontend.UiDomain;
using Moq;

namespace Enigma.Test.Frontend.PresentationFactories;


[TestFixture]
public class TestAspectForDataGridFactory
{

    private IAspectForDataGridFactory _aspectForDataGridFactory;


    [Test]
    public void TestHappyFlow()
    {
        var dmsConversionsMock = new Mock<IDoubleToDmsConversions>();
        dmsConversionsMock.Setup(p => p.ConvertDoubleToPositionsDmsText(3.0)).Returns("3" + EnigmaConstants.DEGREE_SIGN + "00" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN);
        dmsConversionsMock.Setup(p => p.ConvertDoubleToPositionsDmsText(2.5)).Returns("2" + EnigmaConstants.DEGREE_SIGN + "30" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN);
        dmsConversionsMock.Setup(p => p.ConvertDoubleToPositionsDmsText(10.0)).Returns("10" + EnigmaConstants.DEGREE_SIGN + "00" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN);
        var solSysPointSpecsMock = new Mock<ISolarSystemPointSpecifications>();
        solSysPointSpecsMock.Setup(p => p.DetailsForPoint(SolarSystemPoints.Sun)).Returns(new SolarSystemPointDetails(SolarSystemPoints.Sun, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_SUN, false, true, "sun", "a"));
        solSysPointSpecsMock.Setup(p => p.DetailsForPoint(SolarSystemPoints.Moon)).Returns(new SolarSystemPointDetails(SolarSystemPoints.Moon, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_MOON, false, true, "moon", "b"));
        solSysPointSpecsMock.Setup(p => p.DetailsForPoint(SolarSystemPoints.Jupiter)).Returns(new SolarSystemPointDetails(SolarSystemPoints.Jupiter, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_JUPITER, true, true, "jupiter", "g"));
        var aspectSpecsMock = new Mock<IAspectSpecifications>();
        aspectSpecsMock.Setup(p => p.DetailsForAspect(AspectTypes.Conjunction)).Returns(new AspectDetails(AspectTypes.Conjunction, 0.0, "ref.enum.aspect.conjunction", "B", 1.0));
        aspectSpecsMock.Setup(p => p.DetailsForAspect(AspectTypes.Square)).Returns(new AspectDetails(AspectTypes.Square, 90.0, "ref.enum.aspect.square", "E", 0.85));
        _aspectForDataGridFactory = new AspectForDataGridFactory(dmsConversionsMock.Object, solSysPointSpecsMock.Object, aspectSpecsMock.Object);

        List<EffectiveAspect> effAspects = CreateEffAspects();
        List<PresentableAspects> presAspects = _aspectForDataGridFactory.CreateAspectForDataGrid(effAspects);
        Assert.That(presAspects.Count, Is.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(presAspects[0].AspectGlyph, Is.EqualTo("B"));
            Assert.That(presAspects[1].AspectGlyph, Is.EqualTo("E"));
            Assert.That(presAspects[0].Point1TextGlyph, Is.EqualTo("a"));
            Assert.That(presAspects[1].Point1TextGlyph, Is.EqualTo("Asc"));
            Assert.That(presAspects[0].Point2Glyph, Is.EqualTo("b"));
            Assert.That(presAspects[1].Point2Glyph, Is.EqualTo("g"));
            Assert.That(presAspects[0].OrbText, Is.EqualTo("10°00′00″"));
            Assert.That(presAspects[0].ExactnessText, Is.EqualTo("30.00"));
        });
    }

    private List<EffectiveAspect> CreateEffAspects()
    {
        List<EffectiveAspect> effAspects = new();
        effAspects.Add(new EffectiveAspect(SolarSystemPoints.Sun, SolarSystemPoints.Moon, new AspectDetails(AspectTypes.Conjunction, 0.0, "ref.enum.aspect.conjunction", "B", 1.0), 10.0, 3.0));
        effAspects.Add(new EffectiveAspect("Asc", SolarSystemPoints.Jupiter, new AspectDetails(AspectTypes.Square, 90.0, "ref.enum.aspect.square", "E", 0.85), 10.0, 2.5));
        return effAspects;

    }
}