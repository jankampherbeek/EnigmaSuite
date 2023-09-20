// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Responses;

namespace Enigma.Core.Persistency.Helpers;


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