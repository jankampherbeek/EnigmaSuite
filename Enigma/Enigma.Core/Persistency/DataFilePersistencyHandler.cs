// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Core.Persistency;


/// <summary>Handle persistency for datafiles in cases where both the RDBMS and files are involved</summary>
public interface IDataFilePersistencyHandler
{
    /// <summary>Delete a DataFile from the RDBMS and delete all related files</summary>
    /// <param name="id">The id of the data file</param>
    /// <returns>True if the deletion was completed, otherwise false</returns>
    public bool DeleteDataFileAndRemoveFiles(int id);
}

/// <inheritdoc/>
public class DataFilePersistencyHandler(IDataFileDao dataFileDao) : IDataFilePersistencyHandler
{
    /// <inheritdoc/>
    public bool DeleteDataFileAndRemoveFiles(int id)
    {
        var dataFileName = dataFileDao.ReadDataFile(id)?.Name;
        if (dataFileName is null) return false;
        var location = dataFileDao.ReadDataFile(id)?.Location;

        // Check if datafile is referenced by any projects
        var projectsUsingDataFile = dataFileDao.ReadProjectsForDataFile(dataFileName);
        if (projectsUsingDataFile.Count > 0)
        {
            Log.Error($"Cannot delete data file {dataFileName} because it is used by {projectsUsingDataFile.Count} projects: {string.Join(", ", projectsUsingDataFile)}");
            return false;
        }

        // remove files
        if (location != null && Directory.Exists(location))
        {
            var origDir = Path.Combine(location, "orig");
            if (!Directory.Exists(origDir))
            {
                Log.Error($"Directory {location} does not contain folder orig when deleting data file {dataFileName}");
                return false;
            }
            try
            {
                Directory.Delete(location, recursive: true);
            }
            catch (IOException)
            {
                Log.Error($"Could not delete {location} when deleting data file {dataFileName}");
                return false;
            }
        }
        
        var deleted = dataFileDao.DeleteDataFile(id);

        return deleted;
    }
    
}
