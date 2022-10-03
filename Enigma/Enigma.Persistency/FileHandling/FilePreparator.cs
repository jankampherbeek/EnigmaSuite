// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Messages;

namespace Enigma.Persistency.FileHandling;

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


/// <inheritdoc/>
public class DataFilePreparator : IDataFilePreparator
{
    /// <inheritdoc/>
    public bool FolderNameAvailable(string fullPath)
    {
        return !Directory.Exists(fullPath);
    }

    /// <inheritdoc/>
    public ResultMessage MakeFolderStructure(string dataPath)
    {
        int errorCode = ErrorCodes.ERR_NONE;
        string resultTxt = "";
        try
        {
            Directory.CreateDirectory(dataPath);
            Directory.CreateDirectory(dataPath + @"\csv");
            Directory.CreateDirectory(dataPath + @"\json");
        }
        catch (Exception)
        {
            errorCode = ErrorCodes.ERR_DIR_COULD_NOT_BE_CREATED;
            resultTxt = dataPath;
        }
        return new ResultMessage(errorCode, resultTxt); 
    }



}