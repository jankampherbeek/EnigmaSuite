// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Helpers.Interfaces;

namespace Enigma.Core.Helpers.Persistency;

/// <inheritdoc/>
public class FoldersInfo: IFoldersInfo
{
    /// <inheritdoc/>
    public List<string> GetExistingFolderNames(string path, bool includeSubFolders)
    {
        SearchOption option = includeSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        string[] dirs = Directory.GetDirectories(path, "*", option);
        return dirs.ToList();
    }
}
