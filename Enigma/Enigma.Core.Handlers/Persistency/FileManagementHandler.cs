// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Work.Persistency.Interfaces;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Persistency;

/// <inheritdoc/>
public class DataFilePreparationHandler : IDataFilePreparationHandler
{

    private readonly IDataFilePreparator _dataFilePreparator;

    public DataFilePreparationHandler(IDataFilePreparator dataFilePreparator)
    {
        _dataFilePreparator = dataFilePreparator;
    }

    /// <inheritdoc/>
    public bool FolderNameAvailable(string fullPath)
    {
        return _dataFilePreparator.FolderNameAvailable(fullPath);
    }

    /// <inheritdoc/>
    public ResultMessage MakeFolderStructure(string fullPath)
    {
        return _dataFilePreparator.MakeFolderStructure(fullPath);
    }





}
