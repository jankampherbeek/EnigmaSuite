// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistables;
using Serilog;

namespace Enigma.Core.Persistency;

/// <summary>Handles the conversion of PlanetDance data.</summary>
public interface IPdDataFromToRdbmsHandler
{
    /// <summary>Import PlanetDance data in csv format and store the resulting charts in the RDBMS.</summary>
    /// <param name="csvFileLocation">Location of csv file.</param>
    /// <returns>True if there were no errors, otherwise false.</returns>
    public bool ImportPdDataToRdbms(string csvFileLocation);
}

/// <inheritdoc/>
public class PdDataFromToRdbmsHandler(
    IChartDataDao chartDataDao,
    IPdDataFromToPersistableConverter pdDataFromToPersistableConverter,
    ITextFileReader textFileReader)
    : IPdDataFromToRdbmsHandler
{
    /// <inheritdoc/>
    public bool ImportPdDataToRdbms(string csvFileLocation)
    {
        List<PersistableChartData> _allPersistableChartData;
        var csvLinesRead = textFileReader.ReadAllLines(csvFileLocation);
        var csvLines = CreateProcessableCsvLines(csvLinesRead);
        var result = pdDataFromToPersistableConverter.ConvertPdDataToPersistables(csvLines, out _allPersistableChartData);
        if (!result) return result;
        foreach (var newIndex in _allPersistableChartData.Select(pcData => chartDataDao.AddChartData(pcData)))
        {
            Log.Information("Import chart with id: {ChartId}", newIndex);
        }
        return result;
    }

    private List<string> CreateProcessableCsvLines(List<string> csvLinesToProcess)  
    {
        return csvLinesToProcess.Where(line => !line.Contains("Name;Year;Month;") && !(line.Trim().Length < 1)).ToList();
    }
}