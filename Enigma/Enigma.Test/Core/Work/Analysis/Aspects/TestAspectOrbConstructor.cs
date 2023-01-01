// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Analysis.Aspects;
using Enigma.Core.Work.Analysis.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Moq;

namespace Enigma.Test.Core.Work.Analysis.Aspects;

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
        IAspectOrbConstructor _orbConstructor = new AspectOrbConstructor(mockOrbDefinitions.Object);
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
        IAspectOrbConstructor _orbConstructor = new AspectOrbConstructor(mockOrbDefinitions.Object);
        AspectDetails aspectDetails = new(AspectTypes.Quintile, 72.0, "", "", 0.2);
        double actualOrb = _orbConstructor.DefineOrb("MC", CelPoints.Uranus, aspectDetails);
        double expectedOrb = 2.0;
        Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
    }





}
