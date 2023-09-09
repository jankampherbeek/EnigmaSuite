// Enigma Astrology Research.
// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Interfaces;

namespace Enigma.Core.Persistency.Helpers;

/// <inheritdoc/>
public sealed class FoldersInfo : IFoldersInfo
{
    /// <inheritdoc/>
    public List<string> GetExistingFolderNames(string path, bool includeSubFolders)
    {
        SearchOption option = includeSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        string[] dirs = Directory.GetDirectories(path, "*", option);
        return dirs.ToList();
    }
}
