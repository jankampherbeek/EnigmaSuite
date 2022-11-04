// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Helpers.Interfaces;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Interfaces;


/// <summary>Handler for preparation of the file system.</summary>
public interface IDataFilePreparationHandler
{
    /// <summary>Checks if a folder name is available.</summary>
    /// <param name="fullPath">Full path of the folder to check.</param>
    /// <returns>True if the folder is available, otherwise false.</returns>
    public bool FolderNameAvailable(string fullPath);

    /// <summary>Create folders to save data, including the subfolders 'csv' and 'json'.</summary>
    /// <param name="fullPath">Full path of the data folder to create (without the subfolders for csv and json).</param>
    /// <returns>Resultmessage with a description of the action.</returns>
    public ResultMessage MakeFolderStructure(string fullPath);
}

/// <summary>Handler for data names.</summary>
public interface IDataNamesHandler
{
    /// <summary>Retrieve data names from data folders.</summary>
    /// <returns>Data names.</returns>
    public List<string> GetExistingDataNames();

}


/// <summary>Handles the import and conversion to Json of a csv datafile.</summary>
public interface IDataImportHandler
{
    /// <summary>Import a datafile in standard csv and convert it to Json.</summary>
    /// <param name="fullPathSource">Full path to the file to read.</param>
    /// <param name="dataName">Name for the data.</param>
    /// <returns>Resultmessage with a description of the action.</returns>
    public ResultMessage ImportStandardData(string fullPathSource, string dataName);
}
