// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Frontend.Ui.Interfaces;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for the overview of data files</summary>
public sealed class DatafileOverviewModel
{
    private readonly IDataFileManagementApi _fileManagementApi;

    private readonly IDataNameForPresentationFactory _dataNameForPresentationFactory;
    
    public DatafileOverviewModel(IDataFileManagementApi fileManagementApi, 
        IDataNameForPresentationFactory dataNameForPresentationFactory)
    {
        _fileManagementApi = fileManagementApi;
        _dataNameForPresentationFactory = dataNameForPresentationFactory;
    }

    public List<string> GetDataNames()
    {
        List<string> fullPathDataNames = _fileManagementApi.GetDataNames();
        return _dataNameForPresentationFactory.CreateDataNamesForListView(fullPathDataNames);
    }
}
