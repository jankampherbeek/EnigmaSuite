﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.References;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Api;

/// <summary>Api for conversions from Csv to Json.</summary>
public interface IDataHandlerApi
{
    /// <summary>Convert a datafile in standard csv-format to a Json file.</summary>
    /// <remarks>Locations for the files are retrieved from the application settings.</remarks>
    /// <param name="sourceFile">Path to the source file.</param>
    /// <param name="dataName">Name for the datafile.</param>
    /// <param name="dataType">Type of research data.</param>
    /// <returns>Resultmessage with info about this action.</returns>
    public ResultMessage ConvertDataFile2Json(string sourceFile, string dataName, ResearchDataTypes dataType);


}

/// <summary>Api for managing the file system.</summary>
public interface IDataFileManagementApi
{
    /// <summary>Check if a folder can be created.</summary>
    /// <param name="fullPath">The full path of the folder to check.</param>
    /// <returns>True if the folder does not yet exist, otherwise false.</returns>
    public bool FolderIsAvailable(string fullPath);

    /// <summary>Creates folders for research data and additionally subfodlers 'csv' and 'json'.</summary>
    /// <param name="fullPath">The path for the datafiles, not including the csv and json subfolders.</param>
    /// <returns>Resultmessage with info about this action.</returns>
    public ResultMessage CreateFoldersForData(string fullPath);

    /// <summary>Create a list of data names, based in folders in the file system.</summary>
    /// <returns>Dat names.</returns>
    public IEnumerable<string> GetDataNames();
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
public sealed class DataHandlerApi : IDataHandlerApi
{
    private readonly IDataImportHandler _dataImportHandler;

    public DataHandlerApi(IDataImportHandler dataImportHandler) => _dataImportHandler = dataImportHandler;


    /// <inheritdoc/>
    public ResultMessage ConvertDataFile2Json(string sourceFile, string dataName, ResearchDataTypes dataType)
    {
        Guard.Against.NullOrEmpty(dataName);
        Log.Information("DataHandlerApi ConvertDataFile2Json, using sourceFile {Source} and dataName {Data}", sourceFile, dataName);
        return _dataImportHandler.ImportStandardData(sourceFile, dataName, dataType);
    }
}

/// <inheritdoc/>
public sealed class DataFileManagementApi : IDataFileManagementApi
{
    private readonly IDataFilePreparationHandler _preparationHandler;
    private readonly IDataNamesHandler _dataNamesHandler;

    public DataFileManagementApi(IDataFilePreparationHandler preparationHandler, IDataNamesHandler dataNamesHandler)
    {
        _preparationHandler = preparationHandler;
        _dataNamesHandler = dataNamesHandler;
    }

    /// <inheritdoc/>
    public bool FolderIsAvailable(string fullPath)
    {
        Guard.Against.NullOrEmpty(fullPath);
        Log.Information("DataFileManagementApi FolderIsAvailable, using fullPath : {Path}", fullPath);
        return _preparationHandler.FolderNameAvailable(fullPath);
    }

    /// <inheritdoc/>
    public ResultMessage CreateFoldersForData(string fullPath)
    {
        Guard.Against.NullOrEmpty(fullPath);
        Log.Information("DataFileManagementApi CreateFoldersForData, using fullPath : {Path}", fullPath);
        return _preparationHandler.MakeFolderStructure(fullPath);
    }

    /// <inheritdoc/>
    public IEnumerable<string> GetDataNames()
    {
        Log.Information("DataFileManagementApi GetDataNames");
        return _dataNamesHandler.GetExistingDataNames();
    }
}


/// <inheritdoc/>
public sealed class PdDataImportExportApi : IPdDataImportExportApi
{
    private readonly IPdDataFromToRdbmsHandler _handler;

    public PdDataImportExportApi(IPdDataFromToRdbmsHandler handler)
    {
        _handler = handler;
    }
    
    public bool ImportPdDataToRdbms(string csvFilename)
    {
        return _handler.ImportPdDataToRdbms(csvFilename);
    }
}


