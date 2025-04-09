// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.LocationAndTimeZones;
using Enigma.Facades.Se;

namespace Enigma.Test.Core.LocationAndTimeZones;

[TestFixture]
public class TestDayDefinition
{
    [Test]
    [TestCase(2025, 2, "6>=1", 2)]      
    [TestCase(2026, 5, "5>=2", 2)]
    [TestCase(2026, 5, "5>=3", 9)]
    [TestCase(2025, 2, "last2", 26)]   
    public void TestDayDefHandlerHappyFlow(int year, int month, string definition, int expected)
    {
        IJulDayFacade facade = new JulDayFacade();
        IDayDefHandler handler = new DayDefHandler(facade);
        var result = handler.DayFromDefinition(year, month, definition);
        Assert.That(result, Is.EqualTo(expected));
    }
}