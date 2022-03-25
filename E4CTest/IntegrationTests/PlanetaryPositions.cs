// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using E4C.Models.Astron;
using E4C.Models.SeFacade;
using System.Collections.Generic;
using NUnit.Framework;

namespace E4CIntegrationTest.PlanetaryPositions
{
    /* Test info
     * Positions are calculated for March 14, 2022, 21:31 UT, location 6E54 52N13
     * Julian day for ET : 2459653.397330  --> 2459653.396527778
     * True obliquity: 23°26'16.7794
     * Geographic longitude in location: 6.9
     * Geographic latitude in location: 52.2166666667;
     * 
     * Planet Positions from Swiss Ephemeris 

14.3.2022      21:31:00 UT
 delta t: 69.276833 sec jd = 2459653.396527778 (incl delta T), no delta T --> 2459653.3957259628

Ecl. obl.         23°26'16.7794   23°26'11.0084    (true, mean)
Nutation          -0° 0'13.4190    0° 0' 5.7709    (dpsi, deps)

               ecl. long.       ecl. lat.       dist.          speed          house
Sun         24 pi 16'38.5327    0° 0' 0.3582    0.994240647    0°59'47.8393   5.0530
Moon        13 le 22' 6.6877    4°58'36.1316    0.002643012   12°29'33.5701   9.8815
Mercury      7 pi 47'29.9064   -2°13'52.8767    1.288359940    1°39'34.5375   4.5239
Venus        7 aq 51'38.4638    2°38'24.0424    0.634994930    0°56'58.9766   3.6616
Mars         6 aq 27'23.7243   -1° 0'26.1482    1.909728000    0°44'58.6509   3.6673
Jupiter     17 pi 13'24.5469   -0°59'12.0547    5.963781098    0°14'27.4554   4.8136
Saturn      20 aq 20'14.0968   -0°54'30.7289   10.713446259    0° 6'22.8096   4.0246
Uranus      12 ta  4'55.1277   -0°22'21.6506   20.366506202    0° 2'34.1609   7.2177
Neptune     22 pi 55'36.0263   -1° 7'11.6610   30.912569271    0° 2'16.6872   5.00531403
Pluto       28 cp  6' 5.0861   -1°51'14.7979   35.025495695    0° 1'15.4878   3.4708
mean Node   25 ta 40'14.7074    0° 0' 0.0000    0.002569555   -0° 3'10.6591   7.7166
true Node   24 ta 31'38.4189    0° 0' 0.0000    0.002694854   -0° 9' 8.2738   7.6776
mean Apogee 26 ge 33' 3.6334    2°38'45.9284    0.002710625    0° 6'39.9008   8.6992
osc. Apogee 18 ge 24' 7.7112    2° 3'26.5572    0.002708392   -2°50'53.1566   8.4655

     * 
     * 
     * 
     * Expected positions:
     * Sol Sys Point   tropical/geocentric                                                   
     *                     longitude        latitude      speed-long     distance      right ascension declination azimuth  altitude  
     * Sun                 354.2773701944   0.0000995000   0.9966220278   0.994240647
     * Moon                133.3685243611   4.9767032222  12.4926583611   0.002643012
     * Mercury             337.7916406667  -2.2313546389   1.6595937500   1.288359940
     * Venus               307.8606843889   2.6400117778   0.9497157222   0.634994930
     * Mars                306.4565900833  -1.0007263389   0.7496252500   1.909728000 
     * Jupiter             347.2234852500  -0.9866818611   0.2409598333   5.963781098
     * Saturn              320.3372491111  -0.9085358056   0.1063360000  10.713446259
     * Uranus               42.0819799167  -0.3726807222   0.0428224722  20.366506202
     * Neptune             352.9266739722  -1.1199058333   0.0379686667  30.912569271
     * Pluto               298.1014128056  -1.8541105278   0.0209688333  35.025495695
     * Chiron
     * Mean node
     * True node
     * Persephone (Ram)    Calculated: value is zero......
     * Hermes (Ram)        
     * Demeter (Ram)       Calc value +		Longitude { Position = 244,9801927488031, Speed = -0,0039933578161804656 }}
     * Cupido (Uranian)    Calculated for longitude: {PosSpeed { Position = 275,4042224323975, Speed = 0,008921906864882694 }} OK
     * Hades (Uranian)
     * Zeus (Uranian)
     * Kronos (Uranian)
     * Apollon (Uranian)
     * Admetos (Uranian)
     * Vulcanus (Uranian)
     * Poseidon (Uranian)
     * Eris
     * 
     * Sol Sys point   heliocentric longitude latitude
     * 
     * Sol sys point   Topocentric
     *                 longitude  latitude right ascension declination azimuth  altitude 
     * 
     * Sol sys point   Sidereal
     *                 longitude   latitude   declination
     *                 
     *                 
     * Sol sys point   Oblique longitude
     * 
     * 
     */



