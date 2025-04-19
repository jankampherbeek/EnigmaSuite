// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using System.Security.AccessControl;
using CsvHelper;
using CsvHelper.Configuration;
using Enigma.Core.Data;
using Enigma.Core.Persistency;
using Enigma.Core.Research;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;
using Enigma.Domain.Research;
using Serilog;

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
    IResearchProjectParser researchProjectParser,
    ITextFileWriter textFileWriter,
    ITextFileReader textFileReader,
    IControlGroupCreator controlGroupCreator,
    ICsvExporter csvExporter,
    ICsvImporter csvImporter)
    : IProjectCreationHandler
{


    private readonly ApplicationSettings _applicationSettings = ApplicationSettings.Instance;

    public bool CreateProject(ResearchProject project, out int errorCode)
    {
        var projPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name;
        errorCode = 0;
        if (FolderExists(projPath))
        {
            errorCode = ResultCodes.RESEARCH_PROJFOLDER_EXISTS;
            return false;
        }
        if (!CreateFolder(projPath))
        {
            errorCode = ResultCodes.RESEARCH_CANNOT_CREATE_PROJFOLDER;
            return false;
        }
        if (!CreateFolder(projPath + Path.DirectorySeparatorChar + "results"))
        {
            errorCode = ResultCodes.RESEARCH_CANNOT_CREATE_RESULTSFOLDER;
            return false;
        }

        // if (!ReadProject(project, projPath))
        // {
        //     errorCode = ResultCodes.RESEARCH_CANNOT_PARSE_PROJECT2_JSON;
        //     return false;
        // }
        if (!WriteProject(project, projPath))
        {
            errorCode = ResultCodes.RESEARCH_CANNOT_WRITE_JSON4_PROJECT;
            return false;
        }
        if (!CopyDataFile(project, projPath))
        {
            errorCode = ResultCodes.RESEARCH_CANNOT_COPY_DATAFILE;
            return false;
        }
        var projDataPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name + Path.DirectorySeparatorChar + "testdata.csv";
        Log.Information($"Defined location of testdata: {projDataPath}");
        var standardInput = csvImporter.ProcessStandardData(projDataPath);  
        Log.Information(">>>>> Measuring: start creating control group data");
        var controlGroupData = controlGroupCreator.CreateMultipleControlData(standardInput, project.ControlGroupType, project.ControlGroupMultiplication);
        Log.Information(">>>>> Measuring: completed creating control group data");
        var creation = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        Log.Information(">>>>> Measuring: start creating csv for controldata");
        var controlGroupPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name + Path.DirectorySeparatorChar + "controldata.csv";
        csvExporter.WriteStandardInputToCsv(controlGroupData, controlGroupPath);
        Log.Information(">>>>> Measuring: completed creating csv for controldata");
        return true;
    }

    private bool FolderExists(string projPath)
    {
        Log.Information($"Check existence of folder : {projPath}");
        return Directory.Exists(projPath);
    }

    private bool CreateFolder(string projPath)
    {
       // string projPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + projectName;
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

    private ResearchProject ReadProject(string projCsv, string projPath)
    {
     //   var projPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name;

     var proj = new ResearchProject("","","","",ControlGroupTypes.StandardShift, 1);
        try
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                HeaderValidated = null,
                IgnoreBlankLines = true,
            };

            using var reader = new StreamReader(projPath);
            using var csv = new CsvReader(reader, config);
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                proj = csv.GetRecord<ResearchProject>();
            }
        }
        catch (Exception e)
        {
            var errorTxt = $"Received an exception {e.Message} when reading project from {projPath}";
            Log.Error(errorTxt);
            throw new PersistencyException(errorTxt);
        }
        return proj;
    }


    private bool WriteProject(ResearchProject projectDetails, string projPath)
    {
       var config = new CsvConfiguration(CultureInfo.InvariantCulture)
       {
           Delimiter = ",", 
           HasHeaderRecord = true
       };
       try
       {
           using var writer = new StreamWriter(projPath);
           using var csv = new CsvWriter(writer, config);
           csv.WriteHeader<ProjectDetails>();
           csv.NextRecord();
           csv.WriteRecord(projectDetails);
           csv.NextRecord();
       }
       catch (CsvHelperException ex)
       {
           Log.Error($"Could not write to {projPath}. Encountered CsvHelperException {ex.Message}");
           return false;
       }
       catch (IOException ex)
       {
           Log.Error($"Could not write to {projPath}. Encountered IOException {ex.Message}");
           return false;
       }
       return true;
    }

    private bool CopyDataFile(ResearchProject project, string projPath)
    {
        var dataPath = ApplicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + project.DataName + Path.DirectorySeparatorChar + "csv" + Path.DirectorySeparatorChar + project.DataName;
        //var projDataPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name + Path.DirectorySeparatorChar + "testdata.csv";
        var projDataPath = projPath + Path.DirectorySeparatorChar + "testdata.csv";
        try
        {
            File.Copy(dataPath, projDataPath, true);
        }
        catch (Exception e)
        {
            Log.Error("Received an exception {A} when copying file {B} to {C}", e.Message, dataPath, projDataPath);
            return false;
        }
        return true;
    }

}