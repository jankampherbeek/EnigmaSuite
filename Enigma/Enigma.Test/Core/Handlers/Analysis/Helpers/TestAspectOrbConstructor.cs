// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Moq;

namespace Enigma.Test.Core.Handlers.Analysis.Helpers;


// TODO 0.1 Analysis test


[TestFixture]
public class TestAspectOrbConstructor
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestOrb4CelPoint()
    {
        /*        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
                mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Venus)).Returns(new ChartPointOrb(ChartPoints.Venus, 0.9));
                mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Uranus)).Returns(new ChartPointOrb(ChartPoints.Uranus, 0.6));
                IPointMappings pointMappings = new PointMappings(new PointDefinitions());
                IAspectOrbConstructor _orbConstructor = new AspectOrbConstructor(mockOrbDefinitions.Object, pointMappings);
                AspectDetails aspectDetails = new(AspectTypes.Quintile, 72.0, "", "", 0.2);
                double actualOrb = _orbConstructor.DefineOrb(ChartPoints.Venus, ChartPoints.Uranus, aspectDetails);
                double expectedOrb = 1.8;
                Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
          */
    }

    [Test]
    public void TestOrb4MundanePoint()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Mc)).Returns(new ChartPointOrb(ChartPoints.Mc, 1.0));
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Uranus)).Returns(new ChartPointOrb(ChartPoints.Uranus, 0.6));
        /*
            IPointMappings pointMappings = new PointMappings(new PointDefinitions());
            IAspectOrbConstructor _orbConstructor = new AspectOrbConstructor(mockOrbDefinitions.Object, pointMappings);
            AspectDetails aspectDetails = new(AspectTypes.Quintile, 72.0, "", "", 0.2);
            double actualOrb = _orbConstructor.DefineOrb("MC", ChartPoints.Uranus, aspectDetails);
            double expectedOrb = 2.0;
            Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
        */
    }

    [Test]
    public void TestOrb4GeneralCelPoints()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Venus)).Returns(new ChartPointOrb(ChartPoints.Venus, 0.9));
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Uranus)).Returns(new ChartPointOrb(ChartPoints.Uranus, 0.6));
        /*
          IPointMappings pointMappings = new PointMappings(new PointDefinitions());
          IAspectOrbConstructor _orbConstructor = new AspectOrbConstructor(mockOrbDefinitions.Object, pointMappings);
          AspectDetails aspectDetails = new(AspectTypes.Quintile, 72.0, "", "", 0.2);
          GeneralPoint genPointVenus = new GeneralPoint(3, "Venus", PointTypes.CelestialPoint, "ref.enum.celpoint.venus");
          GeneralPoint genPointUranus = new GeneralPoint(8, "Uranus", PointTypes.CelestialPoint, "ref.enum.celpoint.uranus");
          double actualOrb = _orbConstructor.DefineOrb(genPointVenus, genPointUranus, aspectDetails);
          double expectedOrb = 1.8;
          Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
        */
    }

    [Test]
    public void TestOrb4GeneralCelPointAndMundanePoint()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Mc)).Returns(new ChartPointOrb(ChartPoints.Mc, 1.0));
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Uranus)).Returns(new ChartPointOrb(ChartPoints.Uranus, 0.6));

        /*
        IPointMappings pointMappings = new PointMappings(new PointDefinitions());
        IAspectOrbConstructor _orbConstructor = new AspectOrbConstructor(mockOrbDefinitions.Object, pointMappings);
        AspectDetails aspectDetails = new(AspectTypes.Quintile, 72.0, "", "", 0.2);
        GeneralPoint genPointUranus = new GeneralPoint(8, "Uranus", PointTypes.CelestialPoint, "ref.enum.celpoint.uranus");
        GeneralPoint genPointMc = new GeneralPoint(3001, "MC", PointTypes.MundaneSpecialPoint, "ref.enum.mundanepoint.id.mc");
        double expectedOrb = 2.0;
        double actualOrb = _orbConstructor.DefineOrb(genPointMc, genPointUranus, aspectDetails);
        Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
        actualOrb = _orbConstructor.DefineOrb(genPointUranus, genPointMc, aspectDetails);
        Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
        */
    }

}
