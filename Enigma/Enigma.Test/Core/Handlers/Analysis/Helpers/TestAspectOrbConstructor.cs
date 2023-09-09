// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis.Helpers;
using Enigma.Core.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Configuration;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Moq;

namespace Enigma.Test.Core.Handlers.Analysis.Helpers;

[TestFixture]
public class TestAspectOrbConstructor
{
   
    private const double Delta = 0.00000001;

    private const ChartPoints Point1 = ChartPoints.Sun;
    private const ChartPoints Point2 = ChartPoints.Moon;
    private const double BaseOrb = 9.0;
    private const double AspectOrbFactor = 0.5; 
    private readonly Dictionary<ChartPoints, ChartPointConfigSpecs> _chartPointConfigSpecs = new();
    private readonly ChartPointOrb _chartPointOrb1 = new(ChartPoints.Sun, 0.6);
    private readonly ChartPointOrb _chartPointOrb2 = new(ChartPoints.Moon, 0.4);
    
    [Test]
    public void TestHappyFlow()
    {
        var mockOrbDefinitions = CreateMockOrbDefinitions();
        IAspectOrbConstructor orbConstructor = new AspectOrbConstructor(mockOrbDefinitions);
        const double expected = 2.7; // 0.6 * 0.5 * 9.0 = 2.7
        double result = orbConstructor.DefineOrb(Point1, Point2, BaseOrb, AspectOrbFactor, _chartPointConfigSpecs);
        Assert.That(result, Is.EqualTo(expected).Within(Delta));
    }

    private IOrbDefinitions CreateMockOrbDefinitions()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(Point1, _chartPointConfigSpecs)).Returns(_chartPointOrb1);
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(Point2, _chartPointConfigSpecs)).Returns(_chartPointOrb2);
        return mockOrbDefinitions.Object;
    }
}