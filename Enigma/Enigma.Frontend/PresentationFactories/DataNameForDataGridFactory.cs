// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.UiDomain;
using System.Collections.Generic;

namespace Enigma.Frontend.PresentationFactories;

public interface IDataNameForDataGridFactory
{
    /// <summary>Builds a presentable data name to be used in a grid.</summary>
    /// <param name="dataNames">List with datanames.</param>
    /// <returns>Presentable data names.</returns>
    List<PresentableDataName> CreateDataNamesForDataGrid(List<string> fullPathDataNames);
}


public class DataNameForDataGridFactory : IDataNameForDataGridFactory
{
    public List<PresentableDataName> CreateDataNamesForDataGrid(List<string> fullPathDataNames)
    {
        List<PresentableDataName> presentableDataNames = new();
        foreach (var fullPathDataName in fullPathDataNames)
        {
            int pos = fullPathDataName.LastIndexOf(@"\");
            string dataName = fullPathDataName.Substring(pos + 1);
            presentableDataNames.Add(new PresentableDataName(dataName));
        }
        return presentableDataNames;
    }
}