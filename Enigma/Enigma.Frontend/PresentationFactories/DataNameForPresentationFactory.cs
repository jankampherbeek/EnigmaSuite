// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Presentables;

namespace Enigma.Frontend.Ui.PresentationFactories;

public interface IDataNameForPresentationFactory
{
    /// <summary>Builds a presentable data name to be used in a grid.</summary>
    /// <param name="fullPathDataNames">List with datanames.</param>
    /// <returns>Presentable data names.</returns>
    List<PresentableDataName> CreateDataNamesForDataGrid(IEnumerable<string> fullPathDataNames);

    /// <summary>Find existing data files</summary>
    /// <param name="fullPathDataNames">Path for the data files</param>
    /// <returns>The names for the data files based on the file names</returns>
    List<string> CreateDataNamesForListView(IEnumerable<string> fullPathDataNames);
}

public class DataNameForPresentationFactory : IDataNameForPresentationFactory
{
    // TODO 0.3 remove method CreateDataNamesForDataGrid
    public List<PresentableDataName> CreateDataNamesForDataGrid(IEnumerable<string> fullPathDataNames)
    {
        return (from fullPathDataName in fullPathDataNames 
            let pos = fullPathDataName.LastIndexOf(@"\", StringComparison.Ordinal) 
            select fullPathDataName[(pos + 1)..] into dataName 
            select new PresentableDataName(dataName)).ToList();
    }

    /// <inheritdoc/>
    public List<string> CreateDataNamesForListView(IEnumerable<string> fullPathDataNames)
    {
        return (from fullPathDataName in fullPathDataNames 
            let pos = fullPathDataName.LastIndexOf(@"\", StringComparison.Ordinal) 
            select fullPathDataName[(pos + 1)..]).ToList();
    }

}