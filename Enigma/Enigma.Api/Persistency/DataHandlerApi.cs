// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.RequestResponse;
using Serilog;

namespace Enigma.Api.Persistency;

/// <inheritdoc/>
public sealed class DataHandlerApi : IDataHandlerApi
{
    private readonly IDataImportHandler _dataImportHandler;

    public DataHandlerApi(IDataImportHandler dataImportHandler) => _dataImportHandler = dataImportHandler;


    /// <inheritdoc/>
    public ResultMessage ConvertDataFile2Json(string sourceFile, string dataName)
    {
        Guard.Against.NullOrEmpty(dataName);
        Log.Information("DataHandlerApi ConvertDataFile2Json, using sourceFile {Source} and dataName {Data}", sourceFile, dataName);
        return _dataImportHandler.ImportStandardData(sourceFile, dataName);
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
    public List<string> GetDataNames()
    {
        Log.Information("DataFileManagementApi GetDataNames");
        return _dataNamesHandler.GetExistingDataNames();
    }
}


