// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.RequestResponse;

namespace Engima.Api.Persistency;

/// <inheritdoc/>
public class DataHandlerApi : IDataHandlerApi
{
    private readonly IDataImportHandler _dataImportHandler;

    public DataHandlerApi(IDataImportHandler dataImportHandler)
    {
        _dataImportHandler = dataImportHandler;
    }

    /// <inheritdoc/>
    public ResultMessage ConvertDataFile2Json(string sourceFile, string dataName)
    {
        Guard.Against.NullOrEmpty(dataName);
        return _dataImportHandler.ImportStandardData(sourceFile, dataName);
    }
}

/// <inheritdoc/>
public class FileManagementApi: IFileManagementApi
{
    private readonly IDataFilePreparationHandler _preparationHandler;
    private readonly IDataNamesHandler _dataNamesHandler;

    public FileManagementApi(IDataFilePreparationHandler preparationHandler, IDataNamesHandler dataNamesHandler)
    {
        _preparationHandler = preparationHandler;
        _dataNamesHandler = dataNamesHandler;
    }

    /// <inheritdoc/>
    public bool FolderIsAvailable(string fullPath)
    {
        Guard.Against.NullOrEmpty(fullPath);
        return _preparationHandler.FolderNameAvailable(fullPath);
    }

    /// <inheritdoc/>
    public ResultMessage CreateFoldersForData(string fullPath)
    {
        Guard.Against.NullOrEmpty(fullPath);
        return _preparationHandler.MakeFolderStructure(fullPath);
    }

    /// <inheritdoc/>
    public List<string> GetDataNames(string fullPath)
    {
        Guard.Against.NullOrEmpty(fullPath);
        return _dataNamesHandler.GetExistingDataNames(fullPath);
    }
}


