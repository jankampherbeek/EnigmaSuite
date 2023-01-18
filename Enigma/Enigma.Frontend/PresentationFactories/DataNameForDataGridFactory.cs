// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;

public class DataNameForDataGridFactory : IDataNameForDataGridFactory
{
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

}