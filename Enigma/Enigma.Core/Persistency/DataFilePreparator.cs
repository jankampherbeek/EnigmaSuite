// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Core.Persistency;

/// <summary>Handle data file management.</summary>
public interface IDataFilePreparator
{
    /// <summary>Checks if a data name is available in the database.</summary>
    /// <param name="name">Name of the data to check.</param>
    /// <returns>True if the name is available (not in use), otherwise false.</returns>
    bool DataNameAvailable(string name);

    /// <summary>Create folder structure for data files, including subfolders.</summary>
    /// <param name="fullPath">Path to the folder for the data.</param>
    /// <returns>ResultMessage, containing errorcode > zero if an error occurred. 
    /// In case of an error the errorText contains the path of the directory that could not be created.</returns>
    ResultMessage MakeFolderStructure(string fullPath);

    /// <summary>Adds a new data file to the system.</summary>
    /// <param name="dto">The data file DTO containing name and location information.</param>
    /// <returns>ResultMessage indicating success or failure. If successful, the result text contains the ID of the new data file.</returns>
    ResultMessage AddDataFile(DataFileDto dto);
}

/// <inheritdoc/>
public sealed class DataFilePreparator : IDataFilePreparator
{
    private readonly IDataFileDao _dataFileDao;

    public DataFilePreparator(IDataFileDao dataFileDao)
    {
        _dataFileDao = dataFileDao;
    }

    /// <inheritdoc/>
    public bool DataNameAvailable(string name)
    {
        Log.Information("Checking if data name {Name} is available", name);
        return _dataFileDao.IsDataNameAvailable(name);
    }

    /// <inheritdoc/>
    public ResultMessage AddDataFile(DataFileDto dto)
    {
        Log.Information("Adding new data file {Name} at {Location}", dto.Name, dto.Location);
        
        // First create the folder structure
        var folderResult = MakeFolderStructure(dto.Location);
        if (folderResult.ErrorCode != ResultCodes.OK)
        {
            Log.Error("Failed to create folder structure for {Location}", dto.Location);
            return folderResult;
        }

        // Then insert into database
        int dataFileId = _dataFileDao.InsertDataFile(dto);
        if (dataFileId < 0)
        {
            Log.Error("Failed to insert data file {Name} into database", dto.Name);
            // Rollback folder creation
            try
            {
                if (Directory.Exists(dto.Location))
                {
                    Directory.Delete(dto.Location, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to rollback folder creation for {Location}. Error: {Error}", 
                    dto.Location, ex.Message);
            }
            return new ResultMessage(ResultCodes.DB_INSERT_FAILED, "Failed to insert data file into database");
        }

        Log.Information("Successfully added data file {Name} with ID {Id}", dto.Name, dataFileId);
        return new ResultMessage(ResultCodes.OK, dataFileId.ToString());
    }
    
    /// <inheritdoc/>
    public ResultMessage MakeFolderStructure(string fullPath)
    {
        int errorCode = ResultCodes.OK;
        string resultTxt = "";
        try
        {
            Directory.CreateDirectory(fullPath);
            Directory.CreateDirectory(fullPath + @"\orig");  
            Directory.CreateDirectory(fullPath + @"\standard");
        }
        catch (Exception)
        {
            errorCode = ResultCodes.DIR_COULD_NOT_BE_CREATED;
            resultTxt = fullPath;
        }
        return new ResultMessage(errorCode, resultTxt);
    }
}