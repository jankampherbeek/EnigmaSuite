// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.Astron;
using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.ChartAllPositions;
using Enigma.Core.Calc.DateTime.JulDay;
using Enigma.Core.Calc.SeFacades;
using Enigma.Core.Calc.SolSysPoints;
using Enigma.Core.Calc.Util;
using Microsoft.Extensions.DependencyInjection;

namespace
    Enigma.Core.Calc.Services;

public static class CalculationServices
{
    public static void RegisterCalculationServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IAzAltFacade, AzAltFacade>();
        serviceCollection.AddSingleton<ICalcDateTimeApi, CalcDateTimeApi>();
        serviceCollection.AddSingleton<ICalcHelioPos, CalcHelioPos>();
        serviceCollection.AddSingleton<ICalcUtFacade, CalcUtFacade>();
        serviceCollection.AddSingleton<IChartAllPositionsApi, ChartAllPositionsApi>();
        serviceCollection.AddSingleton<IChartAllPositionsHandler, ChartAllPositionsHandler>();
        serviceCollection.AddSingleton<ICheckDateTimeApi, CheckDateTimeApi>();
        serviceCollection.AddSingleton<ICoordinateConversionApi, CoordinateConversionApi>();
        serviceCollection.AddSingleton<ICoTransFacade, CoTransFacade>();
        serviceCollection.AddSingleton<IDateConversionFacade, DateConversionFacade>();
        serviceCollection.AddSingleton<IHorizontalApi, HorizontalApi>();
        serviceCollection.AddSingleton<IHousesApi, HousesApi>();
        serviceCollection.AddSingleton<IHousesFacade, HousesFacade>();
        serviceCollection.AddSingleton<IJulianDayApi, JulianDayApi>();
        serviceCollection.AddSingleton<IJulDayCalc, JulDayCalc>();
        serviceCollection.AddSingleton<IJulDayHandler, JulDayHandler>();
        serviceCollection.AddSingleton<IJulDayFacade, JulDayFacade>();
        serviceCollection.AddSingleton<IObliqueLongitudeApi, ObliqueLongitudeApi>();
        serviceCollection.AddSingleton<IObliquityApi, ObliquityApi>();
        serviceCollection.AddSingleton<IRevJulFacade, RevJulFacade>();
        serviceCollection.AddSingleton<ISeFlags, SeFlags>();
        serviceCollection.AddSingleton<ISolSysPointSECalc, SolSysPointSECalc>();
        serviceCollection.AddSingleton<ISolSysPointsElementsCalc, SolSysPointsElementsCalc>();
        serviceCollection.AddSingleton<ISolSysPointsHandler, SolSysPointsHandler>();
    }
}