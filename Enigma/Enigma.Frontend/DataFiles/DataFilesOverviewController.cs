// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.PresentationFactories;
using Enigma.Frontend.UiDomain;
using Enigma.Persistency.Handlers;
using System.Collections.Generic;

namespace Enigma.Frontend.DataFiles;
public class DataFilesOverviewController
{
    private IDataNameHandler _dataNameHandler;
    private IDataNameForDataGridFactory _dataNameForDataGridFactory;


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