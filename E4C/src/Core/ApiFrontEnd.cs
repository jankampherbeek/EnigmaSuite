// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4C.Core;

using SimpleInjector;
using E4C.Core.Api.Astron;
using E4C.Core.Astron.ChartAllPositions;
using E4C.Core.Astron.CoordinateConversion;
using E4C.Core.Astron.Horizontal;
using E4C.Core.Astron.Houses;
using E4C.Core.Astron.Obliquity;
using E4C.Core.Astron.SolSysPoints;
using E4C.Core.Facades;
using E4C.Shared.References;
using E4C.Core.Api.Datetime;

/// <summary>Frontend for API's, implemented as a singleton.</summary>
public sealed class ApiFrontEnd
{
    private static ApiFrontEnd _instance = null;
    private static readonly object _padlock = new object();
    internal static Container _container;



    ApiFrontEnd()
    {
    }

    public static ApiFrontEnd Instance
    {
        get
        {
            lock (_padlock)
            {
                if (_instance == null)
                {
                    _instance = new ApiFrontEnd();
                    _container = new Container();
                    AddDependencies();
                }
                return _instance;
            }
        }
    }

    internal Container GetContainer()
    {
        return _container;
    }

    private static void AddDependencies()
    {
        _container.Register<IAyanamshaSpecifications, AyanamshaSpecifications>(Lifestyle.Singleton);
        _container.Register<IAzAltFacade, AzAltFacade>(Lifestyle.Singleton);
        _container.Register<ICalcDateTimeApi, CalcDateTimeApi>(Lifestyle.Singleton);
        _container.Register<ICalcHelioPos, CalcHelioPos>(Lifestyle.Singleton);
        _container.Register<ICalcUtFacade, CalcUtFacade>(Lifestyle.Singleton);
        _container.Register<IChartAllPositionsApi, ChartAllPositionsApi>(Lifestyle.Singleton);
        _container.Register<ICheckDateTimeApi, CheckDateTimeApi>(Lifestyle.Singleton);  
        _container.Register<ICoordinateConversionApi, CoordinateConversionApi>(Lifestyle.Singleton);
        _container.Register<ICoordinateConversionCalc, CoordinateConversionCalc>(Lifestyle.Singleton);
        _container.Register<ICoordinateConversionHandler, CoordinateConversionHandler>(Lifestyle.Singleton);
        _container.Register<ICoTransFacade, CoTransFacade>(Lifestyle.Singleton);
        _container.Register<IFlagDefinitions, FlagDefinitions>(Lifestyle.Singleton);
        _container.Register<IChartAllPositionsApi, ChartAllPositionsApi>(Lifestyle.Singleton);
        _container.Register<IChartAllPositionsHandler, ChartAllPositionsHandler>(Lifestyle.Singleton);
        _container.Register<IHorizontalApi, HorizontalApi>(Lifestyle.Singleton);
        _container.Register<IHorizontalCalc, HorizontalCalc>(Lifestyle.Singleton);
        _container.Register<IHorizontalHandler, HorizontalHandler>(Lifestyle.Singleton);
        _container.Register<IHousesApi, HousesApi>(Lifestyle.Singleton);
        _container.Register<IHousesCalc, HousesCalc>(Lifestyle.Singleton);
        _container.Register<IHousesFacade, HousesFacade>(Lifestyle.Singleton);
        _container.Register<IHousesHandler, HousesHandler>(Lifestyle.Singleton);
        _container.Register<IHouseSystemSpecs, HouseSystemSpecs>(Lifestyle.Singleton);
        _container.Register<IJulianDayApi, JulianDayApi>(Lifestyle.Singleton);
        _container.Register<IObliquityApi, ObliquityApi>(Lifestyle.Singleton);
        _container.Register<IObliquityCalc, ObliquityCalc>(Lifestyle.Singleton);
        _container.Register<IObliquityHandler, ObliquityHandler>(Lifestyle.Singleton);
        _container.Register<ISolarSystemPointSpecifications, SolarSystemPointSpecifications>(Lifestyle.Singleton);
        _container.Register<ISolSysPointsHandler, SolSysPointsHandler>(Lifestyle.Singleton);
        _container.Register<ISolSysPointSECalc, SolSysPointSECalc>(Lifestyle.Singleton);
        _container.Register<ISolSysPointsElementsCalc, SolSysPointsElementsCalc>(Lifestyle.Singleton);
    }



}