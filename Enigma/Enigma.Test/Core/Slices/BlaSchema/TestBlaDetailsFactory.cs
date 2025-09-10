// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestBlaDetailsFactory
{
    [Test]
    public void TestCreateBlaPointDetails_HappyFlow()
    {
        var chart = CreateChart();
        var sunDetails = BlaDetailsFactory.CreateBlaPointDetails(ChartPoints.Sun, chart);
        Assert.Multiple(() =>
        {
            Assert.That(sunDetails.Sign, Is.EqualTo(11));
            Assert.That(sunDetails.House, Is.EqualTo(12));
            Assert.That(sunDetails.Longitude, Is.EqualTo(309.1).Within(0.0001));
            Assert.That(sunDetails.MainRuledSign, Is.EqualTo(5));
            Assert.That(sunDetails.MainRuledHouses, Has.Count.EqualTo(1));
            Assert.That(sunDetails.MainRuledHouses[0], Is.EqualTo(7));
            Assert.That(sunDetails.SubRuledSign, Is.EqualTo(10));
            Assert.That(sunDetails.SubRuledHouses, Has.Count.EqualTo(1));
            Assert.That(sunDetails.SubRuledHouses[0], Is.EqualTo(12));
        });
    }

    [Test]
    public void TestCreateBlaPointDetails_NoRuler()
    {
        var chart = CreateChart();
        var uranusDetails = BlaDetailsFactory.CreateBlaPointDetails(ChartPoints.Uranus, chart);
        Assert.Multiple(() =>
        {
            Assert.That(uranusDetails.Sign, Is.EqualTo(4));
            Assert.That(uranusDetails.House, Is.EqualTo(5));
            Assert.That(uranusDetails.Longitude, Is.EqualTo(105.55).Within(0.0001));
            Assert.That(uranusDetails.MainRuledSign, Is.EqualTo(0));
            Assert.That(uranusDetails.MainRuledHouses, Has.Count.EqualTo(0));
            Assert.That(uranusDetails.SubRuledSign, Is.EqualTo(0));
            Assert.That(uranusDetails.SubRuledHouses, Has.Count.EqualTo(0));
        });
    }
    
    
    [Test]
    public void TestCreateBlaHouseDetails_HappyFlow()
    {
        var chart = CreateChart();
        var house8Details = BlaDetailsFactory.CreateBlaHouseDetails(8, chart);
        Assert.Multiple(() =>
        {
           Assert.That(house8Details.HouseNr, Is.EqualTo(8));
           Assert.That(house8Details.MainRuler, Is.EqualTo(ChartPoints.PersephoneCarteret));
           Assert.That(house8Details.SubRuler, Is.EqualTo(ChartPoints.Venus));
           Assert.That(house8Details.PointsInHouse.Count, Is.EqualTo(3));
           Assert.That(house8Details.PointsInHouse, Has.Member(ChartPoints.Beast));
           Assert.That(house8Details.PointsInHouse, Has.Member(ChartPoints.Saturn));
           Assert.That(house8Details.PointsInHouse, Has.Member(ChartPoints.Neptune));
               
        });
    }


    private ChartLongitudes CreateChart()
    {
        var points = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 309.1},
            { ChartPoints.Moon, 121.75},
            { ChartPoints.Mercury, 305.9},
            { ChartPoints.Venus, 356.0},
            { ChartPoints.Mars, 352.6},
            { ChartPoints.Jupiter, 31.95},
            { ChartPoints.Saturn, 207.25},
            { ChartPoints.Uranus, 105.55},
            { ChartPoints.Neptune, 203.85},
            { ChartPoints.Pluto, 142.3},
            { ChartPoints.VulcanusCarteret, 44.9},
            { ChartPoints.PersephoneCarteret, 265.08},
            { ChartPoints.NorthNode, 312.3},
            { ChartPoints.SouthNode, 132.3},
            { ChartPoints.Dragon, 42.3},
            { ChartPoints.Beast, 222.3},
            { ChartPoints.ApogeeMean, 154.0},
            { ChartPoints.Priapus, 334.0 },
            { ChartPoints.ApogeeCorrected, 147.6},
            { ChartPoints.PriapusCorrected , 357.6},
            { ChartPoints.BlackSun, 102.15},
            { ChartPoints.Diamond, 282.12}
        };

        var cusps = new Dictionary<int, double>
        {
            { 1, 314.74 },
            { 2, 16.4 },
            { 3, 50.1 },
            { 4, 71.1 },
            { 5, 88.33 },
            { 6, 106.7 },
            { 7, 134.74 },
            { 8, 196.4 },
            { 9, 230.1 },
            { 10, 251.1 },
            { 11, 268.33 },
            { 12, 286.7 }
        };
        return new ChartLongitudes(points, cusps);
    }
    
    
}