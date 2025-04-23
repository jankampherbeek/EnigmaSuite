// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Enigma.Api;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Responses;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for the overview of data files</summary>
public sealed class DatafileImportModel(IDataFileManagementApi fileManagementApi, IDataHandlerApi dataHandlerApi, ISettingsApi settingsApi)
{
    public List<string> AllDataTypes()
    {
        return ResearchDataTypesExtensions.AllDetails().Select(detail => detail.Name).ToList();
    }
    
    
    /// <summary>Check if a directory does not yet exist.</summary>
    /// <param name="dataName">Name to be used for the data.</param>
    /// <returns>True if a directory for the data with the given name can be created, otherwise false.</returns>
    public bool CheckIfNameCanBeUsed(string dataName)
    {
        return fileManagementApi.DataFileNameIsAvailable(dataName);
    }

    /// <summary>Start processing a csv file and convert it to standard format. If no error occurs, save the Json and a copy of the csv.</summary>
    /// <param name="inputFile">Csv to read.</param>
    /// <param name="dataName">Name for data.</param>
    /// <param name="dataTypeIndex">Index for datatype</param>
    /// <returns>ResultMessage with a descriptive text and an error_code (possibly zero: no error).</returns>
    public ResultMessage PerformImport(string inputFile, string dataName, int dataTypeIndex)
    {
        var dataType = ResearchDataTypesExtensions.DataTypeForIndex(dataTypeIndex);
        var workFolder = settingsApi.ReadSetting("workfolder");        
        var dataPath = workFolder + Path.DirectorySeparatorChar + "datafiles" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + dataName;
        
        var receivedResultMessage = fileManagementApi.CreateFoldersForData(dataPath);
        if (receivedResultMessage.ErrorCode > ResultCodes.OK)
        {
            return receivedResultMessage;
        }
        receivedResultMessage = dataHandlerApi.ConvertDataFile2Standard(inputFile, dataName, dataType);
        return receivedResultMessage;
    }
    
}