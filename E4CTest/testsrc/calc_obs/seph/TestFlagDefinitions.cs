// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.calc.seph;
using E4C.domain.shared.references;
using E4C.domain.shared.reqresp;
using E4C.domain.shared.specifications;
using E4C.Models.Domain;
using E4C.shared.reqresp;
using NUnit.Framework;
using System.Collections.Generic;

namespace E4CTest.calc.seph;

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
        FullChartRequest _request = CreateRequest(false, false, false);
        Assert.AreEqual(258, new FlagDefinitions().DefineFlags(_request));
    }

    [Test]
    public void TestDefineFlagsHeliocentric()
    {
        FullChartRequest _request = CreateRequest(true, false, false);
        Assert.AreEqual(266, new FlagDefinitions().DefineFlags(_request));
    }

    [Test]
    public void TestDefineFlagsTopocentric()
    {
        FullChartRequest _request = CreateRequest(false, true, false);
        Assert.AreEqual(32 * 1024 + 258, new FlagDefinitions().DefineFlags(_request));
    }

    [Test]
    public void TestDefineFlagsSidereal()
    {
        FullChartRequest _request = CreateRequest(false, false, true);
        Assert.AreEqual(64 * 1024 + 258, new FlagDefinitions().DefineFlags(_request));
    }

    [Test]
    public void TestDefineFlagsCombi()
    {
        FullChartRequest _request = CreateRequest(false, true, true);
        Assert.AreEqual(96 * 1024 + 258, new FlagDefinitions().DefineFlags(_request));
    }

    [Test]
    public void TestAddEquatorial()
    {
        int eclipticFlags = 258;
        int equatorialFlags = new FlagDefinitions().AddEquatorial(eclipticFlags);
        Assert.AreEqual(2306, equatorialFlags);
    }

    [Test]
    public void TestFlagsForMundanePositions()
    {
        FullHousesPosRequest request = CreateRequest();
        Assert.AreEqual(2, new FlagDefinitions().DefineFlags(request));
    }

    private FullChartRequest CreateRequest(bool helioCentric, bool topoCentric, bool sidereal)
    {
        double jdUt = 123456.789;
        var location = new Location("", 50.0, 10.0);
        var solSysPoints = new List<SolarSystemPoints>();
        var houseSystem = HouseSystems.Campanus;
        var zodiactType = sidereal ? ZodiacTypes.Sidereal : ZodiacTypes.Tropical;
        var ayanamsha = Ayanamshas.Fagan;
        var observerPosition = ObserverPositions.GeoCentric;
        if (helioCentric) observerPosition = ObserverPositions.HelioCentric;
        if (topoCentric) observerPosition = ObserverPositions.TopoCentric;
        var projectionType = ProjectionTypes.twoDimensional;
        SolSysPointsRequest solSysPointRequest = new(jdUt, location, solSysPoints, zodiactType, ayanamsha, observerPosition, projectionType);
        return new FullChartRequest(solSysPointRequest, houseSystem);
    }

    private FullHousesPosRequest CreateRequest()
    {
        double jdUt = 123456.789;
        var location = new Location("", 50.0, 10.0);
        var houseSystem = HouseSystems.Campanus;
        return new FullHousesPosRequest(jdUt, location, houseSystem);
    }
    

}