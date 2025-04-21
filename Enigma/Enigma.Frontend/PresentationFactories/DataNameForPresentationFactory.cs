// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;

namespace Enigma.Frontend.Ui.PresentationFactories;

public interface IDataNameForPresentationFactory
{
    /// <summary>Builds a presentable data name to be used in a grid.</summary>
    /// <param name="allData">List with dataname dtos.</param>
    /// <returns>Presentable data names.</returns>
    List<PresentableDataName> CreateDataNamesForDataGrid(IEnumerable<DataFileDto> allData);

    /// <summary>Find existing data files</summary>
    /// <param name="allData">Path for the data files</param>
    /// <returns>The names for the data files based on the file names</returns>
    List<string> CreateDataNamesForListView(IEnumerable<DataFileDto> allData);
}

public class DataNameForPresentationFactory : IDataNameForPresentationFactory
{
    public List<PresentableDataName> CreateDataNamesForDataGrid(IEnumerable<DataFileDto> allData)
    {
        List<PresentableDataName> allNames = [];
        foreach (var dataDto in allData)
        {
            allNames.Add(new PresentableDataName(dataDto.Name));
        }
        return allNames;
    }

    /// <inheritdoc/>
    public List<string> CreateDataNamesForListView(IEnumerable<DataFileDto> allData)
    {
        List<string> allNames = [];
        foreach (var dataDto in allData)
        {
            allNames.Add(dataDto.Name);
        }
        return allNames;
    }

}