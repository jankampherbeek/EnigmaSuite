// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Facades.Se;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Facades;


/// <summary>
/// Definitions for Dependency Injection for classes and interfaces for enigma.facades.
/// </summary>

public static class FacadeServices
{
    public static void RegisterFacadesServices(this ServiceCollection serviceCollection)
    {
        // Handlers


        serviceCollection.AddTransient<IAyanamshaFacade, AyanamshaFacade>();
        serviceCollection.AddTransient<IAzAltFacade, AzAltFacade>();
        serviceCollection.AddTransient<ICalcApsidesFacade, CalcApsidesFacade>();
        serviceCollection.AddTransient<ICalcUtFacade, CalcUtFacade>();
        serviceCollection.AddTransient<ICoTransFacade, CoTransFacade>();
        serviceCollection.AddTransient<IDateConversionFacade, DateConversionFacade>();
        serviceCollection.AddTransient<IHousesFacade, HousesFacade>();
        serviceCollection.AddTransient<IJulDayFacade, JulDayFacade>();
        serviceCollection.AddTransient<IOrbitalElementsFacade, OrbitalElementsFacade>();
        serviceCollection.AddTransient<IRevJulFacade, RevJulFacade>();
    }
}