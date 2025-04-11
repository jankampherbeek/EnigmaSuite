// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.LocationAndTimeZones;

namespace Enigma.Test.Core.LocationAndTimeZones;

[TestFixture]
public class TestDstLineReader
{
    private readonly IDstLineReader _reader = new DstLineReader();
    
    [Test]
    public void TestReadDstLinesMatchingRule_Count()
    {
        /* Expected data:
            Nic;1979;1980;3;6>=1;0;00;0;1;00;0;D
            Nic;1979;1980;6;0>=2;0;00;0;0;0;0;S
            Nic;2005;2005;4;10;0;00;0;1;00;0;D
            Nic;2005;2005;10;6>=1;0;00;0;0;0;0;S
            Nic;2006;2006;4;30;2;00;0;1;00;0;D
            Nic;2006;2006;10;6>=1;1;00;0;0;0;0;S
         */
        const string rule = "Nic";
        var expected = 6;
        var result = _reader.ReadDstLinesMatchingRule(rule);
        Assert.That(result, Has.Count.EqualTo(expected));   
    }

    [Test]
    public void TestReadDstLinesMatchingRule_Content()
    {
        const string rule = "Nic";
        const string expected = "Nic;1979;1980;6;0>=2;0;00;0;n;0;0;0;S";
        var lines = _reader.ReadDstLinesMatchingRule(rule);
        Assert.That(lines[1], Is.EqualTo(expected));
    }

    [Test]
    public void TestReadDstLinesMatchingRule_NoRule()
    {
        const string rule = "";
        Assert.Throws<ArgumentException>(() => _reader.ReadDstLinesMatchingRule(rule));
    }

    [Test]
    public void TestReadDstLinesMatchingRule_UnknownRule()
    {
        var result = _reader.ReadDstLinesMatchingRule("Nonexistent/Rule");
        Assert.That(result, Is.Empty);
    }
    
}