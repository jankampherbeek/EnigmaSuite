// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Interfaces;

using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Research.DataFiles;
public class DataFilesOverviewController
{
    private readonly IDataFileManagementApi _fileManagementApi;

    private readonly IDataNameForDataGridFactory _dataNameForDataGridFactory;

    // TODO move functionality to separate class that is also used by ProjectInputController

    public DataFilesOverviewController(IDataFileManagementApi fileManagementApi, IDataNameForDataGridFactory dataNameForDataGridFactory)
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