    [TestFixture]
    public class TestPositionsTropical
    {

        [Test]
        public void TestEclipticalPositionsStandardPlanets()
        {
          

               
            double delta = 0.00000001;

           /*     FullChartResponse response = DefinePositionsTropical();
                FullSolSysPointPos solSysPointPos = response.SolarSystemPointPositions[0];
                double longitudeSun = solSysPointPos.Longitude.Position;
                bool closeEnough = System.Math.Abs(longitudeSun - 354.2773701944) <delta;
               Assert.IsTrue(closeEnough);
               Assert.AreEqual(354.2773701944, longitudeSun, delta);
           */

            // value found:    354.27816734551715
            // diff 0.000797151, about 1.15 minutes, 1m 9 sec, this is delta T!
            // after subtracting deltaT found value: 354.27737143287226 
            // diff is now 0.00000123847 or 0.004 seconds of arc. 
            
            /*
            Assert.AreEqual(0.9966220278, solSysPointPos.Longitude.Speed, delta);
            Assert.AreEqual(0.0000995000, solSysPointPos.Latitude.Position, delta);
            */
        }

        private FullChartResponse DefinePositionsTropical()
        {
            SeInitializer.SetEphePath("../E4C/se");
            var factory = new RequestFactory();
            IHorizontalCoordinatesFacade horCoordFacade = new HorizontalCoordinatesFacade();
            ISePosCelPointFacade posCelPointFacade = new SePosCelPointFacade();
            ISolarSystemPointSpecifications solSysPointSpecs = new SolarSystemPointSpecifications();
            IPositionSolSysPointCalc solSysPointCalc = new PositionSolSysPointCalc(posCelPointFacade, horCoordFacade, solSysPointSpecs);
            IHouseSystemSpecifications houseSystemSpecifications = new HouseSystemSpecifications();
            ISePosHousesFacade posHousesFacade = new SePosHousesFacade();
            ICoordinateConversionFacade posCoordinateConversionFacade = new CoordinateConversionFacade();
            IPositionsMundane posMundane = new PositionsMundane(posHousesFacade, posCoordinateConversionFacade, horCoordFacade, houseSystemSpecifications);
            IObliquityNutationCalc oblNutCalc = new ObliquityNutationCalc(posCelPointFacade);
            IFlagDefinitions flagdefs = new FlagDefinitions();
            IAyanamshaSpecifications ayanamshaSpecs = new AyanamshaSpecifications();
            IFullChartCalc fullChartCalc = new FullChartCalc(oblNutCalc, posMundane, solSysPointCalc, flagdefs, ayanamshaSpecs);
            FullChartRequest request = factory.CreateFullChartRequest("tropicalstandard");
            return fullChartCalc.CalculateFullChart(request);

        }
    }


    public class RequestFactory
    {
        public FullChartRequest CreateFullChartRequest(string requestType)
        {
            double julianDay = 2459653.397330 - 0.00079861111;   // add delta-T
            var location = new Location("LocationName", 6.9, 52.2166666667);
            var zodiacType = ZodiacTypes.Tropical;
            var observerPos = ObserverPositions.GeoCentric;
            var ayanamsha = Ayanamshas.Fagan;
            var projectionType = ProjectionTypes.twoDimensional;
            var houseSystem = HouseSystems.NoHouses;
            var solSysPoints = new List<SolarSystemPoints>(){SolarSystemPoints.Sun, SolarSystemPoints.Moon, SolarSystemPoints.DemeterRam, SolarSystemPoints.HermesRam};

        //    if (requestType == "tropicalstandard")
        //    {
        //        // do nothing
        //    }
            return new FullChartRequest(julianDay, location, solSysPoints, houseSystem, zodiacType, ayanamsha, observerPos, projectionType);
        }
    }

}