// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.LocationAndTimeZones;
using Enigma.Facades.Se;

namespace Enigma.Test.Core.LocationAndTimeZones;

[TestFixture]
public class TestDstParser
{
    [Test]
    public void TestDstParserNeth(){
        IJulDayFacade facade = new JulDayFacade();
        IDayDefHandler handler = new DayDefHandler(facade);
        IDstParser parser = new DstParser(facade, handler);
        const int expectedCount = 12;
        const string expectedLetter = "NST";
        const double expectedOffset = 1.0;
        var dstLines = new List<string>
        {
            "Neth;1916;1916;5;1;0;00;0;1;00;0;NST",
            "Neth;1916;1916;10;1;0;00;0;0;0;0;AMT",
            "Neth;1917;1917;4;16;2;00;0;1;00;0;NST",
            "Neth;1917;1917;9;17;2;00;0;0;0;0;AMT",
            "Neth;1918;1921;4;0>=1;2;00;0;1;00;0;NST",
            "Neth;1918;1921;9;last0;2;00;0;0;0;0;AMT"
        };
        var result = parser.ProcessDstLines(dstLines);
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(expectedCount));
            Assert.That(result[0]?.Letter, Is.EqualTo(expectedLetter));
            Assert.That(result[0]?.Offset, Is.EqualTo(expectedOffset).Within(1E-8));
        });
    }
}