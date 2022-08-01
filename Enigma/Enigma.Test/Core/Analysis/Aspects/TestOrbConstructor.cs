// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis.Aspects;
using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;
using Moq;

namespace Enigma.Test.Core.Analysis.Aspects;

[TestFixture]
public class TestOrbConstructor
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestOrb4SolSysPoint()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineSolSysPointOrb(SolarSystemPoints.Venus)).Returns(new SolSysPointOrb(SolarSystemPoints.Venus, 0.9));
        mockOrbDefinitions.Setup(p => p.DefineSolSysPointOrb(SolarSystemPoints.Uranus)).Returns(new SolSysPointOrb(SolarSystemPoints.Uranus, 0.6));
        IOrbConstructor _orbConstructor = new OrbConstructor(mockOrbDefinitions.Object);
        AspectDetails aspectDetails = new AspectDetails(AspectTypes.Quintile, 72.0, "", "", 0.2);
        double actualOrb = _orbConstructor.DefineOrb(SolarSystemPoints.Venus, SolarSystemPoints.Uranus, aspectDetails);
        double expectedOrb = 1.8;
        Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
    }

    [Test]
    public void TestOrb4MundanePoint()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineMundanePointOrb("MC")).Returns(new MundanePointOrb("MC", 1.0));
        mockOrbDefinitions.Setup(p => p.DefineSolSysPointOrb(SolarSystemPoints.Uranus)).Returns(new SolSysPointOrb(SolarSystemPoints.Uranus, 0.6));
        IOrbConstructor _orbConstructor = new OrbConstructor(mockOrbDefinitions.Object);
        AspectDetails aspectDetails = new AspectDetails(AspectTypes.Quintile, 72.0, "", "", 0.2);
        double actualOrb = _orbConstructor.DefineOrb("MC", SolarSystemPoints.Uranus, aspectDetails);
        double expectedOrb = 2.0;
        Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
    }





}
