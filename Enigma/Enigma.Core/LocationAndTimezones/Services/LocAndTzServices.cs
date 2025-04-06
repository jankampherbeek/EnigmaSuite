// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Core.LocationAndTimeZones.Services;

/// <summary>
/// Definitions for Dependency Injection for classes and interfaces in the package Enigma.Core.LocationAndTimeZones.
/// </summary>
public static class LocAndTzServices
{
    public static void RegisterLocAndTzServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IDayDefHandler, DayDefHandler>();
        serviceCollection.AddTransient<IDstHandler, DstHandler>();
        serviceCollection.AddTransient<IDstLineReader, DstLineReader>();
        serviceCollection.AddTransient<IDstParser, DstParser>();
        serviceCollection.AddTransient<ILocationHandler, LocationHandler>();
        serviceCollection.AddTransient<ITimeZoneLineParser, TimeZoneLineParser>();
        serviceCollection.AddTransient<ITimeZoneReader, TimeZoneReader>();
        serviceCollection.AddTransient<ITzHandler, TzHandler>();
    }
}