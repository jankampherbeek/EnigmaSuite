// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.LocationAndTimeZones;
using Enigma.Core.LocationAndTimeZones;
using Enigma.Facades.Se;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Test.Integration;

public abstract class IntegrationTestBase
{
    protected IServiceProvider ServiceProvider { get; }

    protected IntegrationTestBase()
    {
        
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        
        // APIs
        services.AddScoped<ITimeZoneApi, TimeZoneApi>();
        
        // LocAndTzServices
        services.AddScoped<IDayDefHandler, DayDefHandler>();
        services.AddScoped<IDstHandler, DstHandler>();
        services.AddScoped<IDstLineReader, DstLineReader>();
        services.AddScoped<IDstParser, DstParser>();
        services.AddScoped<ILocationHandler, LocationHandler>();
        services.AddScoped<ITimeZoneLineParser, TimeZoneLineParser>();
        services.AddScoped<ITimeZoneReader, TimeZoneReader>();
        services.AddScoped<ITzHandler, TzHandler>();
        
        // FacadeServices
        services.AddScoped<IAyanamshaFacade, AyanamshaFacade>();
        services.AddScoped<IAzAltFacade, AzAltFacade>();
        services.AddScoped<ICalcApsidesFacade, CalcApsidesFacade>();
        services.AddScoped<ICalcUtFacade, CalcUtFacade>();
        services.AddScoped<ICoTransFacade, CoTransFacade>();
        services.AddScoped<IDateConversionFacade, DateConversionFacade>();
        services.AddScoped<IHousesFacade, HousesFacade>();
        services.AddScoped<IJulDayFacade, JulDayFacade>();
        services.AddScoped<IOrbitalElementsFacade, OrbitalElementsFacade>();
        services.AddScoped<IRevJulFacade, RevJulFacade>();
        
        // etc.
    }
    
    protected T GetService<T>() where T : notnull
    {
        return ServiceProvider.GetRequiredService<T>();
    }
    
}