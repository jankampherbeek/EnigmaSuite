// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core;
using E4C.Core.Api.Astron;
using E4C.Core.Api.Datetime;
using E4C.Core.Facades;
using E4C.Core.Shared.Domain;
using E4C.Shared.Domain;
using E4C.Shared.References;
using E4C.Shared.ReqResp;
using NUnit.Framework;
using System.Collections.Generic;

namespace E4CIt.FullChart;

//[TestFixture] 
public class ChartAllPositionsIntegrationTest
{
    private ApiFactory _apiFactory = new ApiFactory();
    private IChartAllPositionsApi _allChartPositionsApi;
    private readonly string _ephemerisPath = "./se";

  //  [Test]
    public void TestCalculationJulianDayUt()
    {
        SeInitializer.SetEphePath(_ephemerisPath);
        double delta = 0.00000001;
        double expectedJdUt = 2452275.5;
        SimpleDateTime simpleDateTime = new(2002, 1, 1, 0.0, Calendars.Gregorian);   
        IJulianDayApi julianDayApi = _apiFactory.GetJulianDayApi();
        JulianDayRequest request = new(simpleDateTime);
        JulianDayResponse jdResponse = julianDayApi.getJulianDay(request);
        Assert.IsTrue(jdResponse.Success);
        Assert.AreEqual("", jdResponse.ErrorText);
        Assert.AreEqual(expectedJdUt, jdResponse.JulDay, delta);
        SeInitializer.CloseEphemeris();
    }




   // [Test]
    public void TestPositions()
    {
        SeInitializer.SetEphePath(_ephemerisPath);
        double delta = 0.000417;   // 1.5 second of arc
        _allChartPositionsApi = _apiFactory.GetChartAllPositionsApi();
        ChartAllPositionsRequest request = CreateChartAllPositionsRequest();
        ChartAllPositionsResponse response = _allChartPositionsApi.getChart(request);

        Assert.That(response, Is.Not.Null);
        Assert.IsTrue(response.Success);
        Assert.AreEqual("", response.ErrorText);


        /* Positions from astro.com
         *   Celpoint   Long             Speed      Lat         Decl          Long dec      Speed dec    Lat dec     Decl dec
         *  Sun         10°23' 1" CP     1° 1' 8"   0° 0' 1" N  23° 1'58" S   280.38361111   1.01888889  0.00027778  -23.03277778
            Moon         1° 7' 3" LE    14°30' 3"   2°56'48" N  22°47' 1" N   121.1175      14.50083333  2.94666667   22.78361111  
          C Mercury j   25°35' 6" CP     1°32'50"   2° 3' 8" S  23° 2'29" S
          D Venus j      7° 9'41" CP     1°15'30"   0°24' 6" S  23°38'48" S
          E Mars l      16°52'53" PI       43'55"   0°42' 4" S   5°49'32" S
          F Jupiter d   10°40' 0" CN R    - 8' 8"   0° 0' 9" N  23° 0'46" N
          G Saturn c     9°19'26" GE R    - 3'49"   1°48'20" S  20° 3'48" N
          O Uranus k    22°27'30" AQ        2'47"   0°42' 8" S  14°41'28" S
          I Neptune k    7°26'40" AQ        2' 5"   0° 6'19" N  18°18'29" S
          J Pluto i     16° 2'13" SA        2' 9"   9°45'43" N  12°59'42" S   256.03694444   0.03583333  9.76194444  -12.995
          K Mean Node c 26°21'26" GE R    - 3'10"   0° 0' 0" N  23°23'20" N
          L True Node c 27°10'13" GE      - 1'30"   0° 0' 0" N  23°24'32" N
          N Chiron       2° 9'53" CP        6'26"   5°38'39" N  17°46'40" S   242.16472222   0.10722222  5.64416667  -17.77777778
        */




        // Sun
        Assert.AreEqual(280.38361111, response.SolarSystemPointPositions[0].Longitude.Position, delta);
        Assert.AreEqual(1.01888889, response.SolarSystemPointPositions[0].Longitude.Speed, delta);
        Assert.AreEqual(0.00027778, response.SolarSystemPointPositions[0].Latitude.Position, delta);
        Assert.AreEqual(-23.03277778, response.SolarSystemPointPositions[0].Declination.Position, delta);
        // Moon
        Assert.AreEqual(121.1175, response.SolarSystemPointPositions[1].Longitude.Position, delta);
        Assert.AreEqual(14.50083333, response.SolarSystemPointPositions[1].Longitude.Speed, delta);
        Assert.AreEqual(2.94666667, response.SolarSystemPointPositions[1].Latitude.Position, delta);
        Assert.AreEqual(22.78361111, response.SolarSystemPointPositions[1].Declination.Position, delta);
        // Pluto
        Assert.AreEqual(256.03694444, response.SolarSystemPointPositions[9].Longitude.Position, delta);
        Assert.AreEqual(0.03583333, response.SolarSystemPointPositions[9].Longitude.Speed, delta);
        Assert.AreEqual(9.76194444, response.SolarSystemPointPositions[9].Latitude.Position, delta);
        Assert.AreEqual(-12.995, response.SolarSystemPointPositions[9].Declination.Position, delta);
        // Chiron
        Assert.AreEqual(242.16472222, response.SolarSystemPointPositions[11].Longitude.Position, delta);


        SeInitializer.CloseEphemeris();
    }





    private ChartAllPositionsRequest CreateChartAllPositionsRequest()
    {
        return new ChartAllPositionsRequest(CreateSolSysPointsRequest(), HouseSystems.Placidus);
    }


    private SolSysPointsRequest CreateSolSysPointsRequest()
    {
        double jdUt = 2452275.5;  // 2002-1-1 0:00 UT
        Location location = new("Anywhere", 6.0, 52.0);
        List<SolarSystemPoints> points = new()
        {
            SolarSystemPoints.Sun,
            SolarSystemPoints.Moon,
            SolarSystemPoints.Mercury,
            SolarSystemPoints.Venus,
            SolarSystemPoints.Mars,
            SolarSystemPoints.Jupiter,
            SolarSystemPoints.Saturn,
            SolarSystemPoints.Uranus,
            SolarSystemPoints.Neptune,
            SolarSystemPoints.Pluto,
            SolarSystemPoints.TrueNode,
            SolarSystemPoints.Chiron
           

        };
        ZodiacTypes zodiacType = ZodiacTypes.Tropical;
        Ayanamshas ayanamsha = Ayanamshas.Fagan;
        ObserverPositions observerPosition = ObserverPositions.GeoCentric;
        ProjectionTypes projectionType = ProjectionTypes.twoDimensional;
        return new SolSysPointsRequest(jdUt, location, points, zodiacType, ayanamsha, observerPosition, projectionType);
    }



}