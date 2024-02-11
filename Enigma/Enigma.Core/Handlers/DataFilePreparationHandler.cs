// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;
using Enigma.Domain.Responses;

namespace Enigma.Core.Handlers;


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
