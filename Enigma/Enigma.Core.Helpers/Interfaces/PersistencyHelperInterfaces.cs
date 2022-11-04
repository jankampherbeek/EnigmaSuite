// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Helpers.Interfaces;


/// <summary>Handle directories for storing datafiles.</summary>
public interface IDataFilePreparator
{
    /// <summary>Checks if new folder can be created.</summary>
    /// <param name="fullPath">Path to the folder for the data to import.</param>
    /// <returns>True if folder does not exist, otherwise false.</returns>
    bool FolderNameAvailable(string fullPath);

    /// <summary>Create folder structure for data files, including subfolders.</summary>
    /// <param name="fullPath">Path to the folder for the data.</param>
    /// <returns>ResultMessage, containing errorcode > zero if an error occurred. 
    /// In case of an error the errorText contains the path of the directory that could not be created.</returns>
    ResultMessage MakeFolderStructure(string fullPath);
}

/// <summary>
/// Read text from a file.
/// </summary>
public interface ITextFileReader
{
    /// <summary>Read all text from a file into a string.</summary>
    /// <param name="location">Full path to the text file </param>
    /// <returns>If the file is found and no exception occured: the content of the file. Otherwise: an empty string.</returns>
    public string ReadFile(string location);

    /// <summary>Read all lines from a text file into a list.</summary>
    /// <param name="location">Full path to the text file </param>
    /// <returns>If the file is found and no exception occured: all lines from the file. Otherwise: an empty list.</returns>
    public List<string> ReadAllLines(string location);
}

/// <summary>Write text to a file.</summary>
public interface ITextFileWriter
{
    /// <summary>Write a full string to a textfile. Overwrites any existing file with the same name (location).</summary>
    /// <param name="location">Full pathname of the file.</param>
    /// <param name="text">Content to write to the file.</param>
    /// <returns>True is the write was successful, otherwise false.</returns>
    public bool WriteFile(string location, string text);

    /// <summary>Writes multiple lines to a textfile. Overwrites any existing file with the same name (location).</summary>
    /// <param name="location">Full pathname of the file.</param>
    /// <param name="textLines">Content to write to the file.</param>
    /// <returns>True is the write was successful, otherwise false.</returns>
    public bool WriteFile(string location, List<string> textLines);
}

/// <summary>Retrieves information about folders.</summary>
public interface IFoldersInfo
{
    /// <summary>Return names of folders.</summary>
    /// <param name="path">Full path where to look for folders.</param>
    /// <param name="includeSubFolders">Indicates if subfolders should also be searched.</param>
    /// <returns>Folder names.</returns>
    public List<string> GetExistingFolderNames(string path, bool includeSubFolders);
}

/// <summary>Copy a file in the file system.</summary>
public interface IFileCopier
{
    /// <summary>Performs a copy.</summary>
    /// <param name="source">Full path for the original file.</param>
    /// <param name="destination">Full path for the new location of the file.</param>
    /// <returns>True if the copy was successful, otherwise false.</returns>
    public bool CopyFile(string source, string destination);
}