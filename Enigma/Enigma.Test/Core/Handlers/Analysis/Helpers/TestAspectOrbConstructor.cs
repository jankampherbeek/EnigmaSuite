// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Analysis.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Moq;

namespace Enigma.Test.Core.Handlers.Analysis.Helpers;


[TestFixture]
public class TestAspectOrbConstructor
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestDefineOrb()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Venus)).Returns(new ChartPointOrb(ChartPoints.Venus, 0.9));
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Uranus)).Returns(new ChartPointOrb(ChartPoints.Uranus, 0.6));
        IAspectOrbConstructor _orbConstructor = new AspectOrbConstructor(mockOrbDefinitions.Object);
        double baseOrb = 10.0;
        double orbFactor = 0.2;
        double actualOrb = _orbConstructor.DefineOrb(ChartPoints.Venus, ChartPoints.Uranus, baseOrb, orbFactor);
        double expectedOrb = 1.8;
        Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
    }

    [Test]
    public void TestDefineOrbWithMundanePoint()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Venus)).Returns(new ChartPointOrb(ChartPoints.Venus, 0.9));
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(ChartPoints.Mc)).Returns(new ChartPointOrb(ChartPoints.Mc, 1.0));
        IAspectOrbConstructor _orbConstructor = new AspectOrbConstructor(mockOrbDefinitions.Object);
        double baseOrb = 10.0;
        double orbFactor = 0.6;
        double actualOrb = _orbConstructor.DefineOrb(ChartPoints.Venus, ChartPoints.Mc, baseOrb, orbFactor);
        double expectedOrb = 6.0;
        Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
    }


}
