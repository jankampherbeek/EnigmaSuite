// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Persistency;

/// <summary>Retrieves information about folders.</summary>
public interface IFoldersInfo
{
    /// <summary>Return names of folders.</summary>
    /// <param name="path">Full path where to look for folders.</param>
    /// <param name="includeSubFolders">Indicates if subfolders should also be searched.</param>
    /// <returns>Folder names.</returns>
    public List<string> GetExistingFolderNames(string path, bool includeSubFolders);
}


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
