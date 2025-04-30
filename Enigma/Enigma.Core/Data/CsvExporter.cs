// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Persistables;

namespace Enigma.Core.Data;

/// <summary>Writes items to a file in csv format</summary>
public interface ICsvExporter
{
     /// <summary>Convert standard input items to csv and write the results to a file</summary>
     /// <param name="inputItems">The items to process</param>
     /// <param name="fullPath">Full path and filaname of the csv file te write</param>
    public void WriteStandardInputToCsv(IEnumerable<StandardInputItem> inputItems, string fullPath);

     
     /// <summary>Convert research positions to csv and write the results to a file</summary>
     /// <remarks>The file is creaed once and the lines are appended in several batches</remarks>
     /// <param name="inputItems">The research positions to write</param>
     /// <param name="fullPath">Path of the file</param>
    public void WriteResearchPositionsToCsv(IEnumerable<ResearchPositionsForChart> inputItems, string fullPath, CultureInfo culture);
}


public class CsvExporter: ICsvExporter
{
    public void WriteStandardInputToCsv(IEnumerable<StandardInputItem> inputItems, string fullPath)
    {
        var standardInputItems = inputItems.ToList();
        if (standardInputItems.Count == 0) throw new ArgumentException("Input items cannot be empty", nameof(inputItems));
        if (string.IsNullOrWhiteSpace(fullPath))
            throw new ArgumentException("File path cannot be null or empty", nameof(fullPath));
        
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";", 
            HasHeaderRecord = true
        };
        try
        {
            using var writer = new StreamWriter(fullPath);
            using var csv = new CsvWriter(writer, config);
            csv.WriteHeader<StandardInputItem>();
            csv.NextRecord();
            foreach (var item in standardInputItems)
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

    public void WriteResearchPositionsToCsv(IEnumerable<ResearchPositionsForChart> inputItems, string fullPath, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(inputItems);
        if (string.IsNullOrWhiteSpace(fullPath))
            throw new ArgumentException("File path cannot be null or empty", nameof(fullPath));

        try
        {
            using var writer = new StreamWriter(fullPath, true);
            foreach (var item in inputItems)
            {
                var line = string.Join(";", new[]
                {
                    item.Id,
                    string.Join(";", item.Positions.Select(p => $"{p.Abbrev};{p.Position.ToString(culture)}"))
                });
                writer.WriteLine(line);
            }
        }
        catch (IOException ex)
        {
            throw new PersistencyException(
                $"Could not write to {fullPath}. Encountered IOException {ex.Message}");
        }
    }
}