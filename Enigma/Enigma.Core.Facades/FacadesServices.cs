// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Facades.Se.Interfaces;
using Enigma.Facades.Interfaces;
using Enigma.Facades.Se;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Facades.Services;


/// <summary>
/// Definitions for Dependency Injection for classes and interfaces for enigma.facades.
/// </summary>

public static class FacadeServices
{
    public static void RegisterFacadesServices(this ServiceCollection serviceCollection)
    {
        // Handlers
        serviceCollection.AddTransient<IHousesFacade, HousesFacade>();

        serviceCollection.AddTransient<IAyanamshaFacade, AyanamshaFacade>();
        serviceCollection.AddTransient<IAzAltFacade, AzAltFacade>();
        serviceCollection.AddTransient<ICalcUtFacade, CalcUtFacade>();
        serviceCollection.AddTransient<ICoTransFacade, CoTransFacade>();
        serviceCollection.AddTransient<IDateConversionFacade, DateConversionFacade>();
        serviceCollection.AddTransient<IJulDayFacade, JulDayFacade>();
        serviceCollection.AddTransient<IRevJulFacade, RevJulFacade>();


    }
}