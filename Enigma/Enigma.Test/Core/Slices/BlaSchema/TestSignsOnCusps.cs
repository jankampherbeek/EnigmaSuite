// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestSignsOnCusps
{

    [Test] 
    public void TestSignsOnCusps_HappyFlow()
    {
        var cusps = Createcusps();
        var result = SignsOnCusps.DefineSignsOnCusps(cusps);
        Assert.Multiple(() =>
        {
            Assert.That(result[1], Is.EqualTo(4));
            Assert.That(result[2], Is.EqualTo(5));
            Assert.That(result[3], Is.EqualTo(5));
            Assert.That(result[4], Is.EqualTo(7));
            Assert.That(result[5], Is.EqualTo(8));
            Assert.That(result[6], Is.EqualTo(9));
            Assert.That(result[7], Is.EqualTo(10));
            Assert.That(result[8], Is.EqualTo(11));
            Assert.That(result[9], Is.EqualTo(11));
            Assert.That(result[10], Is.EqualTo(1));
            Assert.That(result[11], Is.EqualTo(2));
            Assert.That(result[12], Is.EqualTo(3));
        });
    }

    private Dictionary<int, double> Createcusps()
    {
        return new Dictionary<int, double>
        {
            { 1, 100.0 },
            { 2, 128.0 },
            { 3, 149.0 },
            { 4, 200.0 },
            { 5, 220.0 },
            { 6, 250.0 },
            { 7, 278.0 },
            { 8, 310.0 },
            { 9, 329.0 },
            { 10, 20.0 },
            { 11, 40.0 },
            { 12, 70.0 }
        };
    }
    
}