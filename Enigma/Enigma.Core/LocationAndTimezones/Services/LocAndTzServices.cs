// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Core.LocationAndTimeZones.Services;

/// <summary>
/// Definitions for Dependency Injection for classes and interfaces in the package Enigma.Core.LocationAndTimeZones.
/// </summary>
public static class LocAndTzServices
{
    public static void RegisterLocAndTzServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IDstHandler, DstHandling>();
        serviceCollection.AddTransient<IDstParser, DstParser>();
        serviceCollection.AddTransient<ITimeZoneLineParser, TimeZoneLineParser>();
        serviceCollection.AddTransient<ITimeZoneReader, TimeZoneReader>();
    }
}