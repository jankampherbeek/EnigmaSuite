// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.RequestResponse;

namespace Enigma.Api.Interfaces;

/// <summary>Api for conversions from Csv to Json.</summary>
public interface IDataHandlerApi
{
    /// <summary>Convert a datafile in standard csv-format to a Json file.</summary>
    /// <remarks>Locations for the files are retrieved from the application settings.</remarks>
    /// <param name="sourceFile">Path to the source file.</param>
    /// <param name="dataName">Name for the datafile.</param>
    /// <returns>Resultmessage with info about this action.</returns>
    public ResultMessage ConvertDataFile2Json(string sourceFile, string dataName);
}

/// <summary>Api for managing the file system.</summary>
public interface IDataFileManagementApi
{
    /// <summary>Check if a folder can be created.</summary>
    /// <param name="fullPath">The full path of the folder to check.</param>
    /// <returns>True if the folder does not yet exist, otherwise false.</returns>
    public bool FolderIsAvailable(string fullPath);

    /// <summary>Creates folders for research data and additionally subfodlers 'csv' and 'json'.</summary>
    /// <param name="fullPath">The path for the datafiles, not including the csv and json subfolders.</param>
    /// <returns>Resultmessage with info about this action.</returns>
    public ResultMessage CreateFoldersForData(string fullPath);

    /// <summary>Create a list of data names, based in folders in the file system.</summary>
    /// <returns>Dat names.</returns>
    public List<string> GetDataNames();
}

/// <summary>API for simple read and write access to files.</summary>
public interface IFileAccessApi
{
    /// <summary>Write a file to disk.</summary>
    /// <param name="path">Full path for the file.</param>
    /// <param name="text">Content of the file.</param>
    /// <returns>True if no error occured, otherwise false.</returns>
    public bool WriteFile(string path, string text);

    /// <summary>Read a file from disk.</summary>
    /// <param name="path">Full path for the file.</param>
    /// <returns>Content of the file.</returns>
    public string ReadFile(string path);


}