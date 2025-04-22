// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Enigma.Domain.Persistables;
using Serilog;

namespace Enigma.Core.Data;

/// <summary>Reads standard input data from CSV files</summary>
public interface ICsvStandardDataReader
{
    /// <summary>Reads a CSV file and converts it to a list of StandardInputItems</summary>
    /// <param name="filePath">Path to the CSV file</param>
    /// <returns>List of StandardInputItems</returns>
    List<StandardInputItem> ReadStandardInputData(string filePath);
}

/// <inheritdoc/>
public class CsvStandardDataReader : ICsvStandardDataReader
{
    /// <inheritdoc/>
    public List<StandardInputItem> ReadStandardInputData(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            Log.Error("ReadStandardInputData encountered a filePath that is null or empty");
            throw new ArgumentNullException(nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            Log.Error($"ReadStandardInputData encountered a filePath {filePath} but that file does not exist");
            throw new FileNotFoundException("CSV file not found", filePath);
        }

        Log.Information($"CsvStandardDataReader started reading data from {filePath}");
        
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
           // HeaderValidated = null,
            IgnoreBlankLines = true,
            MissingFieldFound = null,
        };

        try
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<StandardInputItem>().ToList();
            Log.Information($"CsvStandardDataReader completed reading {records.Count} records from {filePath}");
            return records;
        }
        catch (Exception ex)
        {
            Log.Error($"Error reading CSV file {filePath}: {ex.Message}");
            throw;
        }
    }
}