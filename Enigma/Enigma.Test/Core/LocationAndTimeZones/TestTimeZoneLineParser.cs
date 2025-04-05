// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.LocationAndTimeZones;
using Enigma.Facades.Se;

namespace Enigma.Test.Core.LocationAndTimeZones;

[TestFixture]
public class TestTimeZoneLineParser
{
    [Test]
    public void TestParseTzLinesCount()
    {
        var parser = new TimeZoneLineParser(new JulDayFacade());
        var expected = 8;
        var result = parser.ParseTzLines(CreateZoneLines(), "Asia/Hanoi");
        Assert.That(result, Has.Count.EqualTo(expected));
    }
    
    [Test]
    public void TestParseTzLinesContent()
    {
        var parser = new TimeZoneLineParser(new JulDayFacade());
        var expected = new TzLine
        {
            Name = "Asia/Hanoi",
            StdOff = 9.0,
            Rules = "-",
            Format = "+09",
            Until = 2431700.875
        };
        var result = parser.ParseTzLines(CreateZoneLines(), "Asia/Hanoi");
        Assert.That(result.ToArray()[4], Is.EqualTo(expected));
    }
    
    [Test]
    public void TestParseTzLinesHeader()
    {
        var parser = new TimeZoneLineParser(new JulDayFacade());
        var expected = new TzLine
        {
            Name = "Asia/Hanoi",
            StdOff = 7.0 + 3.0/60.0 + 24.0/3600.0,
            Rules = "-",
            Format = "LMT",
            Until = 2417392.5
        };
        var result = parser.ParseTzLines(CreateZoneLines(), "Asia/Hanoi");
        Assert.That(result.ToArray()[0], Is.EqualTo(expected));
    }
    
    [Test]
    public void TestParseTzLines_EmptyName()
    {
        var parser = new TimeZoneLineParser(new JulDayFacade());
        Assert.Throws<ArgumentNullException>(() => parser.ParseTzLines(CreateZoneLines(), ""));
    }
    
    
    
    private static List<string> CreateZoneLines()
    {
        return
        [
            "Zone;Asia/Hanoi;7;03;24;LMT;1906;7;1",
            "7;06;30;-;PLMT;1911;5;1;0;0;0",
            "7;00;0;-;+07;1942;12;31;23;00;0",
            "8;00;0;-;+08;1945;3;14;23;00;0",
            "9;00;0;-;+09;1945;9;2;0;0;0",
            "7;00;0;-;+07;1947;4;1;0;0;0",
            "8;00;0;-;+08;1954;10;1;0;0;0",
            "7;00;0;-;+07;0;1;1;0;0;0"
        ];
    }
    
}