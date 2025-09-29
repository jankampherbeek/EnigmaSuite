// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Integration;

[TestFixture]
public class IntegrationTestBlaSchema: IntegrationTestBase
{
    [Test]
    public void TestSignCounts()
    {
        ChartLongitudes chart = CreateChart();
        var orchestrator = new BlaSchemaOrchestrator(chart);
        var result = orchestrator.GetSignCounts();
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[1], Is.EqualTo(2));
            Assert.That(result[2], Is.EqualTo(2));
            Assert.That(result[3], Is.EqualTo(4));
            Assert.That(result[4], Is.EqualTo(3));
            Assert.That(result[5], Is.EqualTo(2));
            Assert.That(result[6], Is.EqualTo(1));
            Assert.That(result[7], Is.EqualTo(1));
            Assert.That(result[8], Is.EqualTo(3));
            Assert.That(result[9], Is.EqualTo(1));
            Assert.That(result[10], Is.EqualTo(2));
            Assert.That(result[11], Is.EqualTo(2));
            Assert.That(result[12], Is.EqualTo(2));

        });
    }

    [Test]
    public void TestHouseCounts()
    {
        ChartLongitudes chart = CreateChart();
        var orchestrator = new BlaSchemaOrchestrator(chart);
        var result = orchestrator.GetHouseCounts();
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[1], Is.EqualTo(3)); 
            Assert.That(result[2], Is.EqualTo(4));
            Assert.That(result[3], Is.EqualTo(0));
            Assert.That(result[4], Is.EqualTo(3));
            Assert.That(result[5], Is.EqualTo(0));
            Assert.That(result[6], Is.EqualTo(3));
            Assert.That(result[7], Is.EqualTo(5));
            Assert.That(result[8], Is.EqualTo(2));
            Assert.That(result[9], Is.EqualTo(0));
            Assert.That(result[10], Is.EqualTo(2)); 
            Assert.That(result[11], Is.EqualTo(0));
            Assert.That(result[12], Is.EqualTo(1));
            
        });
    }
    
    
    [Test]
    public void TestElementsAndCrosses()
    {
        ChartLongitudes chart = CreateChart();
        var orchestrator = new BlaSchemaOrchestrator(chart);
        var (crossCounts, elementCounts) = orchestrator.GetCrossELementCounts();
        Assert.Multiple(() =>
        {
            Assert.That(crossCounts, Is.Not.Null);
            Assert.That(elementCounts, Is.Not.Null);
            Assert.That(crossCounts[1].Sign, Is.EqualTo(8));
            Assert.That(crossCounts[2].Sign, Is.EqualTo(9));
            Assert.That(crossCounts[3].Sign, Is.EqualTo(8));
            Assert.That(crossCounts[1].House, Is.EqualTo(13)); 
            Assert.That(crossCounts[2].House, Is.EqualTo(6));
            Assert.That(crossCounts[3].House, Is.EqualTo(4));
            Assert.That(crossCounts[1].HCusp, Is.EqualTo(8));
            Assert.That(crossCounts[2].HCusp, Is.EqualTo(11));
            Assert.That(crossCounts[3].HCusp, Is.EqualTo(4)); 
            Assert.That(crossCounts[1].Sum, Is.EqualTo(21)); 
            Assert.That(crossCounts[2].Sum, Is.EqualTo(15));
            Assert.That(crossCounts[3].Sum, Is.EqualTo(12));
            Assert.That(crossCounts[1].Total, Is.EqualTo(29));
            Assert.That(crossCounts[2].Total, Is.EqualTo(26));
            Assert.That(crossCounts[3].Total, Is.EqualTo(16)); 
            
            Assert.That(elementCounts[1].Sign, Is.EqualTo(5));
            Assert.That(elementCounts[2].Sign, Is.EqualTo(5));
            Assert.That(elementCounts[3].Sign, Is.EqualTo(7));
            Assert.That(elementCounts[4].Sign, Is.EqualTo(8));
            Assert.That(elementCounts[1].House, Is.EqualTo(3));
            Assert.That(elementCounts[2].House, Is.EqualTo(9));
            Assert.That(elementCounts[3].House, Is.EqualTo(5));
            Assert.That(elementCounts[4].House, Is.EqualTo(6));
            Assert.That(elementCounts[1].HCusp, Is.EqualTo(3));
            Assert.That(elementCounts[2].HCusp, Is.EqualTo(6));
            Assert.That(elementCounts[3].HCusp, Is.EqualTo(7));
            Assert.That(elementCounts[4].HCusp, Is.EqualTo(7));
            Assert.That(elementCounts[1].Sum, Is.EqualTo(8));
            Assert.That(elementCounts[2].Sum, Is.EqualTo(14));
            Assert.That(elementCounts[3].Sum, Is.EqualTo(12));
            Assert.That(elementCounts[4].Sum, Is.EqualTo(14));
            Assert.That(elementCounts[1].Total, Is.EqualTo(11));
            Assert.That(elementCounts[2].Total, Is.EqualTo(20));
            Assert.That(elementCounts[3].Total, Is.EqualTo(19));
            Assert.That(elementCounts[4].Total, Is.EqualTo(21));

        });

    }



    private ChartLongitudes CreateChart()
    {
        var points = new Dictionary<ChartPoints, double>()
        {
            { ChartPoints.Sun, 136.6 },
            { ChartPoints.Moon, 308.75 },
            { ChartPoints.Mercury, 124.6 },
            { ChartPoints.Venus, 99.95 },
            { ChartPoints.Mars, 181.09 },
            { ChartPoints.Jupiter, 103.3 },
            { ChartPoints.Saturn, 1.35 },
            { ChartPoints.Uranus, 61.12 },
            { ChartPoints.Neptune, 1.85 },
            { ChartPoints.Pluto, 302.25 },
            { ChartPoints.PersephoneCarteret, 337.6 },
            { ChartPoints.VulcanusCarteret, 84.75 },
            { ChartPoints.ApogeeMean, 225.0 },
            { ChartPoints.ApogeeCorrected, 224.1 },
            { ChartPoints.Priapus, 45.0 },
            { ChartPoints.PriapusCorrected, 44.1 },
            { ChartPoints.BlackSun, 103.55 },
            { ChartPoints.Diamond, 283.55 },
            { ChartPoints.NorthNode, 338.6 },
            { ChartPoints.SouthNode, 168.6 },
            { ChartPoints.Beast, 259.85 },
            { ChartPoints.Dragon, 79.85 },
            { ChartPoints.FortunaNoSect, 88.85 },
            { ChartPoints.Ascendant, 276.55 },
            { ChartPoints.Mc, 222.65 }
        };
        var cusps = new Dictionary<int, double>
        {
            {1, 276.55},
            {2, 327.11 },
            {3, 13.52 },
            {4, 42.65 },
            {5, 62.7 },
            {6, 79.58 },
            {7, 96.55 },
            {8, 147.11 },
            {9, 193.52 },
            {10, 222.65 },
            {11, 242.7 },
            {12, 259.58 }
        };
        
        return new ChartLongitudes(points, cusps);
    }
    
}