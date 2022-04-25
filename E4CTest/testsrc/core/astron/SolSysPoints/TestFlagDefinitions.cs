// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Astron.SolSysPoints;
using E4C.Shared.References;
using NUnit.Framework;

namespace E4CTest.Core.Astron.SolSysPoints;

[TestFixture]
public class TestFlagDefinitions
{
    // relevant values:
    // SEFLG_SWIEPH       2L              // use SWISSEPH ephemeris, default
    // SEFLG_HELCTR       8L              // return heliocentric position
    // SEFLG_SPEED        256L            // high precision speed (analyt. comp.)
    // SEFLG_EQUATORIAL   2048L           // equatorial positions are wanted
    // SEFLG_TOPOCTR      (32*1024L)      // topocentric positions
    // SEFLG_SIDEREAL     (64*1024L)      // sidereal positions

    // Flag is always 2L || 256L
    // Variables: heliocentric, topocentric, sidereal

    [Test]
    public void TestDefineFlagsDefault()
    {
        Assert.AreEqual(258, new FlagDefinitions().DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical));
    }

    [Test]
    public void TestDefineFlagsHeliocentric()
    {
        Assert.AreEqual(266, new FlagDefinitions().DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.HelioCentric, ZodiacTypes.Tropical));
    }

    [Test]
    public void TestDefineFlagsTopocentric()
    {
        Assert.AreEqual(32 * 1024 + 258, new FlagDefinitions().DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.TopoCentric, ZodiacTypes.Tropical));
    }

    [Test]
    public void TestDefineFlagsSidereal()
    {
        Assert.AreEqual(64 * 1024 + 258, new FlagDefinitions().DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Sidereal));
    }

    [Test]
    public void TestDefineFlagsCombi()
    {
        Assert.AreEqual(96 * 1024 + 258, new FlagDefinitions().DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.TopoCentric, ZodiacTypes.Sidereal));
    }



}