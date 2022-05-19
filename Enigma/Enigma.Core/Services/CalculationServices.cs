using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.DateTime.JulDay;
using Enigma.Core.Calc.SeFacades;
using Microsoft.Extensions.DependencyInjection;

namespace 
    Enigma.Core.Calc.Services;

public static class CalculationServices
{
    public static void RegisterCalculationServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IJulianDayApi, JulianDayApi>();
        serviceCollection.AddSingleton<IJulDayCalc, JulDayCalc>();
        serviceCollection.AddSingleton<IJulDayHandler, JulDayHandler>();
        serviceCollection.AddSingleton<IJulDayFacade, JulDayFacade>();

    }
}