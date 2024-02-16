// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;
using Enigma.Domain.Persistables;
using Serilog;

namespace Enigma.Core.Handlers;

/// <summary>Handles the conversion of PlanetDance data.</summary>
public interface IPdDataFromToRdbmsHandler
{
    /// <summary>Import PlanetDance data in csv format and store the resulting charts in the RDBMS.</summary>
    /// <param name="csvFileLocation">Location of csv file.</param>
    /// <returns>True if there were no errors, otherwise false.</returns>
    public bool ImportPdDataToRdbms(string csvFileLocation);
}

/// <inheritdoc/>
public class PdDataFromToRdbmsHandler: IPdDataFromToRdbmsHandler
{
    private readonly IChartDataDao _chartDataDao;
    private readonly IPdDataFromToPersistableConverter _pdDataFromToPersistableConverter;
    private readonly ITextFileReader _textFileReader;

    public PdDataFromToRdbmsHandler(IChartDataDao chartDataDao, 
        IPdDataFromToPersistableConverter pdDataFromToPersistableConverter,
        ITextFileReader textFileReader)
    {
        _chartDataDao = chartDataDao;
        _pdDataFromToPersistableConverter = pdDataFromToPersistableConverter;
        _textFileReader = textFileReader;
    }

    /// <inheritdoc/>
    public bool ImportPdDataToRdbms(string csvFileLocation)
    {
        List<PersistableChartData> _allPersistableChartData;
        List<string> csvLinesRead = _textFileReader.ReadAllLines(csvFileLocation);
        List<string> csvLines = CreateProcessableCsvLines(csvLinesRead);
        bool result = _pdDataFromToPersistableConverter.ConvertPdDataToPersistables(csvLines, out _allPersistableChartData);
        if (result)
        {
            foreach (long newIndex in _allPersistableChartData.Select(pcData => _chartDataDao.AddChartData(pcData)))
            {
                Log.Information("Import chart with id: {ChartId}", newIndex);
            }
        }
        return result;
    }

    private List<string> CreateProcessableCsvLines(List<string> csvLinesToProcess)  
    {
        return csvLinesToProcess.Where(line => !line.Contains("Name;Year;Month;") && !(line.Trim().Length < 1)).ToList();
    }
}