// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Responses;

namespace Enigma.Core.Persistency;

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
public sealed class DataFilePreparator : IDataFilePreparator
{
    /// <inheritdoc/>
    public bool FolderNameAvailable(string fullPath)
    {
        return !Directory.Exists(fullPath);
    }

    /// <inheritdoc/>
    public ResultMessage MakeFolderStructure(string fullPath)
    {
        int errorCode = ResultCodes.OK;
        string resultTxt = "";
        try
        {
            Directory.CreateDirectory(fullPath);
            Directory.CreateDirectory(fullPath + @"\csv");
            Directory.CreateDirectory(fullPath + @"\json");
        }
        catch (Exception)
        {
            errorCode = ResultCodes.DIR_COULD_NOT_BE_CREATED;
            resultTxt = fullPath;
        }
        return new ResultMessage(errorCode, resultTxt);
    }



}