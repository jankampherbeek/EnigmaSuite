// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Persistables;

namespace Enigma.Core.Persistency;

/// <summary>Writes items to a file in csv format</summary>
public interface ICsvExporter
{
     /// <summary>Convert standard input items to csv and write the results to a file</summary>
     /// <param name="inputItems">The items to process</param>
     /// <param name="fullPath">Full path and filaname of the csv file te write</param>
    public void WriteStandardInputToCsv(List<StandardInputItem> inputItems, string fullPath);
}


public class CsvExporter: ICsvExporter
{
    public void WriteStandardInputToCsv(List<StandardInputItem> inputItems, string fullPath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";", // Gebruik puntkomma als scheidingsteken
            HasHeaderRecord = true, // Schrijf kolomkoppen
        };
        try
        {
            using var writer = new StreamWriter(fullPath);
            using var csv = new CsvWriter(writer, config);
            csv.WriteHeader<StandardInputItem>();
            csv.NextRecord();
            foreach (var item in inputItems)
            {
                csv.WriteRecord(item);
                csv.NextRecord();
            }
        }
        catch (CsvHelperException ex)
        {
            throw new PersistencyException(
                $"Could not write to {fullPath}. Encountered CsvHelperException {ex.Message}");
        }
        catch (IOException ex)
        {
            throw new PersistencyException(
                $"Could not write to {fullPath}. Encountered IOException {ex.Message}");
        }
    }
    
}