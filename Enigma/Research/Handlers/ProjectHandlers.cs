// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Persistency;
using Enigma.Domain.Research;
using Enigma.Persistency.Interfaces;
using Enigma.Research.Interfaces;
using Serilog;

namespace Enigma.Research.Handlers;

public class ProjectCreationHandler : IProjectCreationHandler
{

    private readonly ApplicationSettings _applicationSettings;
    private readonly IResearchProjectParser _researchProjectParser;
    private readonly ITextFileWriter _textFileWriter;
    private readonly ITextFileReader _textFileReader;
    private readonly IControlGroupCreator _controlGroupCreator;

    public ProjectCreationHandler(IResearchProjectParser researchProjectParser,
        ITextFileWriter textFileWriter,
        ITextFileReader textFileReader,
        IControlGroupCreator controlGroupCreator)
    {
        _researchProjectParser = researchProjectParser;
        _textFileWriter = textFileWriter;
        _textFileReader = textFileReader;
        _applicationSettings = ApplicationSettings.Instance;
        _controlGroupCreator = controlGroupCreator;
    }

    public bool CreateProject(ResearchProject project, out int errorCode)
    {

        errorCode = 0;
        if (FolderExists(project.Name))
        {
            errorCode = ErrorCodes.ERR_RESEARCH_PROJFOLDER_EXISTS;
            return false;
        }
        if (!CreateFolder(project.Name))
        {
            errorCode = ErrorCodes.ERR_RESEARCH_CANNOT_CREATE_PROJFOLDER;
            return false;
        }
        if (!ParseJson(project, out string jsonText))
        {
            errorCode = ErrorCodes.ERR_RESEARCH_CANNOT_PARSE_PROJECT_2_JSON;
            return false;
        }
        if (!WriteJsonToFile(jsonText, project))
        {
            errorCode = ErrorCodes.ERR_RESEARCH_CANNOT_WRITE_JSON_4_PROJECT;
            return false;
        }
        if (!CopyDataFile(project))
        {
            errorCode = ErrorCodes.ERR_RESEARCH_CANNOT_COPY_DATAFILE;
            return false;
        };
        // lees datafile (naam staat in project)

        // maak controlgroup via ControlGroupCreator
        // schrijf data controlgroep weg


        return true;
    }

    private bool FolderExists(string projectName)
    {
        string projPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + projectName;
        return Directory.Exists(projPath);
    }

    private bool CreateFolder(string projectName)
    {
        string projPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + projectName;
        try
        {
            Directory.CreateDirectory(projPath);
        }
        catch (Exception e)
        {
            Log.Error("Received an exception {A} when creating a project folder {B}", e.Message, projPath);
            return false;
        }
        return true;
    }

    private bool ParseJson(ResearchProject project, out string jsonText)
    {
        jsonText = "";
        try
        {
            jsonText = _researchProjectParser.Marshall(project);
        }
        catch (Exception e)
        {
            Log.Error("Received an exception {A} when parsing project {B} to JSON", e.Message, project.Name);
            return false;
        }
        return true;
    }


    private bool WriteJsonToFile(string jsonText, ResearchProject project)
    {
        string projPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name + Path.DirectorySeparatorChar + project.Identification + "_definition.json";
        try
        {
            _textFileWriter.WriteFile(projPath, jsonText);
        }
        catch (Exception e)
        {
            Log.Error("Received an exception {A} when writing Json to file {B}, using the following JSON: {C}", e.Message, projPath, jsonText);
            return false;
        }
        return true;

    }

    private bool CopyDataFile(ResearchProject project)
    {
        string dataPath = _applicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + project.DataName + Path.DirectorySeparatorChar + "date_time_loc.json";
        string projDataPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name + Path.DirectorySeparatorChar + project.Identification + "_data.json";
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

    private bool ReadDataFile(ResearchProject project, out List<StandardInputItem> inputItems)
    {
        string projDataPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name + Path.DirectorySeparatorChar + project.Identification + "_data.json";
        inputItems = new();
        // kijk naar lgoica in CsvHandler in Persistency, en ook naar de controller voor data. Logica hiervan verplaatsen.
        return false;
    }

}