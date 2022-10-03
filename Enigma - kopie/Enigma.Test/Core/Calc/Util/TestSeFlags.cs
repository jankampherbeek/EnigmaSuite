// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;

namespace Enigma.Core.Calc.Util;

[TestFixture]
public class TestSeFlags
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

    private readonly ISeFlags _seFlags = new SeFlags();


    [Test]
    public void TestFlagsWithoutSpecifics()
    {
        // should only contain 2 (using SE) and 256 (using speed) = 258.
        int flags = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical);
        Assert.That(flags, Is.EqualTo(258));
    }

    [Test]
    public void TestFlagsForEquatorial()
    {
        // 2 (SE), 256 (speed) and 2048 (equatorial) = 2306.
        int flags = _seFlags.DefineFlags(CoordinateSystems.Equatorial, ObserverPositions.GeoCentric, ZodiacTypes.Tropical);
        Assert.That(flags, Is.EqualTo(2306));
    }

    [Test]
    public void TestFlagsForHelioCentric()
    {
        // 2 (SE), 256 (speed) and 8 (heliocentric) = 266.
        int flags = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.HelioCentric, ZodiacTypes.Tropical);
        Assert.That(flags, Is.EqualTo(266));
    }

    [Test]
    public void TestFlagsForTopoCentric()
    {
        // 2 (SE), 256 (speed) and 32*1024 (topocentric) = 33026.
        int flags = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.TopoCentric, ZodiacTypes.Tropical);
        Assert.That(flags, Is.EqualTo(33026));
    }

    [Test]
    public void TestFlagsForSidereal()
    {
        // 2 (SE), 256 (speed) and 64*1024 (sidereal) = 65794.
        int flags = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Sidereal);
        Assert.That(flags, Is.EqualTo(65794));
    }

    [Test]
    public void TestFlagsForHelioCentricAndSidereal()
    {
        // 2 (SE), 256 (speed), 8 (heliocentric) and 64*1024 (sidereal) = 65802.
        int flags = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.HelioCentric, ZodiacTypes.Sidereal);
        Assert.That(flags, Is.EqualTo(65802));
    }

    [Test]
    public void TestFlagsForTopoCentricAndSidereal()
    {
        // 2 (SE), 256 (speed), 32*1024 (topocentric) and 64*1024 (sidereal) = 98562.
        int flags = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.TopoCentric, ZodiacTypes.Sidereal);
        Assert.That(flags, Is.EqualTo(98562));
    }


}