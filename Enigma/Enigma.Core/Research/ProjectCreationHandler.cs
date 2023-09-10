// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Core.Research.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Persistency;
using Enigma.Domain.Research;
using Serilog;

namespace Enigma.Core.Research;

public sealed class ProjectCreationHandler : IProjectCreationHandler
{

    private readonly ApplicationSettings _applicationSettings;
    private readonly IResearchProjectParser _researchProjectParser;
    private readonly ITextFileWriter _textFileWriter;
    private readonly ITextFileReader _textFileReader;
    private readonly IControlGroupCreator _controlGroupCreator;
    private readonly IInputDataConverter _inputDataConverter;

    public ProjectCreationHandler(IResearchProjectParser researchProjectParser,
        ITextFileWriter textFileWriter,
        ITextFileReader textFileReader,
        IControlGroupCreator controlGroupCreator,
        IInputDataConverter inputDataConverter)
    {
        _researchProjectParser = researchProjectParser;
        _textFileWriter = textFileWriter;
        _textFileReader = textFileReader;
        _applicationSettings = ApplicationSettings.Instance;
        _controlGroupCreator = controlGroupCreator;
        _inputDataConverter = inputDataConverter;
    }

    public bool CreateProject(ResearchProject project, out int errorCode)
    {

        errorCode = 0;
        if (FolderExists(project.Name))
        {
            errorCode = ResultCodes.RESEARCH_PROJFOLDER_EXISTS;
            return false;
        }
        if (!CreateFolder(project.Name))
        {
            errorCode = ResultCodes.RESEARCH_CANNOT_CREATE_PROJFOLDER;
            return false;
        }
        if (!CreateFolder(project.Name + @"\results"))
        {
            errorCode = ResultCodes.RESEARCH_CANNOT_CREATE_RESULTSFOLDER;
            return false;
        }
        if (!ParseJson(project, out string jsonText))
        {
            errorCode = ResultCodes.RESEARCH_CANNOT_PARSE_PROJECT2_JSON;
            return false;
        }
        if (!WriteJsonToFile(jsonText, project))
        {
            errorCode = ResultCodes.RESEARCH_CANNOT_WRITE_JSON4_PROJECT;
            return false;
        }
        if (!CopyDataFile(project))
        {
            errorCode = ResultCodes.RESEARCH_CANNOT_COPY_DATAFILE;
            return false;
        }
        string projDataPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name + Path.DirectorySeparatorChar + "testdata.json";
        string inputData = _textFileReader.ReadFile(projDataPath);
        StandardInput standardInput = _inputDataConverter.UnMarshallStandardInput(inputData);
        List<StandardInputItem> allInputItems = standardInput.ChartData;



        // maak controlgroup via ControlGroupCreator
        List<StandardInputItem> controlGroupData = _controlGroupCreator.CreateMultipleControlData(allInputItems, project.ControlGroupType, project.ControlGroupMultiplication);
        string creation = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        StandardInput controlGroup = new(project.Name, creation, controlGroupData);
        // schrijf data controlgroep weg
        string controlGroupJson = _inputDataConverter.MarshallStandardInput(controlGroup);

        string controlGroupPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name + Path.DirectorySeparatorChar + "controldata.json";

        _textFileWriter.WriteFile(controlGroupPath, controlGroupJson);



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
        string projPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name + Path.DirectorySeparatorChar + "project.json";
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
        string dataPath = ApplicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + project.DataName + Path.DirectorySeparatorChar + "json" + Path.DirectorySeparatorChar + "date_time_loc.json";
        string projDataPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name + Path.DirectorySeparatorChar + "testdata.json";
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