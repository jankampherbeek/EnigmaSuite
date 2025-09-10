// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestHouseCusps
{

    [Test]
    public void TestHouseCuspsHappyFlow()
    {
        var houseCounts = DefineHouseCounts();
        var signsOnCups = DefineSignsOnCusps();
        var result = HouseCusps.DefineHouseCusps(houseCounts, signsOnCups);
        Assert.Multiple(() =>
        {
            Assert.That(result[1], Is.EqualTo(0));  // sign 1 cusp 2
            Assert.That(result[2], Is.EqualTo(3)); // sign 2 cusp 3 
            Assert.That(result[3], Is.EqualTo(0)); // sign 3 cusp 4
            Assert.That(result[4], Is.EqualTo(3)); // sign 4 cusp 5 and 6
            Assert.That(result[5], Is.EqualTo(1)); // sign 5 cusp 7
            Assert.That(result[6], Is.EqualTo(0)); // sign 6 intercepted 
            Assert.That(result[7], Is.EqualTo(0)); // sign 7 cusp 8
            Assert.That(result[8], Is.EqualTo(4)); // sign 8 cusp 9
            Assert.That(result[9], Is.EqualTo(1)); // sign 9 cusp 10
            Assert.That(result[10], Is.EqualTo(2)); // sign 10 cusp 11 and 12
            Assert.That(result[11], Is.EqualTo(1)); // sign 11 cusp 1
            Assert.That(result[12], Is.EqualTo(0)); // sign 12 intercepted
        });
    }
    
    private static Dictionary<int, int> DefineHouseCounts()
    {
        var houseCounts = new Dictionary<int, int>()
        {
            {1, 1},
            {2, 0 },
            {3, 3 },
            {4, 0 },
            {5, 2 },
            {6, 1 },
            {7, 1 },
            {8, 0 },
            {9, 4 },
            {10, 1 },
            {11, 2 },
            {12, 0 }       
        };
        return houseCounts;
    }

    private static Dictionary<int, int> DefineSignsOnCusps()
    {
        var signsOnCusp = new Dictionary<int, int>()
        {
            { 1, 11 },
            { 2, 1 },
            { 3, 2 },
            { 4, 3 },
            { 5, 4 },
            { 6, 4 },
            { 7, 5 },
            { 8, 7 },
            { 9, 8 },
            { 10, 9 },
            { 11, 10 },
            { 12, 10 }
        };
        return signsOnCusp;
    }
    
    
}