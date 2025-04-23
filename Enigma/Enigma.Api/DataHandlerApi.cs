// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Data;
using Enigma.Core.Handlers;
using Enigma.Core.Persistency;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Api;

/// <summary>Api for conversions from Csv to Json.</summary>
public interface IDataHandlerApi
{
    /// <summary>Convert an inputted datafiles in csv to standard csv-format.</summary>
    /// <remarks>Locations for the files are retrieved from the application settings.</remarks>
    /// <param name="sourceFile">Path to the source file.</param>
    /// <param name="dataName">Name for the datafile.</param>
    /// <param name="dataType">Type of research data.</param>
    /// <returns>Resultmessage with info about this action.</returns>
    public ResultMessage ConvertDataFile2Standard(string sourceFile, string dataName, ResearchDataTypes dataType);
    
}

/// <summary>Api for managing the file system.</summary>
public interface IDataFileManagementApi
{
    /// <summary>Check if a datafile is not yet used</summary>
    /// <param name="name">The name for the datafile to check</param>
    /// <returns>True if the datafile exists, otherwise false.</returns>
    public bool DataFileNameIsAvailable(string name);

    /// <summary>Creates folders for research data and additionally subfodlers 'csv' and 'json'.</summary>
    /// <param name="fullPath">The path for the datafiles, not including the csv and json subfolders.</param>
    /// <returns>Resultmessage with info about this action.</returns>
    public ResultMessage CreateFoldersForData(string fullPath);

    /// <summary>Create a list of data names, based in folders in the file system.</summary>
    /// <returns>Dat names.</returns>
    public IEnumerable<DataFileDto> GetDataNames();

    /// <summary>Read DTO for a specific datafile</summary>
    /// <param name="index">The id of the data file</param>
    /// <returns>The datafile if one is found, otherwise null</returns>
    public DataFileDto? ReadDataFile(int index);

    /// <summary>Read DTO for a specific datafile by its name</summary>
    /// <param name="name">The name of the data file</param>
    /// <returns>The datafile if one is found, otherwise null</returns>
    public DataFileDto? ReadDataFile(string name);
}

/// <summary>Api for import from, and export to PlanetDance data.</summary>
public interface IPdDataImportExportApi
{
    /// <summary>Import Planet Dance data to RDBMS.</summary>
    /// <param name="csvFilename">Full path of the csv file.</param>
    /// <returns>True if no errors occurred, otherwise false.</returns>
    public bool ImportPdDataToRdbms(string csvFilename);
}

/// <inheritdoc/>
public sealed class DataHandlerApi(IDataImportHandler dataImportHandler) : IDataHandlerApi
{
    /// <inheritdoc/>
    public ResultMessage ConvertDataFile2Standard(string sourceFile, string dataName, ResearchDataTypes dataType)
    {
        Guard.Against.NullOrEmpty(dataName);
        Log.Information(
            $"DataHandlerApi Convert data to standard format, using sourceFile {sourceFile} and dataName {dataName}");
        return dataImportHandler.ImportStandardData(sourceFile, dataName, dataType);
    }
}

/// <inheritdoc/>
public sealed class DataFileManagementApi(IDataFilePreparator dataFilePreparator, IDataFileDao dataFileDao) : IDataFileManagementApi
{
    /// <inheritdoc/>
    public bool DataFileNameIsAvailable(string name)
    {
        Guard.Against.NullOrEmpty(name);
        Log.Information($"Check if datafile with name {name} exists in database");
        return dataFileDao.IsDataNameAvailable(name);
    }

    /// <inheritdoc/>
    public ResultMessage CreateFoldersForData(string fullPath)
    {
        Guard.Against.NullOrEmpty(fullPath);
        Log.Information($"Create folders for data at {fullPath}");
        return dataFilePreparator.MakeFolderStructure(fullPath);
    }

    /// <inheritdoc/>
    public IEnumerable<DataFileDto> GetDataNames()
    {
        Log.Information("DataFileManagementApi GetDataNames");
        return dataFileDao.AllDataFiles();
    }

    public DataFileDto? ReadDataFile(int index)
    {
        Guard.Against.NegativeOrZero(index);
        Log.Information($"Read data file with index {index}");
        return dataFileDao.ReadDataFile(index);
    }

    /// <inheritdoc/>
    public DataFileDto? ReadDataFile(string name)
    {
        Guard.Against.NullOrEmpty(name);
        Log.Information($"Read data file with name {name}");
        return dataFileDao.ReadDataFile(name);
    }
}

/// <inheritdoc/>
public sealed class PdDataImportExportApi(IPdDataFromToRdbmsHandler handler) : IPdDataImportExportApi
{
    public bool ImportPdDataToRdbms(string csvFilename)
    {
        return handler.ImportPdDataToRdbms(csvFilename);
    }
}


