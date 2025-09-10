// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestHousePositions
{

    [Test]
    public void TestDefineHousePositions_HappyFlow()
    {
        var chart = CreateTestChartWithNormalHouses();
        var result = HousePositions.DefineHousePositions(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(7)); 
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(7)); 
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(3));
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(7));
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(8));      // on cusp
            Assert.That(result[ChartPoints.Mars], Is.EqualTo(9));       // overflow
            Assert.That(result[ChartPoints.Jupiter], Is.EqualTo(9));    // overflow
            Assert.That(result[ChartPoints.Saturn], Is.EqualTo(11));
        });
    }
    

    [Test]
    public void TestDefineHousePositions_EmptyChart()
    {
        // Test with empty chart
        var chart = CreateEmptyChart();
        
        var result = HousePositions.DefineHousePositions(chart);
        
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestDefineHousePositions_NoCusps()
    {
        // Test chart with only celestial bodies, no cusps
        var chart = CreateChartWithNoCusps();
        
        var result = HousePositions.DefineHousePositions(chart);
        
        Assert.That(result, Is.Empty);
    }

  
    

    private static ChartLongitudes CreateTestChartWithNormalHouses()
    {
        var cusps = new Dictionary<int, double>
        {
            { 1, 100.0 },
            { 2, 130.0 },
            { 3, 160.0 },
            { 4, 190.0 },
            { 5, 220.0 },
            { 6, 250.0 },
            { 7, 280.0 },
            { 8, 310.0 },
            { 9, 340.0 },
            { 10, 10.0 },
            { 11, 40.0 },
            { 12, 70.0 }
        };
        var points = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 302.0 }, // House 7
            { ChartPoints.Moon, 169.0 }, // House 3
            { ChartPoints.Mercury, 290.0 }, // House 7
            { ChartPoints.Venus, 310.0 }, // House 8, on cusp
            { ChartPoints.Mars, 1.0 }, // House 9, just after zero Aries
            { ChartPoints.Jupiter, 359.9 }, // House 9, just before zero Aries
            { ChartPoints.Saturn, 69.999999 }, // House 11, just before cusp 12
        };
        return new ChartLongitudes(points, cusps);
    }


    private static ChartLongitudes CreateEmptyChart()
    {
        var points = new Dictionary<ChartPoints, double>();
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateChartWithNoCusps()
    {
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 15.0 },
            { ChartPoints.Moon, 45.0 }
        };
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(positions, cusps);
    }
    
}
