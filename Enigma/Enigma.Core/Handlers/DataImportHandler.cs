// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Core.Persistency;
using Enigma.Domain.Dtos;
using Enigma.Domain.Responses;

namespace Enigma.Core.Handlers;


/// <summary>Handles the import and conversion to Json of a csv datafile.</summary>
public interface IDataImportHandler
{
    /// <summary>Import a datafile in standard csv and convert it to Json.</summary>
    /// <param name="fullPathSource">Full path to the file to read.</param>
    /// <param name="dataName">Name for the data.</param>
    /// <returns>Resultmessage with a description of the action.</returns>
    public ResultMessage ImportStandardData(string fullPathSource, string dataName);
}


/// <inheritdoc/>
public sealed class DataImportHandler : IDataImportHandler
{
    private readonly IFileCopier _fileCopier;
    private readonly ITextFileReader _textFileReader;
    private readonly ITextFileWriter _textFileWriter;
    private readonly ICsv2JsonConverter _csv2JsonConverter;

    public DataImportHandler(IFileCopier fileCopier, ITextFileReader textFileReader, ITextFileWriter textFileWriter, ICsv2JsonConverter csv2JsonConverter)
    {
        _fileCopier = fileCopier;
        _textFileReader = textFileReader;
        _textFileWriter = textFileWriter;
        _csv2JsonConverter = csv2JsonConverter;
    }

    /// <inheritdoc/>
    public ResultMessage ImportStandardData(string fullPathSource, string dataName)
    {
        string fullCsvPath = ApplicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + dataName + Path.DirectorySeparatorChar + "csv" + Path.DirectorySeparatorChar + dataName + ".csv";
        string fullJsonPath = ApplicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + dataName + Path.DirectorySeparatorChar + "json" + Path.DirectorySeparatorChar + "date_time_loc.json";
        string fullErrorPath = ApplicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + "errors.txt";
        _fileCopier.CopyFile(fullPathSource, fullCsvPath);
        List<string> csvLines = _textFileReader.ReadAllLines(fullCsvPath);
        (bool item1, string? jsonText, List<string>? errorLines) = _csv2JsonConverter.ConvertStandardDataCsvToJson(csvLines, dataName);
        if (item1)
        {
            _textFileWriter.WriteFile(fullJsonPath, jsonText);
            _textFileWriter.WriteFile(fullErrorPath, "Import succesfull, no errors occurred."); 
            return new ResultMessage(0, "File successfully imported."); 
        }
        _textFileWriter.WriteFile(fullErrorPath, errorLines);
        return new ResultMessage(1, "Error in reading csv, check file " + fullErrorPath);
    }

    
}