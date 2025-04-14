// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;
using Enigma.Domain.Responses;
using Serilog;
using Exception = System.Exception;

namespace Enigma.Core.Handlers;


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
    ICsvExporter csvExporter)
    : IDataImportHandler
{
    /// <inheritdoc/>
    public ResultMessage ImportStandardData(string fullPathSource, string dataName, ResearchDataTypes dataType)
    {
        var fullInputPath = ApplicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + dataName + 
                            Path.DirectorySeparatorChar + "csv" + Path.DirectorySeparatorChar + dataName + ".csv";
        var fullOutputPath = ApplicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + dataName + 
                             Path.DirectorySeparatorChar + "json" + Path.DirectorySeparatorChar + "date_time_loc.csv";
        var fullErrorPath = ApplicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + "errors.txt";
        try
        {
            fileCopier.CopyFile(fullPathSource, fullInputPath);
            List<StandardInputItem> inputItems; 
            if (dataType == ResearchDataTypes.PlanetDance)
            {
                inputItems = csvImporter.ProcessPlanetDanceData(fullInputPath);                
            }
            else     // Enigma data 
            {
                // TODO handle Enigma data
                inputItems = new List<StandardInputItem>();
            }
            csvExporter.WriteStandardInputToCsv(inputItems, fullOutputPath);
            return new ResultMessage(0, "File successfully imported");
        }
        catch (Exception e)
        {
            Log.Error($"Could not import data. An exception occurred: {e.Message} using input filePath {fullInputPath} and output file path {fullOutputPath}");
            return new ResultMessage(1, "Error in reading csv, check file " + fullErrorPath);
        }
    }
    
    
    
}