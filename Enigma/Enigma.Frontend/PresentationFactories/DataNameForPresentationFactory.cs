// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Enigma.Frontend.Ui.PresentationFactories;

public class DataNameForPresentationFactory : IDataNameForPresentationFactory
{
    // TODO remove method CreateDataNamesForDataGrid
    public List<PresentableDataName> CreateDataNamesForDataGrid(List<string> fullPathDataNames)
    {
        List<PresentableDataName> presentableDataNames = new();
        foreach (var fullPathDataName in fullPathDataNames)
        {
            int pos = fullPathDataName.LastIndexOf(@"\");
            string dataName = fullPathDataName[(pos + 1)..];
            presentableDataNames.Add(new PresentableDataName(dataName));
        }
        return presentableDataNames;
    }

    /// <inheritdoc/>
    public List<string> CreateDataNamesForListView(List<string> fullPathDataNames)
    {
        return (from fullPathDataName in fullPathDataNames 
            let pos = fullPathDataName.LastIndexOf(@"\", StringComparison.Ordinal) 
            select fullPathDataName[(pos + 1)..]).ToList();
    }

}