// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Responses;
using Serilog;
using Exception = System.Exception;

namespace Enigma.Core.Data;

/// <summary>Handles the import and conversion to Json of a csv datafile.</summary>
public interface IDataImportHandler
{
    /// <summary>Import a datafile in standard csv and convert it to Json.</summary>
    /// <param name="fullPathSource">Full path to the file to read.</param>
    /// <param name="dataName">Name for the data.</param>
    /// <param name="dataType">Type of research data.</param>
    /// <returns>Resultmessage with a description of the action.</returns>
    public ResultMessage ImportStandardData(string fullPathSource, string dataName, ResearchDataTypes dataType);
}

/// <inheritdoc/>
public sealed class DataImportHandler(
    IFileCopier fileCopier,
    ICsvImporter csvImporter,
    ICsvExporter csvExporter,
    ISettingsDao settingsDao,
    IDataFileDao dataFileDao)
    : IDataImportHandler
{
    /// <inheritdoc/>
    public ResultMessage ImportStandardData(string fullPathSource, string dataName, ResearchDataTypes dataType)
    {
        // TODO check values for ResultMessage, or find an alternative solution
        var workFolder = settingsDao.ReadSetting("workfolder");
        var fullInputPath = workFolder + Path.DirectorySeparatorChar + dataName + Path.DirectorySeparatorChar + "orig" +
                            Path.DirectorySeparatorChar + dataName + ".csv";
        var fullOutputPath = workFolder + Path.DirectorySeparatorChar + dataName + Path.DirectorySeparatorChar +
                             "standard" +
                             Path.DirectorySeparatorChar + "date_time_loc.csv";
        var fullErrorPath = workFolder + Path.DirectorySeparatorChar + "dataName" + Path.DirectorySeparatorChar +
                            "errors.txt";
        int dataIndex;
        try
        {
            var dataFileDto = new DataFileDto
            {
                Name = dataName,
                Location = fullOutputPath
            };
            dataIndex = dataFileDao.InsertDataFile(dataFileDto);
        }
        catch (Exception e)
        {
            var errorTxt = $"Encountered excepten {e.Message} when trying to insert datafile {dataName} into database";
            Log.Error(errorTxt);
            return new ResultMessage(2, errorTxt);
        }

        if (dataIndex <= 0) return new ResultMessage(2, "File could not be imported");
        {
            try
            {
                fileCopier.CopyFile(fullPathSource, fullInputPath);
                var inputItems = dataType switch
                {
                    ResearchDataTypes.PlanetDance => csvImporter.ProcessPlanetDanceInputData(fullInputPath),
                    ResearchDataTypes.StandardEnigma => csvImporter.ProcessStandardInputData(fullInputPath),
                    _ => []
                };
                csvExporter.WriteStandardInputToCsv(inputItems, fullOutputPath);
                return new ResultMessage(0, "File successfully imported");
            }
            catch (Exception e)
            {
                var deleted = dataFileDao.DeleteDataFile(dataIndex);
                Log.Error(
                    $"Could not import data. An exception occurred: {e.Message} using input filePath {fullInputPath} and output file path {fullOutputPath}. Database rolled back: {deleted}");
                return new ResultMessage(1,
                    $"Error in reading csv, check file {fullErrorPath}. Database rolled back: {deleted}");
            }
        }
    }
}