// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Core.Persistency;


/// <summary>Handle persistency for projects in cases where both the RDBMS and files are involved</summary>
public interface IProjectPersistencyHandler
{
    /// <summary>Delete a project and all related files</summary>
    /// <param name="id">Id for the project</param>
    /// <returns>True if the deletion was completed, otherwise false</returns>
    public bool DeleteProjectAndFiles(int id);
}


public class ProjectPersistencyHandler(IProjectDao projectDao): IProjectPersistencyHandler
{
    public bool DeleteProjectAndFiles(int id)
    {
        var projectName = projectDao.ReadProject(id)?.Name;
        if (projectName is null) return false;
        var location = projectDao.ReadProject(id)?.Location;

        // remove files
        if (location != null && Directory.Exists(location))
        {
            var projectFile = Path.Combine(location, "project.csv");
            if (!File.Exists(projectFile))
            {
                Log.Error($"Directory {location} does not contain project.csv when deleting project {projectName}");
                return false;
            }
            try
            {
                Directory.Delete(location, recursive: true);
            }
            catch (IOException)
            {
                Log.Error($"Could not delete {location} when deleting project {projectName}");
                return false;
            }
        }
        
        var deleted = projectDao.DeleteProject(id);

        return deleted;
    }
}