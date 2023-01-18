// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.RequestResponse;


namespace Enigma.Core.Handlers.Persistency;

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
        string fullCsvPath = ApplicationSettings.Instance.LocationDataFiles + Path.DirectorySeparatorChar + dataName + Path.DirectorySeparatorChar + "csv" + Path.DirectorySeparatorChar + dataName + ".csv";
        string fullJsonPath = ApplicationSettings.Instance.LocationDataFiles + Path.DirectorySeparatorChar + dataName + Path.DirectorySeparatorChar + "json" + Path.DirectorySeparatorChar + "date_time_loc.json";
        string fullErrorPath = ApplicationSettings.Instance.LocationDataFiles + Path.DirectorySeparatorChar + "errors.json";
        _fileCopier.CopyFile(fullPathSource, fullCsvPath);
        List<string> csvLines = _textFileReader.ReadAllLines(fullCsvPath);
        Tuple<bool, string, List<string>> conversionResult = _csv2JsonConverter.ConvertStandardDataCsvToJson(csvLines, dataName);
        if (conversionResult.Item1)
        {
            string jsonText = conversionResult.Item2;
            _textFileWriter.WriteFile(fullJsonPath, jsonText);
            return new ResultMessage(0, "File successfully imported.");       // TODO use RB
        }
        else
        {
            List<string> errorLines = conversionResult.Item3;
            _textFileWriter.WriteFile(fullErrorPath, errorLines);
            return new ResultMessage(1, "Error in reading csv, check file errors.json.");    // TODO use RB
        }

    }
}