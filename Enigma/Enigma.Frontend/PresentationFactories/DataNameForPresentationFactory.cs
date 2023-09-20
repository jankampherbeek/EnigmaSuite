// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Presentables;

namespace Enigma.Frontend.Ui.PresentationFactories;

public class DataNameForPresentationFactory : IDataNameForPresentationFactory
{
    // TODO remove method CreateDataNamesForDataGrid
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