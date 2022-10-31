// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.RequestResponse;
using Enigma.Persistency.Interfaces;

namespace Enigma.Persistency.FileHandling;


/// <inheritdoc/>
public class DataFilePreparator : IDataFilePreparator
{
    /// <inheritdoc/>
    public bool FolderNameAvailable(string fullPath)
    {
        return !Directory.Exists(fullPath);
    }

    /// <inheritdoc/>
    public ResultMessage MakeFolderStructure(string fullPath)
    {
        int errorCode = ErrorCodes.ERR_NONE;
        string resultTxt = "";
        try
        {
            Directory.CreateDirectory(fullPath);
            Directory.CreateDirectory(fullPath + @"\csv");
            Directory.CreateDirectory(fullPath + @"\json");
        }
        catch (Exception)
        {
            errorCode = ErrorCodes.ERR_DIR_COULD_NOT_BE_CREATED;
            resultTxt = fullPath;
        }
        return new ResultMessage(errorCode, resultTxt);
    }



}