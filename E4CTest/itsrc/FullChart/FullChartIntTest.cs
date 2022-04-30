// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Api.Astron;
using E4C.Core.Astron.ChartAllPositions;
using E4C.Core.Astron.CoordinateConversion;
using E4C.Core.Astron.Horizontal;
using E4C.Core.Astron.Houses;
using E4C.Core.Astron.Obliquity;
using E4C.Core.Astron.SolSysPoints;
using E4C.Core.Facades;
using E4C.Shared.Domain;
using E4C.Shared.References;
using E4C.Shared.ReqResp;
using NUnit.Framework;
using SimpleInjector;
using System.Collections.Generic;

namespace E4CIt.FullChart;

[TestFixture] 
public class FullChartIntTest
{

    private Container _container;
    private IChartAllPositionsApi _api;
    private readonly double _delta = 0.00000001;

    [SetUp]
    public void SetUp()
    {
        _container = new Container();
        AddDependencies();
    }

    [Test]
    public void TestCalculationFullChart()
    {
        PrepareEnvironment();
        ChartAllPositionsRequest request = CreateFullChartRequest();
        ChartAllPositionsResponse response = _api.getChart(request);
        CheckResults(response);
    }

    private void CheckResults(ChartAllPositionsResponse response)
    {
        // Testvalues       Astrolog  Astro.com      PlanetDance
        // Sun longitude     6 TA 28    6 TA 28 11    6 TA 28 10
        // Sun latitude                -0 00 01
        // Sun decl                   +13 40 32       + 13 40 31 
        // Moon longitude   17 PI 37   17 PI 37 34   17 PI 37 35
        // Moon latitude    -4:46'     -4 46 03
        // Moon decl                   -9 16 35       - 09 16 34
        // Merc longitude   26 TA 37   26 TA 37 57   26 TA 37 57
        // Merc latitude    +2:36'     +2 36 44
        // Merc decl                  +21 56 29       + 21 56 29 
        // Jup longitude    27 PI 05   27 PI 05 23
        // Jup latitude     -1:03'
        // Sat longitude    24 AQ 01   24 AQ 01 29
        // Sat latitude     -1:00'
        // Ura longitude    14 TA 18   14 TA 18 03
        // Ura latitude     -0:21'
        // Nep longitude    24 PI 26   24 PI 26 06
        // Nep latitude     -1:08'
        // Pluto longitude  28 CP 35   28 CP 35 48 
        // Pluto latitude   -1:57'
        // Node longitude   23 TA 24   23 TA 24 07
        // Chiron                      13 AR 46 08
        // Ascendant        24 LI 57
        // MC                3 LE 23

        //                            Jul.Dag 2459696.236913 TT, pT 69.3 sec

        Assert.That(response, Is.Not.Null);
        Assert.IsTrue(response.Success);
        Assert.AreEqual("", response.ErrorText);
        Assert.AreEqual(36.4696817563, response.SolarSystemPointPositions[0].Longitude.Position, _delta);
    }

    private void PrepareEnvironment()
    {
        _api = _container.GetInstance<IChartAllPositionsApi>();
    }

    private ChartAllPositionsRequest CreateFullChartRequest()
    {
        return new ChartAllPositionsRequest(CreateSolSysPointsRequest(), HouseSystems.Placidus);
    }


    private SolSysPointsRequest CreateSolSysPointsRequest()
    {
        double jdUt = 2459696.236913 - ((69.3 / 3600.0) / 24.0);               // April, 26 2022, 17:40 UT   
        //double jdUt = 2459696.236913 - ((69.3 / 3600.0) / 24.0);   //  36.469681756277438d  Off by:    0.000040465944785239572  --> 0.144 seconde

        Location location = new("Anywhere", 52.0, 6.0);
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
        //    SolarSystemPoints.Chiron,
            SolarSystemPoints.MeanNode
        };
        ZodiacTypes zodiacType = ZodiacTypes.Tropical;
        Ayanamshas ayanamsha = Ayanamshas.Fagan;
        ObserverPositions observerPosition = ObserverPositions.GeoCentric;
        ProjectionTypes projectionType = ProjectionTypes.twoDimensional;
        return new SolSysPointsRequest(jdUt, location, points, zodiacType, ayanamsha, observerPosition, projectionType);
    }

    private void AddDependencies()
    {
        _container.Register<IAyanamshaSpecifications, AyanamshaSpecifications>(Lifestyle.Singleton);
        _container.Register<IAzAltFacade, AzAltFacade>(Lifestyle.Singleton);
        _container.Register<ICalcHelioPos, CalcHelioPos>(Lifestyle.Singleton);
        _container.Register<ICalcUtFacade, CalcUtFacade>(Lifestyle.Singleton);
        _container.Register<ICoordinateConversionCalc, CoordinateConversionCalc>(Lifestyle.Singleton);
        _container.Register<ICoordinateConversionHandler, CoordinateConversionHandler>(Lifestyle.Singleton);
        _container.Register<ICoTransFacade, CoTransFacade>(Lifestyle.Singleton);
        _container.Register<IFlagDefinitions, FlagDefinitions>(Lifestyle.Singleton);
        _container.Register<IChartAllPositionsApi, ChartAllPositionsApi>(Lifestyle.Singleton);
        _container.Register<IChartAllPositionsHandler, ChartAllPositionsHandler>(Lifestyle.Singleton);
        _container.Register<IHousesCalc, HousesCalc>(Lifestyle.Singleton);
        _container.Register<IHousesFacade, HousesFacade>(Lifestyle.Singleton);
        _container.Register<IHousesHandler, HousesHandler>(Lifestyle.Singleton);
        _container.Register<IHouseSystemSpecs, HouseSystemSpecs>(Lifestyle.Singleton);
        _container.Register<IHorizontalCalc, HorizontalCalc>(Lifestyle.Singleton);
        _container.Register<IHorizontalHandler, HorizontalHandler>(Lifestyle.Singleton);
        _container.Register<IObliquityCalc, ObliquityCalc>(Lifestyle.Singleton);
        _container.Register<IObliquityHandler, ObliquityHandler>(Lifestyle.Singleton);
        _container.Register<ISolarSystemPointSpecifications, SolarSystemPointSpecifications>(Lifestyle.Singleton);
        _container.Register<ISolSysPointsHandler, SolSysPointsHandler>(Lifestyle.Singleton);
        _container.Register<ISolSysPointSECalc, SolSysPointSECalc>(Lifestyle.Singleton);
        _container.Register<ISolSysPointsElementsCalc, SolSysPointsElementsCalc>(Lifestyle.Singleton);
    }

}