// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.IO;
using Enigma.Api.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Responses;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for the overview of data files</summary>
public sealed class DatafileImportModel
{
    private readonly IDataFileManagementApi _fileManagementApi;
    private readonly IDataHandlerApi _dataHandlerApi;

    public DatafileImportModel(IDataFileManagementApi fileManagementApi, IDataHandlerApi dataHandlerApi )
    {
        _fileManagementApi = fileManagementApi;
        _dataHandlerApi = dataHandlerApi;
    }
    
    /// <summary>Check if a directory does not yet exist.</summary>
    /// <param name="dataName">Name to be used for the data.</param>
    /// <returns>True if a directory for the data with the given name can be created, otherwise false.</returns>
    public bool CheckIfNameCanBeUsed(string dataName)
    {
        string fullPath = ApplicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + dataName;
        return _fileManagementApi.FolderIsAvailable(fullPath);
    }

    /// <summary>Start processing a csv file and convert it to Json. If no error occurs, save the Json and a copy of the csv.</summary>
    /// <param name="inputFile">Csv to read.</param>
    /// <param name="dataName">Name for data.</param>
    /// <returns>ResultMessage with a descriptive text and an error_code (possibly zero: no error).</returns>
    public ResultMessage PerformImport(string inputFile, string dataName)
    {
        string dataPath = ApplicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + dataName;
        ResultMessage receivedResultMessage = _fileManagementApi.CreateFoldersForData(dataPath);
        if (receivedResultMessage.ErrorCode > ResultCodes.OK)
        {
            return receivedResultMessage;
        }
        receivedResultMessage = _dataHandlerApi.ConvertDataFile2Json(inputFile, dataName);
        return receivedResultMessage;
    }

}