// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Test.Core.Handlers.Calc.CelestialPoints.Helpers;

[TestFixture]
public class TestSeFlags
{
    private readonly ISeFlags _seFlags = new SeFlags();

    [Test]
    public void TestDefaultFlags()
    {
        int expectedFlags = 258;
        int actualFlags = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical);
        Assert.That(actualFlags, Is.EqualTo(expectedFlags));
    }

    [Test]
    public void TestFlagsEquatorial()
    {
        int expectedFlags = 2306;
        int actualFlags = _seFlags.DefineFlags(CoordinateSystems.Equatorial, ObserverPositions.GeoCentric, ZodiacTypes.Tropical);
        Assert.That(actualFlags, Is.EqualTo(expectedFlags));
    }

    [Test]
    public void TestFlagsHelioCentric()
    {
        int expectedFlags = 266;
        int actualFlags = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.HelioCentric, ZodiacTypes.Tropical);
        Assert.That(actualFlags, Is.EqualTo(expectedFlags));
    }

    [Test]
    public void TestFlagsTopoCentric()
    {
        int expectedFlags = 32 * 1024 + 2 + 256;
        int actualFlags = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.TopoCentric, ZodiacTypes.Tropical);
        Assert.That(actualFlags, Is.EqualTo(expectedFlags));
    }

    [Test]
    public void TestFlagsSidereal()
    {
        int expectedFlags = 64 * 1024 + 2 + 256;
        int actualFlags = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Sidereal);
        Assert.That(actualFlags, Is.EqualTo(expectedFlags));
    }

    [Test]
    public void TestCombinedFlags()
    {
        int expectedFlags = 2 + 256 + 2048 + 32 * 1024 + 64 * 1024;
        int actualFlags = _seFlags.DefineFlags(CoordinateSystems.Equatorial, ObserverPositions.TopoCentric, ZodiacTypes.Sidereal);
        Assert.That(actualFlags, Is.EqualTo(expectedFlags));
    }

}
