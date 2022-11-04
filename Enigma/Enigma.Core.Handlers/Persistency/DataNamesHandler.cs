// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Helpers.Interfaces;

namespace Enigma.Core.Handlers.Persistency;

/// <inheritdoc/>
public class DataNamesHandler: IDataNamesHandler
{
    private readonly IFoldersInfo _foldersInfo;

    public DataNamesHandler(IFoldersInfo foldersInfo)
    {
        _foldersInfo = foldersInfo;
    }

    /// <inheritdoc/>
    public List<string> GetExistingDataNames(string path)
    {
        return _foldersInfo.GetExistingFolderNames(path, false);
    }

}