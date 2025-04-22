// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Core.Data.Services;

/// <summary>
///  DI for data
/// </summary>
public static class DataServices
{
    public static void RegisterDataServices(this ServiceCollection serviceCollection)
    {   
        serviceCollection.AddTransient<ICsvExporter, CsvExporter>();
        serviceCollection.AddTransient<ICsvImporter, CsvImporter>();
        serviceCollection.AddTransient<ICsvStandardDataReader, CsvStandardDataReader>();
        serviceCollection.AddTransient<IDataImportHandler, DataImportHandler>();
    }
}

