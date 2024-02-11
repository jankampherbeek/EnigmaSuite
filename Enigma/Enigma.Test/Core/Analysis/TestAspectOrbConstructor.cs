// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Moq;

namespace Enigma.Test.Core.Analysis;

[TestFixture]
public class TestAspectOrbConstructor
{
   
    private const double DELTA = 0.00000001;

    private const ChartPoints POINT1 = ChartPoints.Sun;
    private const ChartPoints POINT2 = ChartPoints.Moon;
    private const double BASE_ORB = 9.0;
    private const double ASPECT_ORB_FACTOR = 0.5; 
    private readonly Dictionary<ChartPoints, ChartPointConfigSpecs> _chartPointConfigSpecs = new();
    private readonly KeyValuePair<ChartPoints, double> _chartPointOrb1 = new(ChartPoints.Sun, 0.6);
    private readonly KeyValuePair<ChartPoints, double> _chartPointOrb2 = new(ChartPoints.Moon, 0.4);
    
    [Test]
    public void TestHappyFlow()
    {
        var mockOrbDefinitions = CreateMockOrbDefinitions();
        IAspectOrbConstructor orbConstructor = new AspectOrbConstructor(mockOrbDefinitions);
        const double expected = 2.7; // 0.6 * 0.5 * 9.0 = 2.7
        double result = orbConstructor.DefineOrb(POINT1, POINT2, BASE_ORB, ASPECT_ORB_FACTOR, _chartPointConfigSpecs);
        Assert.That(result, Is.EqualTo(expected).Within(DELTA));
    }

    private IOrbDefinitions CreateMockOrbDefinitions()
    {
        var mockOrbDefinitions = new Mock<IOrbDefinitions>();
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(POINT1, _chartPointConfigSpecs)).Returns(_chartPointOrb1);
        mockOrbDefinitions.Setup(p => p.DefineChartPointOrb(POINT2, _chartPointConfigSpecs)).Returns(_chartPointOrb2);
        return mockOrbDefinitions.Object;
    }
}