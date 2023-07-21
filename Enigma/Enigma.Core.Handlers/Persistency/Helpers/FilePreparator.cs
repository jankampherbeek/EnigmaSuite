// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Persistency.Helpers;


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
        int errorCode = ErrorCodes.NONE;
        string resultTxt = "";
        try
        {
            Directory.CreateDirectory(fullPath);
            Directory.CreateDirectory(fullPath + @"\csv");
            Directory.CreateDirectory(fullPath + @"\json");
        }
        catch (Exception)
        {
            errorCode = ErrorCodes.DIR_COULD_NOT_BE_CREATED;
            resultTxt = fullPath;
        }
        return new ResultMessage(errorCode, resultTxt);
    }



}