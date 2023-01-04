// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Analysis.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Moq;

namespace Enigma.Test.Core.Handlers.Analysis.Helpers;

[TestFixture]
public class TestAspectOrbConstructor
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestOrb4CelPoint()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineCelPointOrb(CelPoints.Venus)).Returns(new CelPointOrb(CelPoints.Venus, 0.9));
        mockOrbDefinitions.Setup(p => p.DefineCelPointOrb(CelPoints.Uranus)).Returns(new CelPointOrb(CelPoints.Uranus, 0.6));
        IPointMappings pointMappings = new PointMappings(new PointDefinitions());
        IAspectOrbConstructor _orbConstructor = new AspectOrbConstructor(mockOrbDefinitions.Object, pointMappings);
        AspectDetails aspectDetails = new(AspectTypes.Quintile, 72.0, "", "", 0.2);
        double actualOrb = _orbConstructor.DefineOrb(CelPoints.Venus, CelPoints.Uranus, aspectDetails);
        double expectedOrb = 1.8;
        Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
    }

    [Test]
    public void TestOrb4MundanePoint()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineMundanePointOrb("MC")).Returns(new MundanePointOrb("MC", 1.0));
        mockOrbDefinitions.Setup(p => p.DefineCelPointOrb(CelPoints.Uranus)).Returns(new CelPointOrb(CelPoints.Uranus, 0.6));
        IPointMappings pointMappings = new PointMappings(new PointDefinitions());
        IAspectOrbConstructor _orbConstructor = new AspectOrbConstructor(mockOrbDefinitions.Object, pointMappings);
        AspectDetails aspectDetails = new(AspectTypes.Quintile, 72.0, "", "", 0.2);
        double actualOrb = _orbConstructor.DefineOrb("MC", CelPoints.Uranus, aspectDetails);
        double expectedOrb = 2.0;
        Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
    }

    [Test]
    public void TestOrb4GeneralCelPoints()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineCelPointOrb(CelPoints.Venus)).Returns(new CelPointOrb(CelPoints.Venus, 0.9));
        mockOrbDefinitions.Setup(p => p.DefineCelPointOrb(CelPoints.Uranus)).Returns(new CelPointOrb(CelPoints.Uranus, 0.6));
        IPointMappings pointMappings = new PointMappings(new PointDefinitions());
        IAspectOrbConstructor _orbConstructor = new AspectOrbConstructor(mockOrbDefinitions.Object, pointMappings);
        AspectDetails aspectDetails = new(AspectTypes.Quintile, 72.0, "", "", 0.2);
        GeneralPoint genPointVenus = new GeneralPoint(3, "Venus", PointTypes.CelestialPoint, "ref.enum.celpoint.venus");
        GeneralPoint genPointUranus = new GeneralPoint(8, "Uranus", PointTypes.CelestialPoint, "ref.enum.celpoint.uranus");
        double actualOrb = _orbConstructor.DefineOrb(genPointVenus, genPointUranus, aspectDetails);
        double expectedOrb = 1.8;
        Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
    }

    [Test]
    public void TestOrb4GeneralCelPointAndMundanePoint()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineMundanePointOrb("MC")).Returns(new MundanePointOrb("MC", 1.0));
        mockOrbDefinitions.Setup(p => p.DefineCelPointOrb(CelPoints.Uranus)).Returns(new CelPointOrb(CelPoints.Uranus, 0.6));
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
    }

}
