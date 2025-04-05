// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.LocationAndTimeZones;

namespace Enigma.Test.Core.LocationAndTimeZones;

// This test uses production data for timezone handling.
// This data is pretty constant but might change for each new release.

[TestFixture]
public class TestTimeZoneReader
{
    [Test]
    public void TestReadLinesForTzIndicationNumberOfLines()
    {
        /* Expected data:
            Zone;Europe/Belfast;-0;23;40;LMT;1880;8;2
            -0;25;21;-;DMT;1916;5;21;2;00;0
            -0;25;21;1:00;IST;1916;10;1;2;00s;0
            0;00;0;GB-Eire;%s;1968;10;27;0;0;0
            1;00;0;-;BST;1971;10;31;2;00u;0
            0;00;0;GB-Eire;%s;1996;1;1;0;0;0
            0;00;0;EU;GMT/BST;0;1;1;0;0;0
        */
        var reader = new TimeZoneReader();
        const string tzIndication = "Europe/Belfast";
        var tzLines = reader.ReadLinesForTzIndication(tzIndication);
        Assert.That(tzLines, Has.Count.EqualTo(7));
    }

    [Test]
    public void TestReadLinesForTzIndicationContent()
    {
        var reader = new TimeZoneReader();
        const string tzIndication = "Europe/Belfast";
        var tzLines = reader.ReadLinesForTzIndication(tzIndication);
        var linesArray = tzLines.ToArray();
        Assert.That(linesArray[0], Is.EqualTo("Zone;Europe/Belfast;-0;23;40;LMT;1880;8;2"));
        Assert.That(linesArray[4], Is.EqualTo("1;00;0;-;BST;1971;10;31;2;00u;0"));
    }
    
    [Test]
    public void TestReadLinesForTzIndication_EmptyTzIndication()
    {
        var reader = new TimeZoneReader();
        Assert.Throws<ArgumentException>(() => reader.ReadLinesForTzIndication(""));
    }
    
    [Test]
    public void TestReadLinesForTzIndication_NonexistentTz()
    {
        var reader = new TimeZoneReader();
        var result = reader.ReadLinesForTzIndication("Nonexistent/Zone");
        Assert.That(result, Is.Empty);
    }
    
    
    
    
    
}