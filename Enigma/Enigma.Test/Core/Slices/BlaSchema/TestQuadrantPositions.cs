// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestQuadrantPositions
{
    [Test]
    public void TestDefineQuadrantPositions_HappyFlow()
    {
        var houseDetails = DefineHouseDetails();
        var quadrantCounts = QuadrantPositions.DefineQuadrants(houseDetails);
        Assert.Multiple(() =>
        {
            Assert.That(quadrantCounts, Has.Count.EqualTo(4));
            Assert.That(quadrantCounts[1], Is.EqualTo(7));
            Assert.That(quadrantCounts[2], Is.EqualTo(5));
            Assert.That(quadrantCounts[3], Is.EqualTo(6));
            Assert.That(quadrantCounts[4], Is.EqualTo(5));
        });
    }
    
    private List<BlaHouseDetails> DefineHouseDetails()
    {
        var zeroPoints = new List<ChartPoints>();
        var pointsIn1 = new List<ChartPoints>
        {
            ChartPoints.PriapusCorrected, ChartPoints.Priapus, ChartPoints.Mars, ChartPoints.Venus
        };
        var pointsIn2 = new List<ChartPoints>
        {
            ChartPoints.Jupiter, ChartPoints.Dragon, ChartPoints.VulcanusCarteret
        };
        var pointsIn5 = new List<ChartPoints>
        {
            ChartPoints.BlackSun, ChartPoints.Uranus
        };
        var pointsIn6 = new List<ChartPoints>
        {
            ChartPoints.Moon, ChartPoints.FortunaNoSect, ChartPoints.SouthNode
        };
        var pointsIn7 = new List<ChartPoints>
        {
            ChartPoints.Pluto, ChartPoints.ApogeeCorrected, ChartPoints.ApogeeMean
        };
        var pointsIn8 = new List<ChartPoints>
        {
            ChartPoints.Neptune, ChartPoints.Saturn, ChartPoints.Beast
        };
        var pointsIn10 = new List<ChartPoints>
        {
            ChartPoints.PersephoneCarteret
        };
        var pointsIn11 = new List<ChartPoints>
        {
            ChartPoints.Diamond
        };
        var pointsIn12 = new List<ChartPoints>
        {
            ChartPoints.Mercury, ChartPoints.Sun, ChartPoints.NorthNode
        };
        
        return
        [
            new BlaHouseDetails(1, 11, ChartPoints.Priapus, ChartPoints.Moon, pointsIn1),
            new BlaHouseDetails(2, 1, ChartPoints.Mars, ChartPoints.Pluto, pointsIn2),
            new BlaHouseDetails(3, 2, ChartPoints.Venus, ChartPoints.PersephoneCarteret, zeroPoints),
            new BlaHouseDetails(4, 3, ChartPoints.Mercury, ChartPoints.VulcanusCarteret, zeroPoints),
            new BlaHouseDetails(5, 3, ChartPoints.Mercury, ChartPoints.VulcanusCarteret, pointsIn5),
            new BlaHouseDetails(6, 4, ChartPoints.Moon, ChartPoints.Priapus, pointsIn6),
            new BlaHouseDetails(7, 5, ChartPoints.Sun, ChartPoints.ApogeeMean, pointsIn7),
            new BlaHouseDetails(8, 7, ChartPoints.PersephoneCarteret, ChartPoints.Venus, pointsIn8),
            new BlaHouseDetails(9, 8, ChartPoints.Pluto, ChartPoints.Mars, zeroPoints),
            new BlaHouseDetails(10, 9, ChartPoints.Jupiter, ChartPoints.Neptune, pointsIn10),
            new BlaHouseDetails(11, 9, ChartPoints.Jupiter, ChartPoints.Neptune, pointsIn11),
            new BlaHouseDetails(12, 10, ChartPoints.ApogeeMean, ChartPoints.Sun, pointsIn12)
        ];
    }
}