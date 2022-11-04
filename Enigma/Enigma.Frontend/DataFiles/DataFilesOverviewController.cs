// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Frontend.Ui.Interfaces;

using System.Collections.Generic;

namespace Enigma.Frontend.Ui.DataFiles;
public class DataFilesOverviewController
{
    private readonly IFileManagementApi _fileManagementApi;

    private readonly IDataNameForDataGridFactory _dataNameForDataGridFactory;

    // TODO move functionality to separate class that is also used by ProjectInputController

    public DataFilesOverviewController(IFileManagementApi fileManagementApi, IDataNameForDataGridFactory dataNameForDataGridFactory)
    {
        _fileManagementApi = fileManagementApi;
        _dataNameForDataGridFactory = dataNameForDataGridFactory;
    }

    public List<PresentableDataName> GetDataNames()
    {
        List<string> fullPathDataNames = _fileManagementApi.GetDataNames();
        return _dataNameForDataGridFactory.CreateDataNamesForDataGrid(fullPathDataNames);
    }

}