// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Enigma.Core.Data;
using Enigma.Core.Persistency;
using Enigma.Core.Research;
using Enigma.Domain.Constants;
using Enigma.Domain.Research;
using Serilog;
using ProjectDto = Enigma.Domain.Dtos.ProjectDto;

namespace Enigma.Core.Handlers;

/// <summary>Handler for the creation of a research project.</summary>
public interface IProjectCreationHandler
{
    /// <summary>Handles the creation of a research project and the accompanying controlgroup.</summary>
    /// <param name="project">Definition of the project.</param>
    /// <param name="errorCode">Resulting errorcode.</param>
    /// <returns>True if no error occurred.</returns>
    public bool CreateProject(ResearchProject project, out int errorCode);
}

public sealed class ProjectCreationHandler(
    IControlGroupCreator controlGroupCreator,
    ICsvExporter csvExporter,
    ICsvStandardDataReader csvStandardDataReader,
    ISettingsDao settingsDao,
    IProjectDao projectDao,
    IDataFileDao dataFileDao)
    : IProjectCreationHandler
{

    public bool CreateProject(ResearchProject project, out int errorCode)
    {
        Log.Information("Starting project creation for project: {ProjectName}", project.Name);
        
        // Validate data file
        if (project.IndexDataFile <= 0)
        {
            Log.Error("No data file selected for project: {ProjectName}", project.Name);
            errorCode = ResultCodes.RESEARCH_NO_DATAFILE_SELECTED;
            return false;
        }

        var workFolder = settingsDao.ReadSetting("workfolder");
        Log.Information("Using workfolder: {WorkFolder}", workFolder);
        
        var projPath = workFolder + Path.DirectorySeparatorChar + "projects" + Path.DirectorySeparatorChar + project.Name;
        var projectDto = new ProjectDto
        {
            Name = project.Name,
            Created = DateTime.Now,
            Description = project.Description,
            Location = projPath,
            DataFile = project.IndexDataFile,
            MultiFactor = project.ControlGroupMultiplication
        };
        errorCode = 0;
        
        Log.Information("Inserting project into database");
        var projIndex = projectDao.InsertProject(projectDto);
        if (projIndex < 0)
        {
            Log.Error("Failed to insert project into database");
            return false;
        }
        Log.Information("Project inserted with ID: {ProjIndex}", projIndex);

        if (FolderExists(projPath))
        {
            Log.Warning("Project folder already exists: {Path}", projPath);
            errorCode = ResultCodes.RESEARCH_PROJFOLDER_EXISTS;
        }

        Log.Information("Creating project folder: {Path}", projPath);
        if (!CreateFolder(projPath))
        {
            Log.Error("Failed to create project folder");
            errorCode = ResultCodes.RESEARCH_CANNOT_CREATE_PROJFOLDER;
        }

        Log.Information("Creating results folder");
        if (!CreateFolder(projPath + Path.DirectorySeparatorChar + "results"))
        {
            Log.Error("Failed to create results folder");
            errorCode = ResultCodes.RESEARCH_CANNOT_CREATE_RESULTSFOLDER;
        }

        Log.Information("Writing project details");
        if (!WriteProject(project, projPath))
        {
            Log.Error("Failed to write project details");
            errorCode = ResultCodes.RESEARCH_CANNOT_WRITE_JSON4_PROJECT;
        }

        Log.Information("Copying data file");
        if (!CopyDataFile(project))
        {
            Log.Error("Failed to copy data file");
            errorCode = ResultCodes.RESEARCH_CANNOT_COPY_DATAFILE;
        }

        if (errorCode != 0)
        {
            Log.Error("Error occurred during project creation. Error code: {ErrorCode}", errorCode);
            var deleted = projectDao.DeleteProject(projIndex);
            Log.Information("Project deleted from database: {Deleted}", deleted);
            return false;
        }
        
        Log.Information("Starting control group creation");
        var projDataPath = workFolder + Path.DirectorySeparatorChar + "projects" + Path.DirectorySeparatorChar + project.Name +
                           Path.DirectorySeparatorChar + "testdata.csv";
        Log.Information("Reading test data from: {Path}", projDataPath);
        
        if (!File.Exists(projDataPath))
        {
            Log.Error("Test data file does not exist: {Path}", projDataPath);
            errorCode = ResultCodes.RESEARCH_CANNOT_READ_TESTDATA;
            return false;
        }

        try
        {
            var standardInput = csvStandardDataReader.ReadStandardInputData(projDataPath);
            Log.Information("Successfully read test data, creating control group");
            
            var controlGroupData = controlGroupCreator.CreateMultipleControlData(standardInput, project.ControlGroupType,
                project.ControlGroupMultiplication);
            Log.Information("Control group data created successfully");
            
            var controlGroupDir = workFolder + Path.DirectorySeparatorChar + "projects" + Path.DirectorySeparatorChar + project.Name;
            if (!Directory.Exists(controlGroupDir))
            {
                Log.Error("Control group directory does not exist: {Dir}", controlGroupDir);
                errorCode = ResultCodes.RESEARCH_CANNOT_CREATE_PROJFOLDER;
                return false;
            }

            var controlGroupPath = controlGroupDir + Path.DirectorySeparatorChar + "controldata.csv";
            try
            {
                Log.Information("Attempting to write control group data to: {Path}", controlGroupPath);
                csvExporter.WriteStandardInputToCsv(controlGroupData, controlGroupPath);
                Log.Information("Successfully wrote control group data");
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Error("Unauthorized access when writing control group data: {Message}. Path: {Path}", ex.Message, controlGroupPath);
                errorCode = ResultCodes.RESEARCH_CANNOT_WRITE_CONTROLGROUP;
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("Error writing control group data: {Message}. Path: {Path}", ex.Message, controlGroupPath);
                errorCode = ResultCodes.RESEARCH_CANNOT_WRITE_CONTROLGROUP;
                return false;
            }
        }
        catch (Exception ex)
        {
            Log.Error("Error processing test data: {Message}. Path: {Path}", ex.Message, projDataPath);
            errorCode = ResultCodes.RESEARCH_CANNOT_READ_TESTDATA;
            return false;
        }
        Log.Information("Project creation completed successfully");
        return true;
    }

    private static bool FolderExists(string projPath)
    {
        Log.Information($"Check existence of folder : {projPath}");
        return Directory.Exists(projPath);
    }

    private static bool CreateFolder(string projPath)
    {
        try
        {
            Log.Information($"Create folder for research: {projPath}");
            Directory.CreateDirectory(projPath);
        }
        catch (Exception e)
        {
            Log.Error("Received an exception {A} when creating a project folder {B}", e.Message, projPath);
            return false;
        }
        return true;
    }

    private static bool WriteProject(ResearchProject projectDetails, string projPath)
    {
        var projectFilePath = projPath + Path.DirectorySeparatorChar + "project.csv";
        Log.Information("Writing project details to: {Path}", projectFilePath);
        
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true
        };
        try
        {
            using var writer = new StreamWriter(projectFilePath);
            using var csv = new CsvWriter(writer, config);
            csv.WriteHeader<ProjectDetails>();
            csv.NextRecord();
            csv.WriteRecord(projectDetails);
            csv.NextRecord();
            Log.Information("Successfully wrote project details");
        }
        catch (CsvHelperException ex)
        {
            Log.Error("Could not write to {Path}. Encountered CsvHelperException {Message}", projectFilePath, ex.Message);
            return false;
        }
        catch (IOException ex)
        {
            Log.Error("Could not write to {Path}. Encountered IOException {Message}", projectFilePath, ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            Log.Error("Unexpected error writing project details: {Message}", ex.Message);
            return false;
        }

        return true;
    }

    private bool CopyDataFile(ResearchProject project)
    {
        Log.Information("Starting data file copy for project: {ProjectName}", project.Name);
        Log.Information("Reading data file with index: {Index}", project.IndexDataFile);
        
        var dataFile = dataFileDao.ReadDataFile(project.IndexDataFile);
        if (dataFile == null)
        {
            Log.Error("Data file not found for index: {Index}", project.IndexDataFile);
            return false;
        }
        
        Log.Information("Source data file location: {Location}", dataFile.Location);
        if (!File.Exists(dataFile.Location))
        {
            Log.Error("Source data file does not exist: {Location}", dataFile.Location);
            return false;
        }

        var workFolder = settingsDao.ReadSetting("workfolder");
        var projDataPath = workFolder + Path.DirectorySeparatorChar + "projects" + Path.DirectorySeparatorChar 
                           + project.Name + Path.DirectorySeparatorChar + "testdata.csv";
        Log.Information("Destination path: {Path}", projDataPath);
        
        try
        {
            Log.Information("Copying data file from {Source} to {Destination}", dataFile.Location, projDataPath);
            File.Copy(dataFile.Location, projDataPath, true);
            Log.Information("Successfully copied data file");
        }
        catch (UnauthorizedAccessException ex)
        {
            Log.Error("Unauthorized access when copying file: {Message}", ex.Message);
            return false;
        }
        catch (IOException ex)
        {
            Log.Error("IO error when copying file: {Message}", ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            Log.Error("Unexpected error copying file: {Message}", ex.Message);
            return false;
        }
        return true;
    }
}