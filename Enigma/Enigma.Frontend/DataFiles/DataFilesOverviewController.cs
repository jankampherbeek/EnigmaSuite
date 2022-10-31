// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Frontend.Interfaces;
using Enigma.Persistency.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.DataFiles;
public class DataFilesOverviewController
{
    private readonly IDataNameHandler _dataNameHandler;
    private readonly IDataNameForDataGridFactory _dataNameForDataGridFactory;

    // TODO move functionality to separate class that is also used by ProjectInputController

    public DataFilesOverviewController(IDataNameHandler dataNameHandler, IDataNameForDataGridFactory dataNameForDataGridFactory)
    {
        _dataNameHandler = dataNameHandler;
        _dataNameForDataGridFactory = dataNameForDataGridFactory;
    }

    public List<PresentableDataName> GetDataNames()
    {
        string path = @"c:\enigma_ar\data\";        // TODO release 0.2 replace hardcoded path to data with path from settings.
        List<string> fullPathDataNames = _dataNameHandler.GetExistingDataNames(path);
        return _dataNameForDataGridFactory.CreateDataNamesForDataGrid(fullPathDataNames);
    }

}